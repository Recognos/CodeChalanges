using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge2
{
    public static class IoanaGliganFinal
    {
        const int MaxConcurrency = 5;
        const int TotalNumberOfExecutions = 20;

        static int currentlyActiveOperations = 0;
        static int startedOperations = 0;

        public static void RunMain(string[] args)
        {
            Task[] tasks = new Task[MaxConcurrency];
            for (int i = 0; i < MaxConcurrency; i++)
            {
                tasks[i] = Task.Factory.StartNew(() => LongRunningCpuIntensiveOperationQueueAsync()).Unwrap();
            }
            Task.WhenAll(tasks).Wait();

            if (startedOperations > TotalNumberOfExecutions)
            {
                throw new InvalidOperationException("the fuck`");
            }

            Console.WriteLine("done");
            Console.ReadKey();
        }

        private static async Task LongRunningCpuIntensiveOperationQueueAsync()
        {
            while (startedOperations < TotalNumberOfExecutions)
            {
                var number = Interlocked.Increment(ref startedOperations);
                if (number < TotalNumberOfExecutions)
                {
                    await RealOperation(number);
                }
            }
        }

        private static async Task RealOperation(int number)
        {
            Interlocked.Increment(ref currentlyActiveOperations);

            if (currentlyActiveOperations > 5)
            {
                throw new InvalidOperationException("Too much concurrency");
            }

            Console.WriteLine("Starting {0}", number);
            await Task.Delay(1000);
            Console.WriteLine("Completed {0}", number);
            Interlocked.Decrement(ref currentlyActiveOperations);
        }

    }
}
