using InventoryClassificationGenetic.Algorithm.Contracts;
using InventoryClassificationGenetic.Domain;
using System.Collections.Generic;
using System;
using InventoryClassificationGenetic.Algorithm.Helpers;

namespace InventoryClassificationGenetic.Algorithm
{
    public class FitnessFunction : IFitnessFunction
    {
        public readonly List<Item> inventory;
        private readonly Classification classification;

        public FitnessFunction(List<Item> inventory)
        {
            this.inventory = inventory;

            classification = new Classification(inventory);
        }

        public double GetFitnessScore(Individual individual)
        {
            var fitnessScore = 0.0;

            foreach (var item in inventory)
                fitnessScore += GetClassificationScore(item, individual);

            return fitnessScore;
        }

        private double GetClassificationScore(Item item, Individual individual)
        {
            var itemClass = classification.ClassifyItem(item, individual);

            if (itemClass == item.Class)
                return 1.0;

            if (Math.Abs((int)itemClass - (int)item.Class) == 1)
                return 0.4;

            return 0;
        }
    }
}
