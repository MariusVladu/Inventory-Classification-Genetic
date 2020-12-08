using InventoryClassificationGenetic.Algorithm;
using InventoryClassificationGenetic.Algorithm.Contracts;
using InventoryClassificationGenetic.Domain;
using InventoryClassificationGenetic.Providers;
using InventoryClassificationGenetic.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using InventoryClassificationGenetic.Algorithm.SelectionOperators;
using InventoryClassificationGenetic.Algorithm.CrossoverOperators;
using InventoryClassificationGenetic.Algorithm.MutationOperators;
using System.IO;

namespace InventoryClassificationGenetic.UI
{
    public partial class InventoryClassificationGenetic : Form
    {
        private GeneticAlgorithm geneticAlgorithm;
        private IFitnessFunction fitnessFunction;
        private ISelectionOperator selectionOperator;
        private IElitistSelection elitistSelection;
        private ICrossoverOperator crossoverOperator;
        private IMutationOperator mutationOperator;
        private IInitialPopulationProvider initialPopulationProvider;
        private Settings settings;

        private List<Item> inventory;
        //private List<Point> citiesForDisplay;
        private Graphics bestSolutionGraphics;
        private Pen linePen;
        private Pen pointPen;
        private int lineWidth = 3;
        private int pointWidth = 6;

        private List<double> generationsPlotData;
        private List<double> averageScorePlotData;
        private List<double> bestScorePlotData;

        public InventoryClassificationGenetic()
        {
            InitializeComponent();

            bestSolutionGraphics = panelBestSolution.CreateGraphics();
            linePen = new Pen(Brushes.LightBlue, lineWidth);
            pointPen = Pens.DarkRed;

            chartAverageScore.plt.XLabel("Generation #");
            chartAverageScore.plt.YLabel("Average Fitness Score");
            chartBestScore.plt.XLabel("Generation #");
            chartBestScore.plt.YLabel("Best Fitness Score");

            chartAverageScore.Render();
            chartBestScore.Render();

            buttonNextGeneration.Enabled = false;
            buttonRun.Enabled = false;
            buttonReset.Enabled = false;
        }

        private void InitializeGeneticAlgorithm()
        {
            fitnessFunction = new FitnessFunction(inventory);
            selectionOperator = new TournamentSelection(Convert.ToInt32(inputTournamentSize.Value));
            elitistSelection = new ElitistSelection();
            crossoverOperator = GetSelectedCrossoverOperator();
            mutationOperator = GetSelectedMutationOperator();
            initialPopulationProvider = new InitialPopulationProvider();

            settings = new Settings
            {
                Inventory = inventory,
                NumberOfElites = Convert.ToInt32(inputElites.Value),
                PopulationSize = Convert.ToInt32(inputMaxPopulation.Value),
                CrossoverRate = Convert.ToDouble(inputCrossoverRate.Value),
                MutationRate = Convert.ToDouble(inputMutationRate.Value)
            };

            selectionOperator = new TournamentSelection(Convert.ToInt32(inputTournamentSize.Value));

            geneticAlgorithm = new GeneticAlgorithm(settings, initialPopulationProvider, fitnessFunction, selectionOperator, elitistSelection, crossoverOperator, mutationOperator);

            generationsPlotData = new List<double>();
            averageScorePlotData = new List<double>();
            bestScorePlotData = new List<double>();
            UpdatePlotData();
            Plot();
            //DrawSolution(geneticAlgorithm.CurrentBestSolution, panelBestSolution, bestSolutionGraphics);

            buttonNextGeneration.Enabled = true;
            buttonRun.Enabled = true;
            buttonReset.Enabled = true;
        }

        private void buttonNextGeneration_Click(object sender, EventArgs e)
        {
            geneticAlgorithm.ComputeNextGeneration();

            UpdatePlotData();
            Plot();
            //DrawSolution(geneticAlgorithm.CurrentBestSolution, panelBestSolution, bestSolutionGraphics);
        }

        private void UpdatePlotData()
        {
            generationsPlotData.Add(geneticAlgorithm.CurrentGenerationNumber);
            averageScorePlotData.Add(geneticAlgorithm.AverageScore);
            bestScorePlotData.Add(geneticAlgorithm.CurrentBestSolution.FitnessScore);
        }

        private void Plot()
        {
            var generationsPlotArray = generationsPlotData.ToArray();

            chartAverageScore.plt.Clear();
            chartAverageScore.plt.PlotScatter(generationsPlotArray, averageScorePlotData.ToArray(), Color.Blue);
            chartAverageScore.plt.AxisAuto();
            chartAverageScore.Render();

            chartBestScore.plt.Clear();
            chartBestScore.plt.PlotScatter(generationsPlotArray, bestScorePlotData.ToArray(), Color.Green);
            chartBestScore.plt.AxisAuto();
            chartBestScore.Render();

            ShowBestSolution();
        }

