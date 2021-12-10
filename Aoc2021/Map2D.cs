namespace Aoc2021;

public record Map2D<T>
{
    private static readonly (int, int)[] NeighborDiffs = new[]
        {(-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1)};

    private readonly int _maxX;
    private readonly int _maxY;
    private readonly IList<Position<T>> _positions;
    private readonly Dictionary<Position<T>, IList<Position<T>>> _neighborCache;

    public IList<Position<T>> Positions
    {
        get => _positions;
        init
        {
            _positions = value;
            _maxX = _positions.Max(s => s.X);
            _maxY = _positions.Max(s => s.Y);
            var xyLookup = _positions
                .GroupBy(s => s.X, s => s)
                .ToDictionary(
                    grp => grp.Key,
                    grp => grp.OrderBy(i => i.Y).ToArray())
                .Select(x => x.Value)
                .ToArray();
            
            _neighborCache = _positions.ToDictionary(
                s => s,
                s => CalculateNeighborsFor(s, xyLookup));
        }
    }

    public IList<Position<T>> NeighborsFor(Position<T> position) => _neighborCache[position];

    IList<Position<T>> CalculateNeighborsFor(Position<T> seat, Position<T>[][] xyLookup)
    {
        return _().ToList();

        IEnumerable<Position<T>> _()
        {
            foreach (var (x, y) in NeighborDiffs)
            {
                var (newX, newY) = (seat.X + x, seat.Y + y);

                if (newX < 0) continue;
                if (newX > _maxX) continue;

                if (newY < 0) continue;
                if (newY > _maxY) continue;

                yield return xyLookup[newX][newY];
            }
        }
    }

    public override string ToString()
    {
       var rows = Positions.Chunk(_maxX+1).Select(row => string.Join(",", row.Select(c => c.Value)));
       return string.Join(Environment.NewLine, rows);
    }
}

public record Position(int X, int Y) {}
public record Position<T>(T Value, int X, int Y) : Position(X, Y);