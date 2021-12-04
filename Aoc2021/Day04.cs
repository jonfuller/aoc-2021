namespace Aoc2021;

[Command(name: "day-04")]
public class Day04 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        PartA("input/04_sample");
        PartA("input/04");

        void PartA(string filename)
        {
            var (calls, boards) = ReadInputs(filename);

            var bingo = FindFirstBingo(calls.ToList(), boards.ToList(), console);
            
            console.Output.WriteLine(bingo.Score);
        }
    }

    static BingoBoard FindFirstBingo(IEnumerable<int> calls, IEnumerable<BingoBoard> boards, IConsole console)
    {
        foreach (var call in calls)
        {
            boards = boards.Select(b => b.Play(call)).ToList();
            var bingos = boards.Where(b => b.HasBingo).ToList();
            if (bingos.Any())
                return bingos.First();
        }
        throw new Exception("no bingo found");
    }

    static (IEnumerable<int> calls, IEnumerable<BingoBoard> boards) ReadInputs(string filename)
    {
        var lines = File.ReadAllLines(filename);

        return (ParseCallsLine(lines[0]), ParseBoards(lines.Skip(1)));

        static IEnumerable<int> ParseCallsLine(string callsLine) => callsLine.Split(",").Select(int.Parse);

        static IEnumerable<BingoBoard> ParseBoards(IEnumerable<string> boardLines) => boardLines
            .Chunk(6)
            .Select(lines => new BingoBoard(
                Cells: lines.Skip(1).Select(ParseBoardLine).SelectMany(x => x),
                Calls: Enumerable.Empty<int>()));

        static IEnumerable<BingoCell> ParseBoardLine(string boardLine, int rowNumber) => boardLine
            .Replace("  ", " ")
            .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select((value, i) => new BingoCell(int.Parse(value), i, rowNumber, false));
    }

    record BingoBoard(IEnumerable<BingoCell> Cells, IEnumerable<int> Calls)
    {
        public BingoBoard Play(int callNumber)
        {
            var otherCells = Cells.Where(c => c.Value != callNumber);
            var calledCells = Cells.Where(c => c.Value == callNumber);

            return this with {
                Calls = Calls.Append(element: callNumber).ToList(),
                Cells = otherCells.Concat(calledCells.Select(c => c with { Marked = true })).ToList()
            };
        }
        public IEnumerable<BingoCell> AllUnmarked => Cells.Where(c => !c.Marked);
        public int Score => AllUnmarked.Sum(c => c.Value) * Calls.Last();
        public bool HasBingo
        {
            get
            {
                var cols = Cells.GroupBy(c => c.Column);
                var rows = Cells.GroupBy(c => c.Row);

                return cols.Concat(rows).Any(grp => grp.All(c => c.Marked));
            }
        }

        public override string ToString()
        {
            var lines = Cells
                .OrderBy(c => c.Row)
                .ThenBy(c => c.Column)
                .GroupBy(c => c.Row)
                .Select(rowGrp => string.Join(",", rowGrp.Select(c => c.Marked ? " " : c.Value.ToString())));

            return string.Join(Environment.NewLine, lines);
        }
    }

    record BingoCell(int Value, int Column, int Row, bool Marked);
}
