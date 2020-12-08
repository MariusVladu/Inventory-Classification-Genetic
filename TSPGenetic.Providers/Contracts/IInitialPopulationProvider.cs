using TSPGenetic.Domain;
using System.Collections.Generic;

namespace TSPGenetic.Providers.Contracts
{
    public interface IInitialPopulationProvider
    {
        List<Individual> GetInitialPopulation(int populationSize, int numberOfCities);
    }
}
