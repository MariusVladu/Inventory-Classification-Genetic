using System.Diagnostics.CodeAnalysis;

namespace InventoryClassificationGenetic.Domain
{
    [ExcludeFromCodeCoverage]
    public class Item
    {
        public string Identifier { get; set; }
        public double AnnualDollarUsage { get; set; }
        public double NumberOfRequestsPerYear { get; set; }
        public double UnitPrice { get; set; }
        public ItemClass Class { get; set; }
    }
}
