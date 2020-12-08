using TSPGenetic.Domain;

namespace TSPGenetic.Algorithm.Contracts
{
    public interface IFitnessFunction
    {
        int GetFitnessScore(Individual individual);
    }
}
