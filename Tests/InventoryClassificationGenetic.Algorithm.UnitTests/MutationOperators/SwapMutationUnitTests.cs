using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using InventoryClassificationGenetic.Algorithm.MutationOperators;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.UnitTests.MutationOperators
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SwapMutationUnitTests
    {
        private SwapMutation swapMutation;

        [TestInitialize]
        public void Setup()
        {
            swapMutation = new SwapMutation();
        }

        [TestMethod]
        public void TestThatMutationIsAppliedAsExpected()
        {
            const int index1 = 1;
            const int index2 = 3;
            var initialGenes =  new double[] { 1, 2, 3, 4 };
            var expectedGenes = new double[] { 1, 4, 3, 2 };
            var individual = new Individual { Weights = initialGenes };

            swapMutation.ApplySwapMutation(individual, index1, index2);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }

        [TestMethod]
        public void TestThatWhenIdexesAreEqualNothingIsChanged()
        {
            const int index1 = 1;
            const int index2 = 1;
            var initialGenes = new double[] { 1, 2, 3, 4 };
            var expectedGenes = new double[] { 1, 2, 3, 4 };
            var individual = new Individual { Weights = initialGenes };

            swapMutation.ApplySwapMutation(individual, index1, index2);

            CollectionAssert.AreEqual(expectedGenes, individual.Weights);
        }
    }
}
