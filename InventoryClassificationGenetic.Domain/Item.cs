using System.Diagnostics.CodeAnalysis;

namespace InventoryClassificationGenetic.Domain
{
    [ExcludeFromCodeCoverage]
    public class Item
    {
        public string Identifier { get; set; }
        public double LeadTime { get; set; }
        public double AnnualDemand { get; set; }
        public double AverageUnitCost { get; set; }
        public ItemClass Class { get; set; }
    }
}
