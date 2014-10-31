using System.Collections.Generic;

namespace Challenge1
{
    public sealed class InsertionSortSolution : Solution
    {
        public string Name { get { return "Insert Sort"; } }
        public string Note { get { return "Stable, in place, very fast on almost sorted data, not so good on unsorted data"; } }
        public void SortTokens(ref Token[] tokens) { StableSort(ref tokens); }
        public void SortWords(ref Word[] words) { StableSort(ref words); }

        public static void StableSort<T>(ref T[] array)
           where T : struct, BoxPositioned
        {
            for (int i = 0; i < array.Length; i++)
            {
                int j = i;
                while (j > 0 && Compare(array[j - 1].Box, array[j].Box) > 0)
                {
                    var temp = array[j];
                    array[j] = array[j - 1];
                    array[j - 1] = temp;
                    j--;
                }
            }
        }

        private static int Compare(Box first, Box second)
        {
            var top = Comparer<double>.Default.Compare(second.Top, first.Top);
            return top != 0 ? top : Comparer<double>.Default.Compare(first.Left, second.Left);
        }
    }
}
