using System;
using System.Diagnostics;
using System.Linq;
using FluentAssertions;

namespace Challenge1
{
    public interface Solution
    {
        string Name { get; }
        string Note { get; }
        void SortTokens(ref Token[] tokens);
        void SortWords(ref Word[] words);
    }

    class Program
    {
        private static readonly Solution[] solutions = new Solution[] { new InsertionSortSolution(), new BogdanSolution(), new ArraySortSolution(), new LinqSolution() };
        private static readonly Token[] tokens = Generator.GenerateTokens(20000);
        private static readonly Word[] words = Generator.GenerateWords(20000);

        static void Main(string[] args)
        {
            foreach (var solution in solutions)
            {
                Console.WriteLine("{0}: {1}", solution.Name, solution.Note);
            }

            WarmUp();

            Console.WriteLine("Tokens: {0}", tokens.Length);
            Console.WriteLine("Words: {0}", words.Length);

            foreach (var solution in solutions)
            {
                var time = Enumerable.Range(0, 4)
                    .Select(t => TestTokensSolution(solution).TotalMilliseconds)
                    .Average();

                Console.WriteLine("{0}\t\t{1} (tokens)", solution.Name, TimeSpan.FromMilliseconds(time));
            }

            foreach (var solution in solutions)
            {
                var time = Enumerable.Range(0, 4)
                    .Select(t => TestPreSortedTokensSolution(solution).TotalMilliseconds)
                    .Average();

                Console.WriteLine("{0}\t\t{1} (pre sorted tokens)", solution.Name, TimeSpan.FromMilliseconds(time));
            }

            Console.WriteLine("done. press any key to exit");
            Console.ReadKey();
        }

        private static TimeSpan TestTokensSolution(Solution solution)
        {
            var tokensCopy = Program.tokens.ToArray();

            var watch = Stopwatch.StartNew();
            solution.SortTokens(ref tokensCopy);
            var result = watch.Elapsed;

            tokensCopy.Select(t => t.Box.Top).Should().BeInDescendingOrder();
            return result;

        }

        private static TimeSpan TestPreSortedTokensSolution(Solution solution)
        {
            var tokensCopy = Program.tokens.ToArray();
            solution.SortTokens(ref tokensCopy);

            Swap(tokensCopy, 100, 1000);
            Swap(tokensCopy, 400, 1123);
            Swap(tokensCopy, 500, 3422);
            Swap(tokensCopy, 1100, 30);
            Swap(tokensCopy, 1600, 899);

            var watch = Stopwatch.StartNew();
            solution.SortTokens(ref tokensCopy);
            var result = watch.Elapsed;

            tokensCopy.Select(t => t.Box.Top).Should().BeInDescendingOrder();
            return result;
        }

        private static TimeSpan TestWordsSolution(Solution solution)
        {
            var wordsCopy = Program.words.ToArray();

            var watch = Stopwatch.StartNew();
            solution.SortWords(ref wordsCopy);
            var result = watch.Elapsed;

            wordsCopy.Select(t => t.Box.Top).Should().BeInDescendingOrder();
            return result;
        }

        private static void WarmUp()
        {
            foreach (var solution in solutions)
            {
                Token[] tokens = Generator.GenerateTokens();
                Word[] words = Generator.GenerateWords();

                solution.SortTokens(ref tokens);
                solution.SortWords(ref words);

                tokens.Select(t => t.Box.Top).Should().BeInDescendingOrder();
            }
        }

        private static void Swap<T>(T[] data, int i, int j)
        {
            T temp = data[i];
            data[i] = data[j];
            data[j] = temp;
        }
    }
}
