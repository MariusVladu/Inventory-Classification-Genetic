using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using InventoryClassificationGenetic.Algorithm.CrossoverOperators;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.UnitTests.CrossoverOperators
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PMXCrossoverUnitTests
    {
        private PMXCrossover pmxCrossover;

        [TestInitialize]
        public void Setup()
        {
            pmxCrossover = new PMXCrossover();
        }

        [TestMethod]
        public void TestThatOffspringsAreReturnedAsExpected()
        {
            const int left = 3;
            const int right = 7;
            var parent1 = new Individual { Weights = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 } };
            var parent2 = new Individual { Weights = new double[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 } };
            var expectedOffspring1Genes = new double[] { 9, 3, 2, 4, 5, 6, 7, 1, 8 };
            var expectedOffspring2Genes = new double[] { 1, 7, 3, 8, 2, 6, 5, 4, 9 };

            var result = pmxCrossover.PerformPMXCrossover(parent1, parent2, left, right);

            CollectionAssert.AreEqual(expectedOffspring1Genes, result.Item1.Weights);
            CollectionAssert.AreEqual(expectedOffspring2Genes, result.Item2.Weights);
        }

        [TestMethod]
        public void TestThatOffspringsHaveExpectedLength()
        {
            const int left = 3;
            const int right = 7;
            var parent1 = new Individual { Weights = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 } };
            var parent2 = new Individual { Weights = new double[] { 9, 3, 7, 8, 2, 6, 5, 1, 4 } };
            var expectedLength = parent1.Weights.Length;

            var result = pmxCrossover.PerformPMXCrossover(parent1, parent2, left, right);

            Assert.AreEqual(expectedLength, result.Item1.Weights.Length);
            Assert.AreEqual(expectedLength, result.Item2.Weights.Length);
        }
    }
}
