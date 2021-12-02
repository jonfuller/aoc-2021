namespace Aoc2021;

[Command(name: "day-N")]
public class DayN : ICommand
{
    public async ValueTask ExecuteAsync(IConsole console)
    {
        console.Output.WriteLine(this.GetType().Name);
    }
}
