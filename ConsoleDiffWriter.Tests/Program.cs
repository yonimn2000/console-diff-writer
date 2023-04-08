using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter;
using YonatanMankovich.ConsoleDiffWriter.Color;
using YonatanMankovich.SimpleColorConsole;

RunDiffCharTests();
RunDiffStringTests();
RunDiffLinesTests();

RunColorDiffCharTests();
RunColorDiffStringTests();
RunColorDiffLinesTests();

void RunDiffCharTests()
{
    Console.WriteLine(nameof(CharDiff) + " Tests");
    Console.WriteLine(new string('-', 35));

    CharDiff diff = new CharDiff();
    char character = '#';

    diff.WriteDiff(character);

    Thread.Sleep(1000); // Sleep to show change.

    char newCharacter = 'C';

    diff.WriteDiff(newCharacter);

    Console.WriteLine();
    Console.WriteLine();
}

void RunDiffStringTests()
{
    Console.WriteLine(nameof(StringDiff) + " Tests");
    Console.WriteLine(new string('-', 35));

    StringDiff diff = new StringDiff();
    string str = "0123456789";
    diff.WriteDiff(str);

    Thread.Sleep(1000); // Sleep to show change.

    // Shorter string
    string shorterStr = "ABC";
    diff.WriteDiff(shorterStr);

    Thread.Sleep(1000); // Sleep to show change.

    // Longer string
    string longerStr = "abcdefg";
    diff.WriteDiff(longerStr);

    Console.WriteLine();
    Console.WriteLine();
}

void RunDiffLinesTests()
{
    Console.WriteLine(nameof(LinesDiff) + " Tests");
    Console.WriteLine(new string('-', 35));

    LinesDiff diff = new LinesDiff(new Point(2, Console.CursorTop)); // Test diff at a point.
    IList<string> lines = new List<string>()
    {
        "abcdefghijklmnop",
        "qrstuvwxyz"
    };

    diff.WriteDiff(lines);
    Thread.Sleep(1000); // Sleep to show change

    IList<string> shorterLines = new List<string>()
    {
        "0123456789"
    };

    diff.WriteDiff(shorterLines);
    Thread.Sleep(1000); // Sleep to show change

    IList<string> longerLines = new List<string>()
    {
        "ABCDEFGHIJKLMNOP",
        "qrstuv",
        "Hello world"
    };

    diff.WriteDiff(longerLines);

    Console.WriteLine();
}

void RunColorDiffCharTests()
{
    Console.WriteLine(nameof(ColorCharDiff) + " Tests");
    Console.WriteLine(new string('-', 35));

    ColorCharDiff diff = new ColorCharDiff();
    ColorChar character = new ColorChar('#', textColor: ConsoleColor.Magenta, backColor: ConsoleColor.Cyan);

    diff.WriteDiff(character);

    Thread.Sleep(1000); // Sleep to show change.

    ColorChar newCharacter = new ColorChar('C', textColor: ConsoleColor.Yellow, backColor: ConsoleColor.Blue);

    diff.WriteDiff(newCharacter);

    Console.WriteLine();
    Console.WriteLine();
}

void RunColorDiffStringTests()
{
    Console.WriteLine(nameof(ColorStringDiff) + " Tests");
    Console.WriteLine(new string('-', 35));

    ColorStringDiff diff = new ColorStringDiff();
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

    Thread.Sleep(1000); // Sleep to show change.

    // Colorless string
    ColorString colorlessStr = new ColorString("abcdefg");
    colorlessStr[3] = new ColorChar('X', textColor: ConsoleColor.Red, backColor: ConsoleColor.White);
    diff.WriteDiff(colorlessStr);

    Console.WriteLine();
    Console.WriteLine();
}

void RunColorDiffLinesTests()
{
    Console.WriteLine(nameof(ColorLinesDiff) + " Tests");
    Console.WriteLine(new string('-', 35));

    ColorLinesDiff diff = new ColorLinesDiff(new Point(2, Console.CursorTop));
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

    Thread.Sleep(1000); // Sleep to show change

    ColorLines colorlessLines = new ColorLines()
        .AddLine(new ColorString("ABCDEFGHIJKLMNOP"))
        .AddLine(new ColorString("qrstuv", ConsoleColor.Black, ConsoleColor.Yellow))
        .AddLine(new ColorString("Hello")
            + ' ' + new ColorString("world", ConsoleColor.DarkGreen, ConsoleColor.Gray));

    diff.WriteDiff(longerLines);

    Console.WriteLine();
}