using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.Contracts
{
    public interface IFitnessFunction
    {
        double GetFitnessScore(Individual individual);
    }
}
