namespace Aoc2021;

[Command(name: "day-03")]
public class Day03 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        PartA("input/03_sample");
        PartA("input/03");
        PartB("input/03_sample");
        PartB("input/03");

        void PartA(string filename)
        {
            var binaries = File.ReadAllLines(filename).Select(ParseLine).ToList();
            var (gamma, epsilon) = DetermineFactors(binaries);

            console.Output.WriteLine(gamma * epsilon);
        }

        void PartB(string filename)
        {
            var binaries = File.ReadAllLines(filename).Select(ParseLine).ToList();

            var ratings = DetermineRatings(binaries);

            console.Output.WriteLine(ratings.co2Rating * ratings.oxygenRating);
        }
    }

    static (int oxygenRating, int co2Rating) DetermineRatings(List<List<int>> numbers) =>
        (OxygenRating(numbers), CO2ScrubberRating(numbers));
    static int CO2ScrubberRating(List<List<int>> numbers)
    {
        var numDigits = numbers.First().Count;

        var numbersLeft = numbers;
        for (var i=0; i<numDigits && numbersLeft.Count > 1; i++)
        {
            numbersLeft = DetermineValidCO2NumbersForDigit(numbersLeft, i);
        }

        return BinaryStringToInt(IntSeqToString(numbersLeft.Single()));
    }
    static int OxygenRating(List<List<int>> numbers)
    {
        var numDigits = numbers.First().Count;

        var numbersLeft = numbers;
        for (var i=0; i<numDigits && numbersLeft.Count > 1; i++)
        {
            numbersLeft = DetermineValidOxygenNumbersForDigit(numbersLeft, i);
        }

        return BinaryStringToInt(IntSeqToString(numbersLeft.Single()));
    }
    private static List<List<int>> DetermineValidOxygenNumbersForDigit(List<List<int>> numbers, int digit)
    {
        var mostCommon = MostCommonDigit(numbers, digit);

        return numbers.Where(x => x[digit] == mostCommon).ToList();
    }
    private static List<List<int>> DetermineValidCO2NumbersForDigit(List<List<int>> numbers, int digit)
    {
        var leastCommon = Math.Abs(MostCommonDigit(numbers, digit) - 1);

        return numbers.Where(x => x[digit] == leastCommon).ToList();
    }

    private static (int gamma, int epsilon) DetermineFactors(List<List<int>> numbers)
    {
        var gamma = new List<int>();
        var epsilon = new List<int>();

        for (int i=0; i<numbers.First().Count; i++)
        {
            if (MostCommonDigit(numbers, i) == 0)
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

    private static int MostCommonDigit(List<List<int>> numbers, int digitNumber)
    {
        var allOfDigit = numbers.Select(number => number[digitNumber]).ToList();
        var zeroes = allOfDigit.Count(d => d == 0);
        var ones = allOfDigit.Count(d => d == 1);
        
        if (zeroes == ones) return 1;
        if (zeroes > ones) return 0;
        return 1;
    }
    private static int BinaryStringToInt(string binaryString) => Convert.ToInt32(binaryString, 2);
    private static string IntSeqToString(IEnumerable<int> seq) => string.Join("", seq.Select(i => i.ToString()).ToArray());
    private static List<int> ParseLine(string line) => line.Select(c => int.Parse(new string(c, 1))).ToList();
}
