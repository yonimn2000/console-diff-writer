using System.Collections;
using System.Drawing;

namespace YonatanMankovich.ConsoleDiffWriter.Data
{
    public class ConsoleString : IEnumerable<ConsoleCharacter>
    {
        private IList<ConsoleCharacter> Characters { get; set; }
        public int Length => Characters.Count;

        public ConsoleString()
        {
            Characters = new List<ConsoleCharacter>();
        }

        public ConsoleString(string str, ConsoleColor? textColor = null, ConsoleColor? backColor = null)
        {
            Characters = str.Select(c => new ConsoleCharacter(c, textColor, backColor)).ToList();
        }

        public ConsoleString(ConsoleString str) : this(str.Characters) { }

        public ConsoleString(IEnumerable<ConsoleCharacter> characters)
        {
            Characters = new List<ConsoleCharacter>(characters);
        }

        public ConsoleCharacter this[int index] { get => Characters[index]; set => Characters[index] = value; }

        public ConsoleString AddCharacter(ConsoleCharacter character)
        {
            Characters.Add(character);
            return this;
        }

        public ConsoleString AddString(ConsoleString str)
        {
            foreach (ConsoleCharacter character in str.Characters)
                Characters.Add(character);
            return this;
        }

        public void Write()
        {
            foreach (ConsoleCharacter character in Characters)
                character.Write();
        }

        public void WriteLine()
        {
            Write();
            Console.WriteLine();
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

        public static ConsoleString operator +(ConsoleString str1, ConsoleString str2)
        {
            return new ConsoleString(str1.Characters).AddString(str2);
        }

        public static ConsoleString operator +(ConsoleString str, ConsoleCharacter character)
        {
            return new ConsoleString(str.Characters).AddCharacter(character);
        }

        public override string? ToString()
        {
            return new string(Characters.Select(c => c.Character).ToArray());
        }

        public IEnumerator<ConsoleCharacter> GetEnumerator()
        {
            return Characters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Characters).GetEnumerator();
        }
    }
}