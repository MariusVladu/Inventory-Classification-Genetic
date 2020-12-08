using System;
using InventoryClassificationGenetic.Algorithm.Contracts;
using InventoryClassificationGenetic.Algorithm.Helpers;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.MutationOperators
{
    public class InsertMutation : IMutationOperator
    {
        private static readonly Random random = new Random();

        public void ApplyMutation(Individual individual, double mutationRate)
        {
            if (random.NextDouble() > mutationRate) return;

            int leftIndex = random.Next(individual.Weights.Length);
            int rightIndex = random.Next(individual.Weights.Length);

            ApplyInsertMutation(individual, leftIndex, rightIndex);
        }

        public void ApplyInsertMutation(Individual individual, int leftIndex, int rightIndex)
        {
            CommonFunctions.SwapIfNotInOrder(ref leftIndex, ref rightIndex);
            AdjustIndexesIfTheyAreTooClose(ref leftIndex, ref rightIndex, individual.Weights.Length);

            var geneToInsert = individual.Weights[rightIndex];

            for (int i = rightIndex; i >= leftIndex + 2; i--)
                individual.Weights[i] = individual.Weights[i - 1];

            individual.Weights[leftIndex + 1] = geneToInsert;
        }

        private void AdjustIndexesIfTheyAreTooClose(ref int leftIndex, ref int rightIndex, int numberOfGenes)
        {
            if (rightIndex - leftIndex == 1)
            {
                if (rightIndex < numberOfGenes - 2)
                    rightIndex++;
                else
                    leftIndex--;
            }
            else if (rightIndex - leftIndex == 0)
            {
                if (rightIndex < numberOfGenes - 3)
                    rightIndex += 2;
                else
                    leftIndex -= 2;
            }
        }
    }
}
