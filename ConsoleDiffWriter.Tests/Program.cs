using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Data;
using YonatanMankovich.ConsoleDiffWriter.Diff;

// TODO: Test everything.

ConsoleLines lines = new ConsoleLines()
        .AddLine(new ConsoleString("abcdefg") + new ConsoleString("hijklmnop", ConsoleColor.Magenta))
        .AddLine(new ConsoleString("qrstuv", ConsoleColor.Black, ConsoleColor.Yellow)
           + new ConsoleString("wxyz", ConsoleColor.Black, ConsoleColor.Cyan));

ConsoleDiffLines diffLines = new ConsoleDiffLines(new Point(0, 0));
diffLines.WriteDiff(lines);

for (int i = 0; i < 10; i++)
{
    lines = new ConsoleLines()
    .AddLine(new ConsoleString("ABCDEFGHIJKLMNOP", ConsoleColor.Yellow, ConsoleColor.Blue))
    .AddLine(new ConsoleString(Guid.NewGuid().ToString(), ConsoleColor.Red))
    .AddLine(new ConsoleString("Hello", ConsoleColor.Black, ConsoleColor.Green)
        + new ConsoleCharacter(' ') + new ConsoleString("world", ConsoleColor.DarkGreen, ConsoleColor.Gray));

    diffLines.WriteDiff(lines);
}


ConsoleDiffLines diffBoard = new ConsoleDiffLines(new Point(0, 0));
ConsoleLines board = new ConsoleLines();
const int boardSize = 48;
//diffBoard.FillArea(new Size(boardSize * 2, boardSize), ConsoleColor.White);
for (int i = 0; i < boardSize; i++)
{
    ConsoleString row = new ConsoleString();
    for (int j = 0; j < boardSize; j++)
    {
        ConsoleColor bgColor = j % 2 == 0 && i % 2 == 0 || j % 2 != 0 && i % 2 != 0 ? ConsoleColor.White : ConsoleColor.DarkGray;
        row.AddToEnd(new ConsoleString("  ", backColor: bgColor));
    }
    board.AddLine(row);
}
diffBoard.WriteDiff(board);

board = new ConsoleLines();
for (int i = 0; i < boardSize; i++)
{
    ConsoleString row = new ConsoleString();
    for (int j = 0; j < boardSize; j++)
    {
        ConsoleColor bgColor = j % 2 == 0 && i % 2 == 0 || j % 2 != 0 && i % 2 != 0 ? ConsoleColor.DarkGray : ConsoleColor.White;
        row.AddToEnd(new ConsoleString("  ", backColor: bgColor));
    }
    board.AddLine(row);
}
diffBoard.WriteDiff(board);
diffBoard.Clear();