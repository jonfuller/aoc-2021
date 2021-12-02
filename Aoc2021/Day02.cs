namespace Aoc2021;

[Command(name: "day-02")]
public class Day02 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        // PartA("input/02_sample");
        // PartA("input/02");
        PartB("input/02_sample");
        PartB("input/02");

        void PartA(string filename)
        {
            var instructions = File.ReadAllLines(filename).Select(ParseInstructionLine);
            var finalPosition = Navigate(new Position(0, 0), instructions);
            console.Output.WriteLine(filename);
            console.Output.WriteLine(finalPosition);
            console.Output.WriteLine(finalPosition.x * finalPosition.y);
        }

        void PartB(string filename)
        {
            var instructions = File.ReadAllLines(filename).Select(ParseInstructionLine);
            var finalPosition = Navigate2(new SubConfig(new Position(0, 0), 0), instructions);
            console.Output.WriteLine(filename);
            console.Output.WriteLine(finalPosition);
            console.Output.WriteLine(finalPosition.x * finalPosition.y);
        }

        static Position Navigate(Position currentPosition, IEnumerable<(string direction, int amount)> instructions)
        {
            foreach(var instruction in instructions)
            {
                currentPosition = instruction.direction switch
                {
                    "forward" => currentPosition with { x = currentPosition.x + instruction.amount },
                    "down" => currentPosition with { y = currentPosition.y + instruction.amount },
                    "up" => currentPosition with { y = currentPosition.y - instruction.amount },
                    _ => throw new Exception($"Invalid instruction: {instruction.direction}")
                };
            }
            return currentPosition;
        }

        static Position Navigate2(SubConfig config, IEnumerable<(string direction, int amount)> instructions)
        {
            foreach(var instruction in instructions)
            {
                config = instruction.direction switch
                {
                    "forward" => config with { pos = new Position(config.pos.x + instruction.amount, config.pos.y + (config.aim * instruction.amount)) },
                    "down" => config with { aim = config.aim + instruction.amount },
                    "up" => config with { aim = config.aim - instruction.amount },
                    _ => throw new Exception($"Invalid instruction: {instruction.direction}")
                };
            }
            return config.pos;
        }
    }

    static (string direction, int amount) ParseInstructionLine(string line)
    {
        var split = line.Split(" ");
        return (split[0], int.Parse(split[1]));
    }

    record SubConfig(Position pos, int aim){}
    record Position(int x, int y){}
}

