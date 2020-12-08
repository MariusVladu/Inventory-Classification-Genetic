using TSPGenetic.Domain;
using System.Collections.Generic;

namespace TSPGenetic.Algorithm.Contracts
{
    public interface ISelectionOperator
    {
        Individual SelectOne(List<Solution> solutions);
    }
}
