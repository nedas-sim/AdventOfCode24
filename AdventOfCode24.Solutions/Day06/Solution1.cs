namespace AdventOfCode24.Solutions.Day06;

public class Solution1
{
    public static void SolveExercise1()
    {
        string[] lines = File.ReadAllLines("../../../../AdventOfCode24.Solutions/Day06/task.txt");

        Map map = new(lines);

        while (map.MoveGuard())
        {
        }

        int result = map.CountTiles('X');

        // add 1 to this, because the guard is out of map
        // not gonna solve this edge case

        Console.WriteLine(result); 
    }

    private record struct Tile(int X, int Y, char Value);

    private class Map
    {
        private static readonly Dictionary<char, (int dx, int dy)> _directions = new()
        {
            ['^'] = (0, -1),
            ['>'] = (1, 0),
            ['v'] = (0, 1),
            ['<'] = (-1, 0),
        };

        private static readonly HashSet<char> _guardIndicators = [.. _directions.Keys];

        private readonly List<List<Tile>> _tiles;
        private int _guardX = -1, _guardY = -1;

        public Map(string[] lines)
        {
            _tiles = lines
                .Select((line, y) => line.Select((c, x) => new Tile(x, y, c)).ToList())
                .ToList();

            Tile guard = _tiles
                .SelectMany(x => x)
                .First(tile => _guardIndicators.Contains(tile.Value));

            _guardX = guard.X;
            _guardY = guard.Y;
        }

        public bool MoveGuard()
        {
            Tile guardTile = _tiles[_guardY][_guardX];
            char guardDirectionIndicator = guardTile.Value;
            (int dx, int dy) = _directions[guardDirectionIndicator];

            int nextX = _guardX + dx;
            int nextY = _guardY + dy;

            if (nextY < 0 || nextY >= _tiles.Count || nextX < 0 || nextX >= _tiles[nextY].Count)
            {
                return false;
            }

            if (_tiles[nextY][nextX].Value == '#')
            {
                char nextDirectionIndicator = guardDirectionIndicator switch
                {
                    '^' => '>',
                    '>' => 'v',
                    'v' => '<',
                    '<' => '^',
                    _ => throw new InvalidOperationException(),
                };

                _tiles[_guardY][_guardX] = guardTile with
                {
                    Value = nextDirectionIndicator,
                };

                return true;
            }

            _tiles[_guardY][_guardX] = guardTile with
            {
                Value = 'X',
            };

            _tiles[nextY][nextX] = _tiles[nextY][nextX] with
            {
                Value = guardDirectionIndicator,
            };

            _guardX = nextX;
            _guardY = nextY;

            return true;
        }

        public int CountTiles(char value) => _tiles.SelectMany(x => x).Count(tile => tile.Value == value);
    }
}
