using System;
using System.Linq;

namespace InventoryClassificationGenetic.Domain
{
    public class Solution
    {
        public Individual Individual { get; set; }
        public double FitnessScore { get; set; }

        public override string ToString()
        {
            return $"Weights: {string.Join(", ", Individual.Weights.Select(w => Math.Round(w, 4)))} " +
                $"\nXab: {Math.Round(Individual.Xab, 4)} " +
                $"\nXbc: {Math.Round(Individual.Xbc, 4)} " +
                $"\nScore = {Math.Round(FitnessScore, 4)}";
        }
    }
}
