namespace Aoc2021;

[Command(name: "day-05")]
public class Day05 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        var lineSegments = File.ReadAllLines("input/05_sample").Select(ParseLine).ToArray();

        lineSegments.ForEach(l => console.Output.WriteLine(l));
    }

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
    }
    record Point(int x, int y);
}
