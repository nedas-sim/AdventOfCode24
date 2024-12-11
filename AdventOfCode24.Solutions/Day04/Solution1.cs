namespace AdventOfCode24.Solutions.Day04;

public class Solution1
{
    public static void SolveExercise1()
    {
        List<List<char>> wordMatrix = File.ReadAllLines("../../../../AdventOfCode24.Solutions/Day04/task.txt")
            .Select(line => line.Select(ch => ch).ToList())
            .ToList();

        List<(int, int)> xLocations = wordMatrix
            .Index()
            .Select(tuple => tuple.Item.Index().Where(ch => ch.Item == 'X').Select(ch => (ch.Index, tuple.Index)))
            .SelectMany(x => x)
            .ToList()
            ;

        int totalXmasCount = xLocations
            .Select(xLocation => GetXmasCountForX(wordMatrix, xLocation))
            .Sum();

        Console.WriteLine(totalXmasCount);
    }

    public static void SolveExercise2()
    {
        List<List<char>> wordMatrix = File.ReadAllLines("../../../../AdventOfCode24.Solutions/Day04/task.txt")
            .Select(line => line.Select(ch => ch).ToList())
            .ToList();

        List<(int, int)> aLocations = wordMatrix
            .Index()
            .Select(tuple => tuple.Item.Index().Where(ch => ch.Item == 'A').Select(ch => (ch.Index, tuple.Index)))
            .SelectMany(x => x)
            .ToList()
            ;

        int totalMasCount = aLocations
            .Select(xLocation => GetMasCountForA(wordMatrix, xLocation))
            .Sum();

        Console.WriteLine(totalMasCount);
    }

    private static int GetXmasCountForX(List<List<char>> wordMatrix, (int, int) originalXLocation)
    {
        List<(int, int)> directions = [
            (-1, -1),
            (-1, 0),
            (-1, 1),
            (0, -1),
            (0, 1),
            (1, -1),
            (1, 0),
            (1, 1),
        ];

        int total = 0;

        foreach ((int, int) direction in directions)
        {
            (int, int) xLocation = originalXLocation;

            // Already know that it is X
            if (GoInDirectionAndCheck('M', ref xLocation, direction, wordMatrix) &&
                GoInDirectionAndCheck('A', ref xLocation, direction, wordMatrix) &&
                GoInDirectionAndCheck('S', ref xLocation, direction, wordMatrix))
            {
                total++;
            }
        }

        return total;
    }

    private static int GetMasCountForA(List<List<char>> wordMatrix, (int, int) originalALocation)
    {
        List<(int, int)> directions = [
            (-1, -1),
            (1, -1),
        ];

        foreach ((int, int) direction in directions)
        {
            (int, int) oppositeDirection = (-direction.Item1, -direction.Item2);

            bool oneWay = IsCharInThatDirection('M', originalALocation, direction, wordMatrix) &&
                          IsCharInThatDirection('S', originalALocation, oppositeDirection, wordMatrix);

            bool otherWay = IsCharInThatDirection('S', originalALocation, direction, wordMatrix) &&
                            IsCharInThatDirection('M', originalALocation, oppositeDirection, wordMatrix);

            if (!oneWay && !otherWay)
            {
                return 0;
            }
        }

        return 1;
    }

    private static bool GoInDirectionAndCheck(char toCheck, ref (int, int) xLocation, (int, int) direction, List<List<char>> wordMatrix)
    {
        int width = wordMatrix[0].Count;
        int height = wordMatrix.Count;

        xLocation = (xLocation.Item1 + direction.Item1, xLocation.Item2 + direction.Item2);

        if (xLocation.Item1 < 0 || xLocation.Item1 >= width || xLocation.Item2 < 0 || xLocation.Item2 >= height)
        {
            return false;
        }

        if (wordMatrix[xLocation.Item2][xLocation.Item1] != toCheck)
        {
            return false;
        }

        return true;
    }

    private static bool IsCharInThatDirection(char toCheck, (int, int) location, (int, int) direction, List<List<char>> wordMatrix)
    {
        int width = wordMatrix[0].Count;
        int height = wordMatrix.Count;

        location = (location.Item1 + direction.Item1, location.Item2 + direction.Item2);

        if (location.Item1 < 0 || location.Item1 >= width || location.Item2 < 0 || location.Item2 >= height)
        {
            return false;
        }

        return wordMatrix[location.Item2][location.Item1] == toCheck;
    }
}
