using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Bases;
using YonatanMankovich.SimpleColorConsole;

namespace YonatanMankovich.ConsoleDiffWriter.Color
{
    /// <inheritdoc/>
    public class ColorLinesDiff : ColorLinesDiffBase<ColorLines, ColorStringDiff>
    {

        /// <summary>
        /// Initializes an instance of the <see cref="ColorLinesDiff"/> with 
        /// a <see cref="ColorLines"/> and a <see cref="Point"/>
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ColorLines"/> to.</param>
        /// <param name="lines">The <see cref="ColorLines"/> to keep track of.</param>
        public ColorLinesDiff(Point point, ColorLines lines) : base(point)
        {
            for (int i = 0; i < lines.Count; i++)
                WrittenLines.Add(new ColorStringDiff(new Point(point.X, point.Y + i), lines[i]));
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ColorLinesDiff"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public ColorLinesDiff() : base() { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ColorLinesDiff"/> with
        /// a <see cref="Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ColorLines"/> to.</param>
        public ColorLinesDiff(Point point) : base(point) { }

        /// <summary>
        /// Clears the console area where the lines were written.
        /// </summary>
        public override void Clear()
        {
            WriteDiff(new ColorLines());
        }

        /// <summary>
        /// Writes only the difference between the written <see cref="ColorLines"/>
        /// and the given <see cref="ColorLines"/> to the console.
        /// </summary>
        /// <param name="lines">The new <see cref="ColorLines"/> to overwrite the written <see cref="ColorLines"/> with.</param>
        public override void WriteDiff(ColorLines lines)
        {
            // If the new area has more lines than the one written, add blank lines to the end of the previously written area.
            for (int i = WrittenLines.Count; i < lines.Count; i++)
                WrittenLines.Add(new ColorStringDiff(new Point(Point.X, Point.Y + i), new ColorString()));

            // Write the diff between all the lines of the two areas.
            for (int i = 0; i < lines.Count; i++)
                WrittenLines[i].WriteDiff(lines[i]);

            // If the new area has less lines than the written one,
            // overwrite the old extra lines with spaces and remove them from the list of written lines.
            for (int i = lines.Count; i < WrittenLines.Count; i++)
                new ColorString(new string(' ', WrittenLines[i].Length)).WriteAtPoint(new Point(Point.X, Point.Y + i));
            for (int i = lines.Count; i < WrittenLines.Count; i++)
                WrittenLines.RemoveAt(lines.Count); // Remove last element.

            BringCursorToEnd();
        }

        /// <inheritdoc/>
        public override ColorLines GetWrittenLines()
        {
            return new ColorLines(WrittenLines.Select(c => c.GetWrittenString()));
        }
    }
}