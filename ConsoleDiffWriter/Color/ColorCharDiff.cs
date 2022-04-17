using System.Drawing;
using YonatanMankovich.ConsoleDiffWriter.Bases;
using YonatanMankovich.SimpleColorConsole;

namespace YonatanMankovich.ConsoleDiffWriter.Color
{
    /// <summary>
    /// Represents a structure that keeps track of a <see cref="ColorChar"/> 
    /// written to the console at a specific point and can write just the difference
    /// between it and a new <see cref="ColorChar"/>.
    /// </summary>
    public class ColorCharDiff : CharDiffBase<ColorChar>
    {
        /// <summary>
        /// Initializes an instance of the <see cref="ColorCharDiff"/> with 
        /// a <see cref="ColorChar"/> and a <see cref="Point"/>
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ColorChar"/> to.</param>
        /// <param name="character">The <see cref="ColorChar"/> to keep track of.</param>
        public ColorCharDiff(Point point, ColorChar character) : base(point, character) { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ColorCharDiff"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public ColorCharDiff() : base() { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="ColorCharDiff"/> with 
        /// a <see cref="Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the <see cref="ColorChar"/> to.</param>
        public ColorCharDiff(Point point) : base(point) { }

        /// <inheritdoc/>
        protected override void WriteCharAtPoint(ColorChar character, Point point) => character.WriteAtPoint(point);
    }
}