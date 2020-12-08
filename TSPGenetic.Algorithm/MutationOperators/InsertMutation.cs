using System;
using TSPGenetic.Algorithm.Contracts;
using TSPGenetic.Algorithm.Helpers;
using TSPGenetic.Domain;

namespace TSPGenetic.Algorithm.MutationOperators
{
    public class InsertMutation : IMutationOperator
    {
        private static readonly Random random = new Random();

        public void ApplyMutation(Individual individual, double mutationRate)
        {
            if (random.NextDouble() > mutationRate) return;

            int leftIndex = random.Next(individual.Genes.Length);
            int rightIndex = random.Next(individual.Genes.Length);

            ApplyInsertMutation(individual, leftIndex, rightIndex);
        }

        public void ApplyInsertMutation(Individual individual, int leftIndex, int rightIndex)
        {
            CommonFunctions.SwapIfNotInOrder(ref leftIndex, ref rightIndex);
            AdjustIndexesIfTheyAreTooClose(ref leftIndex, ref rightIndex, individual.Genes.Length);

            var geneToInsert = individual.Genes[rightIndex];

            for (int i = rightIndex; i >= leftIndex + 2; i--)
                individual.Genes[i] = individual.Genes[i - 1];

            individual.Genes[leftIndex + 1] = geneToInsert;
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
