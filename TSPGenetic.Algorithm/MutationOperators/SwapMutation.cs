using System;
using TSPGenetic.Algorithm.Contracts;
using TSPGenetic.Algorithm.Helpers;
using TSPGenetic.Domain;

namespace TSPGenetic.Algorithm.MutationOperators
{
    public class SwapMutation : IMutationOperator
    {
        private static readonly Random random = new Random();

        public void ApplyMutation(Individual individual, double mutationRate)
        {
            if (random.NextDouble() > mutationRate) return;

            int index1 = random.Next(individual.Genes.Length);
            int index2 = random.Next(individual.Genes.Length);

            ApplySwapMutation(individual, index1, index2);
        }

        public void ApplySwapMutation(Individual individual, int index1, int index2)
        {
            CommonFunctions.SwapElements(individual.Genes, index1, index2);
        }
    }
}
