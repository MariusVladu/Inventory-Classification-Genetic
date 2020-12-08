using TSPGenetic.Domain;
using System.Collections.Generic;

namespace TSPGenetic.Algorithm.Contracts
{
    public interface IElitistSelection
    {
        Solution SelectOne(List<Solution> solutions);
        List<Solution> SelectMany(int n, List<Solution> solutions);
    }
}
