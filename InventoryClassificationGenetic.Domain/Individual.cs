namespace InventoryClassificationGenetic.Domain
{
    public class Individual
    {
        public double[] Weights { get; set; }
        public double Xab { get; set; }
        public double Xbc { get; set; }

        public Individual Clone()
        {
            return new Individual
            {
                Weights = (double[])this.Weights.Clone()
            };
        }
    }
}
