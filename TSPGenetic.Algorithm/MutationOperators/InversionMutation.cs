using System;
using System.Collections.Generic;
using TSPGenetic.Algorithm.Contracts;
using TSPGenetic.Algorithm.Helpers;
using TSPGenetic.Domain;

namespace TSPGenetic.Algorithm.MutationOperators
{
    public class InversionMutation : IMutationOperator
    {
        private static readonly Random random = new Random();

        public void ApplyMutation(Individual individual, double mutationRate)
        {
            if (random.NextDouble() > mutationRate) return;

            int leftIndex = random.Next(individual.Genes.Length);
            int rightIndex = random.Next(individual.Genes.Length);

            ApplyInversionMutation(individual, leftIndex, rightIndex);
        }

        public void ApplyInversionMutation(Individual individual, int leftIndex, int rightIndex)
        {
            CommonFunctions.SwapIfNotInOrder(ref leftIndex, ref rightIndex);

            Stack<int> stack = new Stack<int>();
            for (int i = leftIndex; i < rightIndex; i++)
                stack.Push(individual.Genes[i]);

            for (int i = leftIndex; i < rightIndex; i++)
                individual.Genes[i] = stack.Pop();
        }
    }
}
