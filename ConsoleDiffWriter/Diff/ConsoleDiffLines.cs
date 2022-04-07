using System.Collections;
using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Data;

namespace YonatanMankovich.ConsoleDiffWriter.Diff
{
    /// <summary>
    /// Represents a structure that keeps track of a <see cref="ConsoleDiffLines"/> 
    /// written to the console at a specific point and can write just the difference
    /// between it and a new <see cref="ConsoleLines"/>.
    /// </summary>
    public class ConsoleDiffLines : IEnumerable<ConsoleDiffString>
    {
        /// <summary>
        /// The point on the console to which to write the <see cref="ConsoleLines"/> to.
        /// </summary>
        public Point Point { get; }
        private IList<ConsoleDiffString> WrittenLines { get; set; }

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleDiffLines"/> with 
        /// a <see cref="ConsoleLines"/> and a <see cref="System.Drawing.Point"/>
        /// </summary>
        /// <param name="lines">The <see cref="ConsoleLines"/> to keep track of.</param>
        /// <param name="point">The point on the console to which to write the <see cref="ConsoleLines"/> to.</param>
        public ConsoleDiffLines(ConsoleLines lines, Point point)
        {
            WrittenLines = new List<ConsoleDiffString>(lines.Count);
            Point = point;

            for (int i = 0; i < lines.Count; i++)
                WrittenLines.Add(new ConsoleDiffString(lines[i], new Point(point.X, point.Y + i)));
        }

        /// <summary>
        /// Writes the <see cref="ConsoleLines"/> to the console.
        /// </summary>
        public void Write()
        {
            foreach (ConsoleDiffString line in WrittenLines)
                line.Write();
        }

        /// <summary>
        /// Writes only the difference between the written <see cref="ConsoleLines"/>
        /// and the given <see cref="ConsoleLines"/> to the console.
        /// </summary>
        /// <param name="lines">The new <see cref="ConsoleLines"/> to overwrite the written <see cref="ConsoleLines"/> with.</param>
        public void WriteDiff(ConsoleLines lines)
        {
            // If the new area has more lines than the one written, add blank lines to the end of the previously written area.
            for (int i = WrittenLines.Count; i < lines.Count; i++)
                WrittenLines.Add(new ConsoleDiffString(new ConsoleString(), new Point(Point.X, Point.Y + i)));

            // Write the diff between all the lines of the two areas.
            for (int i = 0; i < lines.Count; i++)
                WrittenLines[i].WriteDiff(lines[i]);

            // If the new area has less lines than the written one,
            // overwrite the old extra lines with spaces and remove them from the list of written lines.
            for (int i = lines.Count; i < WrittenLines.Count; i++)
                new ConsoleString(new string(' ', WrittenLines[i].Length)).WriteAtPoint(new Point(Point.X, Point.Y + i));
            for (int i = lines.Count; i < WrittenLines.Count; i++)
                WrittenLines.RemoveAt(lines.Count); // Remove last element.
        }

        /// <summary>
        /// Clears the console area where the lines were written.
        /// </summary>
        public void Clear()
        {
            WriteDiff(new ConsoleLines());
        }

        /// <summary>
        /// Gets the written <see cref="ConsoleLines"/>.
        /// </summary>
        /// <returns>The written <see cref="ConsoleLines"/>.</returns>
        public ConsoleLines GetWrittenLines()
        {
            return new ConsoleLines(WrittenLines.Select(c => c.GetWrittenString()));
        }

        /// <summary>
        /// Returns an enumerator the iterates through the collection of 
        /// <see cref="ConsoleDiffString"/> lines of the current <see cref="ConsoleDiffLines"/>.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="ConsoleDiffLines"/>.</returns>
        public IEnumerator<ConsoleDiffString> GetEnumerator()
        {
            return WrittenLines.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)WrittenLines).GetEnumerator();
        }

        /// <inheritdoc/>
        public override string? ToString()
        {
            return string.Join(Environment.NewLine, WrittenLines);
        }
    }
}