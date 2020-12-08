using System;
using System.Collections.Generic;
using InventoryClassificationGenetic.Algorithm.Contracts;
using InventoryClassificationGenetic.Algorithm.Helpers;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.MutationOperators
{
    public class InversionMutation : IMutationOperator
    {
        private static readonly Random random = new Random();

        public void ApplyMutation(Individual individual, double mutationRate)
        {
            if (random.NextDouble() > mutationRate) return;

            int leftIndex = random.Next(individual.Weights.Length);
            int rightIndex = random.Next(individual.Weights.Length);

            ApplyInversionMutation(individual, leftIndex, rightIndex);
        }

        public void ApplyInversionMutation(Individual individual, int leftIndex, int rightIndex)
        {
            CommonFunctions.SwapIfNotInOrder(ref leftIndex, ref rightIndex);

            var stack = new Stack<double>();
            for (int i = leftIndex; i < rightIndex; i++)
                stack.Push(individual.Weights[i]);

            for (int i = leftIndex; i < rightIndex; i++)
                individual.Weights[i] = stack.Pop();
        }
    }
}