        private void ShowBestSolution()
        {
            if (geneticAlgorithm == null) return;

            var generationNumberString = geneticAlgorithm.CurrentGenerationNumber.ToString().PadLeft(4, '0'); ;
            var averageScore2DecimalPlaces = string.Format("{0:00.00}", geneticAlgorithm.AverageScore);
            labelGenerationInfo.Text = $"Generation #{generationNumberString}\nAverage: {averageScore2DecimalPlaces}\nBest Solution: {geneticAlgorithm.CurrentBestSolution}\nBest Score: {geneticAlgorithm.CurrentBestSolution.FitnessScore}";
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            InitializeGeneticAlgorithm();

            var stopwatch = new Stopwatch();

            for (int i = 0; i < inputGenerationsNumber.Value; i++)
            {
                stopwatch.Start();
                geneticAlgorithm.ComputeNextGeneration();
                stopwatch.Stop();

                UpdatePlotData();

                if (i % 50 == 0)
                {
                    Plot();
                    //DrawSolution(geneticAlgorithm.CurrentBestSolution, panelBestSolution, bestSolutionGraphics);
                }
            }

            Plot();
           // DrawSolution(geneticAlgorithm.CurrentBestSolution, panelBestSolution, bestSolutionGraphics);
            DisplayEllapsedTime(stopwatch.Elapsed);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            InitializeGeneticAlgorithm();
            Plot();
           // DrawSolution(geneticAlgorithm.CurrentBestSolution, panelBestSolution, bestSolutionGraphics);
        }

        private void DisplayEllapsedTime(TimeSpan elapsedTime)
        {
            labelGenerationInfo.Text += $"\nElapsed Time: {elapsedTime.TotalMilliseconds} ms";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.ShowDialog();

            if (string.IsNullOrWhiteSpace(openFileDialog.FileName)) return;

            var fileCitiesProvider = new FileInventoryProvider(openFileDialog.FileName);

            inventory = fileCitiesProvider.Inventory;
            //citiesForDisplay = GetCitiesForDisplay();

            InitializeGeneticAlgorithm();
            labelCitiesInfo.Text = $"{new FileInfo(openFileDialog.FileName).Name} - {inventory.Count} cities";
        }

        //private List<Point> GetCitiesForDisplay()
        //{
        //    var citiesForDisplay = new List<City>();

        //    foreach (var city in inventory)
        //        citiesForDisplay.Add(new City
        //        {
        //            X = Math.Round(city.X),
        //            Y = Math.Round(city.Y)
        //        });

        //    var minX = inventory.Min(c => c.X);
        //    var maxX = inventory.Max(c => c.X);
        //    var offsetX = 0 - minX;
        //    var rangeX = GetRange(minX, maxX);

        //    var minY = inventory.Min(c => c.Y);
        //    var maxY = inventory.Max(c => c.Y);
        //    var offsetY = 0 - minY;
        //    var rangeY = GetRange(minY, maxY);

        //    foreach (var city in citiesForDisplay)
        //    {
        //        city.X += offsetX;
        //        city.Y += offsetY;
        //    }

        //    var scaleX = panelBestSolution.Size.Width;
        //    var scaleY = panelBestSolution.Size.Height;

        //    foreach (var city in citiesForDisplay)
        //    {
        //        city.X = (city.X / rangeX) * scaleX;
        //        city.Y = (city.Y / rangeY) * scaleY;
        //    }

        //    foreach (var city in citiesForDisplay)
        //    {
        //        city.Y = scaleY / 2 - (city.Y - scaleY / 2);
        //    }

        //    return citiesForDisplay
        //        .Select(c => new Point((int)Math.Round(c.X), (int)Math.Round(c.Y)))
        //        .ToList();
        //}

        private double GetRange(double x1, double x2)
        {
            if (x1 <= 0 && x2 >= 0)
                return x2 - x1;

            if (x2 <= 0 && x1 >= 0)
                return x1 - x2;
            
            return Math.Abs(Math.Abs(x1) - Math.Abs(x2));
        }

        //private void DrawSolution(Solution solution, Panel panel, Graphics graphics)
        //{
        //    graphics.Clear(Color.DarkGray);

        //    for (int i = 1; i < solution.Individual.Weights.Length; i++)
        //        graphics.DrawLine(
        //            linePen,
        //            citiesForDisplay[solution.Individual.Weights[i - 1]],
        //            citiesForDisplay[solution.Individual.Weights[i]]);

        //    foreach (var city in citiesForDisplay)
        //        graphics.FillRectangle(pointPen.Brush, city.X - pointWidth / 2, city.Y - pointWidth / 2, pointWidth, pointWidth);
        //}

        private void panelBestSolution_Paint(object sender, PaintEventArgs e)
        {
            if (geneticAlgorithm == null) return;

            //DrawSolution(geneticAlgorithm.CurrentBestSolution, panelBestSolution, bestSolutionGraphics);
        }

        private ICrossoverOperator GetSelectedCrossoverOperator()
        {
            if (radioCycle.Checked) return new CycleCrossover();

            if (radioOrderOne.Checked) return new OrderOneCrossover();

            if (radioPMX.Checked) return new PMXCrossover();

            throw new InvalidOperationException();
        }

        private IMutationOperator GetSelectedMutationOperator()
        {
            if (radioInsert.Checked) return new InsertMutation();

            if (radioInversion.Checked) return new InversionMutation();

            if (radioScramble.Checked) return new ScrambleMutation();

            if (radioSwap.Checked) return new SwapMutation();

            throw new InvalidOperationException();
        }
    }
}
