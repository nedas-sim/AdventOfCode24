namespace AdventOfCode24.Solutions.Day08;

public class Solution1
{
    public static void SolveExercise1()
    {
        string[] lines = File.ReadAllLines("../../../../AdventOfCode24.Solutions/Day08/task.txt");

        Map map = new(lines);
        map.MarkAntinodes1();
        Console.WriteLine(map.CountAntinodes());
    }

    public static void SolveExercise2()
    {
        string[] lines = File.ReadAllLines("../../../../AdventOfCode24.Solutions/Day08/task.txt");

        Map map = new(lines);
        map.MarkAntinodes2();
        map.MarkMultipleFrequencyAntenas();
        Console.WriteLine(map.CountAntinodes());
    }
}

public class Map
{
    private readonly List<List<Tile>> _tiles;
    private readonly List<List<Tile>> _antenaGroups;

    public Map(string[] lines)
    {
        _tiles = lines
            .Select((line, y) => line.Select((c, x) => new Tile(x, y, c, false)).ToList())
            .ToList();

        _antenaGroups = _tiles
            .SelectMany(x => x)
            .Where(tile => tile.Value != '.')
            .GroupBy(tile => tile.Value)
            .Select(group => group.ToList())
            .ToList();
    }

    public void MarkAntinodes1()
    {
        foreach (List<Tile> antenas in _antenaGroups)
        {
            for (int firstAntenaIndex = 0; firstAntenaIndex < antenas.Count - 1; firstAntenaIndex++)
            {
                for (int secondAntenaIndex = firstAntenaIndex + 1; secondAntenaIndex < antenas.Count; secondAntenaIndex++)
                {
                    Tile firstAntena = antenas[firstAntenaIndex];
                    Tile secondAntena = antenas[secondAntenaIndex];

                    int deltaX = firstAntena.X - secondAntena.X;
                    int deltaY = firstAntena.Y - secondAntena.Y;

                    (int firstCheckX, int firstCheckY) = (firstAntena.X + deltaX, firstAntena.Y + deltaY);
                    (int secondCheckX, int secondCheckY) = (secondAntena.X - deltaX, secondAntena.Y - deltaY);

                    if (firstCheckY < 0 || firstCheckY >= _tiles.Count || firstCheckX < 0 || firstCheckX >= _tiles[0].Count)
                    {
                    }
                    else
                    {
                        _tiles[firstCheckY][firstCheckX] = _tiles[firstCheckY][firstCheckX] with
                        {
                            IsAntinode = true,
                        };
                    }

                    if (secondCheckY < 0 || secondCheckY >= _tiles.Count || secondCheckX < 0 || secondCheckX >= _tiles[0].Count)
                    {
                    }
                    else
                    {
                        _tiles[secondCheckY][secondCheckX] = _tiles[secondCheckY][secondCheckX] with
                        {
                            IsAntinode = true,
                        };
                    }
                }
            }
        }
    }

    public void MarkAntinodes2()
    {
        foreach (List<Tile> antenas in _antenaGroups)
        {
            for (int firstAntenaIndex = 0; firstAntenaIndex < antenas.Count - 1; firstAntenaIndex++)
            {
                for (int secondAntenaIndex = firstAntenaIndex + 1; secondAntenaIndex < antenas.Count; secondAntenaIndex++)
                {
                    Tile firstAntena = antenas[firstAntenaIndex];
                    Tile secondAntena = antenas[secondAntenaIndex];

                    int deltaX = firstAntena.X - secondAntena.X;
                    int deltaY = firstAntena.Y - secondAntena.Y;

                    int firstX = firstAntena.X;
                    int firstY = firstAntena.Y;
                    int secondX = secondAntena.X;
                    int secondY = secondAntena.Y;

                    while (true)
                    {
                        (int firstCheckX, int firstCheckY) = (firstX + deltaX, firstY + deltaY);

                        if (firstCheckY < 0 || firstCheckY >= _tiles.Count || firstCheckX < 0 || firstCheckX >= _tiles[0].Count)
                        {
                            break;
                        }

                        _tiles[firstCheckY][firstCheckX] = _tiles[firstCheckY][firstCheckX] with
                        {
                            IsAntinode = true,
                        };

                        firstX = firstCheckX;
                        firstY = firstCheckY;
                    }

                    while (true)
                    {
                        (int secondCheckX, int secondCheckY) = (secondX - deltaX, secondY - deltaY);

                        if (secondCheckY < 0 || secondCheckY >= _tiles.Count || secondCheckX < 0 || secondCheckX >= _tiles[0].Count)
                        {
                            break;
                        }

                        _tiles[secondCheckY][secondCheckX] = _tiles[secondCheckY][secondCheckX] with
                        {
                            IsAntinode = true,
                        };

                        secondX = secondCheckX;
                        secondY = secondCheckY;
                    }
                }
            }
        }
    }

    public void MarkMultipleFrequencyAntenas()
    {
        foreach (Tile antena in _antenaGroups.Where(group => group.Count > 1).SelectMany(x => x))
        {
            _tiles[antena.Y][antena.X] = _tiles[antena.Y][antena.X] with
            {
                IsAntinode = true,
            };
        }
    }

    public int CountAntinodes()
    {
        return _tiles.SelectMany(x => x).Count(tile => tile.IsAntinode);
    }
}

public record struct Tile(int X, int Y, char Value, bool IsAntinode);