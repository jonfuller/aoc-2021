namespace Aoc2021;

[Command(name: "day-05")]
public class Day05 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        var sampleLines = ParseLineSegments("input/05_sample");
        var sampleStraightLines = sampleLines
            .Where(l => l.IsHorizontal || l.IsVertical)
            .ToList();
        console.Output.WriteLine(MultipleOverlaps(sampleStraightLines));
        console.Output.WriteLine(MultipleOverlaps(sampleLines));

        var lines = ParseLineSegments("input/05");
        var straightLines = lines
            .Where(l => l.IsHorizontal || l.IsVertical)
            .ToList();
        console.Output.WriteLine(MultipleOverlaps(straightLines));
        console.Output.WriteLine(MultipleOverlaps(lines));
    }

    static int MultipleOverlaps(IEnumerable<LineSegment> lineSegments) => lineSegments
            .SelectMany(line => line.Points)
            .GroupBy(p => p)
            .Where(p => p.Count() > 1)
            .Count();

    static List<LineSegment> ParseLineSegments(string filename) => File
        .ReadAllLines(filename)
        .Select(ParseLine)
        .ToList();

    static LineSegment ParseLine(string rawLine)
    {
        var split = rawLine.Split(" ");
        return new LineSegment(ParsePoint(split[0]), ParsePoint(split[2]));

        static Point ParsePoint(string rawPoint)
        {
            var split = rawPoint.Split(",").Select(int.Parse).ToArray();
            return new Point(split[0], split[1]);
        }
    }

    record LineSegment(Point start, Point end)
    {
        public bool IsVertical => start.y == end.y;
        public bool IsHorizontal => start.x == end.x;

        public List<Point> Points
        {
            get
            {
                if (IsVertical)
                {
                    return EnumerableX.RangeFrom(start.x, end.x).Select(x => new Point(x, start.y)).ToList();
                }
                else if (IsHorizontal)
                {
                    return EnumerableX.RangeFrom(start.y, end.y).Select(y => new Point(start.x, y)).ToList();
                }
                else
                {
                    var xs = EnumerableX.RangeFrom(start.x, end.x);
                    var ys = EnumerableX.RangeFrom(start.y, end.y);

                    return xs.Zip(ys, (x, y) => new Point(x, y)).ToList();
                }
            }
        }
    }
    record Point(int x, int y);

    public static class EnumerableX
    {
        public static IEnumerable<int> RangeFrom(int start, int end)
        {
            if (start == end)
                return new[] {start};

            if (start < end)
            {
                return Enumerable.Range(start, end-start+1);
            }

            return Enumerable.Range(end, start - end + 1).Reverse().ToList();
        }
    }
}
