using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TSPGenetic.Domain;
using TSPGenetic.Providers.Contracts;

namespace TSPGenetic.Providers
{
    public class FileCitiesProvider : ICitiesProvider
    {
        public List<City> Cities { get; }

        public FileCitiesProvider(string filePath)
        {
            var cities = new List<City>();
            StreamReader file = new StreamReader(filePath);
            string line;

            while ((line = file.ReadLine()) != null)
            {
                var coordinates = line.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(Convert.ToDouble)
                    .ToList();

                cities.Add(new City
                {
                    X = coordinates[0],
                    Y = coordinates[1]
                });
            }

            Cities = cities;
        }
    }
}
