using System.Collections.Generic;

namespace TSPGenetic.Domain
{
    public class Settings
    {
        public List<City> Cities { get; set; }
        public int PopulationSize { get; set; }
        public int NumberOfElites { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationRate { get; set; }
    }
}
