using System;
using System.Collections.Generic;
using System.Linq;
using TSPGenetic.Algorithm.Contracts;
using TSPGenetic.Algorithm.Helpers;
using TSPGenetic.Domain;

namespace TSPGenetic.Algorithm.MutationOperators
{
    public class ScrambleMutation : IMutationOperator
    {
        private static readonly Random random = new Random();

        public void ApplyMutation(Individual individual, double mutationRate)
        {
            if (random.NextDouble() > mutationRate) return;

            int left = random.Next(individual.Genes.Length);
            int right = random.Next(individual.Genes.Length);

            ApplyScrambleMutation(individual, left, right);
        }

        public void ApplyScrambleMutation(Individual individual, int left, int right)
        {
            CommonFunctions.SwapIfNotInOrder(ref left, ref right);

            var genesToScramble = GetGenesToScramble(left, right, individual);
                
            var scrambledGenes = genesToScramble
                .OrderBy(g => random.Next(genesToScramble.Count))
                .ToList();

            for (int i = left; i < right; i++)
                individual.Genes[i] = scrambledGenes[i - left];
        }

        private List<int> GetGenesToScramble(int left, int right, Individual individual)
        {
            var genesToScramble = new List<int>(individual.Genes.Length);
            for (int i = left; i < right; i++)
                genesToScramble.Add(individual.Genes[i]);

            return genesToScramble;
        }
    }
}
