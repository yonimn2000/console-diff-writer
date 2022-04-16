using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Data;
using YonatanMankovich.ConsoleDiffWriter.Diff;

RunCharacterTests();
RunStringTests();
RunLinesTests();
RunDiffCharTests();
RunDiffStringTests();
RunDiffLinesTests();

static void RunCharacterTests()
{
    Console.WriteLine(nameof(ConsoleCharacter) + " Tests");
    Console.WriteLine(new string('-', 35));
    ConsoleCharacter character;

    Console.Write("Blank character: [");
    character = new ConsoleCharacter();
    character.Write();
    Console.WriteLine("]");

    Console.Write("Background character: [");
    character = new ConsoleCharacter(ConsoleColor.Blue);
    character.Write();
    Console.WriteLine("]");

    Console.Write("Text colored character: [");
    character = new ConsoleCharacter('#', textColor: ConsoleColor.Magenta);
    character.Write();
    Console.WriteLine("]");

    Console.Write("Background colored character: [");
    character = new ConsoleCharacter('#', backColor: ConsoleColor.Magenta);
    character.Write();
    Console.WriteLine("]");

    Console.Write("Fully colored character: [");
    character = new ConsoleCharacter('#', textColor: ConsoleColor.Magenta, backColor: ConsoleColor.Cyan);
    character.Write();
    Console.WriteLine("]");

    Console.Write("Write character at point: [ ]");
    character.WriteAtPoint(new Point(Console.CursorLeft - 2, Console.CursorTop));
    Console.WriteLine();

    Console.WriteLine("Character to string: [" + character.ToString() + "]");

    Console.Write("Invalid character: [");
    try
    {
        character = new ConsoleCharacter('\n');
    }
    catch (ArgumentException)
    {
        Console.Write("Caught exception");
    }
    Console.WriteLine("]");
    Console.WriteLine();
}

static void RunStringTests()
{
    Console.WriteLine(nameof(ConsoleString) + " Tests");
    Console.WriteLine(new string('-', 35));
    ConsoleString str;

    Console.Write("Blank string: [");
    str = new ConsoleString();
    str.Write();
    Console.WriteLine("]");

    Console.Write("Background string: [");
    str = new ConsoleString(new string(' ', 10), backColor: ConsoleColor.Blue);
    str.Write();
    Console.WriteLine("]");

    Console.Write("Text colored string: [");
    str = new ConsoleString("0123456789", textColor: ConsoleColor.Magenta);
    str.Write();
    Console.WriteLine("]");

    Console.Write("Background colored string: [");
    str = new ConsoleString("0123456789", backColor: ConsoleColor.Magenta);
    str.Write();
    Console.WriteLine("]");

    Console.Write("Fully colored string: [");
    str = new ConsoleString("0123456789", textColor: ConsoleColor.Magenta, backColor: ConsoleColor.Cyan);
    str.Write();
    Console.WriteLine("]");

    Console.WriteLine("String length (10): [" + str.Length + "]");

    Console.Write("Write string at point: [          ]");
    str.WriteAtPoint(new Point(Console.CursorLeft - str.Length - 1, Console.CursorTop));
    Console.WriteLine();

    Console.WriteLine(nameof(ConsoleString) + " to string: [" + str.ToString() + "]");

    Console.Write("Char at index 7: [");
    str[7].Write();
    Console.WriteLine("]");

    Console.Write("Add char to end: [");
    str.AddToEnd(new ConsoleCharacter('@', textColor: ConsoleColor.Blue, backColor: ConsoleColor.Red));
    str.Write();
    Console.WriteLine("]");

    Console.Write("Add string to end: [");
    str.AddToEnd(new ConsoleString("0123456789", textColor: ConsoleColor.Blue, backColor: ConsoleColor.Green));
    str.Write();
    Console.WriteLine("]");

    Console.WriteLine("Write line: [");
    str.WriteLine();
    Console.WriteLine("]");

    Console.Write("Concat char: [");
    str = new ConsoleString("0123456789", textColor: ConsoleColor.Yellow, backColor: ConsoleColor.Blue);
    ConsoleCharacter character = new ConsoleCharacter('$', textColor: ConsoleColor.Blue, backColor: ConsoleColor.Red);
    ConsoleString newString = str + character;
    newString.Write();
    Console.WriteLine("]");

    Console.Write("Concat string: [");
    ConsoleString str2 = new ConsoleString("0123456789", textColor: ConsoleColor.Blue, backColor: ConsoleColor.Red);
    newString = str + str2;
    newString.Write();
    Console.WriteLine("]");

    Console.WriteLine();
}

