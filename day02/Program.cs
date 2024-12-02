var reports = File.ReadLines("input.txt").Select(reportString => reportString.Split().Select(long.Parse).ToArray());

Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => reports.Select(ReportIsSafe).Count(isSafe => isSafe),
    "part2" => reports.Select(
        levels => levels.Select((_, skipIndex) => ReportIsSafe([..levels[..skipIndex], ..levels[(skipIndex + 1)..]])).Any(isSafe => isSafe)
    ).Count(isSafe => isSafe),
    _ => "Please run with environment variable part=part1 or part=part2"
});

static bool ReportIsSafe(long[] reportLevels) {
    var diffGroups = Enumerable.Range(0, reportLevels.Length - 1).Select(i => LevelDiff(reportLevels[i], reportLevels[i + 1])).GroupBy(diff => diff).ToArray();
    return diffGroups.Length == 1 && diffGroups[0].Key != Diff.UnsafeDiff;
}

static Diff LevelDiff(long a, long b) => (b - a) switch {
    >= 1 and <= 3 => Diff.SafeIncreaseDiff,
    <= -1 and >= -3 => Diff.SafeDecreaseDiff,
    _ => Diff.UnsafeDiff
};

enum Diff { UnsafeDiff, SafeIncreaseDiff, SafeDecreaseDiff }