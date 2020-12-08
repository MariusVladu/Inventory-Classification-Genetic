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

        private List<double> generationsPlotData;
        private List<double> averageScorePlotData;
        private List<double> bestScorePlotData;

        public InventoryClassificationGenetic()
        {
            InitializeComponent();

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
            selectionOperator = new RouletteWheelSelection();
            elitistSelection = new ElitistSelection();
            crossoverOperator = new ContinuousUniformCrossover();
            mutationOperator = new WeightsMutation();
            initialPopulationProvider = new InitialPopulationProvider();

            settings = new Settings
            {
                Inventory = inventory,
                NumberOfCriterias = 3,
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

            buttonNextGeneration.Enabled = true;
            buttonRun.Enabled = true;
            buttonReset.Enabled = true;
        }

        private void buttonNextGeneration_Click(object sender, EventArgs e)
        {
            geneticAlgorithm.ComputeNextGeneration();

            UpdatePlotData();
            Plot();
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
            labelGenerationInfo.Text = $"Generation #{generationNumberString}\nAverage: {averageScore2DecimalPlaces}\nBest Solution: {geneticAlgorithm.CurrentBestSolution}\nBest Score: {Math.Round(geneticAlgorithm.CurrentBestSolution.FitnessScore, 4)}";
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
                }
            }

            Plot();
            DisplayEllapsedTime(stopwatch.Elapsed);
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            InitializeGeneticAlgorithm();
            Plot();
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

            InitializeGeneticAlgorithm();
            labelLoadedFileInfo.Text = $"{new FileInfo(openFileDialog.FileName).Name} - {inventory.Count} items";
        }
    }
}
