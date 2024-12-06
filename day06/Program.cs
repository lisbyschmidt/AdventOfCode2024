using Position = (int Row, int Col);

var matrix = File.ReadLines("input.txt").Select(x => x.ToArray()).ToArray();
Position[] positions = Enumerable.Range(0, matrix.Length).SelectMany(row => Enumerable.Range(0, matrix[row].Length).Select(col => (row, col))).ToArray();

Console.WriteLine("C#\n" + Environment.GetEnvironmentVariable("part") switch {
    "part1" => VisitedPositions(ObstructedPositions())!.Count(),
    "part2" => VisitedPositions(ObstructedPositions())!.Count(p => VisitedPositions(ObstructedPositions(p)) is null),
    _ => "Please run with environment variable part=part1 or part=part2"
});

ISet<Position>? VisitedPositions(ISet<Position> obstructedPositions) {
    var guard = Guard.Create(StartPosition(), Direction.Up);
    var guardStates = new HashSet<Guard>();
    while (IsInBounds(guard.Position())) {
        if (!guardStates.Add(guard)) return null; // Loop detected
        guard = obstructedPositions.Contains(guard.StepForward().Position()) ? guard.TurnRight() : guard.StepForward();
    }
    return guardStates.Select(x => x.Position()).ToHashSet();
}
Position StartPosition() => positions.Single(p => matrix[p.Row][p.Col] == '^');
ISet<Position> ObstructedPositions(params Position[] extraObstructions) => positions.Where(p => matrix[p.Row][p.Col] == '#').Concat(extraObstructions).ToHashSet();
bool IsInBounds(Position p) => p.Row >= 0 && p.Row < matrix.Length && p.Col >= 0 && p.Col < matrix[p.Row].Length;

enum Direction { Up, Down, Left, Right }
record Guard(int Row, int Col, Direction Direction) {
    public static Guard Create(Position startPos, Direction startDir) => new(startPos.Row, startPos.Col, startDir);
    public Position Position() => (Row, Col);
    public Guard StepForward() => Direction switch {
        Direction.Up => this with { Row = Row - 1 },
        Direction.Down => this with { Row = Row + 1 },
        Direction.Left => this with { Col = Col - 1 },
        Direction.Right => this with { Col = Col + 1 }
    };
    public Guard TurnRight() => Direction switch {
        Direction.Up => this with { Direction = Direction.Right },
        Direction.Down => this with { Direction = Direction.Left },
        Direction.Left => this with { Direction = Direction.Up },
        Direction.Right => this with { Direction = Direction.Down }
    };
}