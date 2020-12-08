namespace TSPGenetic.Domain
{
    public class Individual
    {
        public int[] Genes { get; set; }

        public Individual Clone()
        {
            return new Individual
            {
                Genes = (int[])this.Genes.Clone()
            };
        }
    }
}
