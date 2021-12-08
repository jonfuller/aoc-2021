using System.Numerics;

namespace Aoc2021;


[Command(name: "day-06")]
public class Day06 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        console.Output.WriteLine(PartA("input/06_sample", 80));
        console.Output.WriteLine(PartA("input/06", 80));

        console.Output.WriteLine(PartB("input/06_sample", 256).ToString());
        console.Output.WriteLine(PartB("input/06", 256).ToString());

        int PartA(string filename, int numDays) => Enumerable.Range(0, numDays)
            .Aggregate(
                seed: ReadFish(filename),
                (fish, _) =>
                {
                    if (_ % 10 == 0) console.Output.WriteLine($"{_}");
                    return fish.SelectMany(f => f.Update()).ToList();
                })
            .Count;

        BigInteger PartB(string filename, int numDays)
        {
            var fish = ReadFish(filename);

            var fishByAge = Enumerable.Range(0, 9).Aggregate(
                seed: new Dictionary<int, BigInteger>(),
                (acc, i) =>
                {
                    acc.Add(i, new BigInteger(fish.Count(f => f.DaysLeft == i)));
                    return acc;
                });

            for(var day =0; day < numDays; day++)
            {
                UpdateFish();
            }
            
            return fishByAge.Values.Sum();

            void UpdateFish()
            {
                var toRestart = fishByAge[0];
                fishByAge[0] = fishByAge[1];
                fishByAge[1] = fishByAge[2];
                fishByAge[2] = fishByAge[3];
                fishByAge[3] = fishByAge[4];
                fishByAge[4] = fishByAge[5];
                fishByAge[5] = fishByAge[6];
                fishByAge[6] = fishByAge[7] + toRestart;
                fishByAge[7] = fishByAge[8];
                fishByAge[8] = toRestart;
            }
        }
    }

    static List<LanternFish> ReadFish(string filename) => File.ReadAllText(filename)
        .Split(',')
        .Select(int.Parse)
        .Select(i => new LanternFish { DaysLeft = i })
        .ToList();

    public class LanternFish
    {
        public int DaysLeft;

        public static int DaysToSpawn = 6;
        public static int NewbornDaysToSpawn = 8;
            
        public IEnumerable<LanternFish> Update()
        {
            if (DaysLeft == 0)
            {
                DaysLeft = DaysToSpawn;
                return new[] { this, new LanternFish { DaysLeft = NewbornDaysToSpawn } };
            }
            DaysLeft--;
            return new[] { this  };
        }
    }
}
