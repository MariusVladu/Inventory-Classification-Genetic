using InventoryClassificationGenetic.Domain;
using System;

namespace InventoryClassificationGenetic.Algorithm.Contracts
{
    public interface ICrossoverOperator
    {
        Tuple<Individual, Individual> GetOffsprings(Individual parent1, Individual parent2, double crossoverRate);
    }
}
