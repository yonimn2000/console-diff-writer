using System.Collections;
using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Data;

namespace YonatanMankovich.ConsoleDiffWriter.Diff
{
    /// <summary>
    /// Represents a structure that keeps track of a <see cref="ConsoleString"/> 
    /// written to the console at a specific point and can write just the difference
    /// between it and a new <see cref="ConsoleString"/>.
    /// </summary>
    public class ConsoleDiffString : IEnumerable<ConsoleDiffCharacter>
    {
        /// <summary>
        /// The point on the console to which to write the <see cref="ConsoleString"/> to.
        /// </summary>
        public Point Point { get; }

        /// <summary>
        /// The length of the written <see cref="ConsoleString"/>.
        /// </summary>
        public int Length => WrittenString.Count;
        private IList<ConsoleDiffCharacter> WrittenString { get; set; }

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleDiffString"/> with 
        /// a <see cref="ConsoleString"/> and a <see cref="System.Drawing.Point"/>.
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ConsoleString"/> to.</param>
        /// <param name="str">The <see cref="ConsoleString"/> to keep track of.</param>
        public ConsoleDiffString(Point point, ConsoleString str) : this(point)
        {
            for (int i = 0; i < str.Length; i++)
                WrittenString.Add(new ConsoleDiffCharacter(new Point(point.X + i, point.Y), str[i]));
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ConsoleDiffString"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public ConsoleDiffString() : this(new Point(Console.CursorLeft, Console.CursorTop)) { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ConsoleDiffString"/> with 
        /// a <see cref="System.Drawing.Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ConsoleString"/> to.</param>
        public ConsoleDiffString(Point point)
        {
            WrittenString = new List<ConsoleDiffCharacter>();
            Point = point;
        }

        /// <summary>
        /// Writes only the difference between the written <see cref="ConsoleString"/>
        /// and the given <see cref="ConsoleString"/> to the console.
        /// </summary>
        /// <param name="str">The new <see cref="ConsoleString"/> to overwrite the written <see cref="ConsoleString"/> with.</param>
        public void WriteDiff(ConsoleString str)
        {
            using (DiffWriter diffWriter = new DiffWriter())
            {
                // If the new string is longer than the one written, add space characters
                // to the end of the previously written string.
                for (int i = WrittenString.Count; i < str.Length; i++)
                    WrittenString.Add(new ConsoleDiffCharacter(new Point(Point.X + i, Point.Y), new ConsoleCharacter(' ')));

                // Write the diff between all the characters of the two strings.
                for (int i = 0; i < str.Length; i++)
                    diffWriter.WriteDiff(WrittenString[i], str[i]);
            }

            // If the new string is shorter, overwrite the old extra characters with spaces
            // and remove them from the list of written characters.
            if (WrittenString.Count > str.Length)
            {
                new ConsoleString(new string(' ', WrittenString.Count - str.Length))
                    .WriteAtPoint(new Point(WrittenString[0].Point.X + str.Length, WrittenString[0].Point.Y));

                for (int i = WrittenString.Count - 1; i >= str.Length; i--)
                    WrittenString.RemoveAt(str.Length);
            }
        }

        /// <summary>
        /// Clears the console area where the string was written.
        /// </summary>
        public void Clear()
        {
            WriteDiff(new ConsoleString());
        }

        /// <summary>
        /// Gets the written <see cref="ConsoleString"/>.
        /// </summary>
        /// <returns>The written <see cref="ConsoleString"/>.</returns>
        public ConsoleString GetWrittenString()
        {
            return new ConsoleString(WrittenString.Select(c => c.WrittenCharacter));
        }

        /// <summary>
        /// Returns an enumerator the iterates through the collection of 
        /// <see cref="ConsoleDiffCharacter"/>s of the current <see cref="ConsoleDiffString"/>.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="ConsoleDiffString"/>.</returns>
        public IEnumerator<ConsoleDiffCharacter> GetEnumerator()
        {
            return WrittenString.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)WrittenString).GetEnumerator();
        }

        /// <inheritdoc/>
        public override string? ToString()
        {
            return string.Concat(WrittenString);
        }
    }
}