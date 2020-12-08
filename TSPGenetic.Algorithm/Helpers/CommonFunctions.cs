namespace TSPGenetic.Algorithm.Helpers
{
    public static class CommonFunctions
    {
        public static void SwapIfNotInOrder(ref int left, ref int right)
        {
            if (left > right)
            {
                var temp = left;
                left = right;
                right = temp;
            }
        }

        public static void SwapElements<T>(T[] array, int index1, int index2)
        {
            var temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }

        public static void Swap<T>(ref T item1, ref T item2)
        {
            var temp = item1;
            item1 = item2;
            item2 = temp;
        }
    }
}
