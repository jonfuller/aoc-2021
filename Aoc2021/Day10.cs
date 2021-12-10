namespace Aoc2021;

[Command(name: "day-10")]
public class Day10 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        // samples
        // console.Output.WriteLine(ParseLine("{([(<{}[<>[]}>{[]{[(<()>"));
        // console.Output.WriteLine(ParseLine("[[<[([]))<([[{}[[()]]]"));
        // console.Output.WriteLine(ParseLine("[{[{({}]{}}([{[{{{}}([]"));
        // console.Output.WriteLine(ParseLine("[<(<(<(<{}))><([]([]()"));
        // console.Output.WriteLine(ParseLine("<{([([[(<>()){}]>(<<{{"));

        console.Output.WriteLine(PartA("input/10_sample"));
        console.Output.WriteLine(PartA("input/10"));

        int PartA(string filename) => File
            .ReadAllLines(filename)
            .Select(ParseLine)
            .Where(l => !l.IsValid)
            .Select(s => ScoreFor(s.InvalidChar))
            .Sum();
    }

    static int ScoreFor(char closer) => _pairs.Single(p => p.closer == closer).score;
    static ParsedLine ParseLine(string line)
    {
        var stack = new Stack<char>();
        foreach(var element in line)
        {
            if (IsOpener(element))
                stack.Push(element);
            else
            {
                if (stack.Peek() == OpenerFor(element))
                    stack.Pop();
                else
                    return new ParsedLine(IsValid: false, InvalidChar: element, ExpectedChar: CloserFor(stack.Peek()), RawLine: line);
            }

        }
        return new ParsedLine(true, (char)0, (char)0, line);

        bool IsOpener(char element) => _pairs.Select(p => p.opener).Contains(element);
        char OpenerFor(char closer) => _pairs.Single(p => p.closer == closer).opener;
        char CloserFor(char opener) => _pairs.Single(p => p.opener == opener).closer;
    }

    static List<(char opener, char closer, int score)> _pairs = new List<(char opener, char closer, int score)>{
        (opener: '(', closer: ')', score: 3),
        (opener: '[', closer: ']', score: 57),
        (opener: '{', closer: '}', score: 1197),
        (opener: '<', closer: '>', score: 25137),
    };
    public record ParsedLine(bool IsValid, char InvalidChar, char ExpectedChar, string RawLine){}
}
