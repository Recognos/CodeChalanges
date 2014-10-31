
namespace Challenge1
{
    public sealed class BogdanSolution : Solution
    {
        public string Name { get { return "Bogdan Sort"; } }
        public string Note { get { return "Bogdan Galiceanu's solution (Gnome sort, variant of insertion sort)"; } }
        public void SortTokens(ref Token[] tokens) { StableSort(ref tokens); }
        public void SortWords(ref Word[] words) { StableSort(ref words); }

        public static void StableSort<T>(ref T[] elements) where T : BoxPositioned
        {
            var pos = 1;
            var last = 0;
            while (pos < elements.Length)
            {
                if (ShouldSwap(elements[pos - 1], elements[pos]))
                {
                    Swap(elements, pos - 1, pos);
                    if (pos > 1)
                    {
                        if (last == 0)
                        {
                            last = pos;
                        }
                        pos--;
                    }
                    else
                    {
                        pos++;
                    }
                }
                else
                {
                    if (last != 0)
                    {
                        pos = last;
                        last = 0;
                    }
                    pos++;
                }
            }
        }

        private static void Swap<T>(T[] elements, int pos1, int pos2)
        {
            var temp = elements[pos1];
            elements[pos1] = elements[pos2];
            elements[pos2] = temp;
        }

        private static bool ShouldSwap(BoxPositioned element1, BoxPositioned element2)
        {
            return element1.Box.Top < element2.Box.Top
                || (element1.Box.Top == element2.Box.Top && element1.Box.Left > element2.Box.Left);
        }
    }
}
