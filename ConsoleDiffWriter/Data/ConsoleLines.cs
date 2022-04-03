using System.Collections;
using System.Drawing;

namespace YonatanMankovich.ConsoleDiffWriter.Data
{
    public class ConsoleLines : IEnumerable<ConsoleString>
    {
        private IList<ConsoleString> Lines { get; set; }
        public int Count => Lines.Count;

        public ConsoleLines()
        {
            Lines = new List<ConsoleString>();
        }

        public ConsoleLines(IEnumerable<ConsoleString> characters)
        {
            Lines = new List<ConsoleString>(characters);
        }

        public ConsoleString this[int index] { get => Lines[index]; set => Lines[index] = value; }

        public void AddLine(ConsoleString line)
        {
            Lines.Add(line);
        }

        public void Write()
        {
            foreach (ConsoleString str in Lines)
                str.WriteLine();
        }

        public void WriteAtPoint(Point point)
        {
            // Save previous cursor coordinates.
            int prevCursorTop = Console.CursorTop;
            int prevCursorLeft = Console.CursorLeft;

            // Write.
            Console.SetCursorPosition(point.X, point.Y);
            Write();

            // Restore previous cursor coordinates.
            Console.SetCursorPosition(prevCursorLeft, prevCursorTop);
        }

        public override string? ToString()
        {
            return string.Join(Environment.NewLine, Lines.Select(l => l.ToString()));
        }

        public IEnumerator<ConsoleString> GetEnumerator()
        {
            return Lines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Lines).GetEnumerator();
        }
    }
}