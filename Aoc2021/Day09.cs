namespace Aoc2021;

[Command(name: "day-09")]
public class Day09 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        var sampleMap = LoadMap(File.ReadAllLines("input/09_sample"));
        var actualMap = LoadMap(File.ReadAllLines("input/09"));

        PartA(sampleMap);
        PartA(actualMap);

        void PartA(Map2D<int> map)
        {
            var lowPoints = map.Positions.Where(pos => IsLowPoint(pos, map.NeighborsFor(pos))).ToList();

            console.Output.WriteLine(RiskLevel(lowPoints));
        }
    }

    static int RiskLevel(IList<Position<int>> positions) => positions.Sum(p => p.Value + 1);
    static bool IsLowPoint(Position<int> position,  IList<Position<int>> neighbors) =>
        position.Value <= neighbors.MinBy(keySelector: n => n.Value).Value;

    static Map2D<int> LoadMap(IEnumerable<string> lines)
    {
        var positions = lines.SelectMany((line, rowNum) => line
            .Select(c => int.Parse(c.ToString()))
            .Select((val, colNum) => new Position<int>(val, colNum, rowNum)))
            .ToList();
            
        return new Map2D<int>(){ Positions = positions };
    }
}