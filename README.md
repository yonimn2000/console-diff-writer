# Console Diff Writer

A library that helps keep track of text written to the console and efficiently writes only the difference with the updated text instead of rewriting all of it.

The NuGet package is available [here](https://www.nuget.org/packages/YonatanMankovich.ConsoleDiffWriter/).

## Getting Started Examples

Note that if a `Point` is not specified in the `Diff` constructor, the diff will be tracked starting the current `Console` cursor position.

### Plain

#### String (or Char)

```cs
using YonatanMankovich.ConsoleDiffWriter;

StringDiff diff = new StringDiff(); // Or use 'CharDiff' if needed.

diff.WriteDiff("0123456789"); // Write the initial string.
diff.WriteDiff("01234X6789"); // Only the 'X' will be written.
diff.WriteDiff("01234X678"); // The '9' will be overwritten with a space char.

// If writing to a specific point is desired:
StringDiff diffAtPoint = new StringDiff(new Point (6, 9));
```

#### String Lines

```cs
using YonatanMankovich.ConsoleDiffWriter;

LinesDiff diff = new LinesDiff();

diff.WriteDiff(new List<string>()
{
    "----------",
    "----##----",
    "----++----"
}); // Write the initial lines.

diff.WriteDiff(new List<string>()
{
    "----------",
    "----XX----",
    "----++----"
}); // Only '##' will be overwritten with 'XX'.

diff.WriteDiff(new List<string>()
{
    "----------",
    "----XX----"
}); // The last line will be overwritten with spaces.

// If writing to a specific point is desired:
LinesDiff diffAtPoint = new LinesDiff(new Point (6, 9));
```

### Colorful

Everything is the same as before but now in color. See [`SimpleColorConsole`](https://github.com/yonimn2000/simple-color-console) for methods for working with colored characters, strings, and lines.

#### `ColorString` (or `ColorChar`) and `ColorLines`

```cs
using YonatanMankovich.ConsoleDiffWriter.Color;
using YonatanMankovich.SimpleColorConsole;

 // Or use 'ColorCharDiff' or 'ColorLinesDiff' if needed.
ColorStringDiff diff = new ColorStringDiff();

diff.WriteDiff("0123456789".TextYellow()); // Write the initial string.
diff.WriteDiff("01234X6789".TextYellow()); // Only the 'X' will be written.

 // The '9' will be overwritten with a blue character.
diff.WriteDiff("01234X678".TextYellow() + new ColorChar('9').BackBlue());

// If writing to a specific point is desired:
ColorStringDiff diffAtPoint = new ColorStringDiff(new Point (6, 9));
```

## Behavioral Note

`'/n'`, `'/r'`, `'/t'` characters will not work. If a multi-line string is needed, use string lists or [`ColorLines`](https://github.com/yonimn2000/simple-color-console/#colorlines-for-multi-line-colorstrings).

## My Other Projects That Use This Library

[Command Line Minesweeper](https://github.com/yonimn2000/command-line-minesweeper)
