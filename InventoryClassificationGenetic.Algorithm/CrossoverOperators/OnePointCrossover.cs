using InventoryClassificationGenetic.Domain;
using System;
using InventoryClassificationGenetic.Algorithm.CrossoverOperators;

namespace InventoryClassificationGenetic.Algorithm.CrossoverOperators
{
    public class OnePointCrossover : BaseCrossoverOperator
    {
        protected override Tuple<Individual, Individual> PerformCrossover(Individual parent1, Individual parent2)
        {
            var crossoverPoint = random.Next(parent1.Weights.Length);

            return PerformOnePointCrossover(parent1, parent2, crossoverPoint);
        }

        public Tuple<Individual, Individual> PerformOnePointCrossover(Individual parent1, Individual parent2, int crossoverPoint)
        {
            var numberOfGenes = parent1.Weights.Length;
            var offspring1 = new Individual { Weights = new double[numberOfGenes] };
            var offspring2 = new Individual { Weights = new double[numberOfGenes] };

            for (int i = 0; i < numberOfGenes; i++)
            {
                if (i < crossoverPoint)
                {
                    offspring1.Weights[i] = parent1.Weights[i];
                    offspring2.Weights[i] = parent2.Weights[i];
                }
                else
                {
                    offspring1.Weights[i] = parent2.Weights[i];
                    offspring2.Weights[i] = parent1.Weights[i];
                }
            }

            return new Tuple<Individual, Individual>(offspring1, offspring2);
        }
    }
}
