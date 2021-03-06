﻿using System;
using System.Collections.Generic;
using System.Linq;
using InventoryClassificationGenetic.Algorithm.Contracts;
using InventoryClassificationGenetic.Algorithm.Helpers;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.MutationOperators
{
    public class ScrambleMutation : IMutationOperator
    {
        private static readonly Random random = new Random();

        public void ApplyMutation(Individual individual, double mutationRate)
        {
            if (random.NextDouble() > mutationRate) return;

            int left = random.Next(individual.Weights.Length);
            int right = random.Next(individual.Weights.Length);

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
                individual.Weights[i] = scrambledGenes[i - left];
        }

        private List<double> GetGenesToScramble(int left, int right, Individual individual)
        {
            var genesToScramble = new List<double>(individual.Weights.Length);
            for (int i = left; i < right; i++)
                genesToScramble.Add(individual.Weights[i]);

            return genesToScramble;
        }
    }
}
