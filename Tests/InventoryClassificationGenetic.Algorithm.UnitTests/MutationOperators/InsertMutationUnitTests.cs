using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using InventoryClassificationGenetic.Algorithm.MutationOperators;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.UnitTests.MutationOperators
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class InsertMutationUnitTests
    {
        private InsertMutation insertMutation;

        [TestInitialize]
        public void Setup()
        {
            insertMutation = new InsertMutation();
        }

        [TestMethod]
        public void TestThatMutationIsAppliedAsExpected()
        {
            const int leftIndex = 1;
            const int rightIndex = 4;
            var initialGenes =  new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedGenes = new double[] { 1, 2, 5, 3, 4, 6, 7, 8, 9 };
            var individual = new Individual { Weights = initialGenes };

            insertMutation.ApplyInsertMutation(individual, leftIndex, rightIndex);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }

        [TestMethod]
        public void TestThatMutationResultHasOnlyDistinctGenes()
        {
            const int leftIndex = 1;
            const int rightIndex = 4;
            var initialGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var individual = new Individual { Weights = initialGenes };

            insertMutation.ApplyInsertMutation(individual, leftIndex, rightIndex);

            var distinctGenes = individual.Weights.Distinct();
            Assert.AreEqual(initialGenes.Count(), distinctGenes.Count());
        }

        [TestMethod]
        public void TestThatWhenLeftIndexIsGreaterThanRightIndexTheyAreSwappedAndMutationIsAppliedAsExpected()
        {
            const int leftIndex = 4;
            const int rightIndex = 1;
            var initialGenes =  new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedGenes = new double[] { 1, 2, 5, 3, 4, 6, 7, 8, 9 };
            var individual = new Individual { Weights = initialGenes };

            insertMutation.ApplyInsertMutation(individual, leftIndex, rightIndex);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }

        [TestMethod]
        public void TestThatWhenLeftIndexAndRightIndexAreBoth0MutationIsAppliedAsExpected()
        {
            const int leftIndex = 0;
            const int rightIndex = 0;
            var initialGenes =  new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedGenes = new double[] { 1, 3, 2, 4, 5, 6, 7, 8, 9 };
            var individual = new Individual { Weights = initialGenes };

            insertMutation.ApplyInsertMutation(individual, leftIndex, rightIndex);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }

        [TestMethod]
        public void TestThatWhenLeftIndexAndRightIndexAreEqualAtTheMiddleMutationIsAppliedAsExpected()
        {
            const int leftIndex = 3;
            const int rightIndex = 3;
            var initialGenes =  new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedGenes = new double[] { 1, 2, 3, 4, 6, 5, 7, 8, 9 };
            var individual = new Individual { Weights = initialGenes };

            insertMutation.ApplyInsertMutation(individual, leftIndex, rightIndex);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }

        [TestMethod]
        public void TestThatWhenLeftIndexAndRightIndexAreBothMaxMutationIsAppliedAsExpected()
        {
            const int leftIndex = 8;
            const int rightIndex = 8;
            var initialGenes =  new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 9, 8 };
            var individual = new Individual { Weights = initialGenes };

            insertMutation.ApplyInsertMutation(individual, leftIndex, rightIndex);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }

        [TestMethod]
        public void TestThatWhenLeftIndexAndRightIndexAre1DistanceAppartOnTheLeftmostSideMutationIsAppliedAsExpected()
        {
            const int leftIndex = 0;
            const int rightIndex = 1;
            var initialGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedGenes = new double[] { 1, 3, 2, 4, 5, 6, 7, 8, 9 };
            var individual = new Individual { Weights = initialGenes };

            insertMutation.ApplyInsertMutation(individual, leftIndex, rightIndex);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }

        [TestMethod]
        public void TestThatWhenLeftIndexAndRightIndexAre1DistanceAppartAtTheMiddleMutationIsAppliedAsExpected()
        {
            const int leftIndex = 3;
            const int rightIndex = 4;
            var initialGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedGenes = new double[] { 1, 2, 3, 4, 6, 5, 7, 8, 9 };
            var individual = new Individual { Weights = initialGenes };

            insertMutation.ApplyInsertMutation(individual, leftIndex, rightIndex);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }

        [TestMethod]
        public void TestThatWhenLeftIndexAndRightIndexAre1DistanceAppartOnTheRightmostSideMutationIsAppliedAsExpected()
        {
            const int leftIndex = 7;
            const int rightIndex = 8;
            var initialGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 9, 8 };
            var individual = new Individual { Weights = initialGenes };

            insertMutation.ApplyInsertMutation(individual, leftIndex, rightIndex);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }
    }
}
