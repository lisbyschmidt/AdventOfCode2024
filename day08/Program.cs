using Location = (int Row, int Col);

var map = File.ReadLines("input.txt").Select(x => x.ToArray()).ToArray();
var freqGroups = Enumerable.Range(0, map.Length).SelectMany(row => Enumerable.Range(0, map[row].Length)
    .Select(col => (row, col))).GroupBy(loc => map[loc.row][loc.col]).Where(group => group.Key != '.').ToArray();

Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => UniqueLocationCount((fromAnt, toAnt) => new[] { AntiNode(fromAnt, toAnt) }.Where(IsInBounds)),
    "part2" => UniqueLocationCount((fromAnt, toAnt) => InfiniteAntiNodes(fromAnt, toAnt).TakeWhile(IsInBounds)),
    _ => "Please run with environment variable part=part1 or part=part2"
});

int UniqueLocationCount(Func<Location, Location, IEnumerable<Location>> antiNodesFunc) =>
    freqGroups.SelectMany(freqGroup =>
        freqGroup.SelectMany(fromAnt =>
            freqGroup.Except([fromAnt]).SelectMany(toAnt =>
                antiNodesFunc(fromAnt, toAnt)
            ))).Distinct().Count();

static Location AntiNode(Location from, Location to, int factor = 2) =>
    (from.Row + factor * (to.Row - from.Row), from.Col + factor * (to.Col - from.Col));

static IEnumerable<Location> InfiniteAntiNodes(Location from, Location to, int factor = 1)
    { while (true) { yield return AntiNode(from, to, factor); factor++; } }

bool IsInBounds(Location loc) =>
    loc.Row >= 0 && loc.Row < map.Length && loc.Col >= 0 && loc.Col < map[loc.Row].Length;