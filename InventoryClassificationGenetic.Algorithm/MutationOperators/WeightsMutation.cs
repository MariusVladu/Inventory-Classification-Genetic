using InventoryClassificationGenetic.Algorithm.Contracts;
using InventoryClassificationGenetic.Domain;
using System;
using System.Linq;

namespace InventoryClassificationGenetic.Algorithm.MutationOperators
{
    public class WeightsMutation : IMutationOperator
    {
        private static readonly Random random = new Random();

        public void ApplyMutation(Individual individual, double mutationRate)
        {
            if (random.NextDouble() > mutationRate) return;

            var randomWeightIndex = random.Next(individual.Weights.Length);
            var randomWeightValue = Math.Round(random.NextDouble());

            ApplyWeightsMutation(individual, randomWeightIndex, randomWeightValue);
        }

        public void ApplyWeightsMutation(Individual individual, int randomWeightIndex, double randomWeightValue)
        {
            individual.Weights[randomWeightIndex] = randomWeightValue;

            NormalizeIndividual(individual);
        }

        private static void NormalizeIndividual(Individual individual)
        {
            var min = individual.Weights.Min();

            for (int i = 0; i < individual.Weights.Length; i++)
                individual.Weights[i] -= min;

            var sum = individual.Weights.Sum();

            for (int i = 0; i < individual.Weights.Length; i++)
                individual.Weights[i] /= sum;
        }
    }
}
