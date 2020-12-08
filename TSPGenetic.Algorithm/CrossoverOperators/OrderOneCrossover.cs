using System;
using System.Collections.Generic;
using TSPGenetic.Algorithm.CrossoverOperators;
using TSPGenetic.Algorithm.Helpers;
using TSPGenetic.Domain;

namespace TSPGenetic.Algorithm.CrossoverOperators
{
    public class OrderOneCrossover : BaseCrossoverOperator
    {
        protected override Tuple<Individual, Individual> PerformCrossover(Individual parent1, Individual parent2)
        {
            var left = random.Next(parent1.Genes.Length);
            var right = random.Next(parent1.Genes.Length);

            return PerformOrderOneCrossover(parent1, parent2, left, right);
        }

        public Tuple<Individual, Individual> PerformOrderOneCrossover(Individual parent1, Individual parent2, int left, int right)
        {
            var numberOfGenes = parent1.Genes.Length;
            CommonFunctions.SwapIfNotInOrder(ref left, ref right);

            var offspring1 = GetOffspring(left, right, numberOfGenes, parent1, parent2);
            var offspring2 = GetOffspring(left, right, numberOfGenes, parent2, parent1);

            return new Tuple<Individual, Individual>(offspring1, offspring2);
        }

        private Individual GetOffspring(int left, int right, int numberOfGenes, Individual parent1, Individual parent2)
        {
            var offspring = new Individual { Genes = new int[numberOfGenes] };

            for (int i = left; i < right; i++)
                offspring.Genes[i] = parent1.Genes[i];

            var genesToAddInOrder = GetGenesInOrder(left, right, numberOfGenes, offspring, parent2);

            for (int i = right; i < numberOfGenes; i++)
                offspring.Genes[i] = genesToAddInOrder.Dequeue();

            for (int i = 0; i < left; i++)
                offspring.Genes[i] = genesToAddInOrder.Dequeue();

            return offspring;
        }

        private Queue<int> GetGenesInOrder(int left, int right, int numberOfGenes, Individual offspring, Individual parent)
        {
            var genesInOrder = new Queue<int>(numberOfGenes);

            for (int i = right; i < numberOfGenes; i++)
                if(!GeneAlreadyExists(parent.Genes[i], left, right, offspring))
                    genesInOrder.Enqueue(parent.Genes[i]);

            for (int i = 0; i < right; i++)
                if (!GeneAlreadyExists(parent.Genes[i], left, right, offspring))
                    genesInOrder.Enqueue(parent.Genes[i]);

            return genesInOrder;
        }

        private bool GeneAlreadyExists(int gene, int left, int right, Individual offspring)
        {
            for (int i = left; i < right; i++)
                if (offspring.Genes[i] == gene) 
                    return true;

            return false;
        }
    }
}
