using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter;
using YonatanMankovich.SimpleColorConsole;

RunDiffCharTests();
RunDiffStringTests();
RunDiffLinesTests();

void RunDiffCharTests()
{
    Console.WriteLine(nameof(ConsoleDiffCharacter) + " Tests");
    Console.WriteLine(new string('-', 35));

    ConsoleDiffCharacter diff = new ConsoleDiffCharacter();
    ColorChar character = new ColorChar('#', textColor: ConsoleColor.Magenta, backColor: ConsoleColor.Cyan);

    diff.WriteDiff(character);

    Thread.Sleep(1000); // Sleep to show change.

    ColorChar newCharacter = new ColorChar('C', textColor: ConsoleColor.Yellow, backColor: ConsoleColor.Blue);

    diff.WriteDiff(newCharacter);

    Console.WriteLine();
    Console.WriteLine();
}

void RunDiffStringTests()
{
    Console.WriteLine(nameof(ConsoleDiffString) + " Tests");
    Console.WriteLine(new string('-', 35));

    ConsoleDiffString diff = new ConsoleDiffString();
    ColorString str = new ColorString("0123456789", textColor: ConsoleColor.Magenta, backColor: ConsoleColor.Cyan);
    diff.WriteDiff(str);

    Thread.Sleep(1000); // Sleep to show change.

    // Shorter string
    ColorString shorterStr = new ColorString("ABC", textColor: ConsoleColor.Yellow, backColor: ConsoleColor.Blue);
    diff.WriteDiff(shorterStr);

    Thread.Sleep(1000); // Sleep to show change.

    // Longer string
    ColorString longerStr = new ColorString("abcdefg", textColor: ConsoleColor.DarkCyan, backColor: ConsoleColor.Red);
    diff.WriteDiff(longerStr);

    Console.WriteLine();
    Console.WriteLine();
}

void RunDiffLinesTests()
{
    Console.WriteLine(nameof(ConsoleDiffLines) + " Tests");
    Console.WriteLine(new string('-', 35));

    ConsoleDiffLines diff = new ConsoleDiffLines(new Point(2, Console.CursorTop));
    ColorLines lines = new ColorLines()
        .AddLine(new ColorString("abcdefg") + new ColorString("hijklmnop", ConsoleColor.Magenta))
        .AddLine(new ColorString("qrstuv", ConsoleColor.Black, ConsoleColor.Yellow)
           + new ColorString("wxyz", ConsoleColor.Black, ConsoleColor.Cyan));

    diff.WriteDiff(lines);
    Thread.Sleep(1000); // Sleep to show change

    ColorLines shorterLines = new ColorLines()
        .AddLine(new ColorString("0123456789", ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta));

    diff.WriteDiff(shorterLines);
    Thread.Sleep(1000); // Sleep to show change

    ColorLines longerLines = new ColorLines()
        .AddLine(new ColorString("ABCDEFGHIJKLMNOP", ConsoleColor.Yellow, ConsoleColor.Blue))
        .AddLine(new ColorString("qrstuv", ConsoleColor.Black, ConsoleColor.Yellow))
        .AddLine(new ColorString("Hello", ConsoleColor.Black, ConsoleColor.Green)
            + ' ' + new ColorString("world", ConsoleColor.DarkGreen, ConsoleColor.Gray));

    diff.WriteDiff(longerLines);

    Console.WriteLine();
}