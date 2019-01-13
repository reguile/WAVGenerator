using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

/// <summary>
/// Represent the header data at the start of a .wav file.
/// </summary>
public struct wavHeader
{
    public UInt32 ChunkID;
    public UInt32 ChunkSize;
    public UInt32 Format;
    public UInt32 SubChunk1ID;
    public UInt32 SubChunk1Size;
    public UInt16 AudioFormat;
    public UInt16 NumChannels;
    public UInt32 SampleRate;
    public UInt32 ByteRate;
    public UInt16 BlockAlign;
    public UInt16 BitsPerSample;
    public UInt32 SubChunk2ID;
    public UInt32 SubChunk2Size;
}

namespace Custom_Wav_Generator
{
    public partial class WAVGenerator : Form
    {

        public Burst_Generator()
        {
            InitializeComponent();
        }

        public enum burstState
        {
            burstOn,
            burstOff
        }

        private void generateBurstWAV(object sender, CancelEventArgs e)
        {
            double frequency = getFrequency();
            double length = getLength();
            int sampleRate = getSampleRate();
            string waveform = getWaveform();
            int bitDepth = getBitDepth();

            // This is the total number of samples the file needs.
            // TODO: switch to int64 to handle larger files.
            int numSamples = Convert.ToInt32(Math.Ceiling(sampleRate * length));

            double amplitude;

            string fileName = saveFileDialog.FileName;
            wavHeader header = initWAVHeader(numSamples);
            byte[] headerArray = StructureToByteArray(header);

            double waveformValue;
            int waveformValueInt;

            double t = 0;
            byte[] byteArray;

            // Period to complete one cycle.
            double period = 1 / frequency;
            double periodSamples = sampleRate * period;

            try
            {
                // overwrite the file if it exists.
                // I should really add a prompt to confirm this...
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Dealing with binary data so use a binary writer.
                using (BinaryWriter bw = new BinaryWriter(File.OpenWrite(fileName)))
                {

                    // Start the file by writing the header data.
                    bw.Write(headerArray);

                    // The amplitude depends on bit depth.
                    double amplitudeBurst = getAmplitude(bitDepth);

                    // Generate and write the audio data.
                    for (int i = 0; i < numSamples; i++)
                    {

                        // represents the time of the sample.
                        t = (Convert.ToDouble(i) / sampleRate);

                        amplitude = setAmplitude(periodSamples, i, amplitudeBurst);

                        switch (waveform)
                        {
                            case ("Sine"):
                                waveformValue = amplitude * Math.Sin(2 * Math.PI * t * frequency);
                                break;

                            case ("Square"):
                                waveformValue = squareWaveValue(amplitude, period, t);
                                break;

                            case ("Triangle"):
                                waveformValue = triangleWaveValue(amplitude, period, t);
                                break;

                            case ("Sawtooth"):
                                waveformValue = sawWaveValue(amplitude, period, t);
                                break;

                            default:
                                // we should never reach this, but in case, clear waveform value and throw exception
                                waveformValue = 0;
                                break;
                        }

                        // sine wave
                        waveformValueInt = Convert.ToInt32(waveformValue);
                        byteArray = BitConverter.GetBytes(waveformValueInt);

                        // First 2 bytes, common to all bit depths
                        bw.Write(byteArray[0]);
                        bw.Write(byteArray[1]);

                        // Only write 3rd byte for 24 and 32 bit audio
                        if (bitDepth >= 24)
                        {
                            bw.Write(byteArray[2]);
                        }

                        // Only write 4th byte for 32 bit audio
                        if (bitDepth == 32)
                        {
                            bw.Write(byteArray[3]);
                        }

                    }
                }

                MessageBox.Show("File Created.", "File Created", MessageBoxButtons.OK);

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Sets whether the burst or low amplitude is set for a particular sample
        /// Performance-wise this is pretty awful, but it works
        /// </summary>
        /// <param name="periodSamples"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private double setAmplitude(double periodSamples, int i, double amplitudeBurst)
        {
            int bitDepth = getBitDepth();

            double amplitude;
            double lowLevel = getLowLevel() / 100;          // converted from percent

            int burstOnCycles = getBurstCycles();
            int numCycles = getIntervalCycles();

            // The low level is the maximum level scaled by the chosen percentage.
            double amplitudeLow = amplitudeBurst * lowLevel;

            // Time in samples for the function to complete the specified number of burst cycles
            double burstLength = periodSamples * burstOnCycles;

            // The total time in samples to complete one burst plus low level period
            double intervalLength = periodSamples * numCycles;

            /// Okay this DEFINITELY can be optimized but oh well
            /// Also modulo on a double feels really wrong to me.
            /// Basically, check if the interval has exceeded the
            /// burst interval. If so, 
            if ((i % intervalLength) < burstLength)
            {
                amplitude = amplitudeBurst;
            }

            // seriously I should only be setting the value when the state changes
            // Whatever maybe the compiler is smarter than I am.
            else
            {
                amplitude = amplitudeLow;
            }

            return amplitude;
        }

        private static double squareWaveValue(double amplitude, double period, double t)
        {
            double waveformValue;
            // check if we're in the positive or negative part of the waveform
            if ((t % period) < (period / 2))
            {
                waveformValue = amplitude;
            }

            else
            {
                waveformValue = (amplitude * -1);
            }

            return waveformValue;
        }

        /// <summary>
        /// Function that returns the y value of a triangle wave at time t
        /// given the amplitude, period, and time value
        /// 
        /// An odd triangle wave can be represented as a piecewise function.
        /// A = max amplitude, T = period, t = current sample time
        /// 
        /// This is repeated every period, which is why we use the time modulo period
        /// 
        /// 0 <= t <= T/4,  y = 4A/T * t
        /// T/4 < t < 3T/4, y = 2A - 4At/T
        /// 3T/4 <= t <= T, y = -4A + 4At/T
        /// 
        /// </summary>
        /// <param name="amp"></param>
        /// <param name="period"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        double triangleWaveValue(double amp, double period, double t)
        {
            double value;

            double ampScaled = 4 * amp;             // this constant shows up a bit, so simplify calculations a bit. 
            double tmod = t % period;
            double tFrac = tmod / period;
            double quarterPeriod = period / 4;

            // This if else block represents the piecewise function described above
            if (tmod <= quarterPeriod)
            {
                value = ampScaled * tFrac;
            }
            else if ((tmod > quarterPeriod) && (tmod < (3 * quarterPeriod)))
            {
                value = ampScaled * (0.5 - tFrac);
            }

            // 3/4*period < tmod < period
            else
            {
                value = -1 * ampScaled * (1 - tFrac);
            }

            return value;
        }

        double sawWaveValue(double amp, double period, double t)
        {
            double value;

            double ampScaled = 2 * amp;             // this constant shows up a bit, so simplify calculations a bit. 
            double tmod = t % period;
            double tFrac = tmod / period;
            double halfPeriod = period / 2;


            if (tmod <= halfPeriod)
            {
                value = ampScaled * tFrac;
            }

            // period / 2 < tmod < period
            else
            {
                value = -1 * ampScaled * (1 - tFrac);
            }

            return value;
        }

        /// <summary>
        /// Returns the appropriate value to use as maximum amplitude
        /// based on the bit depth. 
        /// </summary>
        /// <param name="bitDepth"></param>
        /// <returns></returns>
        private static double getAmplitude(int bitDepth)
        {
            double amplitudeBurst;

            // maximum value of a signed 24 bit number
            // 2 ^ (24 - 1) - 1
            const int int24Max = 8388607;

            // Assign the amplitude to the appropriate max value depending on how many bits are used
            // Amplitude values are close to the full scale value for each bit depth
            switch (bitDepth)
            {
                case (16):
                    amplitudeBurst = Int16.MaxValue;
                    break;
                case (24):
                    amplitudeBurst = int24Max;
                    break;
                case (32):
                    amplitudeBurst = Int32.MaxValue;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Bit Depth", "You really shouldn't be seeing this");
            }

            return amplitudeBurst;
        }

        string getWaveform()
        {
            string waveform = "";
            try
            {
                waveform = waveformBox.SelectedItem.ToString();

            }
            catch (Exception)
            {

            }

            return waveform;
        }

        /// <summary>
        /// All of the error checking needs to be redone. I'm just using
        /// arbitrary hardcoded values.
        /// </summary>
        /// <returns></returns>
        int getSampleRate()
        {
            int sampleRate = Convert.ToInt32(sampleRateBox.Text);
            Utilities.checkValid(sampleRate, 8000, 200000, "Sample Rate");
            return sampleRate;
        }

        double getLength()
        {
            double length = Convert.ToDouble(lengthBox.Text);
            Utilities.checkValid(length, 0.01, 61, "Audio Length");
            return length;
        }

        double getFrequency()
        {
            double frequency = Convert.ToDouble(frequencyBox.Text);
            Utilities.checkValid(frequency, 1, 20001, "Frequency");
            return frequency;
        }

        int getBitDepth()
        {
            return Convert.ToInt32(bitDepthBox.Text);
        }

        double getLowLevel()
        {
            double lowLevel = Convert.ToDouble(lowLevelBox.Text);
            Utilities.checkValid(lowLevel, 0.1, 100.000001, "Low Level");
            return lowLevel;
        }

        // TODO add logic for max cycles check
        int getBurstCycles()
        {
            int burstCycles = Convert.ToInt32(burstCyclesBox.Text);
            Utilities.checkValid(burstCycles, 0, 500.000001, "Burst Cycles");
            return burstCycles;
        }

        int getIntervalCycles()
        {
            int intervalCycles = Convert.ToInt32(intervalCyclesBox.Text);
            Utilities.checkValid(intervalCycles, getBurstCycles(), 10000.000001, "Interval Cycles");
            return intervalCycles;
        }

        /// <summary>
        /// Converts a struct to byte array for easy file writing.
        /// Shamelessly stolen from stack overflow.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        byte[] StructureToByteArray(object obj)
        {
            int len = Marshal.SizeOf(obj);

            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }


        /// <summary>
        /// Returns a struct representing a WAV header. 
        /// Because the number of samples is not determined until the
        /// data is complete, this is created last and appended to the data.
        /// </summary>
        /// <returns></returns>
        private wavHeader initWAVHeader(int numSamples)
        {
            wavHeader header = new wavHeader();

            // TODO make these fields user selectable
            // I made these ints to simplify the conversion required for the header fields
            int numChannels = 1;
            int sampleRate = getSampleRate();
            int bitsPerSample = getBitDepth();

            int subChunk2Size = numSamples * numChannels * (bitsPerSample / 8);

            // "RIFF" chunk descriptor
            header.ChunkID = 0x46464952;   // "RIFF"
            header.ChunkSize = Convert.ToUInt32(subChunk2Size + 36);     // I don't understand why this is even needed honestly
            header.Format = 0x45564157;   // "WAVE"

            // "fmt " sub chunk
            header.SubChunk1ID = 0x20746d66;   // "fmt "
            header.SubChunk1Size = 16;           // 16 for PCM
            header.AudioFormat = 1;            // 1 indicates PCM, others indicate compression
            header.NumChannels = Convert.ToUInt16(numChannels);  // 1: mono, 2: stereo
            header.SampleRate = Convert.ToUInt32(sampleRate);
            header.ByteRate = Convert.ToUInt32(sampleRate * numChannels * (bitsPerSample / 8));
            header.BlockAlign = Convert.ToUInt16(numChannels * (bitsPerSample / 8));
            header.BitsPerSample = Convert.ToUInt16(bitsPerSample);

            // "data" sub chunk
            header.SubChunk2ID = 0x61746164;   // "data" 
            header.SubChunk2Size = Convert.ToUInt32(subChunk2Size);

            return header;
        }

        /// <summary>
        /// Opens the save file dialog when clicking the save file button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveBurstFile_Click(object sender, EventArgs e)
        {
            saveFileDialog.ShowDialog();
        }

        /// <summary>
        ///  Checks user input to text boxes to make sure it is valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void keyPressed(object sender, KeyPressEventArgs e)
        {
            if (Utilities.isCharNumber(e.KeyChar, sender))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Checks that an entered key is an int
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public void keyPressedInt(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            if (!char.IsControl(key) && !char.IsDigit(key))
            {
                e.Handled = true;
            }
        }

        private void Burst_Generator_Load(object sender, EventArgs e)
        {

        }
    }
}
