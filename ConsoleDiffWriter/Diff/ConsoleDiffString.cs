using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Data;

namespace YonatanMankovich.ConsoleDiffWriter.Diff
{
    public class ConsoleDiffString
    {
        public Point Point { get; }
        public int Length => WrittenString.Count;
        private IList<ConsoleDiffCharacter> WrittenString { get; set; }

        public ConsoleDiffString(ConsoleString str, Point point)
        {
            WrittenString = new List<ConsoleDiffCharacter>(str.Length);
            Point = point;

            for (int i = 0; i < str.Length; i++)
                WrittenString.Add(new ConsoleDiffCharacter(str[i], new Point(point.X + i, point.Y)));
        }

        public void Write()
        {
            foreach (ConsoleDiffCharacter character in WrittenString)
                character.Write();
        }

        public void WriteDiff(ConsoleString str)
        {
            // If the new string is longer than the one written, add space characters
            // to the end of the previously written string.
            for (int i = WrittenString.Count; i < str.Length; i++)
                WrittenString.Add(new ConsoleDiffCharacter(new ConsoleCharacter(' '), new Point(Point.X + i, Point.Y)));

            // Write the diff between all the characters of the two strings.
            for (int i = 0; i < str.Length; i++)
                WrittenString[i].WriteDiff(str[i]);

            // If the new string is shorter, overwrite the old extra characters with spaces
            // and remove them from the list of written characters.
            int originalDrawnStringLength = WrittenString.Count;
            for (int i = str.Length; i < originalDrawnStringLength; i++)
            {
                new ConsoleCharacter(' ').WriteAtPoint(new Point(WrittenString[0].Point.X + i, WrittenString[0].Point.Y));
                WrittenString.RemoveAt(str.Length); // Remove last element.
            }
        }
    }
}