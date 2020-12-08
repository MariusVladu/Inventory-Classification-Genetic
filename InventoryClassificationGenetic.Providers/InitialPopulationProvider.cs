using InventoryClassificationGenetic.Domain;
using InventoryClassificationGenetic.Providers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventoryClassificationGenetic.Providers
{
    public class InitialPopulationProvider : IInitialPopulationProvider
    {
        private readonly static Random random = new Random();

        public List<Individual> GetInitialPopulation(int populationSize, int numberOfCities)
        {
            var initialPopulation = new List<Individual>();

            for (int i = 0; i < populationSize; i++)
                initialPopulation.Add(GetRandomIndividual(numberOfCities));

            return initialPopulation;
        }

        private Individual GetRandomIndividual(int numberOfCities)
        {
            var randomGenes = Enumerable.Range(0, numberOfCities)
                .OrderBy(c => random.Next())
                .ToArray();

            return new Individual
            {
                Genes = randomGenes
            };
        }
    }
}
