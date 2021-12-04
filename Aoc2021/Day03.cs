namespace Aoc2021;

[Command(name: "day-03")]
public class Day03 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        PartA("input/03_sample");
        PartA("input/03");

        void PartA(string filename)
        {
            var binaries = File.ReadAllLines(filename).Select(ParseLine).ToList();
            var (gamma, epsilon) = DetermineFactors(binaries);

            console.Output.WriteLine(gamma * epsilon);
        }
    }

    private static (int gamma, int epsilon) DetermineFactors(List<List<int>> digits)
    {
        var gamma = new List<int>();
        var epsilon = new List<int>();

        for (int i=0; i<digits.First().Count; i++)
        {
            if (MostCommon(digits, i) == 0)
            {
                gamma.Add(0);
                epsilon.Add(1);
            }
            else
            {
                gamma.Add(1);
                epsilon.Add(0);
            }
        }

        return (BinaryStringToInt(IntSeqToString(gamma)), BinaryStringToInt(IntSeqToString(epsilon)));
    }

    private static int MostCommon(List<List<int>> digits, int digitNumber) =>
        digits.Select(b => b[digitNumber]).Sum() < digits.Count/2
            ? 0
            : 1;
    private static int BinaryStringToInt(string binaryString) => Convert.ToInt32(binaryString, 2);
    private static string IntSeqToString(IEnumerable<int> seq) => string.Join("", seq.Select(i => i.ToString()).ToArray());
    private static List<int> ParseLine(string line) => line.Select(c => int.Parse(new string(c, 1))).ToList();
}
