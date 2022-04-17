using System.Drawing;

namespace YonatanMankovich.ConsoleDiffWriter.Bases
{
    /// <summary>
    /// Represents a structure that keeps track of a character
    /// written to the console at a specific point and can write 
    /// just the difference between it and a new character.
    /// </summary>
    public abstract class CharDiffBase<C> where C : new()
    {
        /// <summary>
        /// The point on the console to which to write the character to.
        /// </summary>
        public Point Point { get; }

        /// <summary>
        /// Gets the written character.
        /// </summary>
        public C WrittenCharacter { get; protected set; }

        private bool AlreadyWritten { get; set; } = false;

        /// <summary>
        /// Initializes an instance of the <see cref="CharDiffBase{C}"/> with 
        /// a character and a <see cref="Point"/>
        /// </summary>
        /// <param name="point">The point on the console to which to write the character to.</param>
        /// <param name="character">The character to keep track of.</param>
        protected CharDiffBase(Point point, C character)
        {
            Point = point;

            if (character == null)
                throw new ArgumentNullException(nameof(character));

            WrittenCharacter = character;
        }

        /// <summary>
        /// Initializes an empty instance of the <see cref="CharDiffBase{C}"/> with 
        /// a <see cref="System.Drawing.Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the character to.</param>
        protected CharDiffBase(Point point) : this(point, new C()) { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="CharDiffBase{C}"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        protected CharDiffBase() : this(new Point(Console.CursorLeft, Console.CursorTop)) { }

        /// <summary>
        /// Clears the console area where the character was written.
        /// </summary>
        public void Clear() => WriteDiff(new C());

        /// <summary>
        /// Compares the given <paramref name="character"/> to the <see cref="WrittenCharacter"/>.
        /// </summary>
        /// <param name="character">The new character to compare the <see cref="WrittenCharacter"/> with.</param>
        /// <returns><see langword="true"/> if they are different; otherwise, <see langword="false"/>.</returns>
        public bool IsCharDifferentFromWrittenChar(C character) => !AlreadyWritten || !WrittenCharacter!.Equals(character);

        /// <inheritdoc/>
        public override string? ToString() => WrittenCharacter!.ToString();

        /// <summary>
        /// Writes only the difference between the <see cref="WrittenCharacter"/>
        /// and the given character to the console.
        /// </summary>
        /// <param name="newChar">The new character to overwrite the <see cref="WrittenCharacter"/> with.</param>
        public void WriteDiff(C newChar)
        {
            // Write only if the character changed.
            if (IsCharDifferentFromWrittenChar(newChar))
            {
                WriteCharAtPoint(newChar, Point);
                UpdateWrittenCharacter(newChar);
            }
        }

        /// <summary>
        /// Updates the <see cref="WrittenCharacter"/> with the given character.
        /// </summary>
        /// <param name="newChar">The character.</param>
        internal void UpdateWrittenCharacter(C newChar)
        {
            WrittenCharacter = newChar;
            AlreadyWritten = true;
        }

        /// <summary>
        /// Writes the character at a point.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="point">The point.</param>
        protected abstract void WriteCharAtPoint(C character, Point point);
    }
}