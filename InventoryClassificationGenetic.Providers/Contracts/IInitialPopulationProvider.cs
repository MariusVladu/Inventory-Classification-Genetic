using InventoryClassificationGenetic.Domain;
using System.Collections.Generic;

namespace InventoryClassificationGenetic.Providers.Contracts
{
    public interface IInitialPopulationProvider
    {
        List<Individual> GetInitialPopulation(int populationSize, int numberOfCities);
    }
}
