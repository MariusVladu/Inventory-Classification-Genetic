using System;
using InventoryClassificationGenetic.Algorithm.Contracts;
using InventoryClassificationGenetic.Algorithm.Helpers;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.MutationOperators
{
    public class SwapMutation : IMutationOperator
    {
        private static readonly Random random = new Random();

        public void ApplyMutation(Individual individual, double mutationRate)
        {
            if (random.NextDouble() > mutationRate) return;

            int index1 = random.Next(individual.Weights.Length);
            int index2 = random.Next(individual.Weights.Length);

            ApplySwapMutation(individual, index1, index2);
        }

        public void ApplySwapMutation(Individual individual, int index1, int index2)
        {
            CommonFunctions.SwapElements(individual.Weights, index1, index2);
        }
    }
}
