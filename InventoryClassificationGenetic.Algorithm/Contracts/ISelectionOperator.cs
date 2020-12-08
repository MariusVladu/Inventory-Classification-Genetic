using InventoryClassificationGenetic.Domain;
using System.Collections.Generic;

namespace InventoryClassificationGenetic.Algorithm.Contracts
{
    public interface ISelectionOperator
    {
        Individual SelectOne(List<Solution> solutions);
    }
}
