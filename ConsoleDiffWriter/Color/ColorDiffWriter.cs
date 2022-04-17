using System.Drawing;
using YonatanMankovich.SimpleColorConsole;

namespace YonatanMankovich.ConsoleDiffWriter.Color
{
    /// <summary>
    /// Represents a structure that helps write <see cref="ColorCharDiff"/> more efficiently.
    /// </summary>
    internal class ColorDiffWriter : ContinuousColorConsoleWriter, IDisposable
    {
        private Point? LastPoint { get; set; }

        /// <summary>
        /// Initializes an instance of the <see cref="ColorDiffWriter"/> class 
        /// and saves the relevant <see cref="Console"/> properties.
        /// </summary>
        public ColorDiffWriter() : base() { }

        /// <summary>
        /// Writes the difference between the given <see cref="ColorCharDiff"/> and the 
        /// <see cref="ColorChar"/> without moving the cursor position or changing the console 
        /// colors unless needed based on the previously written <see cref="ColorChar"/>.
        /// </summary>
        /// <param name="diffChar">The <see cref="ColorCharDiff"/>.</param>
        /// <param name="newChar">The new <see cref="ColorChar"/>.</param>
        public void WriteDiff(ColorCharDiff diffChar, ColorChar newChar)
        {
            if (diffChar.IsCharDifferentFromWrittenChar(newChar))
            {
                if (!LastPoint.HasValue || LastPoint.Value.X + 1 != diffChar.Point.X || LastPoint.Value.Y != diffChar.Point.Y)
                {
                    if (OperatingSystem.IsWindows() && diffChar.Point.Y == Console.BufferHeight)
                        Console.BufferHeight++;

                    Console.SetCursorPosition(diffChar.Point.X, diffChar.Point.Y);
                }

                Write(newChar);
                diffChar.UpdateWrittenCharacter(newChar);
                LastPoint = diffChar.Point;
            }
        }
    }
}