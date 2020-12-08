using TSPGenetic.Domain;
using System;
using TSPGenetic.Algorithm.CrossoverOperators;

namespace TSPGenetic.Algorithm.CrossoverOperators
{
    public class OnePointCrossover : BaseCrossoverOperator
    {
        protected override Tuple<Individual, Individual> PerformCrossover(Individual parent1, Individual parent2)
        {
            var crossoverPoint = random.Next(parent1.Genes.Length);

            return PerformOnePointCrossover(parent1, parent2, crossoverPoint);
        }

        public Tuple<Individual, Individual> PerformOnePointCrossover(Individual parent1, Individual parent2, int crossoverPoint)
        {
            var numberOfGenes = parent1.Genes.Length;
            var offspring1 = new Individual { Genes = new int[numberOfGenes] };
            var offspring2 = new Individual { Genes = new int[numberOfGenes] };

            for (int i = 0; i < numberOfGenes; i++)
            {
                if (i < crossoverPoint)
                {
                    offspring1.Genes[i] = parent1.Genes[i];
                    offspring2.Genes[i] = parent2.Genes[i];
                }
                else
                {
                    offspring1.Genes[i] = parent2.Genes[i];
                    offspring2.Genes[i] = parent1.Genes[i];
                }
            }

            return new Tuple<Individual, Individual>(offspring1, offspring2);
        }
    }
}
