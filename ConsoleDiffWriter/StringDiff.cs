using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Bases;

namespace YonatanMankovich.ConsoleDiffWriter
{

    /// <inheritdoc />
    public class StringDiff : StringDiffBase<string, CharDiff>
    {
        /// <summary>
        /// Initializes an instance of the <see cref="StringDiff"/> with 
        /// a <see cref="string"/> and a <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="string"/> to.</param>
        /// <param name="str">The <see cref="string"/> to keep track of.</param>
        public StringDiff(Point point, string str) : base(point)
        {
            for (int i = 0; i < str.Length; i++)
                WrittenString.Add(new CharDiff(new Point(point.X + i, point.Y), str[i]));
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="StringDiff"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public StringDiff() : base() { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="StringDiff"/> with 
        /// a <see cref="Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the string to.</param>
        public StringDiff(Point point) : base(point) { }

        /// <inheritdoc/>
        public override void Clear() => WriteDiff("");

        /// <inheritdoc/>
        public override string GetWrittenString() => string.Concat(WrittenString.Select(c => c.WrittenCharacter));

        /// <inheritdoc/>
        public override void WriteDiff(string str)
        {
            // If the new string is longer than the one written, add space characters
            // to the end of the previously written string.
            for (int i = WrittenString.Count; i < str.Length; i++)
                WrittenString.Add(new CharDiff(new Point(Point.X + i, Point.Y), ' '));

            // Write the diff between all the characters of the two strings.
            for (int i = 0; i < str.Length; i++)
                WrittenString[i].WriteDiff(str[i]);

            // If the new string is shorter, overwrite the old extra characters with spaces
            // and remove them from the list of written characters.
            if (WrittenString.Count > str.Length)
            {
                WriteStringAtPoint(new string(' ', WrittenString.Count - str.Length),
                    new Point(WrittenString[0].Point.X + str.Length, WrittenString[0].Point.Y));

                for (int i = WrittenString.Count - 1; i >= str.Length; i--)
                    WrittenString.RemoveAt(str.Length);
            }
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
    }
}