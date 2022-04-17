using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Color;

namespace YonatanMankovich.ConsoleDiffWriter.Bases
{
    /// <summary>
    /// Represents a structure that keeps track of a lines written to the console at a 
    /// specific point and can write just the difference between it and a new lines.
    /// </summary>
    public abstract class ColorLinesDiffBase<L, S> : IEnumerable<S> where S : new()
    {
        /// <summary>
        /// The point on the console to which to write the lines to.
        /// </summary>
        public Point Point { get; }

        /// <summary>
        /// The written lines.
        /// </summary>
        protected IList<S> WrittenLines { get; set; }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ColorLinesDiffBase{L, S}"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public ColorLinesDiffBase() : this(new Point(Console.CursorLeft, Console.CursorTop)) { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ColorLinesDiffBase{L, S}"/> with
        /// a <see cref="Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the lines to.</param>
        public ColorLinesDiffBase(Point point)
        {
            WrittenLines = new List<S>();
            Point = point;
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
        /// Clears the console area where the lines were written.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Writes only the difference between the written lines and the given lines to the console.
        /// </summary>
        /// <param name="lines">The new lines to overwrite the written lines with.</param>
        public abstract void WriteDiff(L lines);

        /// <summary>
        /// Gets the written lines.
        /// </summary>
        /// <returns>The written lines.</returns>
        public abstract L GetWrittenLines();

        /// <summary>
        /// Returns an enumerator the iterates through the collection of 
        /// <see cref="ColorStringDiff"/> lines of the current <see cref="ColorLinesDiff"/>.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="ColorLinesDiff"/>.</returns>
        public IEnumerator<S> GetEnumerator() => WrittenLines.GetEnumerator();

        /// <inheritdoc />
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => ((System.Collections.IEnumerable)WrittenLines).GetEnumerator();

        /// <inheritdoc/>
        public override string? ToString() => string.Join(Environment.NewLine, WrittenLines);
    }
}