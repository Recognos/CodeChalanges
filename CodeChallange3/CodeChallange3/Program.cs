using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallange3
{
    /// <summary>
    /// Problem: 
    /// You need to download a number of files from different URIs. 
    /// You have N possible download URIs but only care about the first M results returned.
    /// You have to start downloading all N and return when M have completed. ( M < N ).
    /// 
    /// Challenge:
    /// Modify the DownloadFilesEfficiently to start downloading all URIs but return the first maxNumberOfFilesToDlwnload results.
    /// </summary>
    class Program
    {
        const int TotalNumberOfURIs = 20;
        const int FirstResultsCutoff = 5;

        static void Main(string[] args)
        {
            string[] uris = GetUris();

            var result = DownloadFilesEfficiently(uris, FirstResultsCutoff);

            foreach (var line in result)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("done");
            Console.ReadKey();
        }

        private static string[] DownloadFilesEfficiently(string[] uris, int maxNumberOfFilesToDlwnload)
        {
            // modify this to return after the first maxNumberOfFilesToDlwnload calls to DownloadFile complete

            var tasks = uris.Select(u => Task.Factory.StartNew(() => DownloadFile(u)).Unwrap());
            var result = Task.WhenAll(tasks).Result;
            return result;
        }

        private static string[] GetUris()
        {
            string[] uris = Enumerable.Range(0, 20)
                .Select(i => string.Format("http://server/file.{0}.txt", i))
                .ToArray();
            return uris;
        }

        private static async Task<string> DownloadFile(string uri)
        {
            int delay = RandomDuration();
            Console.WriteLine(delay);
            await Task.Delay(delay);
            return "FILE " + uri;
        }

        [ThreadStatic]
        private static Random rnd;

        private static int RandomDuration()
        {
            if (rnd == null)
            {
                rnd = new Random();
            }
            return rnd.Next(1, 10) * 500;
        }

    }
}
