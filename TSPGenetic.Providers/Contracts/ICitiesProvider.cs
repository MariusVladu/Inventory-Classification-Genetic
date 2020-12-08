using System.Collections.Generic;
using TSPGenetic.Domain;

namespace TSPGenetic.Providers.Contracts
{
    public interface ICitiesProvider
    {
        List<City> Cities { get; }
    }
}
