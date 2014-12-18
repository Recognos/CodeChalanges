using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge2
{
    class PaulParau1
    {
        const int MaxConcurrency = 5;
        const int TotalNumberOfExecutions = 20;

        static int currentlyActiveOperations = 0;
        static int startedOperations = 0;

        public static void RunMain()
        {
            for (int i = 0; i < TotalNumberOfExecutions; i += MaxConcurrency)
            {
                var tasks = new Task[MaxConcurrency];
                for (int j = 0; j < MaxConcurrency; j++)
                {
                    tasks[j] = Task.Factory.StartNew(() => LongRunningCpuIntensiveOperationAsync()).Unwrap();
                }

                Task.WhenAll(tasks).Wait();
            }

            Console.WriteLine("done");
            Console.ReadKey();
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
