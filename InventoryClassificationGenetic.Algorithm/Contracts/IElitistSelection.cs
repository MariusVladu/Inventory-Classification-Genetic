using InventoryClassificationGenetic.Domain;
using System.Collections.Generic;

namespace InventoryClassificationGenetic.Algorithm.Contracts
{
    public interface IElitistSelection
    {
        Solution SelectOne(List<Solution> solutions);
        List<Solution> SelectMany(int n, List<Solution> solutions);
    }
}
