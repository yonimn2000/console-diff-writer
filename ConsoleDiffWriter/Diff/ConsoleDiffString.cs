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
        /// a <see cref="ConsoleString"/> and a <see cref="System.Drawing.Point"/>
        /// </summary>
        /// <param name="str">The <see cref="ConsoleString"/> to keep track of.</param>
        /// <param name="point">The point on the console to which to write the <see cref="ConsoleString"/> to.</param>
        public ConsoleDiffString(ConsoleString str, Point point)
        {
            WrittenString = new List<ConsoleDiffCharacter>(str.Length);
            Point = point;

            for (int i = 0; i < str.Length; i++)
                WrittenString.Add(new ConsoleDiffCharacter(str[i], new Point(point.X + i, point.Y)));
        }

        /// <summary>
        /// Writes the <see cref="ConsoleString"/> to the console.
        /// </summary>
        public void Write()
        {
            foreach (ConsoleDiffCharacter character in WrittenString)
                character.Write();
        }

        /// <summary>
        /// Writes only the difference between the written <see cref="ConsoleString"/>
        /// and the given <see cref="ConsoleString"/> to the console.
        /// </summary>
        /// <param name="str">The new <see cref="ConsoleString"/> to overwrite the written <see cref="ConsoleString"/> with.</param>
        public void WriteDiff(ConsoleString str)
        {
            // If the new string is longer than the one written, add space characters
            // to the end of the previously written string.
            for (int i = WrittenString.Count; i < str.Length; i++)
                WrittenString.Add(new ConsoleDiffCharacter(new ConsoleCharacter(' '), new Point(Point.X + i, Point.Y)));

            // Write the diff between all the characters of the two strings.
            for (int i = 0; i < str.Length; i++)
                WrittenString[i].WriteDiff(str[i]);

            // If the new string is shorter, overwrite the old extra characters with spaces
            // and remove them from the list of written characters.
            int originalDrawnStringLength = WrittenString.Count;
            for (int i = str.Length; i < originalDrawnStringLength; i++)
            {
                new ConsoleCharacter(' ').WriteAtPoint(new Point(WrittenString[0].Point.X + i, WrittenString[0].Point.Y));
                WrittenString.RemoveAt(str.Length); // Remove last element.
            }
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