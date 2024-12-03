var memory = File.ReadAllText("input.txt");

Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => SumOfMultiplications(memory),
    "part2" => EnabledMultiplications(memory).Sum(SumOfMultiplications),
    _ => "Please run with environment variable part=part1 or part=part2"
});

static long SumOfMultiplications(string memory) =>
    Regex.Matches(memory, @"mul\((\d{1,3}),(\d{1,3})\)").Select(x => long.Parse(x.Groups[1].Value) * long.Parse(x.Groups[2].Value)).Sum();

static IEnumerable<string> EnabledMultiplications(string memory) =>
    Regex.Matches($"do(){memory}", @"(do\(\))(.*?)(don't\(\)|$)", RegexOptions.Singleline).Select(x => x.Groups[2].Value);