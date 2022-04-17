using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Bases;

namespace YonatanMankovich.ConsoleDiffWriter
{
    /// <inheritdoc />
    public class CharDiff : CharDiffBase<char>
    {
        /// <summary>
        /// Initializes an instance of the <see cref="CharDiff"/> with 
        /// a <see cref="char"/> and a <see cref="Point"/>
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="char"/> to.</param>
        /// <param name="character">The <see cref="char"/> to keep track of.</param>
        public CharDiff(Point point, char character) : base(point, character) { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="CharDiff"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public CharDiff() : base() { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="CharDiff"/> with 
        /// a <see cref="Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="char"/> to.</param>
        public CharDiff(Point point) : base(point) { }

        /// <inheritdoc/>
        protected override void WriteCharAtPoint(char character, Point point)
        {
            // Save current cursor coordinates.
            Point prevPoint = new Point(Console.CursorLeft, Console.CursorTop);

            // Write at the given position.
            Console.SetCursorPosition(point.X, point.Y);
            Console.Write(character);

            // Restore saved cursor coordinates.
            Console.SetCursorPosition(prevPoint.X, prevPoint.Y);
        }
    }
}