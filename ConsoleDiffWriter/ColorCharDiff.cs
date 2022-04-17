using System.Drawing;
using YonatanMankovich.SimpleColorConsole;

namespace YonatanMankovich.ConsoleDiffWriter
{
    /// <summary>
    /// Represents a structure that keeps track of a <see cref="ColorChar"/> 
    /// written to the console at a specific point and can write just the difference
    /// between it and a new <see cref="ColorChar"/>.
    /// </summary>
    public class ColorCharDiff
    {
        /// <summary>
        /// The point on the console to which to write the <see cref="ColorChar"/> to.
        /// </summary>
        public Point Point { get; }

        /// <summary>
        /// Gets the written <see cref="ColorChar"/>.
        /// </summary>
        public ColorChar WrittenCharacter { get; private set; }

        private bool AlreadyWritten { get; set; } = false;

        /// <summary>
        /// Initializes an instance of the <see cref="ColorCharDiff"/> with 
        /// a <see cref="ColorChar"/> and a <see cref="System.Drawing.Point"/>
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ColorChar"/> to.</param>
        /// <param name="character">The <see cref="ColorChar"/> to keep track of.</param>
        public ColorCharDiff(Point point, ColorChar character)
        {
            WrittenCharacter = character;
            Point = point;
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ColorCharDiff"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public ColorCharDiff() : this(new Point(Console.CursorLeft, Console.CursorTop)) { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ColorCharDiff"/> with 
        /// a <see cref="System.Drawing.Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ColorChar"/> to.</param>
        public ColorCharDiff(Point point)
        {
            WrittenCharacter = new ColorChar();
            Point = point;
        }

        /// <summary>
        /// Writes only the difference between the <see cref="WrittenCharacter"/>
        /// and the given <see cref="ColorChar"/> to the console.
        /// </summary>
        /// <param name="newChar">The new <see cref="ColorChar"/> to overwrite the <see cref="WrittenCharacter"/> with.</param>
        public void WriteDiff(ColorChar newChar)
        {
            // Write only if the character changed.
            if (IsCharDifferentFromWrittenChar(newChar))
            {
                newChar.WriteAtPoint(Point);
                UpdateWrittenCharacter(newChar);
            }
        }

        /// <summary>
        /// Updates the <see cref="WrittenCharacter"/> with the given <see cref="ColorChar"/>.
        /// </summary>
        /// <param name="newChar">The <see cref="ColorChar"/>.</param>
        internal void UpdateWrittenCharacter(ColorChar newChar)
        {
            WrittenCharacter = newChar;
            AlreadyWritten = true;
        }

        /// <summary>
        /// Clears the console area where the character was written.
        /// </summary>
        public void Clear()
        {
            WriteDiff(new ColorChar());
        }

        /// <summary>
        /// Compares the given <paramref name="character"/> to the <see cref="WrittenCharacter"/>.
        /// </summary>
        /// <param name="character">The new character to compare the <see cref="WrittenCharacter"/> with.</param>
        /// <returns><see langword="true"/> if they are different; otherwise, <see langword="false"/>.</returns>
        public bool IsCharDifferentFromWrittenChar(ColorChar character)
        {
            return !AlreadyWritten || !character.Equals(WrittenCharacter);
        }

        /// <inheritdoc/>
        public override string? ToString()
        {
            return WrittenCharacter.ToString();
        }
    }
}