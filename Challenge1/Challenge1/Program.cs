using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge1
{
    class Program
    {
        static void Main(string[] args)
        {
            Token[] tokens = Generator.GenerateTokens();
            Word[] words = Generator.GenerateWords();

            tokens.OrderByDescending(t => t.Box.Top).ThenBy(t => t.Box.Left);

            StableSort(tokens);
            StableSort(words);
        }
    }
}
