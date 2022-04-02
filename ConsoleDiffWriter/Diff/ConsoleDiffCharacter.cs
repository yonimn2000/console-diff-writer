using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Data;

namespace YonatanMankovich.ConsoleDiffWriter.Diff
{
    public class ConsoleDiffCharacter
    {
        public Point Point { get; }
        private ConsoleCharacter WrittenCharacter { get; set; }
        private bool IsFirstWrite { get; set; } = false;

        public ConsoleDiffCharacter(ConsoleCharacter drawnCharacter, Point point)
        {
            WrittenCharacter = drawnCharacter;
            Point = point;
        }

        public void Write()
        {
            WriteDiff(WrittenCharacter);
        }

        public void WriteDiff(ConsoleCharacter character)
        {
            // Write only if the character changed.
            if (IsFirstWrite && character.Equals(WrittenCharacter))
                return;

            character.WriteAtPoint(Point);
            WrittenCharacter = character;
            IsFirstWrite = true;
        }
    }
}