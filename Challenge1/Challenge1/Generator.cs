
using System;
using System.Linq;
namespace Challenge1
{
    public static class Generator
    {
        private static readonly Random rnd = new Random();

        public static Token[] GenerateTokens(int maxCount = 100)
        {
            return Enumerable.Range(1, rnd.Next(1, maxCount))
                .Select(i => new Token((char)('a' + rnd.Next(0, 25)), RandomBox()))
                .ToArray();
        }

        public static Word[] GenerateWords(int maxCount = 100)
        {
            return Enumerable.Range(1, rnd.Next(1, maxCount))
                .Select(i => new Word(Generator.GenerateTokens(10)))
                .ToArray();
        }

        private static Box RandomBox()
        {
            return new Box(rnd.Next(0, 100), rnd.Next(0, 100), rnd.Next(0, 100), rnd.Next(0, 100));
        }
    }
}
