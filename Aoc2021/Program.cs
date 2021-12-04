global using MoreLinq;
global using CliFx;
global using CliFx.Attributes;
global using CliFx.Infrastructure;

await new CliApplicationBuilder()
    .AddCommandsFromThisAssembly()
    .Build()
    .RunAsync(new string[] {$"day-{DateTime.Today:dd}"});
