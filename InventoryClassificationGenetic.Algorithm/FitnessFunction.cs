using InventoryClassificationGenetic.Algorithm.Contracts;
using InventoryClassificationGenetic.Domain;
using System.Collections.Generic;
using System;

namespace InventoryClassificationGenetic.Algorithm
{
    public class FitnessFunction : IFitnessFunction
    {
        public readonly List<City> cities;
        public readonly double[,] distances;

        public FitnessFunction(List<City> cities)
        {
            this.cities = cities;

            distances = CalculateDistances();
        }

        public int GetFitnessScore(Individual individual)
        {
            var totalDistance = 0.0;
            
            for (int i = 1; i < cities.Count; i++)
                totalDistance += distances[individual.Genes[i - 1], individual.Genes[i]];

            return (int)totalDistance;
        }

        private double[,] CalculateDistances()
        {
            var distances = new double[cities.Count, cities.Count];

            for (int i = 0; i < cities.Count; i++)
                for (int j = 0; j < cities.Count; j++)
                    distances[i, j] = CalculateDistanceBetweenTwoCities(cities[i], cities[j]);

            return distances;
        }

        private double CalculateDistanceBetweenTwoCities(City city1, City city2)
        {
            return Math.Sqrt(Math.Pow(city1.X - city2.X, 2) + Math.Pow(city1.Y - city2.Y, 2));
        }
    }
}
