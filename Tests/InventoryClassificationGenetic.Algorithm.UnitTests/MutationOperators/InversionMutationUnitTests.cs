using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using InventoryClassificationGenetic.Algorithm.MutationOperators;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.UnitTests.MutationOperators
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class InversionMutationUnitTests
    {
        private InversionMutation inversionMutation;

        [TestInitialize]
        public void Setup()
        {
            inversionMutation = new InversionMutation();
        }

        [TestMethod]
        public void TestThatMutationIsAppliedAsExpected()
        {
            const int leftIndex = 1;
            const int rightIndex = 5;
            var initialGenes =  new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedGenes = new double[] { 1, 5, 4, 3, 2, 6, 7, 8, 9 };
            var individual = new Individual { Weights = initialGenes };

            inversionMutation.ApplyInversionMutation(individual, leftIndex, rightIndex);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }

        [TestMethod]
        public void TestThatWhenLeftIndexIsGreaterThanRightIndexTheyAreSwappedAndMutationIsAppliedAsExpected()
        {
            const int leftIndex = 5;
            const int rightIndex = 1;
            var initialGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedGenes = new double[] { 1, 5, 4, 3, 2, 6, 7, 8, 9 };
            var individual = new Individual { Weights = initialGenes };

            inversionMutation.ApplyInversionMutation(individual, leftIndex, rightIndex);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }

        [TestMethod]
        public void TestThatWhenLeftIndexIsEqualToRightIndexMutationNothingIsChanged()
        {
            const int leftIndex = 1;
            const int rightIndex = 1;
            var initialGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var expectedGenes = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var individual = new Individual { Weights = initialGenes };

            inversionMutation.ApplyInversionMutation(individual, leftIndex, rightIndex);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }
    }
}
