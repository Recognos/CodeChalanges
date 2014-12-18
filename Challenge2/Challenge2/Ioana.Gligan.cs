
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Challenge2
{
    public static class IoanaGligan
    {
        const int MaxConcurrency = 5;
        const int TotalNumberOfExecutions = 20;

        static int currentlyActiveOperations = 0;
        static int startedOperations = 0;

        public static void RunMain(string[] args)
        {
            for (int i = 0; i < TotalNumberOfExecutions / MaxConcurrency + 1; i++)
            {
                RunMaxTasksInParallel(i * MaxConcurrency, Math.Min((i + 1) * MaxConcurrency, TotalNumberOfExecutions));
            }

            Console.WriteLine("done");
            Console.ReadKey();

        }

        private static void RunMaxTasksInParallel(int startIndex, int endIndex)
        {
            Task[] tasks = new Task[endIndex - startIndex];
            for (int j = startIndex; j < endIndex; j++)
            {
                tasks[j - startIndex] = Task.Factory.StartNew(() => LongRunningCpuIntensiveOperationAsync()).Unwrap();
            }
            Task.WhenAll(tasks).Wait();
        }


        private static async Task LongRunningCpuIntensiveOperationAsync()
        {
            await RealOpeartion();
        }

        // Simulates async work. You can't modify tis method
        private static async Task RealOpeartion()
        {
            Interlocked.Increment(ref currentlyActiveOperations);

            if (currentlyActiveOperations > MaxConcurrency)
            {
                throw new InvalidOperationException("Too much concurrency");
            }

            var number = Interlocked.Increment(ref startedOperations);
            Console.WriteLine("Starting {0}", number);
            await Task.Delay(1000);
            Console.WriteLine("Completed {0}", number);
            Interlocked.Decrement(ref currentlyActiveOperations);
        }
    }
}
