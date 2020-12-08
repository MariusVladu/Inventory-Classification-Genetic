using TSPGenetic.Algorithm.Contracts;
using TSPGenetic.Domain;
using TSPGenetic.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TSPGenetic.Algorithm
{
    public class GeneticAlgorithm
    {
        private readonly IFitnessFunction fitnessFunction;
        private readonly ISelectionOperator selectionOperator;
        private readonly IElitistSelection elitistSelection;
        private readonly ICrossoverOperator crossoverOperator;
        private readonly IMutationOperator mutationOperator;
        private static readonly Random random = new Random();

        private readonly Settings settings;

        private List<Individual> currentPopulation;
        public List<Solution> CurrentSolutions;
        public int CurrentGenerationNumber;
        public double AverageScore;
        public Solution CurrentBestSolution;

        public GeneticAlgorithm(Settings settings,
            IInitialPopulationProvider initialPopulationProvider,
            IFitnessFunction fitnessFunction,
            ISelectionOperator selectionOperator,
            IElitistSelection elitistSelection,
            ICrossoverOperator crossoverOperator,
            IMutationOperator mutationOperator)
        {
            this.settings = settings;
            this.fitnessFunction = fitnessFunction;
            this.selectionOperator = selectionOperator;
            this.elitistSelection = elitistSelection;
            this.crossoverOperator = crossoverOperator;
            this.mutationOperator = mutationOperator;

            currentPopulation = initialPopulationProvider.GetInitialPopulation(settings.PopulationSize, settings.Cities.Count);
            ComputeCurrentGenerationData();
        }

        public List<Solution> ComputeNextGeneration()
        {
            var nextGeneration = new List<Individual>();

            while (nextGeneration.Count < settings.PopulationSize)
            {
                var parent1 = selectionOperator.SelectOne(CurrentSolutions);
                var parent2 = selectionOperator.SelectOne(CurrentSolutions);

                var offsprings = crossoverOperator.GetOffsprings(parent1, parent2, settings.CrossoverRate);

                mutationOperator.ApplyMutation(offsprings.Item1, settings.MutationRate);
                mutationOperator.ApplyMutation(offsprings.Item2, settings.MutationRate);

                AddIfNotDupplicate(nextGeneration, offsprings.Item1);
                AddIfNotDupplicate(nextGeneration, offsprings.Item2);
            }

            AddElites(nextGeneration);

            currentPopulation = nextGeneration;
            ComputeCurrentGenerationData();

            return CurrentSolutions;
        }

        private void AddElites(List<Individual> nextGeneration)
        {
            for (int i = 0; i <= settings.NumberOfElites; i++)
                nextGeneration.RemoveAt(random.Next(nextGeneration.Count));

            var elitesToAdd = elitistSelection.SelectMany(settings.NumberOfElites, CurrentSolutions);

            nextGeneration.AddRange(elitesToAdd.Select(x => x.Individual));
        }

        private void AddIfNotDupplicate(List<Individual> nextGeneration, Individual offspring)
        {
            if (!nextGeneration.Any(x => x .Equals(offspring)))
            {
                nextGeneration.Add(offspring);
            }
        }

        private void ComputeCurrentGenerationData()
        {
            CurrentGenerationNumber++;
            CurrentSolutions = GetCurrentSolutions();
            AverageScore = CurrentSolutions.Average(s => s.FitnessScore);
            CurrentBestSolution = CurrentSolutions.OrderBy(s => s.FitnessScore).First();
        }

        private List<Solution> GetCurrentSolutions()
        {
            var solutions = new List<Solution>();
            foreach (var individual in currentPopulation)
                solutions.Add(new Solution
                {
                    Individual = individual,
                    FitnessScore = fitnessFunction.GetFitnessScore(individual)
                });

            return solutions;
        }
    }
}
