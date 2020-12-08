using InventoryClassificationGenetic.Domain;
using InventoryClassificationGenetic.Providers.Contracts;
using System;
using System.Collections.Generic;

namespace InventoryClassificationGenetic.Providers
{
    public class InitialPopulationProvider : IInitialPopulationProvider
    {
        private readonly static Random random = new Random();

        public List<Individual> GetInitialPopulation(int populationSize, int numberOfCriterias)
        {
            var initialPopulation = new List<Individual>();

            for (int i = 0; i < populationSize; i++)
                initialPopulation.Add(GetRandomIndividual(numberOfCriterias));

            return initialPopulation;
        }

        private Individual GetRandomIndividual(int numberOfCriterias)
        {
            var weights = new double[numberOfCriterias];
            var remainingTotalWeight = 10000;

            for (int i = 0; i < numberOfCriterias; i++)
            {
                var randomWeight = random.Next(0, remainingTotalWeight);
                remainingTotalWeight -= randomWeight;

                weights[i] = randomWeight / 10000.0;
            }

            for (int i = 0; i < numberOfCriterias; i++)
                weights[i] += remainingTotalWeight / 10000.0;

            return new Individual
            {
                Weights = weights
            };
        }
    }
}
