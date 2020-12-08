using System.Linq;

namespace InventoryClassificationGenetic.Domain
{
    public class Solution
    {
        public Individual Individual { get; set; }
        public double FitnessScore { get; set; }

        public override string ToString()
        {
            return $"{string.Join( ", ", Individual.Weights)} - Score = {FitnessScore}";
        }
    }
}
