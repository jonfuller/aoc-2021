global using MoreLinq;

var depthsSample = File.ReadAllLines("input/01_sample").Select(int.Parse);
var depthsActual = File.ReadAllLines("input/01").Select(int.Parse);

PartA();
PartB();

void PartA()
{
    Console.WriteLine(CountWindowIncreases(1, depthsSample));
    Console.WriteLine(CountWindowIncreases(1, depthsActual));
}

void PartB()
{
    Console.WriteLine(CountWindowIncreases(3, depthsSample));
    Console.WriteLine(CountWindowIncreases(3, depthsActual));
}

static int CountWindowIncreases(int windowSize, IEnumerable<int> depths) => depths
    .Window(windowSize)
    .Pairwise((a, b) => b.Sum() > a.Sum())
    .Count(x => x);