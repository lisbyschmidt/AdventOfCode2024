var (left, right) = File.ReadLines("input.txt").Aggregate(seed: (Array.Empty<long>(), Array.Empty<long>()),
    (arrays, line) => ([..arrays.Item1, long.Parse(line[..5])], [..arrays.Item2, long.Parse(line[^5..])]));

Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => Distance(),
    "part2" => Similarity(),
    _ => "Please run with environment variable part=part1 or part=part2"
});

long Distance() => left.Order().Zip(right.Order(), (leftValue, rightValue) => Math.Abs(leftValue - rightValue)).Sum();

long Similarity() {
    var rightAppearances = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
    return left.Select(leftValue => leftValue * rightAppearances.GetValueOrDefault(leftValue, 0)).Sum();
}