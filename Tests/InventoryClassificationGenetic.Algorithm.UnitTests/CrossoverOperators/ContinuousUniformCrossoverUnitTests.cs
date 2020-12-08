using InventoryClassificationGenetic.Algorithm.CrossoverOperators;
using InventoryClassificationGenetic.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace InventoryClassificationGenetic.Algorithm.UnitTests.CrossoverOperators
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ContinuousUniformCrossoverUnitTests
    {
        private ContinuousUniformCrossover continuousUniformCrossover;

        [TestInitialize]
        public void Setup()
        {
            continuousUniformCrossover = new ContinuousUniformCrossover();
        }

        [TestMethod]
        public void TestThatCrossoverReturnsOffspringsWithTotalWeight1()
        {
            const double s = 0.2;
            var parent1 = new Individual { Weights = new double[] { 0.1, 0.2, 0.3, 0.4 } };
            var parent2 = new Individual { Weights = new double[] { 0.6, 0.1, 0.1, 0.2 } };

            var result = continuousUniformCrossover.PerformContinuousUniformCrossover(parent1, parent2, s);

            Assert.AreEqual(1, Math.Round(result.Item1.Weights.Sum(), 6));
            Assert.AreEqual(1, Math.Round(result.Item2.Weights.Sum(), 6));
        }

        [TestMethod]
        public void TestThatWhenSIsLowerThan0CrossoverReturnsOffspringsWithTotalWeight1()
        {
            const double s = -0.4;
            var parent1 = new Individual { Weights = new double[] { 0.1, 0.2, 0.3, 0.4 } };
            var parent2 = new Individual { Weights = new double[] { 0.6, 0.1, 0.1, 0.2 } };

            var result = continuousUniformCrossover.PerformContinuousUniformCrossover(parent1, parent2, s);

            Assert.AreEqual(1, Math.Round(result.Item1.Weights.Sum(), 6));
            Assert.AreEqual(1, Math.Round(result.Item2.Weights.Sum(), 6));
        }

        [TestMethod]
        public void TestThatAfterCrossoverXabIsGreaterThanXbc()
        {
            const double s = 0.2;
            var parent1 = new Individual { Weights = new double[] { 0.1, 0.2, 0.3, 0.4 }, Xab = 0.5, Xbc = 0.2 };
            var parent2 = new Individual { Weights = new double[] { 0.6, 0.1, 0.1, 0.2 }, Xab = 0.7, Xbc = 0.4 };

            var result = continuousUniformCrossover.PerformContinuousUniformCrossover(parent1, parent2, s);

            Assert.IsTrue(result.Item1.Xab > result.Item1.Xbc);
            Assert.IsTrue(result.Item2.Xab > result.Item2.Xbc);
        }

        [TestMethod]
        public void TestThatWhenSIsLowerThan0AfterCrossoverXabIsGreaterThanXbc()
        {
            const double s = -0.5;
            var parent1 = new Individual { Weights = new double[] { 0.1, 0.2, 0.3, 0.4 }, Xab = 0.5, Xbc = 0.2 };
            var parent2 = new Individual { Weights = new double[] { 0.6, 0.1, 0.1, 0.2 }, Xab = 0.7, Xbc = 0.4 };

            var result = continuousUniformCrossover.PerformContinuousUniformCrossover(parent1, parent2, s);

            Assert.IsTrue(result.Item1.Xab > result.Item1.Xbc);
            Assert.IsTrue(result.Item2.Xab > result.Item2.Xbc);
        }
    }
}
