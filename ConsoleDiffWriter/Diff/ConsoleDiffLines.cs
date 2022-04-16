﻿using System.Collections;
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
        /// <param name="point">The point on the console to which to write the <see cref="ConsoleLines"/> to.</param>
        /// <param name="lines">The <see cref="ConsoleLines"/> to keep track of.</param>
        public ConsoleDiffLines(Point point, ConsoleLines lines) : this(point)
        {
            for (int i = 0; i < lines.Count; i++)
                WrittenLines.Add(new ConsoleDiffString(new Point(point.X, point.Y + i), lines[i]));
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ConsoleDiffLines"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public ConsoleDiffLines() : this(new Point(Console.CursorLeft, Console.CursorTop)) { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ConsoleDiffLines"/> with
        /// a <see cref="System.Drawing.Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ConsoleLines"/> to.</param>
        public ConsoleDiffLines(Point point)
        {
            WrittenLines = new List<ConsoleDiffString>();
            Point = point;
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
                WrittenLines.Add(new ConsoleDiffString(new Point(Point.X, Point.Y + i), new ConsoleString()));

            // Write the diff between all the lines of the two areas.
            for (int i = 0; i < lines.Count; i++)
                WrittenLines[i].WriteDiff(lines[i]);

            // If the new area has less lines than the written one,
            // overwrite the old extra lines with spaces and remove them from the list of written lines.
            for (int i = lines.Count; i < WrittenLines.Count; i++)
                new ConsoleString(new string(' ', WrittenLines[i].Length)).WriteAtPoint(new Point(Point.X, Point.Y + i));
            for (int i = lines.Count; i < WrittenLines.Count; i++)
                WrittenLines.RemoveAt(lines.Count); // Remove last element.

            BringCursorToEnd();
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
        /// Brings the cursor to the beginning of the line after the drawn lines.
        /// </summary>
        public void BringCursorToEnd()
        {
            int newTop = Point.Y + WrittenLines.Count;
            if (OperatingSystem.IsWindows() && newTop >= Console.BufferHeight)
                Console.BufferHeight = newTop + 1;

            Console.SetCursorPosition(0, newTop);
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