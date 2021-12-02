namespace Aoc2021;

[Command(name: "day-02")]
public class Day02 : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        PartA("input/02_sample");
        PartA("input/02");
        PartB("input/02_sample");
        PartB("input/02");

        void PartA(string filename)
        {
            var instructions = File.ReadAllLines(filename).Select(ParseInstructionLine);
            var finalPosition = Navigate(new Position(0, 0), instructions);
            console.Output.WriteLine($"Part A - {filename}");
            console.Output.WriteLine(finalPosition);
            console.Output.WriteLine(finalPosition.x * finalPosition.y);
        }

        void PartB(string filename)
        {
            var instructions = File.ReadAllLines(filename).Select(ParseInstructionLine);
            var finalConfig = Navigate2(new SubConfig(new Position(0, 0), 0), instructions);
            console.Output.WriteLine($"Part B - {filename}");
            console.Output.WriteLine(finalConfig);
            console.Output.WriteLine(finalConfig.pos.x * finalConfig.pos.y);
        }

        static Position Navigate(Position currentPosition, IEnumerable<Instruction> instructions) => instructions
            .Aggregate(currentPosition,
                (pos, instruction) => instruction.direction switch
                {
                    "forward" => pos with { x = pos.x + instruction.amount },
                    "down" => pos with { y = pos.y + instruction.amount },
                    "up" => pos with { y = pos.y - instruction.amount },
                    _ => throw new Exception($"Invalid instruction: {instruction.direction}")
                });

        static SubConfig Navigate2(SubConfig config, IEnumerable<Instruction> instructions) => instructions
            .Aggregate(config,
                (cfg, instruction) => instruction.direction switch
                {
                    "forward" => cfg with {
                        pos = new Position(cfg.pos.x + instruction.amount, cfg.pos.y + (cfg.aim * instruction.amount)) },
                    "down" => cfg with { aim = cfg.aim + instruction.amount },
                    "up" => cfg with { aim = cfg.aim - instruction.amount },
                    _ => throw new Exception($"Invalid instruction: {instruction.direction}")
                });
    }

    static Instruction ParseInstructionLine(string line)
    {
        var split = line.Split(" ");
        return new Instruction(split[0], int.Parse(split[1]));
    }

    record Instruction(string direction, int amount);
    record SubConfig(Position pos, int aim);
    record Position(int x, int y);
}

