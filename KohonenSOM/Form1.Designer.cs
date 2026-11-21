namespace KohonenSOM
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            tabControlMain = new TabControl();
            tabPageSimulation = new TabPage();
            btnStop = new Button();
            progressBarTraining = new ProgressBar();
            pictureBoxError = new PictureBox();
            lblError = new Label();
            lblEpoch = new Label();
            btnReset = new Button();
            btnStart = new Button();
            pictureBoxMap = new PictureBox();
            groupBoxParameters = new GroupBox();
            numLearningRate = new NumericUpDown();
            label1 = new Label();
            label3 = new Label();
            numEpochs = new NumericUpDown();
            numMapSize = new NumericUpDown();
            label2 = new Label();
            tabPageExperiments = new TabPage();
            btnSaveChart = new Button();
            lblChartTitle = new Label();
            pictureBoxExperimentChart = new PictureBox();
            comboBoxChartType = new ComboBox();
            lblExperimentStatus = new Label();
            btnExportCsv = new Button();
            btnRunExperiments = new Button();
            dataGridViewResults = new DataGridView();
            tabControlMain.SuspendLayout();
            tabPageSimulation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxError).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMap).BeginInit();
            groupBoxParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numLearningRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numEpochs).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMapSize).BeginInit();
            tabPageExperiments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxExperimentChart).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).BeginInit();
            SuspendLayout();
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabPageSimulation);
            tabControlMain.Controls.Add(tabPageExperiments);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Location = new Point(0, 0);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(1360, 908);
            tabControlMain.TabIndex = 9;
            // 
            // tabPageSimulation
            // 
            tabPageSimulation.Controls.Add(btnStop);
            tabPageSimulation.Controls.Add(progressBarTraining);
            tabPageSimulation.Controls.Add(pictureBoxError);
            tabPageSimulation.Controls.Add(lblError);
            tabPageSimulation.Controls.Add(lblEpoch);
            tabPageSimulation.Controls.Add(btnReset);
            tabPageSimulation.Controls.Add(btnStart);
            tabPageSimulation.Controls.Add(pictureBoxMap);
            tabPageSimulation.Controls.Add(groupBoxParameters);
            tabPageSimulation.Location = new Point(4, 24);
            tabPageSimulation.Name = "tabPageSimulation";
            tabPageSimulation.Padding = new Padding(3);
            tabPageSimulation.Size = new Size(1352, 880);
            tabPageSimulation.TabIndex = 0;
            tabPageSimulation.Text = "Symulacja";
            tabPageSimulation.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(43, 392);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(260, 40);
            btnStop.TabIndex = 17;
            btnStop.Text = "Zatrzymaj uczenie";
            btnStop.UseVisualStyleBackColor = true;
            // 
            // progressBarTraining
            // 
            progressBarTraining.Location = new Point(43, 521);
            progressBarTraining.Name = "progressBarTraining";
            progressBarTraining.Size = new Size(230, 23);
            progressBarTraining.Step = 1;
            progressBarTraining.Style = ProgressBarStyle.Continuous;
            progressBarTraining.TabIndex = 16;
            // 
            // pictureBoxError
            // 
            pictureBoxError.BackColor = Color.White;
            pictureBoxError.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxError.Location = new Point(333, 596);
            pictureBoxError.Name = "pictureBoxError";
            pictureBoxError.Size = new Size(800, 110);
            pictureBoxError.TabIndex = 15;
            pictureBoxError.TabStop = false;
            // 
            // lblError
            // 
            lblError.AutoSize = true;
            lblError.Font = new Font("Microsoft Sans Serif", 8.25F);
            lblError.Location = new Point(49, 568);
            lblError.Name = "lblError";
            lblError.Size = new Size(100, 13);
            lblError.TabIndex = 14;
            lblError.Text = "Błąd kwantyzacji: —";
            // 
            // lblEpoch
            // 
            lblEpoch.AutoSize = true;
            lblEpoch.Font = new Font("Microsoft Sans Serif", 8.25F);
            lblEpoch.Location = new Point(49, 505);
            lblEpoch.Name = "lblEpoch";
            lblEpoch.Size = new Size(79, 13);
            lblEpoch.TabIndex = 13;
            lblEpoch.Text = "Epoka: 0 / 100";
            // 
            // btnReset
            // 
            btnReset.Location = new Point(43, 438);
            btnReset.Name = "btnReset";
            btnReset.Size = new Size(260, 40);
            btnReset.TabIndex = 12;
            btnReset.Text = "Reset";
            btnReset.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            btnStart.Location = new Point(43, 346);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(260, 40);
            btnStart.TabIndex = 11;
            btnStart.Text = "Rozpocznij uczenie";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // pictureBoxMap
            // 
            pictureBoxMap.BackColor = Color.White;
            pictureBoxMap.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxMap.Location = new Point(333, 26);
            pictureBoxMap.Name = "pictureBoxMap";
            pictureBoxMap.Size = new Size(800, 550);
            pictureBoxMap.TabIndex = 10;
            pictureBoxMap.TabStop = false;
            // 
            // groupBoxParameters
            // 
            groupBoxParameters.Controls.Add(numLearningRate);
            groupBoxParameters.Controls.Add(label1);
            groupBoxParameters.Controls.Add(label3);
            groupBoxParameters.Controls.Add(numEpochs);
            groupBoxParameters.Controls.Add(numMapSize);
            groupBoxParameters.Controls.Add(label2);
            groupBoxParameters.Location = new Point(43, 26);
            groupBoxParameters.Name = "groupBoxParameters";
            groupBoxParameters.Size = new Size(260, 300);
            groupBoxParameters.TabIndex = 9;
            groupBoxParameters.TabStop = false;
            groupBoxParameters.Text = "Parametry sieci";
            // 
            // numLearningRate
            // 
            numLearningRate.DecimalPlaces = 2;
            numLearningRate.Increment = new decimal(new int[] { 5, 0, 0, 131072 });
            numLearningRate.Location = new Point(6, 248);
            numLearningRate.Maximum = new decimal(new int[] { 1, 0, 0, 0 });
            numLearningRate.Minimum = new decimal(new int[] { 1, 0, 0, 131072 });
            numLearningRate.Name = "numLearningRate";
            numLearningRate.Size = new Size(120, 23);
            numLearningRate.TabIndex = 9;
            numLearningRate.Value = new decimal(new int[] { 3, 0, 0, 65536 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 66);
            label1.Name = "label1";
            label1.Size = new Size(126, 15);
            label1.TabIndex = 4;
            label1.Text = "Rozmiar mapy (N × N)";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 230);
            label3.Name = "label3";
            label3.Size = new Size(123, 15);
            label3.TabIndex = 6;
            label3.Text = "Współczynnik uczenia";
            // 
            // numEpochs
            // 
            numEpochs.Location = new Point(6, 176);
            numEpochs.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            numEpochs.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numEpochs.Name = "numEpochs";
            numEpochs.Size = new Size(120, 23);
            numEpochs.TabIndex = 8;
            numEpochs.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // numMapSize
            // 
            numMapSize.Location = new Point(6, 84);
            numMapSize.Maximum = new decimal(new int[] { 30, 0, 0, 0 });
            numMapSize.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            numMapSize.Name = "numMapSize";
            numMapSize.Size = new Size(120, 23);
            numMapSize.TabIndex = 7;
            numMapSize.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 158);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 5;
            label2.Text = "Liczba epok";
            // 
            // tabPageExperiments
            // 
            tabPageExperiments.Controls.Add(btnSaveChart);
            tabPageExperiments.Controls.Add(lblChartTitle);
            tabPageExperiments.Controls.Add(pictureBoxExperimentChart);
            tabPageExperiments.Controls.Add(comboBoxChartType);
            tabPageExperiments.Controls.Add(lblExperimentStatus);
            tabPageExperiments.Controls.Add(btnExportCsv);
            tabPageExperiments.Controls.Add(btnRunExperiments);
            tabPageExperiments.Controls.Add(dataGridViewResults);
            tabPageExperiments.Location = new Point(4, 24);
            tabPageExperiments.Name = "tabPageExperiments";
            tabPageExperiments.Padding = new Padding(3);
            tabPageExperiments.Size = new Size(1352, 880);
            tabPageExperiments.TabIndex = 1;
            tabPageExperiments.Text = "Eksperymenty";
            tabPageExperiments.UseVisualStyleBackColor = true;
            // 
            // btnSaveChart
            // 
            btnSaveChart.Enabled = false;
            btnSaveChart.Location = new Point(765, 375);
            btnSaveChart.Name = "btnSaveChart";
            btnSaveChart.Size = new Size(200, 40);
            btnSaveChart.TabIndex = 7;
            btnSaveChart.Text = "Zapisz wykres PNG";
            btnSaveChart.UseVisualStyleBackColor = true;
            // 
            // lblChartTitle
            // 
            lblChartTitle.AutoSize = true;
            lblChartTitle.Location = new Point(52, 533);
            lblChartTitle.Name = "lblChartTitle";
            lblChartTitle.Size = new Size(208, 15);
            lblChartTitle.TabIndex = 6;
            lblChartTitle.Text = "Wykres porównawczy eksperymentów";
            // 
            // pictureBoxExperimentChart
            // 
            pictureBoxExperimentChart.BackColor = Color.White;
            pictureBoxExperimentChart.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxExperimentChart.Location = new Point(52, 551);
            pictureBoxExperimentChart.Name = "pictureBoxExperimentChart";
            pictureBoxExperimentChart.Size = new Size(750, 260);
            pictureBoxExperimentChart.TabIndex = 5;
            pictureBoxExperimentChart.TabStop = false;
            // 
            // comboBoxChartType
            // 
            comboBoxChartType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxChartType.FormattingEnabled = true;
            comboBoxChartType.Location = new Point(52, 476);
            comboBoxChartType.Name = "comboBoxChartType";
            comboBoxChartType.Size = new Size(260, 23);
            comboBoxChartType.TabIndex = 4;
            // 
            // lblExperimentStatus
            // 
            lblExperimentStatus.AutoSize = true;
            lblExperimentStatus.Location = new Point(279, 375);
            lblExperimentStatus.Name = "lblExperimentStatus";
            lblExperimentStatus.Size = new Size(244, 15);
            lblExperimentStatus.TabIndex = 3;
            lblExperimentStatus.Text = "Gotowy do przeprowadzenia eksperymentów";
            // 
            // btnExportCsv
            // 
            btnExportCsv.Enabled = false;
            btnExportCsv.Location = new Point(548, 375);
            btnExportCsv.Name = "btnExportCsv";
            btnExportCsv.Size = new Size(200, 40);
            btnExportCsv.TabIndex = 2;
            btnExportCsv.Text = "Eksportuj do CSV";
            btnExportCsv.UseVisualStyleBackColor = true;
            // 
            // btnRunExperiments
            // 
            btnRunExperiments.Location = new Point(52, 375);
            btnRunExperiments.Name = "btnRunExperiments";
            btnRunExperiments.Size = new Size(200, 40);
            btnRunExperiments.TabIndex = 1;
            btnRunExperiments.Text = "Uruchom eksperymenty";
            btnRunExperiments.UseVisualStyleBackColor = true;
            // 
            // dataGridViewResults
            // 
            dataGridViewResults.AllowUserToAddRows = false;
            dataGridViewResults.AllowUserToDeleteRows = false;
            dataGridViewResults.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewResults.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResults.Location = new Point(52, 26);
            dataGridViewResults.Name = "dataGridViewResults";
            dataGridViewResults.ReadOnly = true;
            dataGridViewResults.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewResults.Size = new Size(750, 329);
            dataGridViewResults.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1360, 908);
            Controls.Add(tabControlMain);
            MinimumSize = new Size(1000, 700);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Symulator sieci Kohonena (SOM)";
            tabControlMain.ResumeLayout(false);
            tabPageSimulation.ResumeLayout(false);
            tabPageSimulation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxError).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxMap).EndInit();
            groupBoxParameters.ResumeLayout(false);
            groupBoxParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numLearningRate).EndInit();
            ((System.ComponentModel.ISupportInitialize)numEpochs).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMapSize).EndInit();
            tabPageExperiments.ResumeLayout(false);
            tabPageExperiments.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxExperimentChart).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResults).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private TabControl tabControlMain;
        private TabPage tabPageSimulation;
        private TabPage tabPageExperiments;
        private Button btnStop;
        private ProgressBar progressBarTraining;
        private PictureBox pictureBoxError;
        private Label lblError;
        private Label lblEpoch;
        private Button btnReset;
        private Button btnStart;
        private PictureBox pictureBoxMap;
        private GroupBox groupBoxParameters;
        private NumericUpDown numLearningRate;
        private Label label1;
        private Label label3;
        private NumericUpDown numEpochs;
        private NumericUpDown numMapSize;
        private Label label2;
        private Label lblExperimentStatus;
        private Button btnExportCsv;
        private Button btnRunExperiments;
        private DataGridView dataGridViewResults;
        private PictureBox pictureBoxExperimentChart;
        private ComboBox comboBoxChartType;
        private Label lblChartTitle;
        private Button btnSaveChart;
    }
}
