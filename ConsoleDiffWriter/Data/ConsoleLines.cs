using System.Collections;
using System.Drawing;

namespace YonatanMankovich.ConsoleDiffWriter.Data
{
    /// <summary>
    /// Represents a collection of colored console string lines.
    /// </summary>
    public class ConsoleLines : IEnumerable<ConsoleString>
    {
        private IList<ConsoleString> Lines { get; set; }

        /// <summary>
        /// Gets the number of lines in the current <see cref="ConsoleLines"/> object.
        /// </summary>
        public int Count => Lines.Count;

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleLines"/> class without any lines.
        /// </summary>
        public ConsoleLines()
        {
            Lines = new List<ConsoleString>();
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleLines"/> class 
        /// with the <see cref="IEnumerable{T}"/> of <see cref="ConsoleString"/> lines.
        /// </summary>
        public ConsoleLines(IEnumerable<ConsoleString> lines)
        {
            Lines = new List<ConsoleString>(lines);
        }

        /// <summary>
        /// Gets or sets the <see cref="ConsoleString"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="ConsoleString"/> at the specified index.</returns>
        public ConsoleString this[int index] { get => Lines[index]; set => Lines[index] = value; }

        /// <summary>
        /// Adds a <see cref="ConsoleString"/> to the end of the current <see cref="ConsoleLines"/> structure.
        /// </summary>
        /// <param name="line">The <see cref="ConsoleString"/> to add.</param>
        /// <returns>The updated self.</returns>
        public ConsoleLines AddLine(ConsoleString line)
        {
            Lines.Add(line);
            return this;
        }

        /// <summary>
        /// Adds a blank line to the end of the current <see cref="ConsoleLines"/> structure.
        /// </summary>
        /// <returns>The updated self.</returns>
        public ConsoleLines AddLine()
        {
            Lines.Add(new ConsoleString());
            return this;
        }

        /// <summary>
        /// Adds a the given <see cref="ConsoleLines"/> to the end of the current <see cref="ConsoleLines"/> structure.
        /// </summary>
        /// <param name="lines">The <see cref="ConsoleLines"/> to add.</param>
        /// <returns>The updated self.</returns>
        public ConsoleLines AddLines(ConsoleLines lines)
        {
            foreach (ConsoleString line in lines)
                AddLine(line);
            return this;
        }

        /// <summary>
        /// Adds a <see cref="ConsoleCharacter"/> to the end of the last line
        /// of the current <see cref="ConsoleLines"/> structure.
        /// </summary>
        /// <param name="character">The <see cref="ConsoleString"/> to add.</param>
        /// <returns>The updated self.</returns>
        public ConsoleLines AddToEndOfLastLine(ConsoleCharacter character)
        {
            GetLastLine().AddToEnd(character);
            return this;
        }

        /// <summary>
        /// Adds a <see cref="ConsoleString"/> to the end of the last line
        /// of the current <see cref="ConsoleLines"/> structure.
        /// </summary>
        /// <param name="str">The <see cref="ConsoleString"/> to add.</param>
        /// <returns>The updated self.</returns>
        public ConsoleLines AddToEndOfLastLine(ConsoleString str)
        {
            GetLastLine().AddToEnd(str);
            return this;
        }

        /// <summary>
        /// Gets the last <see cref="ConsoleString"/> line of the current <see cref="ConsoleLines"/> structure. 
        /// </summary>
        /// <returns>The last <see cref="ConsoleString"/> line of the current <see cref="ConsoleLines"/> structure.</returns>
        public ConsoleString GetLastLine()
        {
            if (Count == 0)
                AddLine();
            return Lines[Lines.Count - 1];
        }

        /// <summary>
        /// Writes the current <see cref="ConsoleLines"/> to the console.
        /// </summary>
        public void Write()
        {
            foreach (ConsoleString str in Lines)
                str.WriteLine();
        }

        /// <summary>
        /// Writes the current <see cref="ConsoleLines"/> to the console at a given point.
        /// </summary>
        /// <param name="point">The position in the console to write the current <see cref="ConsoleLines"/> to.</param>
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

        /// <inheritdoc/>
        public override string? ToString()
        {
            return string.Join(Environment.NewLine, Lines.Select(l => l.ToString()));
        }

        /// <summary>
        /// Returns an enumerator the iterates through the collection of 
        /// <see cref="ConsoleString"/> lines of the current <see cref="ConsoleLines"/>.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="ConsoleLines"/>.</returns>
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