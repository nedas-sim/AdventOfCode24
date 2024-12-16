using System.Diagnostics;

namespace AdventOfCode24.Solutions.Day06;

public class Solution2
{
    // Almost 10 times faster
    public static void SolveExercise2()
    {
        string[] lines = File.ReadAllLines("../../../../AdventOfCode24.Solutions/Day06/task.txt");

        int width = lines[0].Length;
        int height = lines.Length;

        int totalCount = 0;

        Console.WriteLine($"Will do {width * height} iterations");

        Stopwatch sw = Stopwatch.StartNew();
        Parallel.For(0, width * height, new ParallelOptions
        {
            MaxDegreeOfParallelism = Environment.ProcessorCount,
        }, (obstacleIndex) =>
        {
            Map map = new(lines);

            if (map.IsTileAt(obstacleIndex % width, obstacleIndex / width, '#', '^', 'v', '<', '>'))
            {
                return;
            }

            map.SetTile(obstacleIndex % width, obstacleIndex / width, '#');

            int count = 0;
            while (map.MoveGuard())
            {
                count++;
                if (count > 100000) // shit solution, I'll take it
                {
                    Interlocked.Increment(ref totalCount);
                    return;
                }
            }
        });
        sw.Stop();

        Console.WriteLine($"Ran for {sw.ElapsedMilliseconds}ms");
        Console.WriteLine(totalCount);
    }
}
