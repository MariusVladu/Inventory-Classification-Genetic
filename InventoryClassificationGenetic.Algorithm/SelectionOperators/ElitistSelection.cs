using InventoryClassificationGenetic.Algorithm.Contracts;
using InventoryClassificationGenetic.Domain;
using System.Collections.Generic;
using System.Linq;

namespace InventoryClassificationGenetic.Algorithm.SelectionOperators
{
    public class ElitistSelection : IElitistSelection
    {
        public List<Solution> SelectMany(int n, List<Solution> solutions)
        {
            return solutions.OrderBy(s => s.FitnessScore).Take(n).ToList();
        }

        Solution IElitistSelection.SelectOne(List<Solution> solutions)
        {
            return solutions.OrderBy(s => s.FitnessScore).First();
        }
    }
}