static void RunLinesTests()
{
    Console.WriteLine(nameof(ConsoleLines) + " Tests");
    Console.WriteLine(new string('-', 35));
    ConsoleLines lines;

    Console.Write("Blank lines: [");
    lines = new ConsoleLines();
    lines.Write();
    Console.WriteLine("]" + Environment.NewLine);

    Console.Write("Add line: [");
    lines = new ConsoleLines();
    ConsoleString line1 = new ConsoleString("ABCDEFGHIJKLMNOPQRSTUVWXYZ", textColor: ConsoleColor.Cyan, backColor: ConsoleColor.Red);
    ConsoleString line2 = new ConsoleString("0123456789");
    ConsoleString line3 = new ConsoleString("!@#$%^&*()_+", textColor: ConsoleColor.DarkBlue, backColor: ConsoleColor.Yellow);
    lines.AddLine(line1).AddLine(line2).AddLine(line3).Write();
    Console.WriteLine("]" + Environment.NewLine);

    Console.Write("Add lines: [");
    new ConsoleLines(lines).AddLines(lines).Write();
    Console.WriteLine("]" + Environment.NewLine);

    Console.Write("Add blank line: [");
    new ConsoleLines(lines).AddLine().AddLines(lines).Write();
    Console.WriteLine("]" + Environment.NewLine);

    Console.Write("Add char to end of last line: [");
    new ConsoleLines(lines).AddToEndOfLastLine(new ConsoleCharacter('~', backColor: ConsoleColor.Magenta)).Write();
    Console.WriteLine("]" + Environment.NewLine);

    Console.Write("Add string to end of last line: [");
    new ConsoleLines().AddLine(line1).AddLine(line2).AddToEndOfLastLine(line1).Write();
    Console.WriteLine("]" + Environment.NewLine);

    Console.WriteLine("Write at point: [");
    lines.WriteAtPoint(new Point(4, Console.CursorTop));
    Console.WriteLine("]" + Environment.NewLine);

    Console.WriteLine("Write to string: [" + lines.ToString() + "]" + Environment.NewLine);

    Console.Write("Concat lines: [");
    ConsoleLines newLines = new ConsoleLines().AddLine(line1).AddLine(line2) + new ConsoleLines().AddLine(line3);
    newLines.Write();
    Console.WriteLine("]" + Environment.NewLine);

    Console.WriteLine();
}

void RunDiffCharTests()
{
    Console.WriteLine(nameof(ConsoleDiffCharacter) + " Tests");
    Console.WriteLine(new string('-', 35));

    ConsoleDiffCharacter diff = new ConsoleDiffCharacter();
    ConsoleCharacter character = new ConsoleCharacter('#', textColor: ConsoleColor.Magenta, backColor: ConsoleColor.Cyan);

    diff.WriteDiff(character);

    Thread.Sleep(1000); // Sleep to show change.

    ConsoleCharacter newCharacter = new ConsoleCharacter('C', textColor: ConsoleColor.Yellow, backColor: ConsoleColor.Blue);

    diff.WriteDiff(newCharacter);

    Console.WriteLine();
    Console.WriteLine();
}

void RunDiffStringTests()
{
    Console.WriteLine(nameof(ConsoleDiffString) + " Tests");
    Console.WriteLine(new string('-', 35));

    ConsoleDiffString diff = new ConsoleDiffString();
    ConsoleString str = new ConsoleString("0123456789", textColor: ConsoleColor.Magenta, backColor: ConsoleColor.Cyan);
    diff.WriteDiff(str);

    Thread.Sleep(1000); // Sleep to show change.

    // Shorter string
    ConsoleString shorterStr = new ConsoleString("ABC", textColor: ConsoleColor.Yellow, backColor: ConsoleColor.Blue);
    diff.WriteDiff(shorterStr);

    Thread.Sleep(1000); // Sleep to show change.

    // Longer string
    ConsoleString longerStr = new ConsoleString("abcdefg", textColor: ConsoleColor.DarkCyan, backColor: ConsoleColor.Red);
    diff.WriteDiff(longerStr);

    Console.WriteLine();
    Console.WriteLine();
}

void RunDiffLinesTests()
{
    Console.WriteLine(nameof(ConsoleDiffLines) + " Tests");
    Console.WriteLine(new string('-', 35));

    ConsoleDiffLines diff = new ConsoleDiffLines(new Point(2, Console.CursorTop));
    ConsoleLines lines = new ConsoleLines()
        .AddLine(new ConsoleString("abcdefg") + new ConsoleString("hijklmnop", ConsoleColor.Magenta))
        .AddLine(new ConsoleString("qrstuv", ConsoleColor.Black, ConsoleColor.Yellow)
           + new ConsoleString("wxyz", ConsoleColor.Black, ConsoleColor.Cyan));

    diff.WriteDiff(lines);
    Thread.Sleep(1000); // Sleep to show change

    ConsoleLines shorterLines = new ConsoleLines()
        .AddLine(new ConsoleString("0123456789", ConsoleColor.DarkCyan, ConsoleColor.DarkMagenta));

    diff.WriteDiff(shorterLines);
    Thread.Sleep(1000); // Sleep to show change

    ConsoleLines longerLines = new ConsoleLines()
        .AddLine(new ConsoleString("ABCDEFGHIJKLMNOP", ConsoleColor.Yellow, ConsoleColor.Blue))
        .AddLine(new ConsoleString("qrstuv", ConsoleColor.Black, ConsoleColor.Yellow))
        .AddLine(new ConsoleString("Hello", ConsoleColor.Black, ConsoleColor.Green)
            + ' ' + new ConsoleString("world", ConsoleColor.DarkGreen, ConsoleColor.Gray));

    diff.WriteDiff(longerLines);

    Console.WriteLine();
}