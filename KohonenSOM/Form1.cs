using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KohonenSOM
{
    public partial class Form1 : Form
    {
        private KohonenNetwork? network;
        private readonly List<double[]> trainingData = new();
        private readonly Random random = new();

        private bool isTraining = false;
        private bool stopRequested = false;

        private int currentEpoch = 0;
        private int totalEpochs = 0;
        private double savedLearningRate = 0;

        private readonly List<double> errorHistory = new();

        public Form1()
        {
            InitializeComponent();
            btnExportCsv.Click += BtnExportCsv_Click;
            btnRunExperiments.Click += BtnRunExperiments_Click;
            pictureBoxMap.Paint += PictureBoxMap_Paint;
            pictureBoxError.Paint += PictureBoxError_Paint;
            btnStart.Click += BtnStart_Click;
            btnReset.Click += BtnReset_Click;
            btnStop.Click += BtnStop_Click;
            btnSaveChart.Click += BtnSaveChart_Click;
            GenerateData();
            btnSaveChart.Enabled = false;
            comboBoxChartType.Items.AddRange(new object[]
            {
                "Rozmiar mapy",
                "Liczba epok",
                "Wspó³czynnik uczenia"
            });

            comboBoxChartType.SelectedIndexChanged += (s, e) =>
            {
                pictureBoxExperimentChart.Invalidate();
            };

            pictureBoxExperimentChart.Paint += PictureBoxExperimentChart_Paint;

            comboBoxChartType.SelectedIndex = 0;

        }

        // Generowanie przyk³adowych danych
        private void GenerateData()
        {
            trainingData.Clear();

            // Trzy skupiska punktów
            AddCluster(0.25, 0.25, 100);
            AddCluster(0.75, 0.30, 100);
            AddCluster(0.50, 0.75, 100);

            pictureBoxMap.Invalidate();
        }

        private void AddCluster(double cx, double cy, int count)
        {
            for (int i = 0; i < count; i++)
            {
                double x = Math.Clamp(
                    cx + (random.NextDouble() - 0.5) * 0.25,
                    0, 1);

                double y = Math.Clamp(
                    cy + (random.NextDouble() - 0.5) * 0.25,
                    0, 1);

                trainingData.Add(new double[] { x, y });
            }
        }

        // Rysowanie danych i neuronów
        private void PictureBoxMap_Paint(
            object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = pictureBoxMap.ClientSize.Width;
            int height = pictureBoxMap.ClientSize.Height;
            int margin = 30;

            float ScaleX(double x) =>
                margin + (float)x * (width - 2 * margin);

            float ScaleY(double y) =>
                margin + (float)y * (height - 2 * margin);

            // Punkty danych wejœciowych
            using (Brush dataBrush = new SolidBrush(Color.LightGray))
            {
                foreach (var point in trainingData)
                {
                    float x = ScaleX(point[0]);
                    float y = ScaleY(point[1]);

                    g.FillEllipse(dataBrush, x - 3, y - 3, 6, 6);
                }
            }

            if (network == null)
                return;

            int size = network.MapSize;

            using Pen linePen = new Pen(Color.SteelBlue, 1.5f);
            using Brush neuronBrush = new SolidBrush(Color.DarkBlue);

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    float x = ScaleX(network.Weights[i, j, 0]);
                    float y = ScaleY(network.Weights[i, j, 1]);

                    
                    if (i + 1 < size)
                    {
                        g.DrawLine(linePen, x, y,
                            ScaleX(network.Weights[i + 1, j, 0]),
                            ScaleY(network.Weights[i + 1, j, 1]));
                    }

                    if (j + 1 < size)
                    {
                        g.DrawLine(linePen, x, y,
                            ScaleX(network.Weights[i, j + 1, 0]),
                            ScaleY(network.Weights[i, j + 1, 1]));
                    }

                    g.FillEllipse(neuronBrush, x - 4, y - 4, 8, 8);
                }
            }
        }
        private void BtnStop_Click(object? sender, EventArgs e)
        {
            stopRequested = true;
            btnStop.Enabled = false;
        }

        private void PictureBoxExperimentChart_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Clear(Color.White);

            if (comboBoxChartType.SelectedItem == null)
                return;

            string selectedExperiment = comboBoxChartType.SelectedItem.ToString()!;

            var results = new List<(string label, double error)>();

            foreach (DataGridViewRow row in dataGridViewResults.Rows)
            {
                if (row.IsNewRow)
                    continue;

                string experiment = row.Cells["Experiment"].Value?.ToString() ?? "";

                if (experiment != selectedExperiment)
                    continue;

                string label = selectedExperiment switch
                {
                    "Rozmiar mapy" => row.Cells["Map"].Value?.ToString() ?? "",
                    "Liczba epok" => row.Cells["Epochs"].Value?.ToString() ?? "",
                    _ => row.Cells["Rate"].Value?.ToString() ?? ""
                };

                string errorText = row.Cells["Error"].Value?.ToString() ?? "0";

                if (double.TryParse(errorText, out double error))
                    results.Add((label, error));
            }

            if (results.Count == 0)
            {
                g.DrawString(
                    "Uruchom eksperymenty, aby wyœwietliæ wykres.",
                    Font,
                    Brushes.Gray,
                    20,
                    20);
                return;
            }

            int width = pictureBoxExperimentChart.ClientSize.Width;
            int height = pictureBoxExperimentChart.ClientSize.Height;

            int left = 75;
            int right = width - 30;
            int top = 25;
            int bottom = height - 70;

            if (right <= left || bottom <= top)
                return;

            using Pen axisPen = new Pen(Color.Black, 1.5f);
            using Pen gridPen = new Pen(Color.LightGray, 1);
            using Pen linePen = new Pen(Color.RoyalBlue, 2.5f);
            using Brush pointBrush = new SolidBrush(Color.RoyalBlue);

            
            g.DrawLine(axisPen, left, top, left, bottom);
            g.DrawLine(axisPen, left, bottom, right, bottom);
            
            string yAxisLabel = "Œredni b³¹d kwantyzacji";

            using Font axisFont = new Font("Segoe UI", 9, FontStyle.Bold);

            g.DrawString(
                yAxisLabel,
                axisFont,
                Brushes.Black,
                left + 10,
                4);

            double maxError = results.Max(r => r.error) * 1.15;

            if (maxError <= 0)
                maxError = 1;

            
            for (int i = 0; i <= 5; i++)
            {
                double value = maxError * i / 5.0;
                float y = bottom - (float)(value / maxError * (bottom - top));

                g.DrawLine(gridPen, left, y, right, y);

                g.DrawString(
                    value.ToString("F4"),
                    Font,
                    Brushes.Black,
                    5,
                    y - 7);
            }
            
            string xAxisLabel = selectedExperiment switch
            {
                "Rozmiar mapy" => "Rozmiar mapy",
                "Liczba epok" => "Liczba epok",
                "Wspó³czynnik uczenia" => "Wspó³czynnik uczenia",
                _ => ""
            };

            SizeF xLabelSize = g.MeasureString(xAxisLabel, axisFont);

            g.DrawString(
                xAxisLabel,
                axisFont,
                Brushes.Black,
                (width - xLabelSize.Width) / 2,
                height - 24);
            PointF? previousPoint = null;

            for (int i = 0; i < results.Count; i++)
            {
                float x = results.Count == 1
                    ? (left + right) / 2f
                    : left + i * (right - left) / (float)(results.Count - 1);

                float y = bottom -
                    (float)(results[i].error / maxError * (bottom - top));

                PointF point = new PointF(x, y);

                if (previousPoint.HasValue)
                    g.DrawLine(linePen, previousPoint.Value, point);

                g.FillEllipse(
                    pointBrush,
                    x - 5,
                    y - 5,
                    10,
                    10);

                string valueLabel = results[i].error.ToString("F4");

                SizeF valueSize = g.MeasureString(valueLabel, Font);

                g.DrawString(
                    valueLabel,
                    Font,
                    Brushes.Black,
                    x - valueSize.Width / 2,
                    y - 24);

                string label = results[i].label;

                SizeF labelSize = g.MeasureString(label, Font);

                g.DrawString(
                    label,
                    Font,
                    Brushes.Black,
                    x - labelSize.Width / 2,
                    bottom + 12);

                previousPoint = point;
            }
        }

        private void PictureBoxError_Paint(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int width = pictureBoxError.ClientSize.Width;
            int height = pictureBoxError.ClientSize.Height;

            int left = 45;
            int right = 15;
            int top = 12;
            int bottom = 25;

            int plotWidth = width - left - right;
            int plotHeight = height - top - bottom;

            if (plotWidth <= 0 || plotHeight <= 0)
                return;

            using Pen axisPen = new Pen(Color.Gray, 1);
            using Pen graphPen = new Pen(Color.DarkRed, 2);

            
            g.DrawLine(axisPen, left, top, left, height - bottom);
            g.DrawLine(axisPen, left, height - bottom,
                width - right, height - bottom);

            if (errorHistory.Count == 0)
                return;

            double maxError = Math.Max(0.001, errorHistory.Max());

            using Font font = new Font("Segoe UI", 8);
            using Brush textBrush = new SolidBrush(Color.Black);

            g.DrawString(maxError.ToString("F3"), font,
                textBrush, 2, top);

            g.DrawString("0", font, textBrush,
                25, height - bottom - 8);

            g.DrawString("Epoki", font, textBrush,
                width / 2 - 20, height - 20);

            if (errorHistory.Count < 2)
                return;

            PointF[] points = new PointF[errorHistory.Count];

            for (int i = 0; i < errorHistory.Count; i++)
            {
                float x = left +
                    (float)i / (errorHistory.Count - 1) * plotWidth;

                float y = top + plotHeight -
                    (float)(errorHistory[i] / maxError) * plotHeight;

                points[i] = new PointF(x, y);
            }

            g.DrawLines(graphPen, points);
        }

        // Rozpoczêcie uczenia

        private async void BtnStart_Click(object? sender, EventArgs e)
        {
            if (isTraining)
                return;

            isTraining = true;
            stopRequested = false;

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            btnReset.Enabled = false;
            numMapSize.Enabled = false;
            numEpochs.Enabled = false;
            numLearningRate.Enabled = false;

            try
            {
                // Nowe uczenie - tylko jeœli nie ma przerwanej sieci
                if (network == null || currentEpoch >= totalEpochs)
                {
                    int mapSize = (int)numMapSize.Value;

                    totalEpochs = (int)numEpochs.Value;
                    savedLearningRate = (double)numLearningRate.Value;

                    network = new KohonenNetwork(mapSize);



                    pictureBoxMap.Invalidate();
                    pictureBoxMap.Update();

                    MessageBox.Show(
                        "Sieæ zosta³a zainicjalizowana.\n" + "", "Epoka 0",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);





                    currentEpoch = 0;
                    errorHistory.Clear();

                    progressBarTraining.Value = 0;
                    pictureBoxError.Invalidate();

                    lblEpoch.Text = $"Epoka: 0 / {totalEpochs}";
                    lblError.Text = "B³¹d kwantyzacji: —";
                }

                
                for (int epoch = currentEpoch; epoch < totalEpochs; epoch++)
                {
                    if (stopRequested)
                        break;

                    double progress = (double)epoch / totalEpochs;

                    double rate = savedLearningRate *
                                  Math.Exp(-3 * progress);

                    double radius = Math.Max(
                        0.5,
                        (network.MapSize / 2.0) *
                        Math.Exp(-3 * progress));

                    
                    network.TrainEpoch(trainingData, rate, radius);

                    
                    currentEpoch = epoch + 1;

                    lblEpoch.Text =
                        $"Epoka: {currentEpoch} / {totalEpochs}";

                    
                    int progressPercent =
                        (int)(currentEpoch * 100.0 / totalEpochs);

                    progressBarTraining.Value = progressPercent;
                    progressBarTraining.Value = Math.Min(
                        progressPercent + 1,
                        progressBarTraining.Maximum);
                    progressBarTraining.Value = progressPercent;

                    
                    double currentError =
                        network.CalculateError(trainingData);

                    errorHistory.Add(currentError);

                    lblError.Text =
                        $"B³¹d kwantyzacji: {currentError:F4}";

                    
                    pictureBoxMap.Invalidate();
                    pictureBoxError.Invalidate();

                    await Task.Delay(30);
                }

                double error = network.CalculateError(trainingData);
                
                if (stopRequested)
                {
                    btnStart.Text = "Wznów uczenie";
                    /*
                    MessageBox.Show(
                        $"Uczenie zatrzymane!\n\n" +
                        $"Epoka: {currentEpoch} / {totalEpochs}\n" +
                        $"B³¹d kwantyzacji: {error:F4}",
                        "Uczenie zatrzymane",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);*/
                }
                else
                {
                    btnStart.Text = "Rozpocznij uczenie";
                    
                    MessageBox.Show(
                        $"Uczenie zakoñczone!\n\n" +
                        $"Liczba epok: {currentEpoch}\n" +
                        $"B³¹d kwantyzacji: {error:F4}",
                        "Wyniki uczenia",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
            finally
            {
                isTraining = false;

                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnReset.Enabled = true;
                bool trainingFinished = currentEpoch >= totalEpochs;

                numMapSize.Enabled = trainingFinished;
                numEpochs.Enabled = trainingFinished;
                numLearningRate.Enabled = trainingFinished;
            }
        }


        private void BtnSaveChart_Click(object? sender, EventArgs e)
        {
            if (dataGridViewResults.Rows.Count == 0)
            {
                MessageBox.Show(
                    "Najpierw uruchom eksperymenty.",
                    "Brak danych",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "Obraz PNG (*.png)|*.png";
            saveDialog.Title = "Zapisz wykres eksperymentów";

            string chartName = comboBoxChartType.SelectedItem?.ToString()
                ?? "Wykres";

            string safeName = chartName
                .Replace(" ", "_")
                .Replace("³", "l")
                .Replace("ó", "o")
                .Replace("œ", "s")
                .Replace("æ", "c")
                .Replace("ê", "e");

            saveDialog.FileName = $"SOM_{safeName}.png";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                using Bitmap bitmap = new Bitmap(
                    pictureBoxExperimentChart.Width,
                    pictureBoxExperimentChart.Height);

                pictureBoxExperimentChart.DrawToBitmap(
                    bitmap,
                    new Rectangle(
                        0,
                        0,
                        bitmap.Width,
                        bitmap.Height));

                bitmap.Save(
                    saveDialog.FileName,
                    System.Drawing.Imaging.ImageFormat.Png);

                MessageBox.Show(
                    "Wykres zosta³ zapisany!",
                    "Eksport PNG",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Nie uda³o siê zapisaæ wykresu:\n{ex.Message}",
                    "B³¹d",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // Resetowanie sieci

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            if (isTraining)
                return;

            // Usuniêcie dotychczasowej sieci
            network = null;

            // Wyzerowanie postêpu
            currentEpoch = 0;
            totalEpochs = 0;
            savedLearningRate = 0;
            stopRequested = false;

            // Wyczyszczenie historii b³êdu
            errorHistory.Clear();

            // Wygenerowanie nowych danych
            GenerateData();

            // Resetowanie interfejsu
            lblEpoch.Text = $"Epoka: 0 / {numEpochs.Value}";
            lblError.Text = "B³¹d kwantyzacji: —";

            progressBarTraining.Value = 0;

            btnStart.Text = "Rozpocznij uczenie";
            btnStop.Enabled = false;
            numMapSize.Enabled = true;
            numEpochs.Enabled = true;
            numLearningRate.Enabled = true;
            // Odœwie¿enie wizualizacji
            pictureBoxMap.Invalidate();
            pictureBoxError.Invalidate();
            
        }

        
        private void BtnExportCsv_Click(object? sender, EventArgs e)
        {
            if (dataGridViewResults.Rows.Count == 0)
            {
                MessageBox.Show("Brak wyników do eksportu.");
                return;
            }

            using SaveFileDialog saveDialog = new SaveFileDialog();

            saveDialog.Filter = "Pliki CSV (*.csv)|*.csv";
            saveDialog.FileName = "Wyniki_eksperymentow_SOM.csv";
            saveDialog.Title = "Zapisz wyniki eksperymentów";

            if (saveDialog.ShowDialog() != DialogResult.OK)
                return;

            try
            {
                StringBuilder csv = new StringBuilder();

                
                var headers = dataGridViewResults.Columns
                    .Cast<DataGridViewColumn>()
                    .Select(column => column.HeaderText);

                csv.AppendLine(string.Join(";", headers));

                
                foreach (DataGridViewRow row in dataGridViewResults.Rows)
                {
                    if (row.IsNewRow)
                        continue;

                    var values = row.Cells
                        .Cast<DataGridViewCell>()
                        .Select(cell => EscapeCsv(cell.Value?.ToString() ?? ""));

                    csv.AppendLine(string.Join(";", values));
                }

                
                File.WriteAllText(
                    saveDialog.FileName,
                    csv.ToString(),
                    new UTF8Encoding(true));

                MessageBox.Show(
                    "Wyniki zosta³y zapisane pomyœlnie!",
                    "Eksport zakoñczony",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Nie uda³o siê zapisaæ pliku:\n{ex.Message}",
                    "B³¹d eksportu",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private string EscapeCsv(string value)
        {
            if (value.Contains(';') ||
                value.Contains('"') ||
                value.Contains('\n'))
            {
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            }

            return value;
        }
        private async void BtnRunExperiments_Click(object? sender, EventArgs e)
        {
            btnRunExperiments.Enabled = false;
            btnExportCsv.Enabled = false;
            btnSaveChart.Enabled = false;

            dataGridViewResults.Rows.Clear();
            dataGridViewResults.Columns.Clear();

            dataGridViewResults.Columns.Add("Experiment", "Eksperyment");
            dataGridViewResults.Columns.Add("Map", "Mapa");
            dataGridViewResults.Columns.Add("Epochs", "Epoki");
            dataGridViewResults.Columns.Add("Rate", "Uczenie");
            dataGridViewResults.Columns.Add("Error", "Œredni b³¹d");
            dataGridViewResults.Columns.Add("Time", "Œredni czas (ms)");

            var configurations = new List<(string name, int size, int epochs, double rate)>();

            foreach (int size in new[] { 4, 8, 12 })
                configurations.Add(("Rozmiar mapy", size, 100, 0.30));

            foreach (int epochs in new[] { 10, 50, 100, 500 })
                configurations.Add(("Liczba epok", 8, epochs, 0.30));

            foreach (double rate in new[] { 0.05, 0.10, 0.30, 0.50 })
                configurations.Add(("Wspó³czynnik uczenia", 8, 100, rate));

            const int repetitions = 5;

            try
            {
                for (int i = 0; i < configurations.Count; i++)
                {
                    var config = configurations[i];

                    lblExperimentStatus.Text =
                        $"Eksperyment {i + 1} / {configurations.Count}: {config.name}";

                    // Trening wykonujemy poza w¹tkiem interfejsu
                    var result = await Task.Run(() =>
                    {
                        double totalError = 0;
                        double totalTime = 0;

                        for (int repeat = 0; repeat < repetitions; repeat++)
                        {
                            var som = new KohonenNetwork(config.size);
                            var stopwatch = Stopwatch.StartNew();

                            for (int epoch = 0; epoch < config.epochs; epoch++)
                            {
                                double progress = (double)epoch / config.epochs;

                                double rate = config.rate *
                                              Math.Exp(-3 * progress);

                                double radius = Math.Max(
                                    0.5,
                                    (config.size / 2.0) *
                                    Math.Exp(-3 * progress));

                                som.TrainEpoch(trainingData, rate, radius);
                            }

                            stopwatch.Stop();

                            totalError += som.CalculateError(trainingData);
                            totalTime += stopwatch.Elapsed.TotalMilliseconds;
                        }

                        return (
                            error: totalError / repetitions,
                            time: totalTime / repetitions
                        );
                    });

                    dataGridViewResults.Rows.Add(
                        config.name,
                        $"{config.size}×{config.size}",
                        config.epochs,
                        config.rate.ToString("F2"),
                        result.error.ToString("F4"),
                        result.time.ToString("F2")
                    );
                }

                lblExperimentStatus.Text = "Eksperymenty zakoñczone!";
                pictureBoxExperimentChart.Invalidate();

                btnExportCsv.Enabled = true;
                btnSaveChart.Enabled = true;
                MessageBox.Show("Wszystkie eksperymenty zosta³y zakoñczone.","Eksperymenty SOM",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                lblExperimentStatus.Text = "B³¹d eksperymentu";

                MessageBox.Show(ex.Message, "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnRunExperiments.Enabled = true;
            }
        }

    }
}
