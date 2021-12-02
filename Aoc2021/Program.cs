global using MoreLinq;

var depthsSample = File.ReadAllLines("input/01_sample").Select(int.Parse);
var depthsActual = File.ReadAllLines("input/01").Select(int.Parse);

Console.WriteLine(CountIncreases(depthsSample));
Console.WriteLine(CountIncreases(depthsActual));

static int CountIncreases(IEnumerable<int> depths) => depths
    .Pairwise((a, b) => b > a)
    .Count(x => x);

