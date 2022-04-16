using YonatanMankovich.ConsoleDiffWriter.Data;

namespace YonatanMankovich.ConsoleDiffWriter
{
    /// <summary>
    /// Represents a structure that helps writing <see cref="ConsoleCharacter"/>s more efficiently.
    /// </summary>
    internal class Writer : IDisposable
    {
        protected bool InitialCursorVisible { get; }
        protected ConsoleColor InitialTextColor { get; }
        protected ConsoleColor InitialBackColor { get; }

        protected ConsoleColor? LastTextColor { get; set; }
        protected ConsoleColor? LastBackColor { get; set; }

        /// <summary>
        /// Initializes an instance of the <see cref="Writer"/> class and saves the relevant <see cref="Console"/> properties.
        /// </summary>
        public Writer()
        {
            if (OperatingSystem.IsWindows())
                InitialCursorVisible = Console.CursorVisible;

            InitialTextColor = Console.ForegroundColor;
            InitialBackColor = Console.BackgroundColor;

            Console.CursorVisible = false;
        }

        public void Write(ConsoleCharacter character)
        {
            bool needToChangeColor = NeedToChangeColor(character);
            if (needToChangeColor)
            {
                if (character.BackColor.HasValue)
                    Console.BackgroundColor = character.BackColor.Value;

                if (character.TextColor.HasValue)
                    Console.ForegroundColor = character.TextColor.Value;
            }

            Console.Write(character.Character);

            if (needToChangeColor)
            {
                LastTextColor = character.TextColor;
                LastBackColor = character.BackColor;
            }
        }

        private bool NeedToChangeColor(ConsoleCharacter newChar)
        {
            return (newChar.Character != ' ' && newChar.TextColor != LastTextColor) || newChar.BackColor != LastBackColor;
        }

        /// <summary>
        /// Reverts the <see cref="Console"/> properties to the saved ones from the current object initialization.
        /// </summary>
        public void RevertPreviousConsoleProperties()
        {
            if (OperatingSystem.IsWindows())
                Console.CursorVisible = InitialCursorVisible;

            Console.ForegroundColor = InitialTextColor;
            Console.BackgroundColor = InitialBackColor;
        }

        /// <summary>
        /// See <see cref="RevertPreviousConsoleProperties"/>
        /// </summary>
        public void Dispose() => RevertPreviousConsoleProperties();
    }
}