using System.Collections.Generic;
using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Providers.Contracts
{
    public interface ICitiesProvider
    {
        List<City> Cities { get; }
    }
}
