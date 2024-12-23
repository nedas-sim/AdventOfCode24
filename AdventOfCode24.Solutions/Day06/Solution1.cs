﻿using System.Diagnostics;

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

    public static void SolveExercise2()
    {
        string[] lines = File.ReadAllLines("../../../../AdventOfCode24.Solutions/Day06/task.txt");

        int width = lines[0].Length;
        int height = lines.Length;

        int totalCount = 0;

        Stopwatch sw = Stopwatch.StartNew();
        for (int obstacleIndex = 0; obstacleIndex < width * height; obstacleIndex++)
        {
            if (obstacleIndex % 500 == 0)
            {
                Console.WriteLine($"Iteration: {obstacleIndex + 1}");
            }

            Map map = new(lines);

            if (map.IsTileAt(obstacleIndex % width, obstacleIndex / width, '#', '^', 'v', '<', '>'))
            {
                continue;
            }

            map.SetTile(obstacleIndex % width, obstacleIndex / width, '#');

            int count = 0;
            while (map.MoveGuard())
            {
                count++;
                if (count > 100000) // shit solution, I'll take it
                {
                    totalCount++;
                    break;
                }
            }
        }
        sw.Stop();

        Console.WriteLine($"Ran for {sw.ElapsedMilliseconds}ms");

        Console.WriteLine(totalCount);
    }
}
