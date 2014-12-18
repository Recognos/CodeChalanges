using System;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge2
{
    class Program
    {
        static int counter = 0;

        static void Main(string[] args)
        {
            const int totalCount = 20;
            Task[] tasks = new Task[totalCount];

            for (int i = 0; i < totalCount; i++)
            {
                tasks[i] = Task.Factory.StartNew(() => LongRunningCpuIntensiveOperation()).Unwrap();
            }

            Task.WhenAll(tasks).Wait();

            Console.WriteLine("done");
            Console.ReadKey();
        }

        private static async Task LongRunningCpuIntensiveOperation()
        {
            var number = Interlocked.Increment(ref counter);
            Console.WriteLine("Starting {0}", number);
            await Task.Delay(1000); // simulate long running
            Console.WriteLine("Completed {0}", number);
        }
    }
}
