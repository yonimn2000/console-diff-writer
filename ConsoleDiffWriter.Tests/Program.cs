using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Data;
using YonatanMankovich.ConsoleDiffWriter.Diff;

ConsoleLines area = new ConsoleLines();
area.AddLine(new ConsoleString("abcdefg") + new ConsoleString("hijklmnop", ConsoleColor.Magenta));
area.AddLine(new ConsoleString("qrstuv", ConsoleColor.Black, ConsoleColor.Yellow)
    + new ConsoleString("wxyz", ConsoleColor.Black, ConsoleColor.Cyan));

ConsoleDiffLines diffArea = new ConsoleDiffLines(area, new Point(0, 0));
diffArea.Write();

area = new ConsoleLines();
area.AddLine(new ConsoleString("123456789", ConsoleColor.Red));
area.AddLine(new ConsoleString("ABCDEFGHIJKLMNOP", ConsoleColor.Yellow, ConsoleColor.Blue));
area.AddLine(new ConsoleString("Hello", ConsoleColor.Black, ConsoleColor.Green) 
    + new ConsoleCharacter(' ') + new ConsoleString("world", ConsoleColor.DarkGreen, ConsoleColor.Gray));

diffArea.WriteDiff(area);