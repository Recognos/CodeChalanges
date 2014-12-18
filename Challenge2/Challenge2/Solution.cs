using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge2
{
    class Solution
    {
        const int MaxConcurrency = 5;
        const int TotalNumberOfExecutions = 20;

        static int currentlyActiveOperations = 0;
        static int startedOperations = 0;

        static SemaphoreSlim semaphore = new SemaphoreSlim(MaxConcurrency);

        private static void RunMain()
        {
            Task[] tasks = new Task[TotalNumberOfExecutions];

            for (int i = 0; i < TotalNumberOfExecutions; i++)
            {
                tasks[i] = Task.Factory.StartNew(() => LongRunningCpuIntensiveOperationAsync()).Unwrap();
            }

            Task.WhenAll(tasks).Wait();

            Console.WriteLine("done");
            Console.ReadKey();
        }

        private static async Task LongRunningCpuIntensiveOperationAsync()
        {
            try
            {
                await semaphore.WaitAsync();
                await RealOpeartion();
            }
            finally
            {
                semaphore.Release();
            }
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
