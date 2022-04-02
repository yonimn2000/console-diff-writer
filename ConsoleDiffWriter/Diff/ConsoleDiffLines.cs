using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Data;

namespace YonatanMankovich.ConsoleDiffWriter.Diff
{
    public class ConsoleDiffLines
    {
        public Point Point { get; }
        private IList<ConsoleDiffString> WrittenLines { get; set; }

        public ConsoleDiffLines(ConsoleLines lines, Point point)
        {
            WrittenLines = new List<ConsoleDiffString>(lines.Count);
            Point = point;

            for (int i = 0; i < lines.Count; i++)
                WrittenLines.Add(new ConsoleDiffString(lines[i], new Point(point.X, point.Y + i)));
        }

        public void Write()
        {
            foreach (ConsoleDiffString line in WrittenLines)
                line.Write();
        }

        public void WriteDiff(ConsoleLines lines)
        {
            // If the new area has more lines than the one written, add blank lines to the end of the previously written area.
            for (int i = WrittenLines.Count; i < lines.Count; i++)
                WrittenLines.Add(new ConsoleDiffString(new ConsoleString(), new Point(Point.X, Point.Y + i)));

            // Write the diff between all the lines of the two areas.
            for (int i = 0; i < lines.Count; i++)
            {
                WrittenLines[i].WriteDiff(lines[i]);
                Console.WriteLine();
            }

            // If the new area has less lines than the written one,
            // overwrite the old extra lines with spaces and remove them from the list of written lines.
            int originalDrawnLinesCount = WrittenLines.Count;
            for (int i = lines.Count; i < originalDrawnLinesCount; i++)
            {
                new ConsoleString(new string(' ', WrittenLines[i].Length)).WriteAtPoint(new Point(Point.X, Point.Y + i));
                WrittenLines.RemoveAt(lines.Count); // Remove last element.
            }
        }
    }
}