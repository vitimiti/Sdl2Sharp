using System.Drawing;

using Sdl2Sharp.Extensions;

using Xunit;

namespace Sdl2Sharp.UnitTests;

public class ExtensionsTests
{
    [Fact]
    public void EnclosePointsWithoutClipExtensionWorks()
    {
        Point[] points = { new(0, 0), new(4, 0), new(0, 4), new(4, 4) };
        Rectangle? result = points.EnclosePoints();
        Assert.NotNull(result);
        Assert.Equal(new Rectangle(0, 0, 5, 5), result);
    }

    [Fact]
    public void EnclosePointsWithClipExtensionWorks()
    {
        Point[] points = { new(0, 0), new(4, 0), new(0, 4), new(4, 4) };
        Rectangle clip = new(1, 1, 4, 4);
        Rectangle? result = points.EnclosePoints(clip);
        Assert.NotNull(result);
        Assert.Equal(new Rectangle(4, 4, 1, 1), result);
    }

    [Fact]
    public void IntersectLineWithRectangleWorks()
    {
        Rectangle rectangle = new(0, 0, 4, 4);
        (Point point1, Point point2) line = (new Point(2, 7), new Point(2, -3));
        (Point point1, Point point2)? newLine = rectangle.IntersectLine(line.point1, line.point2);
        Assert.NotNull(newLine);
        Assert.Equal((new Point(2, 3), new Point(2, 0)), newLine);
    }
}