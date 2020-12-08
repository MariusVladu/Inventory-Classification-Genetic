using InventoryClassificationGenetic.Domain;

namespace InventoryClassificationGenetic.Algorithm.Contracts
{
    public interface IMutationOperator
    {
        void ApplyMutation(Individual individual, double mutationRate);
    }
}
