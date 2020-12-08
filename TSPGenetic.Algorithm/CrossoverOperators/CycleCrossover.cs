using System;
using System.Linq;
using TSPGenetic.Algorithm.Helpers;
using TSPGenetic.Domain;

namespace TSPGenetic.Algorithm.CrossoverOperators
{
    public class CycleCrossover : BaseCrossoverOperator
    {
        private const int UnsetValue = -1;

        protected override Tuple<Individual, Individual> PerformCrossover(Individual parent1, Individual parent2)
        {
            return PerformCycleCrossover(parent1, parent2);
        }

        public Tuple<Individual, Individual> PerformCycleCrossover(Individual parent1, Individual parent2)
        {
            var offspring1 = GetOffspring(parent1, parent2);
            var offspring2 = GetOffspring(parent2, parent1);

            return new Tuple<Individual, Individual>(offspring1, offspring2);
        }

        private Individual GetOffspring(Individual parent1, Individual parent2)
        {
            var offspring = new Individual { Genes = Enumerable.Repeat(UnsetValue, parent1.Genes.Length).ToArray() };

            var currentParent1 = parent1;
            var currentParent2 = parent2;

            while(offspring.Genes.Any(x => x == UnsetValue))
            {
                var firstFreeValueIndex = GetIndexOf(UnsetValue, offspring);

                var index = firstFreeValueIndex;
                do
                {
                    offspring.Genes[index] = currentParent1.Genes[index];

                    var correspondingValue = currentParent2.Genes[index];

                    index = GetIndexOf(correspondingValue, currentParent1);
                } while (index != firstFreeValueIndex);

                CommonFunctions.Swap(ref currentParent1, ref currentParent2);
            }

            return offspring;
        }

        private int GetIndexOf(int value, Individual parent)
        {
            for (int i = 0; i < parent.Genes.Length; i++)
                if (parent.Genes[i] == value)
                    return i;

            return -1;
        }
    }
}
