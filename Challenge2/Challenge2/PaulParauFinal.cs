using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge2
{
    class PaulParauFinal
    {
        const int MaxConcurrency = 5;
        const int TotalNumberOfExecutions = 20;
        static int currentlyActiveOperations = 0;
        static int startedOperations = 0;

        public static void RunMain()
        {
            var tasks = new Dictionary<int, Task<int>>();

            for (int j = 0; j < MaxConcurrency; j++)
            {
                int x = j;
                tasks.Add(x, Task.Factory.StartNew(() => LongRunningCpuIntensiveOperationAsync(x)).Unwrap());
            }

            int nbExecuting = 5;

            while (nbExecuting < TotalNumberOfExecutions)
            {
                var id = Task.WhenAny(tasks.Values).Unwrap().Result;
                tasks.Remove(id);

                var x = nbExecuting;
                tasks.Add(x, Task.Factory.StartNew(() => LongRunningCpuIntensiveOperationAsync(x)).Unwrap());
                nbExecuting++;
            }

            Task.WhenAll(tasks.Values).Wait();

            Console.WriteLine("done");
            Console.ReadKey();
        }

        private static async Task<int> LongRunningCpuIntensiveOperationAsync(int id)
        {
            await RealOpeartion();
            return id;
        }

        private static async Task RealOpeartion()
        {
            Interlocked.Increment(ref currentlyActiveOperations);
            if (currentlyActiveOperations > 5)
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
