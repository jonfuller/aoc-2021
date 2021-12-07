namespace Aoc2021;

[Command(name: "day-07")]
public class Day07 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        console.Output.WriteLine(CheapestFuelToSamePosition("input/07_sample", TravelA));
        console.Output.WriteLine(CheapestFuelToSamePosition("input/07", TravelA));
        console.Output.WriteLine(CheapestFuelToSamePosition("input/07_sample", TravelB));
        console.Output.WriteLine(CheapestFuelToSamePosition("input/07", TravelB));
    }

    static int CheapestFuelToSamePosition(string inputFile, Func<int, int, int> fuelCalculator)
    {
        var positions = File.ReadAllText(inputFile).Split(",").Select(int.Parse).ToList();

        var max = positions.Max();
        var min = positions.Min();

        return EnumerableX
            .RangeFrom(min, max)
            .Select(depth => (depth, totalFuel: positions.Select(position => fuelCalculator(position, depth)).Sum()))
            .MinBy(keySelector: f => f.totalFuel)
            .totalFuel;
    }

    static int TravelA(int currentDepth, int destinationDepth) => Math.Abs(currentDepth - destinationDepth);
    static int TravelB(int currentDepth, int destinationDepth)
    {
        var n = Math.Abs(currentDepth - destinationDepth);

        return ((int)Math.Pow(n, 2) + n) / 2; // Sigma (g, g=1..n)
    }
}
