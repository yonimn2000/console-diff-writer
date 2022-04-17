using System.Collections;
using System.Drawing;
using YonatanMankovich.SimpleColorConsole;

namespace YonatanMankovich.ConsoleDiffWriter.Bases
{
    /// <summary>
    /// Represents a structure that keeps track of a string written to the console at a 
    /// specific point and can write just the difference between it and a new string.
    /// </summary>
    public abstract class StringDiffBase<S, C> : IEnumerable<C> where C : new()
    {
        /// <summary>
        /// The point on the console to which to write the <see cref="ColorString"/> to.
        /// </summary>
        public Point Point { get; }

        /// <summary>
        /// The length of the written <see cref="ColorString"/>.
        /// </summary>
        public int Length => WrittenString.Count;

        /// <summary>
        /// Gets or sets the written string.
        /// </summary>
        protected IList<C> WrittenString { get; set; }

        /// <summary>
        /// Initializes an empty instance of the <see cref="StringDiffBase{S, C}"/>
        /// at the current <see cref="Console"/> cursor position.
        /// </summary>
        public StringDiffBase() : this(new Point(Console.CursorLeft, Console.CursorTop)) { }

        /// <summary>
        /// Initializes an empty instance of the <see cref="StringDiffBase{S, C}"/> with 
        /// a <see cref="System.Drawing.Point"/> at which to track the diff.
        /// </summary>
        /// <param name="point">The point on the console to which to write the string to.</param>
        public StringDiffBase(Point point)
        {
            WrittenString = new List<C>();
            Point = point;
        }

        /// <summary>
        /// Clears the console area where the string was written.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Writes only the difference between the written <see cref="ColorString"/>
        /// and the given <see cref="ColorString"/> to the console.
        /// </summary>
        /// <param name="s">The new string to overwrite the written string with.</param>
        public abstract void WriteDiff(S s);

        /// <summary>
        /// Gets the written string.
        /// </summary>
        /// <returns>The written string.</returns>
        public abstract S GetWrittenString();

        /// <summary>
        /// Returns an enumerator the iterates through the collection of 
        /// characters of the current string diff.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the string diff.</returns>
        public IEnumerator<C> GetEnumerator()
        {
            return WrittenString.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)WrittenString).GetEnumerator();
        }

        /// <inheritdoc/>
        public override string? ToString()
        {
            return string.Concat(WrittenString);
        }
    }
}