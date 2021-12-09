namespace Aoc2021;

[Command(name: "day-08")]
public class Day08 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        var samples = File.ReadAllLines("input/08_sample").Select(ParseLine).ToList();
        var actuals = File.ReadAllLines("input/08").Select(ParseLine).ToList();

        console.Output.WriteLine(PartA(samples));
        console.Output.WriteLine(PartA(actuals));

        static int PartA(List<SegmentLine> lines)
        {
            return lines
                .Select(line => line.outputs.Count(o => IsUniqueSegments(o.Length)))
                .Sum();
        }

        static bool IsUniqueSegments(int segmentCount) => segmentCount == 2
            || segmentCount == 4
            || segmentCount == 3
            || segmentCount ==7;
    }

    static SegmentLine ParseLine(string line)
    {
        var splits = line.Split("|", System.StringSplitOptions.RemoveEmptyEntries | System.StringSplitOptions.TrimEntries);

        return new SegmentLine(splits[0].Split(" ").ToList(), splits[1].Split(" ").ToList());
    }
}

public record SegmentLine(List<string> inputs, List<string> outputs);
