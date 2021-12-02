global using MoreLinq;

var depthsSample = File.ReadAllLines("input/01_sample").Select(int.Parse);
var depthsActual = File.ReadAllLines("input/01").Select(int.Parse);

PartA();
PartB();

void PartA()
{
    Console.WriteLine(CountIncreases(depthsSample));
    Console.WriteLine(CountIncreases(depthsActual));
}

void PartB()
{
    Console.WriteLine(CountWindowIncreases(3, depthsSample));
    Console.WriteLine(CountWindowIncreases(3, depthsActual));
}

static int CountIncreases(IEnumerable<int> depths) => depths
    .Pairwise((a, b) => b > a)
    .Count(x => x);

static int CountWindowIncreases(int windowSize, IEnumerable<int> depths) => depths
    .Window(windowSize)
    .Pairwise((a, b) => b.Sum() > a.Sum())
    .Count(x => x);