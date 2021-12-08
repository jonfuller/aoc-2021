using System.Numerics;

namespace Aoc2021;

public static class EnumerableX
{
    public static IEnumerable<int> RangeFrom(int start, int end)
    {
        if (start == end)
            return new[] {start};

        if (start < end)
        {
            return Enumerable.Range(start, end-start+1);
        }

        return Enumerable.Range(end, start - end + 1).Reverse().ToList();
    }

    public static BigInteger Sum(this IEnumerable<BigInteger> target) => target.Aggregate(
        seed: new BigInteger(0),
        func: (acc, v) => acc + v);
}
