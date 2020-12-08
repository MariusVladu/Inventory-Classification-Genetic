using InventoryClassificationGenetic.Domain;
using System;
using System.Linq;

namespace InventoryClassificationGenetic.Algorithm.CrossoverOperators
{
    public class ContinuousUniformCrossover : BaseCrossoverOperator
    {
        protected override Tuple<Individual, Individual> PerformCrossover(Individual parent1, Individual parent2)
        {
            var s = random.NextDouble() - 0.5;

            return PerformContinuousUniformCrossover(parent1, parent2, s);
        }

        public Tuple<Individual, Individual> PerformContinuousUniformCrossover(Individual parent1, Individual parent2, double s)
        {
            var numberOfGenes = parent1.Weights.Length;
            var offspring1 = new Individual { Weights = new double[numberOfGenes] };
            var offspring2 = new Individual { Weights = new double[numberOfGenes] };

            for (int i = 0; i < numberOfGenes; i++)
            {
                offspring1.Weights[i] = s * parent1.Weights[i] + (1 - s) * parent2.Weights[i];
                offspring2.Weights[i] = (1 - s) * parent1.Weights[i] + s * parent2.Weights[i];
            }

            offspring1.Xab = s * parent1.Xab + (1 - s) * parent2.Xab;
            offspring2.Xab = (1 - s) * parent1.Xab + s * parent2.Xab;

            offspring1.Xbc = s * parent1.Xbc + (1 - s) * parent2.Xbc;
            offspring2.Xbc = (1 - s) * parent1.Xbc + s * parent2.Xbc;

            if (s < 0)
            {
                NormalizeOffspring(offspring1);
                NormalizeOffspring(offspring2);
            }

            return new Tuple<Individual, Individual>(offspring1, offspring2);
        }

        private static void NormalizeOffspring(Individual offspring)
        {
            var min = offspring.Weights.Min();

            if (min < 0)
            {
                for (int i = 0; i < offspring.Weights.Length; i++)
                    offspring.Weights[i] -= min;

                var sum = offspring.Weights.Sum();

                for (int i = 0; i < offspring.Weights.Length; i++)
                    offspring.Weights[i] /= sum;

                if (offspring.Xab < offspring.Xbc)
                {
                    var temp = offspring.Xab;
                    offspring.Xab = offspring.Xbc;
                    offspring.Xbc = temp;
                }
            }
        }
    }
}
