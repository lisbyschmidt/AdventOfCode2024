var equations = File.ReadLines("input.txt").Select(x => Regex.Matches(x, @"\d+").Select(y => long.Parse(y.Value)).ToArray());

Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => equations.Where(x => EquationResults(x[1..], (a, b) => [a + b, a * b]).Contains(x[0])).Sum(x => x[0]),
    "part2" => equations.Where(x => EquationResults(x[1..], (a, b) => [a + b, a * b, long.Parse($"{a}{b}")]).Contains(x[0])).Sum(x => x[0]),
    _ => "Please run with environment variable part=part1 or part=part2"
});

static long[] EquationResults(long[] equation, Func<long, long, long[]> pairResults) =>
    equation.Length switch {
        1 => equation,
        _ => pairResults(equation[0], equation[1]).SelectMany(
            pairResult => EquationResults([pairResult, .. equation[2..]], pairResults)
        ).ToArray()
    };