using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Bases;

namespace YonatanMankovich.ConsoleDiffWriter
{
    /// <inheritdoc/>
    public class LinesDiff : ColorLinesDiffBase<IList<string>, StringDiff>
    {
        /// <summary>
        /// Initializes an instance of the <see cref="LinesDiff"/> with 
        /// a list of lines and a <see cref="Point"/>
        /// </summary>
        /// <param name="point">The point on the console to which to write the list of lines to.</param>
        /// <param name="lines">The list of lines to keep track of.</param>
        public LinesDiff(Point point, IList<string> lines) : base(point)
        {
            for (int i = 0; i < lines.Count; i++)
                WrittenLines.Add(new StringDiff(new Point(point.X, point.Y + i), lines[i]));
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="LinesDiff"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public LinesDiff() : base() { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="LinesDiff"/> with
        /// a <see cref="Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the list of lines to.</param>
        public LinesDiff(Point point) : base(point) { }

        /// <inheritdoc/>
        public override void Clear() => WriteDiff(new List<string>());

        /// <inheritdoc/>
        public override void WriteDiff(IList<string> lines)
        {
            // If the new area has more lines than the one written, add blank lines to the end of the previously written area.
            for (int i = WrittenLines.Count; i < lines.Count; i++)
                WrittenLines.Add(new StringDiff(new Point(Point.X, Point.Y + i), ""));

            // Write the diff between all the lines of the two areas.
            for (int i = 0; i < lines.Count; i++)
                WrittenLines[i].WriteDiff(lines[i]);

            // If the new area has less lines than the written one,
            // overwrite the old extra lines with spaces and remove them from the list of written lines.
            for (int i = lines.Count; i < WrittenLines.Count; i++)
                WriteStringAtPoint(new string(' ', WrittenLines[i].Length), new Point(Point.X, Point.Y + i));
            for (int i = lines.Count; i < WrittenLines.Count; i++)
                WrittenLines.RemoveAt(lines.Count); // Remove last element.

            BringCursorToEnd();
        }

        private void WriteStringAtPoint(string str, Point point)
        {
            // Save current cursor coordinates.
            Point prevPoint = new Point(Console.CursorLeft, Console.CursorTop);

            // Write at the given position.
            Console.SetCursorPosition(point.X, point.Y);
            Console.Write(str);

            // Restore saved cursor coordinates.
            Console.SetCursorPosition(prevPoint.X, prevPoint.Y);
        }

        /// <inheritdoc/>
        public override IList<string> GetWrittenLines() => new List<string>(WrittenLines.Select(c => c.GetWrittenString()));
    }
}