using System;
using InventoryClassificationGenetic.Algorithm.Contracts;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.CrossoverOperators
{
    public abstract class BaseCrossoverOperator : ICrossoverOperator
    {
        protected static readonly Random random = new Random();

        public Tuple<Individual, Individual> GetOffsprings(Individual parent1, Individual parent2, double crossoverRate)
        {
            ValidateParameters(parent1, parent2, crossoverRate);

            if (random.NextDouble() < crossoverRate)
            {
                return PerformCrossover(parent1, parent2);
            }

            return CloneParents(parent1, parent2);
        }

        protected abstract Tuple<Individual, Individual> PerformCrossover(Individual parent1, Individual parent2);

        private Tuple<Individual, Individual> CloneParents(Individual parent1, Individual parent2)
        {
            return new Tuple<Individual, Individual>(parent1.Clone(), parent2.Clone());
        }

        private void ValidateParameters(Individual parent1, Individual parent2, double crossoverRate)
        {
            if (parent1 == null) throw new ArgumentNullException(nameof(parent1));
            if (parent1.Genes == null) throw new ArgumentNullException(nameof(parent1.Genes));
            if (parent2 == null) throw new ArgumentNullException(nameof(parent2));
            if (parent2.Genes == null) throw new ArgumentNullException(nameof(parent2.Genes));
            if (crossoverRate < 0 || crossoverRate > 1) throw new ArgumentException($"{nameof(crossoverRate)} must be in [0, 1]");
        }
    }
}
