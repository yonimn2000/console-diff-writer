using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Data;

namespace YonatanMankovich.ConsoleDiffWriter.Diff
{
    /// <summary>
    /// Represents a structure that keeps track of a <see cref="ConsoleCharacter"/> 
    /// written to the console at a specific point and can write just the difference
    /// between it and a new <see cref="ConsoleCharacter"/>.
    /// </summary>
    public class ConsoleDiffCharacter
    {
        /// <summary>
        /// The point on the console to which to write the <see cref="ConsoleCharacter"/> to.
        /// </summary>
        public Point Point { get; }

        /// <summary>
        /// Gets the written <see cref="ConsoleCharacter"/>.
        /// </summary>
        public ConsoleCharacter WrittenCharacter { get; private set; }

        private bool AlreadyWritten { get; set; } = false;

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleDiffCharacter"/> with 
        /// a <see cref="ConsoleCharacter"/> and a <see cref="System.Drawing.Point"/>
        /// </summary>
        /// <param name="character">The <see cref="ConsoleCharacter"/> to keep track of.</param>
        /// <param name="point">The point on the console to which to write the <see cref="ConsoleCharacter"/> to.</param>
        public ConsoleDiffCharacter(ConsoleCharacter character, Point point)
        {
            WrittenCharacter = character;
            Point = point;
        }

        /// <summary>
        /// Writes the <see cref="ConsoleCharacter"/> to the console.
        /// </summary>
        public void Write()
        {
            WriteDiff(WrittenCharacter);
        }

        /// <summary>
        /// Writes only the difference between the <see cref="WrittenCharacter"/>
        /// and the given <see cref="ConsoleCharacter"/> to the console.
        /// </summary>
        /// <param name="character">The new <see cref="ConsoleCharacter"/> to overwrite the <see cref="WrittenCharacter"/> with.</param>
        public void WriteDiff(ConsoleCharacter character)
        {
            // Write only if the character changed.
            if (IsDifferentFromCharacter(character))
            {
                character.WriteAtPoint(Point);
                WrittenCharacter = character;
                AlreadyWritten = true;
            }
        }

        /// <summary>
        /// Compares the given <paramref name="character"/> to the <see cref="WrittenCharacter"/>.
        /// </summary>
        /// <param name="character">The new character to compare the <see cref="WrittenCharacter"/> with.</param>
        /// <returns><see langword="true"/> if they are different; otherwise, <see langword="false"/>.</returns>
        public bool IsDifferentFromCharacter(ConsoleCharacter character)
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