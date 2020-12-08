using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using TSPGenetic.Providers.Contracts;

namespace TSPGenetic.Providers.UnitTests
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
                Assert.AreEqual(numberOfGenes, individual.Genes.Length);
            }
        }

        [TestMethod]
        public void TestThatAllPopulationMembersHaveOnlyDistinctGenes()
        {
            var initialPopulation = initialPopulationProvider.GetInitialPopulation(50, 20);

            foreach (var individual in initialPopulation)
            {
                var distinctGenes = individual.Genes.Distinct();
                Assert.AreEqual(individual.Genes.Length, distinctGenes.Count());
            }
        }
    }
}
