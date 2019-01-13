namespace WAVGenerator
{
    partial class WAVGenerator
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.saveBurstFile = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.frequencyLabel = new System.Windows.Forms.Label();
            this.frequencyBox = new System.Windows.Forms.TextBox();
            this.lowLevelBox = new System.Windows.Forms.TextBox();
            this.lowCycleValue = new System.Windows.Forms.Label();
            this.lengthBox = new System.Windows.Forms.TextBox();
            this.lengthLabel = new System.Windows.Forms.Label();
            this.sampleRateLabel = new System.Windows.Forms.Label();
            this.sampleRateBox = new System.Windows.Forms.ComboBox();
            this.bitDepthLabel = new System.Windows.Forms.Label();
            this.bitDepthBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.burstCyclesBox = new System.Windows.Forms.TextBox();
            this.intervalCyclesBox = new System.Windows.Forms.TextBox();
            this.Waveform = new System.Windows.Forms.Label();
            this.waveformBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bitDepthBox)).BeginInit();
            this.SuspendLayout();
            // 
            // saveBurstFile
            // 
            this.saveBurstFile.Location = new System.Drawing.Point(196, 180);
            this.saveBurstFile.Name = "saveBurstFile";
            this.saveBurstFile.Size = new System.Drawing.Size(96, 23);
            this.saveBurstFile.TabIndex = 8;
            this.saveBurstFile.Text = "Generate File";
            this.saveBurstFile.UseVisualStyleBackColor = true;
            this.saveBurstFile.Click += new System.EventHandler(this.saveBurstFile_Click);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "wav";
            this.saveFileDialog.Filter = "WAV file|*.wav";
            this.saveFileDialog.Title = "Save WAV file";
            this.saveFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.generateBurstWAV);
            // 
            // frequencyLabel
            // 
            this.frequencyLabel.AutoSize = true;
            this.frequencyLabel.Location = new System.Drawing.Point(23, 89);
            this.frequencyLabel.Name = "frequencyLabel";
            this.frequencyLabel.Size = new System.Drawing.Size(79, 13);
            this.frequencyLabel.TabIndex = 11;
            this.frequencyLabel.Text = "Frequency (Hz)";
            // 
            // frequencyBox
            // 
            this.frequencyBox.Location = new System.Drawing.Point(125, 86);
            this.frequencyBox.MaxLength = 6;
            this.frequencyBox.Name = "frequencyBox";
            this.frequencyBox.Size = new System.Drawing.Size(69, 20);
            this.frequencyBox.TabIndex = 3;
            this.frequencyBox.Text = "1000";
            this.frequencyBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            // 
            // lowLevelBox
            // 
            this.lowLevelBox.Location = new System.Drawing.Point(125, 121);
            this.lowLevelBox.MaxLength = 4;
            this.lowLevelBox.Name = "lowLevelBox";
            this.lowLevelBox.Size = new System.Drawing.Size(69, 20);
            this.lowLevelBox.TabIndex = 4;
            this.lowLevelBox.Text = "5";
            this.lowLevelBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            // 
            // lowCycleValue
            // 
            this.lowCycleValue.AutoSize = true;
            this.lowCycleValue.Location = new System.Drawing.Point(23, 124);
            this.lowCycleValue.Name = "lowCycleValue";
            this.lowCycleValue.Size = new System.Drawing.Size(73, 13);
            this.lowCycleValue.TabIndex = 12;
            this.lowCycleValue.Text = "Low Level (%)";
            // 
            // lengthBox
            // 
            this.lengthBox.Location = new System.Drawing.Point(380, 18);
            this.lengthBox.MaxLength = 4;
            this.lengthBox.Name = "lengthBox";
            this.lengthBox.Size = new System.Drawing.Size(69, 20);
            this.lengthBox.TabIndex = 5;
            this.lengthBox.Text = "1";
            this.lengthBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressed);
            // 
            // lengthLabel
            // 
            this.lengthLabel.AutoSize = true;
            this.lengthLabel.Location = new System.Drawing.Point(267, 21);
            this.lengthLabel.Name = "lengthLabel";
            this.lengthLabel.Size = new System.Drawing.Size(54, 13);
            this.lengthLabel.TabIndex = 13;
            this.lengthLabel.Text = "Length (s)";
            // 
            // sampleRateLabel
            // 
            this.sampleRateLabel.AutoSize = true;
            this.sampleRateLabel.Location = new System.Drawing.Point(267, 55);
            this.sampleRateLabel.Name = "sampleRateLabel";
            this.sampleRateLabel.Size = new System.Drawing.Size(90, 13);
            this.sampleRateLabel.TabIndex = 14;
            this.sampleRateLabel.Text = "Sample Rate (Hz)";
            // 
            // sampleRateBox
            // 
            this.sampleRateBox.FormattingEnabled = true;
            this.sampleRateBox.Items.AddRange(new object[] {
            "44100",
            "48000",
            "88200",
            "96000",
            "192000"});
            this.sampleRateBox.Location = new System.Drawing.Point(380, 52);
            this.sampleRateBox.MaxLength = 6;
            this.sampleRateBox.Name = "sampleRateBox";
            this.sampleRateBox.Size = new System.Drawing.Size(69, 21);
            this.sampleRateBox.TabIndex = 6;
            this.sampleRateBox.Text = "44100";
            this.sampleRateBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyPressedInt);
            // 
            // bitDepthLabel
            // 
            this.bitDepthLabel.AutoSize = true;
            this.bitDepthLabel.Location = new System.Drawing.Point(267, 89);
            this.bitDepthLabel.Name = "bitDepthLabel";
            this.bitDepthLabel.Size = new System.Drawing.Size(51, 13);
            this.bitDepthLabel.TabIndex = 15;
            this.bitDepthLabel.Text = "Bit Depth";
            // 
            // bitDepthBox
            // 
            this.bitDepthBox.Increment = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.bitDepthBox.Location = new System.Drawing.Point(380, 89);
            this.bitDepthBox.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.bitDepthBox.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.bitDepthBox.Name = "bitDepthBox";
            this.bitDepthBox.ReadOnly = true;
            this.bitDepthBox.Size = new System.Drawing.Size(69, 20);
            this.bitDepthBox.TabIndex = 7;
            this.bitDepthBox.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Burst Cycles";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Interval Cycles";
            // 
            // burstCyclesBox
            // 
            this.burstCyclesBox.Location = new System.Drawing.Point(125, 18);
            this.burstCyclesBox.MaxLength = 6;
            this.burstCyclesBox.Name = "burstCyclesBox";
            this.burstCyclesBox.Size = new System.Drawing.Size(69, 20);
            this.burstCyclesBox.TabIndex = 1;
            this.burstCyclesBox.Text = "10";
            // 
            // intervalCyclesBox
            // 
            this.intervalCyclesBox.Location = new System.Drawing.Point(125, 50);
            this.intervalCyclesBox.MaxLength = 6;
            this.intervalCyclesBox.Name = "intervalCyclesBox";
            this.intervalCyclesBox.Size = new System.Drawing.Size(69, 20);
            this.intervalCyclesBox.TabIndex = 2;
            this.intervalCyclesBox.Text = "100";
            // 
            // Waveform
            // 
            this.Waveform.AutoSize = true;
            this.Waveform.Location = new System.Drawing.Point(267, 124);
            this.Waveform.Name = "Waveform";
            this.Waveform.Size = new System.Drawing.Size(56, 13);
            this.Waveform.TabIndex = 17;
            this.Waveform.Text = "Waveform";
            // 
            // waveformBox
            // 
            this.waveformBox.FormattingEnabled = true;
            this.waveformBox.Items.AddRange(new object[] {
            "Sine",
            "Triangle",
            "Square",
            "Sawtooth"});
            this.waveformBox.Location = new System.Drawing.Point(380, 120);
            this.waveformBox.Name = "waveformBox";
            this.waveformBox.Size = new System.Drawing.Size(69, 21);
            this.waveformBox.TabIndex = 18;
            // 
            // Burst_Generator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 236);
            this.Controls.Add(this.waveformBox);
            this.Controls.Add(this.Waveform);
            this.Controls.Add(this.intervalCyclesBox);
            this.Controls.Add(this.burstCyclesBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bitDepthBox);
            this.Controls.Add(this.bitDepthLabel);
            this.Controls.Add(this.sampleRateBox);
            this.Controls.Add(this.sampleRateLabel);
            this.Controls.Add(this.lengthBox);
            this.Controls.Add(this.lengthLabel);
            this.Controls.Add(this.lowLevelBox);
            this.Controls.Add(this.lowCycleValue);
            this.Controls.Add(this.frequencyBox);
            this.Controls.Add(this.frequencyLabel);
            this.Controls.Add(this.saveBurstFile);
            this.Name = "Burst_Generator";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Burst WAV Generator";
            this.Load += new System.EventHandler(this.Burst_Generator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bitDepthBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveBurstFile;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Label frequencyLabel;
        private System.Windows.Forms.TextBox frequencyBox;
        private System.Windows.Forms.TextBox lowLevelBox;
        private System.Windows.Forms.Label lowCycleValue;
        private System.Windows.Forms.TextBox lengthBox;
        private System.Windows.Forms.Label lengthLabel;
        private System.Windows.Forms.Label sampleRateLabel;
        private System.Windows.Forms.ComboBox sampleRateBox;
        private System.Windows.Forms.Label bitDepthLabel;
        private System.Windows.Forms.NumericUpDown bitDepthBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox burstCyclesBox;
        private System.Windows.Forms.TextBox intervalCyclesBox;
        private System.Windows.Forms.Label Waveform;
        private System.Windows.Forms.ComboBox waveformBox;

#endregion
    }
}

