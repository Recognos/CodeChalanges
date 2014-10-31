using System.Collections.Generic;
using System.Linq;

namespace Challenge1
{
    public class LinqSolution : Solution
    {
        public static volatile object hook;

        public string Name { get { return "LINQ Sort"; } }
        public string Note { get { return "Stable sort, not in place - Introspection sort - very smart & fast algorithm"; } }
        public void SortTokens(ref Token[] tokens) { StableSort(ref tokens); }
        public void SortWords(ref Word[] words) { StableSort(ref words); }

        public static void StableSort<T>(ref T[] array)
           where T : struct, BoxPositioned
        {
            // stable sort, not in place
            array = array.OrderByDescending(t => t.Box.Top)
                .ThenBy(t => t.Box.Left)
                .ToArray();
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
