using TSPGenetic.Domain;

namespace TSPGenetic.Algorithm.Contracts
{
    public interface IMutationOperator
    {
        void ApplyMutation(Individual individual, double mutationRate);
    }
}
