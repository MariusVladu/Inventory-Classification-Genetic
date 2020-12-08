using InventoryClassificationGenetic.Domain;
using System.Collections.Generic;
using System.Linq;

namespace InventoryClassificationGenetic.Algorithm.Helpers
{
    public class Classification
    {
        private readonly double minAnnualDollarUsage;
        private readonly double maxAnnualDollarUsage;

        private readonly double minNumberOfRequestsPerYear;
        private readonly double maxNumberOfRequestsPerYear;

        private readonly double minUnitPrice;
        private readonly double maxUnitPrice;

        public Classification(List<Item> inventory)
        {
            minAnnualDollarUsage = inventory.Min(x => x.AnnualDollarUsage);
            maxAnnualDollarUsage = inventory.Max(x => x.AnnualDollarUsage);

            minNumberOfRequestsPerYear = inventory.Min(x => x.NumberOfRequestsPerYear);
            maxNumberOfRequestsPerYear = inventory.Max(x => x.NumberOfRequestsPerYear);

            minUnitPrice = inventory.Min(x => x.UnitPrice);
            maxUnitPrice = inventory.Max(x => x.UnitPrice);
        }

        public ItemClass ClassifyItem(Item item, Individual individual)
        {
            var weightedSum = 0.0;

            weightedSum += individual.Weights[0] * (item.AnnualDollarUsage - minAnnualDollarUsage) / (maxAnnualDollarUsage - minAnnualDollarUsage);
            weightedSum += individual.Weights[1] * (item.NumberOfRequestsPerYear - minNumberOfRequestsPerYear) / (maxNumberOfRequestsPerYear - minNumberOfRequestsPerYear);
            weightedSum += individual.Weights[2] * (item.UnitPrice - minUnitPrice) / (maxUnitPrice - minUnitPrice);

            if (individual.Xab <= weightedSum)
                return ItemClass.A;

            if (individual.Xbc <= weightedSum && weightedSum < individual.Xab)
                return ItemClass.B;

            return ItemClass.C;
        }
    }
}
