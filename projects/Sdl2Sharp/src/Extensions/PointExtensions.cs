using System.Drawing;

using Sdl2Sharp.Interop;

namespace Sdl2Sharp.Extensions;

/// <summary>Extensions for the <see cref="Point" /> class.</summary>
public static class PointExtensions
{
    /// <summary>Enclose an array of points.</summary>
    /// <param name="points">
    ///     The <see cref="Array" /> of <see cref="Point" />s to enclose into a <see cref="Rectangle" />
    /// </param>
    /// <param name="clip">
    ///     The <see cref="Rectangle" /> to clip the <paramref name="points" /> before enclosing them or
    ///     <see langword="null" /> to use no clip.
    /// </param>
    /// <returns>
    ///     A <see cref="Rectangle" /> enclosing all <paramref name="points" /> or <see langword="null" /> if the
    ///     <paramref name="points" /> couldn't be enclosed.
    /// </returns>
    /// <remarks><paramref name="clip" /> is <see langword="null" /> by default.</remarks>
    public static Rectangle? EnclosePoints(this Point[] points, Rectangle? clip = null)
    {
        Sdl.NativePoint[] nativePoints = new Sdl.NativePoint[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            nativePoints[i].X = points[i].X;
            nativePoints[i].Y = points[i].Y;
        }

        if (clip is null)
        {
            bool isValid = Sdl.EnclosePoints(nativePoints, points.Length, IntPtr.Zero, out Sdl.NativeRectangle result);
            return isValid ? new Rectangle(result.X, result.Y, result.W, result.H) : null;
        }

        Sdl.NativeRectangle nativeClip = new()
        {
            X = clip.Value.X, Y = clip.Value.Y, W = clip.Value.Width, H = clip.Value.Height
        };

        bool clippedIsValid = Sdl.EnclosePoints(nativePoints, points.Length, in nativeClip,
            out Sdl.NativeRectangle clippedResult);

        return clippedIsValid
            ? new Rectangle(clippedResult.X, clippedResult.Y, clippedResult.W, clippedResult.H)
            : null;
    }
}