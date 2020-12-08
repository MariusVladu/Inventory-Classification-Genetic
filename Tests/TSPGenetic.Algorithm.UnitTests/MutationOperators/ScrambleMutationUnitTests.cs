using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TSPGenetic.Algorithm.MutationOperators;
using TSPGenetic.Domain;

namespace TSPGenetic.Algorithm.UnitTests.MutationOperators
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
            var initialGenes = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var individual = new Individual { Genes = initialGenes };

            scrambleMutation.ApplyScrambleMutation(individual, left, right);

            var distinctGenes = individual.Genes.Distinct();
            Assert.AreEqual(initialGenes.Count(), distinctGenes.Count());
        }

        [TestMethod]
        public void TestThatAtLeastOneGeneIsChanged()
        {
            const int left = 1;
            const int right = 5;
            var initialGenes = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var unchangedGenes = (int[])initialGenes.Clone();
            var individual = new Individual { Genes = initialGenes };

            scrambleMutation.ApplyScrambleMutation(individual, left, right);

            CollectionAssert.AreNotEqual(unchangedGenes, individual.Genes);
        }

        [TestMethod]
        public void TestThatGenesOutsideSelectIntervalRemainUnchanged()
        {
            const int left = 1;
            const int right = 5;
            var initialGenes = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var unchangedGenes = (int[])initialGenes.Clone();
            var individual = new Individual { Genes = initialGenes };

            scrambleMutation.ApplyScrambleMutation(individual, left, right);

            for (int i = 0; i < left; i++)
                Assert.AreEqual(unchangedGenes[i], individual.Genes[i]);

            for (int i = right; i < initialGenes.Length; i++)
                Assert.AreEqual(unchangedGenes[i], individual.Genes[i]);
        }
    }
}
