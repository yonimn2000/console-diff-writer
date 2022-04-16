using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Data;
using YonatanMankovich.ConsoleDiffWriter.Diff;

namespace YonatanMankovich.ConsoleDiffWriter
{
    /// <summary>
    /// Represents a structure that helps write <see cref="ConsoleDiffCharacter"/> more efficiently.
    /// </summary>
    internal class DiffWriter : Writer, IDisposable
    {
        private Point? LastPoint { get; set; }

        /// <summary>
        /// Initializes an instance of the <see cref="DiffWriter"/> class 
        /// and saves the relevant <see cref="Console"/> properties.
        /// </summary>
        public DiffWriter() : base() { }

        /// <summary>
        /// Writes the difference between the given <see cref="ConsoleDiffCharacter"/> and the 
        /// <see cref="ConsoleCharacter"/> without moving the cursor position or changing the console 
        /// colors unless needed based on the previously written <see cref="ConsoleCharacter"/>.
        /// </summary>
        /// <param name="diffChar"></param>
        /// <param name="newChar"></param>
        public void WriteDiff(ConsoleDiffCharacter diffChar, ConsoleCharacter newChar)
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