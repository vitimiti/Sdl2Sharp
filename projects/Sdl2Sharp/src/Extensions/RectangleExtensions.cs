using System.Drawing;

using Sdl2Sharp.Interop;

namespace Sdl2Sharp.Extensions;

/// <summary>Extensions for the <see cref="Rectangle" /> class.</summary>
public static class RectangleExtensions
{
    /// <summary>Intersect a line with a <see cref="Rectangle" />.</summary>
    /// <param name="rectangle">The <see cref="Rectangle" /> to intersect with.</param>
    /// <param name="point1">The first line <see cref="Point" />.</param>
    /// <param name="point2">The second line <see cref="Point" />.</param>
    /// <returns>
    ///     A tuple with the two line <see cref="Point" />s intersected or <see langword="null" /> if the line couldn't be
    ///     intersected.
    /// </returns>
    public static (Point point1, Point point2)? IntersectLine(this Rectangle rectangle, Point point1, Point point2)
    {
        Sdl.NativeRectangle nativeRectangle = new()
        {
            X = rectangle.X, Y = rectangle.Y, W = rectangle.Width, H = rectangle.Height
        };

        int x1 = point1.X;
        int y1 = point1.Y;
        int x2 = point2.X;
        int y2 = point2.Y;
        bool isValid = Sdl.IntersectRectangleAndLine(in nativeRectangle, ref x1, ref y1, ref x2, ref y2);
        return isValid ? (new Point(x1, y1), new Point(x2, y2)) : null;
    }
}