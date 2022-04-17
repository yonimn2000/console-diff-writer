using System.Collections;
using System.Drawing;
using YonatanMankovich.SimpleColorConsole;

namespace YonatanMankovich.ConsoleDiffWriter
{
    /// <summary>
    /// Represents a structure that keeps track of a <see cref="ColorString"/> 
    /// written to the console at a specific point and can write just the difference
    /// between it and a new <see cref="ColorString"/>.
    /// </summary>
    public class ColorStringDiff : IEnumerable<ColorCharDiff>
    {
        /// <summary>
        /// The point on the console to which to write the <see cref="ColorString"/> to.
        /// </summary>
        public Point Point { get; }

        /// <summary>
        /// The length of the written <see cref="ColorString"/>.
        /// </summary>
        public int Length => WrittenString.Count;
        private IList<ColorCharDiff> WrittenString { get; set; }

        /// <summary>
        /// Initializes an instance of the <see cref="ColorStringDiff"/> with 
        /// a <see cref="ColorString"/> and a <see cref="System.Drawing.Point"/>.
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ColorString"/> to.</param>
        /// <param name="str">The <see cref="ColorString"/> to keep track of.</param>
        public ColorStringDiff(Point point, ColorString str) : this(point)
        {
            for (int i = 0; i < str.Length; i++)
                WrittenString.Add(new ColorCharDiff(new Point(point.X + i, point.Y), str[i]));
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ColorStringDiff"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public ColorStringDiff() : this(new Point(Console.CursorLeft, Console.CursorTop)) { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ColorStringDiff"/> with 
        /// a <see cref="System.Drawing.Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ColorString"/> to.</param>
        public ColorStringDiff(Point point)
        {
            WrittenString = new List<ColorCharDiff>();
            Point = point;
        }

        /// <summary>
        /// Writes only the difference between the written <see cref="ColorString"/>
        /// and the given <see cref="ColorString"/> to the console.
        /// </summary>
        /// <param name="str">The new <see cref="ColorString"/> to overwrite the written <see cref="ColorString"/> with.</param>
        public void WriteDiff(ColorString str)
        {
            using (ColorDiffWriter diffWriter = new ColorDiffWriter())
            {
                // If the new string is longer than the one written, add space characters
                // to the end of the previously written string.
                for (int i = WrittenString.Count; i < str.Length; i++)
                    WrittenString.Add(new ColorCharDiff(new Point(Point.X + i, Point.Y), new ColorChar(' ')));

                // Write the diff between all the characters of the two strings.
                for (int i = 0; i < str.Length; i++)
                    diffWriter.WriteDiff(WrittenString[i], str[i]);
            }

            // If the new string is shorter, overwrite the old extra characters with spaces
            // and remove them from the list of written characters.
            if (WrittenString.Count > str.Length)
            {
                new ColorString(new string(' ', WrittenString.Count - str.Length))
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
            WriteDiff(new ColorString());
        }

        /// <summary>
        /// Gets the written <see cref="ColorString"/>.
        /// </summary>
        /// <returns>The written <see cref="ColorString"/>.</returns>
        public ColorString GetWrittenString()
        {
            return new ColorString(WrittenString.Select(c => c.WrittenCharacter));
        }

        /// <summary>
        /// Returns an enumerator the iterates through the collection of 
        /// <see cref="ColorCharDiff"/>s of the current <see cref="ColorStringDiff"/>.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="ColorStringDiff"/>.</returns>
        public IEnumerator<ColorCharDiff> GetEnumerator()
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