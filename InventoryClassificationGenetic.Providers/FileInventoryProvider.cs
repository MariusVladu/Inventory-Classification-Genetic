using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using InventoryClassificationGenetic.Domain;
using InventoryClassificationGenetic.Providers.Contracts;

namespace InventoryClassificationGenetic.Providers
{
    public class FileInventoryProvider : IInventoryProvider
    {
        public List<Item> Inventory { get; }

        public FileInventoryProvider(string filePath)
        {
            var inventory = new List<Item>();
            StreamReader file = new StreamReader(filePath);
            string line;

            file.ReadLine();

            while ((line = file.ReadLine()) != null)
            {
                var criterias = line.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .ToList();

                inventory.Add(new Item
                {
                    Identifier = criterias[0],
                    LeadTime = Convert.ToDouble(criterias[1]),
                    AnnualDemand = Convert.ToDouble(criterias[2]),
                    AverageUnitCost = Convert.ToDouble(criterias[3]),
                    Class = (ItemClass)Enum.Parse(typeof(ItemClass), criterias[4])
                });
            }

            Inventory = inventory;
        }
    }
}
