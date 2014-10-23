using System.Collections.Generic;
using System.Linq;

namespace Challenge1
{
    public struct Box
    {
        private readonly double[] box;

        public Box(double top, double left, double width, double height)
        {
            this.box = new[] { top, left, width, height };
        }

        public double Top { get { return this.box[0]; } }
        public double Left { get { return this.box[1]; } }
        public double Width { get { return this.box[2]; } }
        public double Height { get { return this.box[3]; } }

        public static Box Union(IEnumerable<Box> boxes)
        {
            var top = boxes.Min(b => b.Top);
            var left = boxes.Min(b => b.Left);
            var right = boxes.Max(b => b.Left + b.Width);
            var bottom = boxes.Max(b => b.Top + b.Height);
            return new Box(top, left, right - left, bottom - top);
        }
    }

    public interface BoxPositioned
    {
        Box Box { get; }
    }

    public struct Token : BoxPositioned
    {
        private readonly char @char;
        private readonly Box box;

        public Token(char @char, Box box)
        {
            this.@char = @char;
            this.box = box;
        }

        public char Char { get { return this.@char; } }
        public Box Box { get { return this.box; } }
    }

    public struct Word : BoxPositioned
    {
        private readonly Token[] tokens;
        private readonly Box box;

        public Word(Token[] tokens)
        {
            this.tokens = tokens;
            this.box = Box.Union(tokens.Select(t => t.Box));
        }

        public Box Box { get { return this.box; } }
        public Token[] Tokens { get { return this.tokens; } }
    }
}
