namespace AdventOfCode24.Solutions.Day02;

public class Solution1
{
    public static void SolveExercise1()
    {
        int safeReportCount = File
            .ReadAllLines("../../../../AdventOfCode24.Solutions/Day02/task.txt")
            .Select(inputLine => inputLine.Split(' '))
            .Select(numsAsStrings => numsAsStrings.Select(int.Parse).ToList())
            .Select(levels =>
            {
                List<int> deltas = levels
                    .Take(levels.Count - 1)
                    .Zip(levels.Skip(1))
                    .Select(consecutivePair => consecutivePair.First - consecutivePair.Second)
                    .ToList();

                bool allIncreasing = deltas.All(delta => delta < 0);
                bool allDecreasing = deltas.All(delta => delta > 0);
                bool maxValueIsAtLeast3 = deltas.All(delta => Math.Abs(delta) <= 3);

                return (allIncreasing || allDecreasing) && maxValueIsAtLeast3;
            })
            .Count(isValid => isValid);

        Console.WriteLine(safeReportCount);
    }

    public static void SolveExercise2()
    {
        int safeReportCount = File
            .ReadAllLines("../../../../AdventOfCode24.Solutions/Day02/task.txt")
            .Select(inputLine => inputLine.Split(' '))
            .Select(numsAsStrings => numsAsStrings.Select(int.Parse).ToList())
            .Select(levels =>
            {
                for (int indexToLeaveOut = 0; indexToLeaveOut < levels.Count; indexToLeaveOut++)
                {
                    List<int> levelsWithoutOne = levels
                        .Where((_, index) => index != indexToLeaveOut)
                        .ToList();

                    List<int> deltas = levelsWithoutOne
                        .Take(levelsWithoutOne.Count - 1)
                        .Zip(levelsWithoutOne.Skip(1))
                        .Select(consecutivePair => consecutivePair.First - consecutivePair.Second)
                        .ToList();

                    bool allIncreasing = deltas.All(delta => delta < 0);
                    bool allDecreasing = deltas.All(delta => delta > 0);
                    bool maxValueIsAtLeast3 = deltas.All(delta => Math.Abs(delta) <= 3);

                    if ((allIncreasing || allDecreasing) && maxValueIsAtLeast3)
                    {
                        return true;
                    }
                }

                return false;                
            })
            .Count(isValid => isValid);

        Console.WriteLine(safeReportCount);
    }
}
