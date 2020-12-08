using System.Collections.Generic;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Providers.Contracts
{
    public interface IInventoryProvider
    {
        List<Item> Inventory { get; }
    }
}
