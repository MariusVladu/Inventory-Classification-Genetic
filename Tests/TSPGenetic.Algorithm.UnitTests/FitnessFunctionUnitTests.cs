using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TSPGenetic.Domain;

namespace TSPGenetic.Algorithm.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FitnessFunctionUnitTests
    {
        private FitnessFunction fitnessFunction;

        [TestInitialize]
        public void Setup()
        {
            fitnessFunction = new FitnessFunction(GetTestCities());
        }

        [TestMethod]
        public void TestThatWhenCitiesAreInOrderFitnessScoreIsReturnedAsExpected()
        {
            var individual = new Individual { Genes = new int[] { 0, 1, 2, 3 } };
            const int expectedFitnessScore = 10;

            var result = fitnessFunction.GetFitnessScore(individual);

            Assert.AreEqual(expectedFitnessScore, result);
        }

        [TestMethod]
        public void TestThatWhenCitiesAreInRandomOrderFitnessScoreIsReturnedAsExpected()
        {
            var individual = new Individual { Genes = new int[] { 1, 3, 0, 2 } };
            const int expectedFitnessScore = 14;

            var result = fitnessFunction.GetFitnessScore(individual);

            Assert.AreEqual(expectedFitnessScore, result);
        }

        [TestMethod]
        public void TestThatDistancesAreComputedAsExpected()
        {
            var expectedDistances = new double[,]
            {
                { 0.0, 3.0, 5.0, 4.0 },
                { 3.0, 0.0, 3.162277, 5.0 },
                { 5.0, 3.162277, 0.0, 4.123105 },
                { 4.0, 5.0, 4.123105, 0.0 }
            };

            fitnessFunction = new FitnessFunction(GetTestCities());

            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    fitnessFunction.distances[i, j] = Math.Round(fitnessFunction.distances[i, j], 6, MidpointRounding.ToZero);

            CollectionAssert.AreEqual(expectedDistances, fitnessFunction.distances);
        }

        private List<City> GetTestCities()
        {
            return new List<City>
            {
                new City { X = 0, Y = 0 },
                new City { X = 3, Y = 0 },
                new City { X = 4, Y = 3 },
                new City { X = 0, Y = 4 }
            };
        }
    }
}
