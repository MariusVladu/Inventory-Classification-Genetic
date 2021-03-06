﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using InventoryClassificationGenetic.Algorithm.MutationOperators;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.UnitTests.MutationOperators
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ScrambleMutationUnitTests
    {
        private ScrambleMutation scrambleMutation;

        [TestInitialize]
        public void Setup()
        {
            scrambleMutation = new ScrambleMutation();
        }

        [TestMethod]
        public void TestThatResultHasOnlyDistinctGenes()
        {
            const int left = 1;
            const int right = 5;
            var initialGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var individual = new Individual { Weights = initialGenes };

            scrambleMutation.ApplyScrambleMutation(individual, left, right);

            var distinctGenes = individual.Weights.Distinct();
            Assert.AreEqual(initialGenes.Count(), distinctGenes.Count());
        }

        [TestMethod]
        public void TestThatAtLeastOneGeneIsChanged()
        {
            const int left = 1;
            const int right = 5;
            var initialGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var unchangedGenes = (double[])initialGenes.Clone();
            var individual = new Individual { Weights = initialGenes };

            scrambleMutation.ApplyScrambleMutation(individual, left, right);

            CollectionAssert.AreNotEqual(unchangedGenes, individual.Weights);
        }

        [TestMethod]
        public void TestThatGenesOutsideSelectIntervalRemainUnchanged()
        {
            const int left = 1;
            const int right = 5;
            var initialGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var unchangedGenes = (double[])initialGenes.Clone();
            var individual = new Individual { Weights = initialGenes };

            scrambleMutation.ApplyScrambleMutation(individual, left, right);

            for (int i = 0; i < left; i++)
                Assert.AreEqual(unchangedGenes[i], individual.Weights[i]);

            for (int i = right; i < initialGenes.Length; i++)
                Assert.AreEqual(unchangedGenes[i], individual.Weights[i]);
        }
    }
}
