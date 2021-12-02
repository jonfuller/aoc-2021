namespace Aoc2021;

[Command(name: "day-02")]
public class Day02 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        var instructions = File.ReadAllLines("input/02_sample").Select(ParseInstructionLine);

        var currentPosition = new Position(0, 0);
        foreach(var instruction in instructions)
        {
            currentPosition = instruction.instruction switch
            {
                "forward" => currentPosition with { x = currentPosition.x + instruction.amount },
                "down" => currentPosition with { y = currentPosition.y + instruction.amount },
                "up" => currentPosition with { y = currentPosition.y - instruction.amount },
                _ => throw new Exception($"Invalid instruction: {instruction.instruction}")
            };
        }
        console.Output.WriteLine(currentPosition);

    }

    static (string instruction, int amount) ParseInstructionLine(string line)
    {
        var split = line.Split(" ");
        return (split[0], int.Parse(split[1]));
    }

    record Position(int x, int y){}
}

