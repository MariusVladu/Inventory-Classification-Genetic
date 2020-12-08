using System;
using System.Linq;
using InventoryClassificationGenetic.Algorithm.Helpers;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.CrossoverOperators
{
    public class CycleCrossover : BaseCrossoverOperator
    {
        private const double UnsetValue = -1.0;

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
            var offspring = new Individual { Weights = Enumerable.Repeat(UnsetValue, parent1.Weights.Length).ToArray() };

            var currentParent1 = parent1;
            var currentParent2 = parent2;

            while(offspring.Weights.Any(x => x == UnsetValue))
            {
                var firstFreeValueIndex = GetIndexOf(UnsetValue, offspring);

                var index = firstFreeValueIndex;
                do
                {
                    offspring.Weights[index] = currentParent1.Weights[index];

                    var correspondingValue = currentParent2.Weights[index];

                    index = GetIndexOf(correspondingValue, currentParent1);
                } while (index != firstFreeValueIndex);

                CommonFunctions.Swap(ref currentParent1, ref currentParent2);
            }

            return offspring;
        }

        private int GetIndexOf(double value, Individual parent)
        {
            for (int i = 0; i < parent.Weights.Length; i++)
                if (parent.Weights[i] == value)
                    return i;

            return -1;
        }
    }
}
