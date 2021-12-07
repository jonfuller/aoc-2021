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
}
