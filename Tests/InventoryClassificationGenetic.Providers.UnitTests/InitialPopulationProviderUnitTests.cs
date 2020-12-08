using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using InventoryClassificationGenetic.Providers.Contracts;

namespace InventoryClassificationGenetic.Providers.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class InitialPopulationProviderUnitTests
    {
        private IInitialPopulationProvider initialPopulationProvider;

        [TestInitialize]
        public void Setup()
        {
            initialPopulationProvider = new InitialPopulationProvider();
        }

        [TestMethod]
        public void TestThatPopulationHasExpectedNumberOfIndividuals()
        {
            const int populationSize = 50;

            var initialPopulation = initialPopulationProvider.GetInitialPopulation(populationSize, 20);

            Assert.AreEqual(populationSize, initialPopulation.Count);
        }

        [TestMethod]
        public void TestThatEachIndividualHasExpectedNumberOfGenes()
        {
            const int numberOfGenes = 20;

            var initialPopulation = initialPopulationProvider.GetInitialPopulation(50, numberOfGenes);

            foreach (var individual in initialPopulation)
            {
                Assert.AreEqual(numberOfGenes, individual.Weights.Length);
            }
        }

        [TestMethod]
        public void TestThatAllPopulationMembersHaveWeightsTotalSum1()
        {
            var initialPopulation = initialPopulationProvider.GetInitialPopulation(50, 5);

            foreach (var individual in initialPopulation)
            {
                var weightsSum = individual.Weights.Sum();
                Assert.IsTrue(1.0 - weightsSum < 0.01);
            }
        }
    }
}
