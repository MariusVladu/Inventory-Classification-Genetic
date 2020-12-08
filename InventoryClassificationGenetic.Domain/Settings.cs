﻿using System.Collections.Generic;

namespace InventoryClassificationGenetic.Domain
{
    public class Settings
    {
        public List<Item> Inventory { get; set; }
        public int PopulationSize { get; set; }
        public int NumberOfElites { get; set; }
        public double CrossoverRate { get; set; }
        public double MutationRate { get; set; }
    }
}
