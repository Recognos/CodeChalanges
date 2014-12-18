using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge2
{
    /// <summary>
    /// Intro: 
    /// You need to execute a potentially large number of operations that are IO and CPU intensive (like OCRing a document) and you need to 
    /// limit the level of concurrency at which the operations are executed. Scheduling to many CPU intensive operations is bad for performance.
    /// 
    /// Challenge:
    /// Modify the code below so that the concurrency level at which RealOperation() is executed never exceeds the value of MaxConcurrency.
    /// - RealOperation() must be executed TotalNumberOfExecutions times.
    /// - RealOperation() the max number of concurrent calls to RealOperation() must be MaxConcurrency
    /// </summary>
    class Program
    {
        const int MaxConcurrency = 5;
        const int TotalNumberOfExecutions = 20;

        static int currentlyActiveOperations = 0;
        static int startedOperations = 0;

        static void Main(string[] args)
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
