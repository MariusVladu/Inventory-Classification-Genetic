using System.Linq;

namespace TSPGenetic.Domain
{
    public class Solution
    {
        public Individual Individual { get; set; }
        public int FitnessScore { get; set; }

        public override string ToString()
        {
            return $"{string.Join( ", ", Individual.Genes)} - Score = {FitnessScore}";
        }
    }
}
