using System.Collections;
using System.Drawing;

namespace YonatanMankovich.ConsoleDiffWriter.Data
{
    /// <summary>
    /// Represents a colored console string of characters.
    /// </summary>
    public class ConsoleString : IEnumerable<ConsoleCharacter>
    {
        private IList<ConsoleCharacter> Characters { get; set; }

        /// <summary>
        /// Gets the number of characters in the current <see cref="ConsoleString"/> object.
        /// </summary>
        public int Length => Characters.Count;

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleString"/> class with an empty string.
        /// </summary>
        public ConsoleString()
        {
            Characters = new List<ConsoleCharacter>();
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleString"/> class with a <see cref="string"/> and colors.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="textColor">The text color of the string as will be displayed in the console.</param>
        /// <param name="backColor">The background color of the string as will be displayed in the console.</param>
        public ConsoleString(string str, ConsoleColor? textColor = null, ConsoleColor? backColor = null)
        {
            Characters = str.Select(c => new ConsoleCharacter(c, textColor, backColor)).ToList();
        }

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleString"/> class 
        /// with the value of another <see cref="ConsoleString"/>.
        /// </summary>
        public ConsoleString(ConsoleString str) : this(str.Characters) { }

        /// <summary>
        /// Initializes an instance of the <see cref="ConsoleString"/> class 
        /// with the <see cref="IEnumerable{T}"/> of <see cref="ConsoleCharacter"/>s.
        /// </summary>
        public ConsoleString(IEnumerable<ConsoleCharacter> characters)
        {
            Characters = new List<ConsoleCharacter>(characters);
        }

        /// <summary>
        /// Gets or sets the <see cref="ConsoleCharacter"/> at the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="ConsoleCharacter"/> at the specified index.</returns>
        public ConsoleCharacter this[int index] { get => Characters[index]; set => Characters[index] = value; }

        /// <summary>
        /// Adds a <see cref="ConsoleCharacter"/> to the end of the current <see cref="ConsoleString"/>.
        /// </summary>
        /// <param name="character">The <see cref="ConsoleCharacter"/> to add.</param>
        /// <returns>The updated self.</returns>
        public ConsoleString AddCharacter(ConsoleCharacter character)
        {
            Characters.Add(character);
            return this;
        }

        /// <summary>
        /// Adds a <see cref="ConsoleString"/> to the end of the current <see cref="ConsoleString"/>.
        /// </summary>
        /// <param name="str">The <see cref="ConsoleString"/> to add.</param>
        /// <returns>The updated self.</returns>
        public ConsoleString AddString(ConsoleString str)
        {
            foreach (ConsoleCharacter character in str.Characters)
                Characters.Add(character);
            return this;
        }

        /// <summary>
        /// Writes the current <see cref="ConsoleString"/> to the console.
        /// </summary>
        public void Write()
        {
            foreach (ConsoleCharacter character in Characters)
                character.Write();
        }

        /// <summary>
        /// Writes the current <see cref="ConsoleString"/> to the console and adds a line terminator.
        /// </summary>
        public void WriteLine()
        {
            Write();
            Console.WriteLine();
        }

        /// <summary>
        /// Writes the current <see cref="ConsoleString"/> to the console at a given point.
        /// </summary>
        /// <param name="point">The position in the console to write the current <see cref="ConsoleString"/> to.</param>
        public void WriteAtPoint(Point point)
        {
            // Save current cursor coordinates.
            Point prevPoint = new Point(Console.CursorLeft, Console.CursorTop);

            // Write.
            Console.SetCursorPosition(point.X, point.Y);
            Write();

            // Restore the saved cursor coordinates.
            Console.SetCursorPosition(prevPoint.X, prevPoint.Y);
        }

        /// <summary>
        /// Concatenates one <see cref="ConsoleString"/> to another.
        /// </summary>
        /// <param name="left">The left <see cref="ConsoleString"/>.</param>
        /// <param name="right">The right <see cref="ConsoleString"/>.</param>
        /// <returns>
        /// A new <see cref="ConsoleString"/> that is a combination of 
        /// the left and right <see cref="ConsoleString"/>s.
        /// </returns>
        public static ConsoleString operator +(ConsoleString left, ConsoleString right)
        {
            return new ConsoleString(left.Characters).AddString(right);
        }

        /// <summary>
        /// Concatenates a <see cref="ConsoleCharacter"/> to a <see cref="ConsoleString"/>.
        /// </summary>
        /// <param name="str">The <see cref="ConsoleString"/>.</param>
        /// <param name="character">The <see cref="ConsoleCharacter"/>.</param>
        /// <returns>
        /// A new <see cref="ConsoleString"/> that is a combination of 
        /// the left <see cref="ConsoleString"/> and the right <see cref="ConsoleCharacter"/>s.
        /// </returns>
        public static ConsoleString operator +(ConsoleString str, ConsoleCharacter character)
        {
            return new ConsoleString(str.Characters).AddCharacter(character);
        }

        /// <inheritdoc/>
        public override string? ToString()
        {
            return new string(Characters.Select(c => c.Character).ToArray());
        }

        /// <summary>
        /// Returns an enumerator the iterates through the collection of 
        /// <see cref="ConsoleCharacter"/>s of the current <see cref="ConsoleString"/>.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the <see cref="ConsoleString"/>.</returns>
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