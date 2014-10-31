using System;
using System.Collections.Generic;

namespace Challenge1
{
    public class ArraySortSolution : Solution
    {
        public string Name { get { return "Array Sort"; } }
        public string Note { get { return "In place, unstable sort - Introspection sort - very smart & fast algorithm"; } }
        public void SortTokens(ref Token[] tokens) { StableSort(ref tokens); }
        public void SortWords(ref Word[] words) { StableSort(ref words); }

        public static void StableSort<T>(ref T[] array)
           where T : struct, BoxPositioned
        {
            // unstable sort
            Array.Sort(array, BoxComparer<T>.Default);
        }

        public sealed class BoxComparer<T> : IComparer<T> where T : struct, BoxPositioned
        {
            public static readonly IComparer<T> Default = new BoxComparer<T>();

            public int Compare(T x, T y)
            {
                var top = Comparer<double>.Default.Compare(y.Box.Top, x.Box.Top);
                return top != 0 ? top : Comparer<double>.Default.Compare(x.Box.Left, y.Box.Left);
            }
        }
    }
}
