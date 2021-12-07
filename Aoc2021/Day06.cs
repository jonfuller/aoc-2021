namespace Aoc2021;

[Command(name: "day-06")]
public class Day06 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        console.Output.WriteLine(PartA("input/06_sample"));
        console.Output.WriteLine(PartA("input/06"));
    }

    public int PartA(string filename) => Enumerable.Range(0, 80)
        .Aggregate(
            seed: ReadFish(filename).ToList(),
            (fish, _) => fish.SelectMany(f => f.Update()).ToList())
        .Count;

    static IEnumerable<LanternFish> ReadFish(string filename) => File.ReadAllText(filename)
        .Split(',')
        .Select(int.Parse)
        .Select(i => new LanternFish(DaysLeft: i, SpawnCount: 0));

    public record LanternFish(int DaysLeft, int SpawnCount = 0)
    {
        public static int DaysToSpawn = 6;
        public static int NewbornDaysToSpawn = 8;
            
        public IEnumerable<LanternFish> Update()
        {
            if (DaysLeft == 0)
                return new[] {
                    new LanternFish(DaysLeft: DaysToSpawn, SpawnCount: SpawnCount+1),
                    new LanternFish(DaysLeft: NewbornDaysToSpawn, SpawnCount: 0)};
            return new[] { this with { DaysLeft = DaysLeft - 1 } };
        }

        //public static LanternFish CreateAdultFish(int daysLeft) => new LanternFish(daysLeft, SpawnCount: 0);
        //public static LanternFish CreateNewbornFish() => new LanternFish(DaysLeft: 8, SpawnCount: 0);
    }
}
