using System.Drawing;

namespace YonatanMankovich.ConsoleDiffWriter.Data
{
    public class ConsoleCharacter : IEquatable<ConsoleCharacter?>
    {
        public char Character { get; set; }
        public ConsoleColor? TextColor { get; set; }
        public ConsoleColor? BackColor { get; set; }

        public ConsoleCharacter(char character, ConsoleColor? textColor = null, ConsoleColor? backColor = null)
        {
            Character = character;
            TextColor = textColor;
            BackColor = backColor;
        }

        public void Write()
        {
            // Save previous console colors.
            ConsoleColor prevBgColor = Console.BackgroundColor;
            ConsoleColor prevFgColor = Console.ForegroundColor;

            // Write.
            if (BackColor != null)
                Console.BackgroundColor = (ConsoleColor)BackColor;

            if (TextColor != null)
                Console.ForegroundColor = (ConsoleColor)TextColor;

            Console.Write(Character);

            // Restore previous console colors.
            Console.BackgroundColor = prevBgColor;
            Console.ForegroundColor = prevFgColor;
        }

        public void WriteAtPoint(Point point)
        {
            // Save previous cursor coordinates.
            int prevCursorTop = Console.CursorTop;
            int prevCursorLeft = Console.CursorLeft;

            // Write.
            Console.SetCursorPosition(point.X, point.Y);
            Write();

            // Restore previous cursor coordinates.
            Console.SetCursorPosition(prevCursorLeft, prevCursorTop);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as ConsoleCharacter);
        }

        public bool Equals(ConsoleCharacter? other)
        {
            return other != null &&
                   Character == other.Character &&
                   TextColor == other.TextColor &&
                   BackColor == other.BackColor;
        }

        public override string? ToString()
        {
            return Character.ToString();
        }
    }
}