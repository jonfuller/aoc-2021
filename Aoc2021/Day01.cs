namespace Aoc2021;

[Command(name: "day-01")]
public class Day01 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        var depthsSample = File.ReadAllLines("input/01_sample").Select(int.Parse);
        var depthsActual = File.ReadAllLines("input/01").Select(int.Parse);

        PartA();
        PartB();

        void PartA()
        {
            console.Output.WriteLine(CountWindowIncreases(1, depthsSample));
            console.Output.WriteLine(CountWindowIncreases(1, depthsActual));
        }

        void PartB()
        {
            console.Output.WriteLine(CountWindowIncreases(3, depthsSample));
            console.Output.WriteLine(CountWindowIncreases(3, depthsActual));
        }

        static int CountWindowIncreases(int windowSize, IEnumerable<int> depths) => depths
            .Window(windowSize)
            .Pairwise((a, b) => b.Sum() > a.Sum())
            .Count(x => x);
    }
}
