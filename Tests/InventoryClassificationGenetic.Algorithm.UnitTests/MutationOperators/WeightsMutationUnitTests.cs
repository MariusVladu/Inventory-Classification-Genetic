using InventoryClassificationGenetic.Algorithm.MutationOperators;
using InventoryClassificationGenetic.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace InventoryClassificationGenetic.Algorithm.UnitTests.MutationOperators
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class WeightsMutationUnitTests
    {
        private WeightsMutation weightsMutation;

        [TestInitialize]
        public void Setup()
        {
            weightsMutation = new WeightsMutation();
        }

        [TestMethod]
        public void TestThatAfterMutationWeightsSumIs1()
        {
            const int index = 1;
            const int weightValue = 0;
            var initialGenes = new double[] { 0.1, 0.2, 0.3, 0.4 };
            var individual = new Individual { Weights = initialGenes };

            weightsMutation.ApplyWeightsMutation(individual, index, weightValue);
            var weightsSum = individual.Weights.Sum();

            Assert.AreEqual(1, weightsSum);
        }

        [TestMethod]
        public void TestThatMutationSetsExpectedGeneTo0()
        {
            const int index = 1;
            const int weightValue = 0;
            var initialGenes = new double[] { 0.1, 0.2, 0.3, 0.4 };
            var individual = new Individual { Weights = initialGenes };

            weightsMutation.ApplyWeightsMutation(individual, index, weightValue);

            Assert.AreEqual(0, individual.Weights[index]);
        }

        [TestMethod]
        public void TestThatXabXbcRemainUnchangedAfterMutation()
        {
            const int index = 1;
            const int weightValue = 1;
            const double Xab = 0.5;
            const double Xbc = 0.2;
            var initialGenes = new double[] { 0.1, 0.2, 0.3, 0.4 };
            var individual = new Individual 
            { 
                Weights = initialGenes,
                Xab = Xab,
                Xbc = Xbc
            };

            weightsMutation.ApplyWeightsMutation(individual, index, weightValue);

            Assert.AreEqual(Xab, individual.Xab);
            Assert.AreEqual(Xbc, individual.Xbc);
        }
    }
}
