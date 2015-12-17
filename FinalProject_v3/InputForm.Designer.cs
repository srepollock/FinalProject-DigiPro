namespace FinalProject_v3
{
    partial class InputForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.HFTChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.lengthOfData = new System.Windows.Forms.NumericUpDown();
            this.sampLabel = new System.Windows.Forms.Label();
            this.sampUpDown = new System.Windows.Forms.NumericUpDown();
            this.panel4 = new System.Windows.Forms.Panel();
            this.freqWaveChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.audioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hzToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.chartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chartToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.triangleWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangleWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.welchWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plotFrequencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descreteFourierTransformThreadsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.threads1MenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.threads2MenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.threads3MenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.threads4MenuButton = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterAudioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel8 = new System.Windows.Forms.Panel();
            this.playButton = new System.Windows.Forms.Button();
            this.stopRec = new System.Windows.Forms.Button();
            this.recButton = new System.Windows.Forms.Button();
            this.panel9 = new System.Windows.Forms.Panel();
            this.clearFreqButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HFTChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lengthOfData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sampUpDown)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.freqWaveChart)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15.76139F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 72.7781F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 11.46051F));
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel8, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel9, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1090, 566);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.HFTChart);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(174, 27);
            this.panel5.Name = "panel5";
            this.tableLayoutPanel1.SetRowSpan(this.panel5, 2);
            this.panel5.Size = new System.Drawing.Size(787, 264);
            this.panel5.TabIndex = 6;
            // 
            // HFTChart
            // 
            chartArea1.AxisX.ScaleView.SmallScrollSizeType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Seconds;
            chartArea1.AxisX.ScaleView.Zoomable = false;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.HFTChart.ChartAreas.Add(chartArea1);
            this.HFTChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.HFTChart.Legends.Add(legend1);
            this.HFTChart.Location = new System.Drawing.Point(0, 0);
            this.HFTChart.Name = "HFTChart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.MarkerSize = 3;
            series1.Name = "DFT";
            this.HFTChart.Series.Add(series1);
            this.HFTChart.Size = new System.Drawing.Size(787, 264);
            this.HFTChart.TabIndex = 0;
            this.HFTChart.Text = "DFT Data";
            title1.Name = "HFT";
            this.HFTChart.Titles.Add(title1);
            this.HFTChart.Click += new System.EventHandler(this.HFTChart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Length";
            // 
            // lengthOfData
            // 
            this.lengthOfData.Location = new System.Drawing.Point(3, 87);
            this.lengthOfData.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.lengthOfData.Name = "lengthOfData";
            this.lengthOfData.ReadOnly = true;
            this.lengthOfData.Size = new System.Drawing.Size(137, 20);
            this.lengthOfData.TabIndex = 7;
            this.lengthOfData.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // sampLabel
            // 
            this.sampLabel.AutoSize = true;
            this.sampLabel.Location = new System.Drawing.Point(3, 13);
            this.sampLabel.Name = "sampLabel";
            this.sampLabel.Size = new System.Drawing.Size(47, 13);
            this.sampLabel.TabIndex = 6;
            this.sampLabel.Text = "Samples";
            // 
            // sampUpDown
            // 
            this.sampUpDown.Location = new System.Drawing.Point(3, 29);
            this.sampUpDown.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.sampUpDown.Name = "sampUpDown";
            this.sampUpDown.ReadOnly = true;
            this.sampUpDown.Size = new System.Drawing.Size(137, 20);
            this.sampUpDown.TabIndex = 5;
            this.sampUpDown.Value = new decimal(new int[] {
            22050,
            0,
            0,
            0});
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.freqWaveChart);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(174, 297);
            this.panel4.Name = "panel4";
            this.tableLayoutPanel1.SetRowSpan(this.panel4, 2);
            this.panel4.Size = new System.Drawing.Size(787, 266);
            this.panel4.TabIndex = 9;
            // 
            // freqWaveChart
            // 
            chartArea2.AxisX.ScaleView.Zoomable = false;
            chartArea2.CursorX.IsUserEnabled = true;
            chartArea2.CursorX.IsUserSelectionEnabled = true;
            chartArea2.Name = "ChartArea1";
            this.freqWaveChart.ChartAreas.Add(chartArea2);
            this.freqWaveChart.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.freqWaveChart.Legends.Add(legend2);
            this.freqWaveChart.Location = new System.Drawing.Point(0, 0);
            this.freqWaveChart.Name = "freqWaveChart";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Freq";
            this.freqWaveChart.Series.Add(series2);
            this.freqWaveChart.Size = new System.Drawing.Size(787, 266);
            this.freqWaveChart.TabIndex = 0;
            this.freqWaveChart.Text = "Frequency";
            this.freqWaveChart.Click += new System.EventHandler(this.freqWaveChart_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tableLayoutPanel1.SetColumnSpan(this.menuStrip1, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.menuStrip1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.audioToolStripMenuItem,
            this.chartToolStripMenuItem,
            this.chartToolStripMenuItem1,
            this.optionsToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(964, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // audioToolStripMenuItem
            // 
            this.audioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hzToolStripMenuItem2,
            this.hzToolStripMenuItem,
            this.hzToolStripMenuItem1});
            this.audioToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.audioToolStripMenuItem.Name = "audioToolStripMenuItem";
            this.audioToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.audioToolStripMenuItem.Text = "Audio Sample Rate";
            // 
            // hzToolStripMenuItem2
            // 
            this.hzToolStripMenuItem2.Name = "hzToolStripMenuItem2";
            this.hzToolStripMenuItem2.Size = new System.Drawing.Size(118, 22);
            this.hzToolStripMenuItem2.Text = "11025Hz";
            this.hzToolStripMenuItem2.Click += new System.EventHandler(this.hzToolStripMenuItem2_Click);
            // 
            // hzToolStripMenuItem
            // 
            this.hzToolStripMenuItem.Name = "hzToolStripMenuItem";
            this.hzToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.hzToolStripMenuItem.Text = "22050Hz";
            this.hzToolStripMenuItem.Click += new System.EventHandler(this.hzToolStripMenuItem_Click);
            // 
            // hzToolStripMenuItem1
            // 
            this.hzToolStripMenuItem1.Name = "hzToolStripMenuItem1";
            this.hzToolStripMenuItem1.Size = new System.Drawing.Size(118, 22);
            this.hzToolStripMenuItem1.Text = "44100Hz";
            this.hzToolStripMenuItem1.Click += new System.EventHandler(this.hzToolStripMenuItem1_Click);
            // 
            // chartToolStripMenuItem
            // 
            this.chartToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectToolStripMenuItem,
            this.zoomToolStripMenuItem});
            this.chartToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.chartToolStripMenuItem.Name = "chartToolStripMenuItem";
            this.chartToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.chartToolStripMenuItem.Text = "Selection Options";
            // 
            // selectToolStripMenuItem
            // 
            this.selectToolStripMenuItem.Checked = true;
            this.selectToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.selectToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.selectToolStripMenuItem.Name = "selectToolStripMenuItem";
            this.selectToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.selectToolStripMenuItem.Text = "Select";
            this.selectToolStripMenuItem.Click += new System.EventHandler(this.selectToolStripMenuItem_Click);
            // 
            // zoomToolStripMenuItem
            // 
            this.zoomToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
            this.zoomToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.zoomToolStripMenuItem.Text = "Zoom";
            this.zoomToolStripMenuItem.Click += new System.EventHandler(this.zoomToolStripMenuItem_Click);
            // 
            // chartToolStripMenuItem1
            // 
            this.chartToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.triangleWindowToolStripMenuItem,
            this.rectangleWindowToolStripMenuItem,
            this.welchWindowToolStripMenuItem,
            this.plotFrequencyToolStripMenuItem});
            this.chartToolStripMenuItem1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.chartToolStripMenuItem1.Name = "chartToolStripMenuItem1";
            this.chartToolStripMenuItem1.Size = new System.Drawing.Size(125, 20);
            this.chartToolStripMenuItem1.Text = "Windowing Options";
            // 
            // triangleWindowToolStripMenuItem
            // 
            this.triangleWindowToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.triangleWindowToolStripMenuItem.Name = "triangleWindowToolStripMenuItem";
            this.triangleWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.triangleWindowToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.triangleWindowToolStripMenuItem.Text = "Triangle Window";
            this.triangleWindowToolStripMenuItem.Click += new System.EventHandler(this.triangleWindowToolStripMenuItem_Click);
            // 
            // rectangleWindowToolStripMenuItem
            // 
            this.rectangleWindowToolStripMenuItem.Name = "rectangleWindowToolStripMenuItem";
            this.rectangleWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.rectangleWindowToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.rectangleWindowToolStripMenuItem.Text = "Rectangle Window";
            this.rectangleWindowToolStripMenuItem.Click += new System.EventHandler(this.rectangleWindowToolStripMenuItem_Click);
            // 
            // welchWindowToolStripMenuItem
            // 
            this.welchWindowToolStripMenuItem.Name = "welchWindowToolStripMenuItem";
            this.welchWindowToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.W)));
            this.welchWindowToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.welchWindowToolStripMenuItem.Text = "Welch Window";
            this.welchWindowToolStripMenuItem.Click += new System.EventHandler(this.welchWindowToolStripMenuItem_Click);
            // 
            // plotFrequencyToolStripMenuItem
            // 
            this.plotFrequencyToolStripMenuItem.Name = "plotFrequencyToolStripMenuItem";
            this.plotFrequencyToolStripMenuItem.Size = new System.Drawing.Size(246, 22);
            this.plotFrequencyToolStripMenuItem.Text = "Plot Signal";
            this.plotFrequencyToolStripMenuItem.Click += new System.EventHandler(this.plotFrequencyToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descreteFourierTransformThreadsToolStripMenuItem});
            this.optionsToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.optionsToolStripMenuItem.Text = "Threading Options";
            // 
            // descreteFourierTransformThreadsToolStripMenuItem
            // 
            this.descreteFourierTransformThreadsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.threads1MenuButton,
            this.threads2MenuButton,
            this.threads3MenuButton,
            this.threads4MenuButton});
            this.descreteFourierTransformThreadsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.descreteFourierTransformThreadsToolStripMenuItem.Name = "descreteFourierTransformThreadsToolStripMenuItem";
            this.descreteFourierTransformThreadsToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.descreteFourierTransformThreadsToolStripMenuItem.Text = "Descrete Fourier Transform Threads";
            // 
            // threads1MenuButton
            // 
            this.threads1MenuButton.Checked = true;
            this.threads1MenuButton.CheckOnClick = true;
            this.threads1MenuButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.threads1MenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.threads1MenuButton.Name = "threads1MenuButton";
            this.threads1MenuButton.Size = new System.Drawing.Size(80, 22);
            this.threads1MenuButton.Text = "1";
            this.threads1MenuButton.Click += new System.EventHandler(this.threads1MenuButton_Click);
            // 
            // threads2MenuButton
            // 
            this.threads2MenuButton.CheckOnClick = true;
            this.threads2MenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.threads2MenuButton.Name = "threads2MenuButton";
            this.threads2MenuButton.Size = new System.Drawing.Size(80, 22);
            this.threads2MenuButton.Text = "2";
            this.threads2MenuButton.Click += new System.EventHandler(this.threads2MenuButton_Click);
            // 
            // threads3MenuButton
            // 
            this.threads3MenuButton.CheckOnClick = true;
            this.threads3MenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.threads3MenuButton.Name = "threads3MenuButton";
            this.threads3MenuButton.Size = new System.Drawing.Size(80, 22);
            this.threads3MenuButton.Text = "3";
            this.threads3MenuButton.Click += new System.EventHandler(this.threads3MenuButton_Click);
            // 
            // threads4MenuButton
            // 
            this.threads4MenuButton.CheckOnClick = true;
            this.threads4MenuButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.threads4MenuButton.Name = "threads4MenuButton";
            this.threads4MenuButton.Size = new System.Drawing.Size(80, 22);
            this.threads4MenuButton.Text = "4";
            this.threads4MenuButton.Click += new System.EventHandler(this.threads4MenuButton_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterAudioToolStripMenuItem});
            this.filterToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.filterToolStripMenuItem.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.filterToolStripMenuItem.Text = "Filter";
            // 
            // filterAudioToolStripMenuItem
            // 
            this.filterAudioToolStripMenuItem.Name = "filterAudioToolStripMenuItem";
            this.filterAudioToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.filterAudioToolStripMenuItem.Text = "Low Pass Filter";
            this.filterAudioToolStripMenuItem.Click += new System.EventHandler(this.filterAudioToolStripMenuItem_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.playButton);
            this.panel8.Controls.Add(this.stopRec);
            this.panel8.Controls.Add(this.recButton);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(967, 27);
            this.panel8.Name = "panel8";
            this.tableLayoutPanel1.SetRowSpan(this.panel8, 4);
            this.panel8.Size = new System.Drawing.Size(120, 536);
            this.panel8.TabIndex = 13;
            // 
            // playButton
            // 
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.Location = new System.Drawing.Point(0, 87);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(102, 23);
            this.playButton.TabIndex = 2;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // stopRec
            // 
            this.stopRec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stopRec.Location = new System.Drawing.Point(0, 58);
            this.stopRec.Name = "stopRec";
            this.stopRec.Size = new System.Drawing.Size(102, 23);
            this.stopRec.TabIndex = 1;
            this.stopRec.Text = "Stop Recording";
            this.stopRec.UseVisualStyleBackColor = true;
            this.stopRec.Click += new System.EventHandler(this.stopRec_Click);
            // 
            // recButton
            // 
            this.recButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.recButton.Location = new System.Drawing.Point(0, 29);
            this.recButton.Name = "recButton";
            this.recButton.Size = new System.Drawing.Size(102, 23);
            this.recButton.TabIndex = 0;
            this.recButton.Text = "Record";
            this.recButton.UseVisualStyleBackColor = true;
            this.recButton.Click += new System.EventHandler(this.recButton_Click);
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.clearFreqButton);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(3, 432);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(165, 131);
            this.panel9.TabIndex = 14;
            // 
            // clearFreqButton
            // 
            this.clearFreqButton.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.clearFreqButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearFreqButton.Location = new System.Drawing.Point(43, 58);
            this.clearFreqButton.Name = "clearFreqButton";
            this.clearFreqButton.Size = new System.Drawing.Size(75, 23);
            this.clearFreqButton.TabIndex = 6;
            this.clearFreqButton.Text = "Clear";
            this.clearFreqButton.UseVisualStyleBackColor = true;
            this.clearFreqButton.Click += new System.EventHandler(this.clearFreqButton_Click);
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.sampUpDown);
            this.panel1.Controls.Add(this.lengthOfData);
            this.panel1.Controls.Add(this.sampLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 129);
            this.panel1.TabIndex = 15;
            // 
            // InputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1090, 566);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "InputForm";
            this.Text = "InputForm";
            this.Load += new System.EventHandler(this.InputForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HFTChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lengthOfData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sampUpDown)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.freqWaveChart)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataVisualization.Charting.Chart HFTChart;
        private System.Windows.Forms.Label sampLabel;
        private System.Windows.Forms.NumericUpDown sampUpDown;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DataVisualization.Charting.Chart freqWaveChart;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem audioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chartToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem plotFrequencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangleWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectangleWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem welchWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem descreteFourierTransformThreadsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem threads1MenuButton;
        private System.Windows.Forms.ToolStripMenuItem threads2MenuButton;
        private System.Windows.Forms.ToolStripMenuItem threads3MenuButton;
        private System.Windows.Forms.ToolStripMenuItem threads4MenuButton;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Button recButton;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button stopRec;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button clearFreqButton;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown lengthOfData;
        private System.Windows.Forms.ToolStripMenuItem hzToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterAudioToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
    }
}