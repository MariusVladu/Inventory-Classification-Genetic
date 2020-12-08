using InventoryClassificationGenetic.Domain;
using System.Collections.Generic;
using System.Linq;

namespace InventoryClassificationGenetic.Algorithm.Helpers
{
    public class Classification
    {
        private readonly double minLeadTime;
        private readonly double maxLeadTime;

        private readonly double minAnnualDemand;
        private readonly double maxAnnualDemand;

        private readonly double minAverageUnitCost;
        private readonly double maxAverageUnitCost;

        public Classification(List<Item> inventory)
        {
            minLeadTime = inventory.Min(x => x.LeadTime);
            maxLeadTime = inventory.Max(x => x.LeadTime);

            minAnnualDemand = inventory.Min(x => x.AnnualDemand);
            maxAnnualDemand = inventory.Max(x => x.AnnualDemand);

            minAverageUnitCost = inventory.Min(x => x.AverageUnitCost);
            maxAverageUnitCost = inventory.Max(x => x.AverageUnitCost);
        }

        public ItemClass ClassifyItem(Item item, Individual individual)
        {
            var weightedSum = 0.0;

            weightedSum += individual.Weights[0] * (item.LeadTime - minLeadTime) / (maxLeadTime - minLeadTime);
            weightedSum += individual.Weights[1] * (item.AnnualDemand - minAnnualDemand) / (maxAnnualDemand - minAnnualDemand);
            weightedSum += individual.Weights[2] * (item.AverageUnitCost - minAverageUnitCost) / (maxAverageUnitCost - minAverageUnitCost);

            if (individual.Xab <= weightedSum)
                return ItemClass.A;

            if (individual.Xbc <= weightedSum && weightedSum < individual.Xab)
                return ItemClass.B;

            return ItemClass.C;
        }
    }
}
