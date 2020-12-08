using System;
using System.Linq;
using InventoryClassificationGenetic.Algorithm.CrossoverOperators;
using InventoryClassificationGenetic.Algorithm.Helpers;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.CrossoverOperators
{
    public class PMXCrossover : BaseCrossoverOperator
    {
        private const double UnsetValue = -1.0;

        protected override Tuple<Individual, Individual> PerformCrossover(Individual parent1, Individual parent2)
        {
            var left = random.Next(parent1.Weights.Length);
            var right = random.Next(parent1.Weights.Length);

            return PerformPMXCrossover(parent1, parent2, left, right);
        }

        public Tuple<Individual, Individual> PerformPMXCrossover(Individual parent1, Individual parent2, int left, int right)
        {
            var numberOfGenes = parent1.Weights.Length;
            CommonFunctions.SwapIfNotInOrder(ref left, ref right);

            var offspring1 = GetOffspring(left, right, parent1, parent2);
            var offspring2 = GetOffspring(left, right, parent2, parent1);

            return new Tuple<Individual, Individual>(offspring1, offspring2);
        }

        private Individual GetOffspring(int left, int right, Individual parent1, Individual parent2)
        {
            var offspring = new Individual { Weights = Enumerable.Repeat(UnsetValue, parent1.Weights.Length).ToArray() };

            for (int i = left; i < right; i++)
                offspring.Weights[i] = parent1.Weights[i];

            for (int i = left; i < right; i++)
            {
                if (!HasBeenCopied(parent2.Weights[i], offspring, left, right))
                {
                    var copiedValue = offspring.Weights[i];
                    int index;
                    do
                    {
                        index = GetIndexOf(copiedValue, parent2);
                        copiedValue = offspring.Weights[index];
                    } while (index >= left && index < right);

                    offspring.Weights[index] = parent2.Weights[i];
                }
            }

            CopyUnsetIndexesFromParent(offspring, parent2);

            return offspring;
        }

        private int GetIndexOf(double value, Individual parent2)
        {
            for (int i = 0; i < parent2.Weights.Length; i++)
                if (parent2.Weights[i] == value)
                    return i;

            return -1;
        }

        private bool HasBeenCopied(double value, Individual offspring, int left, int right)
        {
            for (int i = left; i < right; i++)
                if (offspring.Weights[i] == value)
                    return true;

            return false;
        }

        private void CopyUnsetIndexesFromParent(Individual offspring, Individual parent2)
        {
            for (int i = 0; i < offspring.Weights.Length; i++)
                if (offspring.Weights[i] == UnsetValue)
                    offspring.Weights[i] = parent2.Weights[i];
        }
    }
}
