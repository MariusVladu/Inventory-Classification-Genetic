using System;
using System.Linq;
using InventoryClassificationGenetic.Algorithm.CrossoverOperators;
using InventoryClassificationGenetic.Algorithm.Helpers;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.CrossoverOperators
{
    public class PMXCrossover : BaseCrossoverOperator
    {
        private const int UnsetValue = -1;

        protected override Tuple<Individual, Individual> PerformCrossover(Individual parent1, Individual parent2)
        {
            var left = random.Next(parent1.Genes.Length);
            var right = random.Next(parent1.Genes.Length);

            return PerformPMXCrossover(parent1, parent2, left, right);
        }

        public Tuple<Individual, Individual> PerformPMXCrossover(Individual parent1, Individual parent2, int left, int right)
        {
            var numberOfGenes = parent1.Genes.Length;
            CommonFunctions.SwapIfNotInOrder(ref left, ref right);

            var offspring1 = GetOffspring(left, right, parent1, parent2);
            var offspring2 = GetOffspring(left, right, parent2, parent1);

            return new Tuple<Individual, Individual>(offspring1, offspring2);
        }

        private Individual GetOffspring(int left, int right, Individual parent1, Individual parent2)
        {
            var offspring = new Individual { Genes = Enumerable.Repeat(UnsetValue, parent1.Genes.Length).ToArray() };

            for (int i = left; i < right; i++)
                offspring.Genes[i] = parent1.Genes[i];

            for (int i = left; i < right; i++)
            {
                if (!HasBeenCopied(parent2.Genes[i], offspring, left, right))
                {
                    int copiedValue = offspring.Genes[i];
                    int index;
                    do
                    {
                        index = GetIndexOf(copiedValue, parent2);
                        copiedValue = offspring.Genes[index];
                    } while (index >= left && index < right);

                    offspring.Genes[index] = parent2.Genes[i];
                }
            }

            CopyUnsetIndexesFromParent(offspring, parent2);

            return offspring;
        }

        private int GetIndexOf(int value, Individual parent2)
        {
            for (int i = 0; i < parent2.Genes.Length; i++)
                if (parent2.Genes[i] == value)
                    return i;

            return -1;
        }

        private bool HasBeenCopied(int value, Individual offspring, int left, int right)
        {
            for (int i = left; i < right; i++)
                if (offspring.Genes[i] == value)
                    return true;

            return false;
        }

        private void CopyUnsetIndexesFromParent(Individual offspring, Individual parent2)
        {
            for (int i = 0; i < offspring.Genes.Length; i++)
                if (offspring.Genes[i] == UnsetValue)
                    offspring.Genes[i] = parent2.Genes[i];
        }
    }
}
