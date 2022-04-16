using System.Drawing;
using YonatanMankovich.SimpleColorConsole;

namespace YonatanMankovich.ConsoleDiffWriter
{
    /// <summary>
    /// Represents a structure that keeps track of a <see cref="ColorCharacter"/> 
    /// written to the console at a specific point and can write just the difference
    /// between it and a new <see cref="ColorCharacter"/>.
    /// </summary>
    public class ConsoleDiffCharacter
    {
        /// <summary>
        /// The point on the console to which to write the <see cref="ColorCharacter"/> to.
        /// </summary>
        public Point Point { get; }

        /// <summary>
        /// Gets the written <see cref="ColorCharacter"/>.
        /// </summary>
        public ColorCharacter WrittenCharacter { get; private set; }

        private bool AlreadyWritten { get; set; } = false;

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleDiffCharacter"/> with 
        /// a <see cref="ColorCharacter"/> and a <see cref="System.Drawing.Point"/>
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ColorCharacter"/> to.</param>
        /// <param name="character">The <see cref="ColorCharacter"/> to keep track of.</param>
        public ConsoleDiffCharacter(Point point, ColorCharacter character)
        {
            WrittenCharacter = character;
            Point = point;
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ConsoleDiffCharacter"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public ConsoleDiffCharacter() : this(new Point(Console.CursorLeft, Console.CursorTop)) { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ConsoleDiffCharacter"/> with 
        /// a <see cref="System.Drawing.Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ColorCharacter"/> to.</param>
        public ConsoleDiffCharacter(Point point)
        {
            WrittenCharacter = new ColorCharacter();
            Point = point;
        }

        /// <summary>
        /// Writes only the difference between the <see cref="WrittenCharacter"/>
        /// and the given <see cref="ColorCharacter"/> to the console.
        /// </summary>
        /// <param name="newChar">The new <see cref="ColorCharacter"/> to overwrite the <see cref="WrittenCharacter"/> with.</param>
        public void WriteDiff(ColorCharacter newChar)
        {
            // Write only if the character changed.
            if (IsCharDifferentFromWrittenChar(newChar))
            {
                newChar.WriteAtPoint(Point);
                UpdateWrittenCharacter(newChar);
            }
        }

        /// <summary>
        /// Updates the <see cref="WrittenCharacter"/> with the given <see cref="ColorCharacter"/>.
        /// </summary>
        /// <param name="newChar">The <see cref="ColorCharacter"/>.</param>
        internal void UpdateWrittenCharacter(ColorCharacter newChar)
        {
            WrittenCharacter = newChar;
            AlreadyWritten = true;
        }

        /// <summary>
        /// Clears the console area where the character was written.
        /// </summary>
        public void Clear()
        {
            WriteDiff(new ColorCharacter());
        }

        /// <summary>
        /// Compares the given <paramref name="character"/> to the <see cref="WrittenCharacter"/>.
        /// </summary>
        /// <param name="character">The new character to compare the <see cref="WrittenCharacter"/> with.</param>
        /// <returns><see langword="true"/> if they are different; otherwise, <see langword="false"/>.</returns>
        public bool IsCharDifferentFromWrittenChar(ColorCharacter character)
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