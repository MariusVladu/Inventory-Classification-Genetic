using TSPGenetic.Algorithm.Contracts;
using TSPGenetic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TSPGenetic.Algorithm.SelectionOperators
{
    public class RouletteWheelSelection : ISelectionOperator
    {
        private static readonly Random random = new Random();

        public Individual SelectOne(List<Solution> solutions)
        {
            var totalScore = solutions.Sum(x => x.FitnessScore);
            var probabilities = solutions.Select(x => (double)x.FitnessScore / totalScore).ToArray();

            var cumulativeProbabilities = new double[solutions.Count];
            cumulativeProbabilities[0] = probabilities[0];

            for (int i = 1; i < solutions.Count; i++)
                cumulativeProbabilities[i] = cumulativeProbabilities[i - 1] + probabilities[i];

            var randomValue = random.NextDouble();
            for (int i = 0; i < solutions.Count; i++)
                if (randomValue < cumulativeProbabilities[i]) return solutions[i].Individual;

            return solutions.Last().Individual;
        }
    }
}
