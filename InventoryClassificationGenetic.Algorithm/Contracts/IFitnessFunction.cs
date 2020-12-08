using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.Contracts
{
    public interface IFitnessFunction
    {
        int GetFitnessScore(Individual individual);
    }
}
