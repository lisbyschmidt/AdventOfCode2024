using Index = (int Row, int Col);

var charMatrix = File.ReadLines("input.txt").Select(x => x.ToArray()).ToArray();

Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => IndicesWithLetter('X').Select(fromX =>
        new[] { North(fromX, 3), South(fromX, 3), West(fromX, 3), East(fromX, 3), NorthWest(fromX, 3), SouthEast(fromX, 3), NorthEast(fromX, 3), SouthWest(fromX, 3) }
            .Select(IndicesAsString).Count(x => x == "MAS")).Sum(),
    "part2" => IndicesWithLetter('A').Select(fromA =>
        new[] { NorthWest(fromA, 1), SouthEast(fromA, 1), NorthEast(fromA, 1), SouthWest(fromA, 1) }
            .Select(IndicesAsString).ToArray() is ["M", "S", "M", "S"] or ["M", "S", "S", "M"] or ["S", "M", "M", "S"] or ["S", "M", "S", "M"] ? 1 : 0).Sum(),
    _ => "Please run with environment variable part=part1 or part=part2"
});

IEnumerable<Index> North(Index i, int distance) => Enumerable.Range(1, distance).Select(x => (i.Row + x, i.Col));
IEnumerable<Index> South(Index i, int distance) => Enumerable.Range(1, distance).Select(x => (i.Row - x, i.Col));
IEnumerable<Index> West(Index i, int distance) => Enumerable.Range(1, distance).Select(x => (i.Row, i.Col - x));
IEnumerable<Index> East(Index i, int distance) => Enumerable.Range(1, distance).Select(x => (i.Row, i.Col + x));
IEnumerable<Index> NorthWest(Index i, int distance) => Enumerable.Range(1, distance).Select(x => (i.Row + x, i.Col - x));
IEnumerable<Index> SouthEast(Index i, int distance) => Enumerable.Range(1, distance).Select(x => (i.Row - x, i.Col + x));
IEnumerable<Index> NorthEast(Index i, int distance) => Enumerable.Range(1, distance).Select(x => (i.Row + x, i.Col + x));
IEnumerable<Index> SouthWest(Index i, int distance) => Enumerable.Range(1, distance).Select(x => (i.Row - x, i.Col - x));

string IndicesAsString(IEnumerable<Index> indices) => new string(indices.Where(IsInBounds).Select(x => charMatrix[x.Row][x.Col]).ToArray());

bool IsInBounds(Index i) => i is { Row: >= 0 and < 140, Col: >= 0 and < 140 };

IEnumerable<Index> IndicesWithLetter(char letter) {
    for (var i = 0; i < 140; i++)
    for (var j = 0; j < 140; j++)
        if (charMatrix[i][j] == letter)
            yield return (i, j);
}