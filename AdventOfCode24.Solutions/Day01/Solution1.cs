namespace AdventOfCode24.Solutions.Day01;

public class Solution1
{
    public static void SolveExercise1()
    {
        (List<int> firstList, List<int> secondList) = File
            .ReadAllLines("../../../../AdventOfCode24.Solutions/Day01/task01.txt")
            .Select(inputLine => inputLine.Split(' '))
            .Select(array => (int.Parse(array[0]), int.Parse(array[^1])))
            .Aggregate((new List<int>(), new List<int>()), (result, tuple) =>
            {
                result.Item1.Add(tuple.Item1);
                result.Item2.Add(tuple.Item2);

                return result;
            });

        firstList.Sort();
        secondList.Sort();

        int finalSum = firstList
            .Zip(secondList, (v1, v2) => Math.Abs(v1 - v2))
            .Sum();

        Console.WriteLine(finalSum);
    }

    public static void SolveExercise2()
    {
        (List<int> firstList, Dictionary<int, int> secondListFrequencyMap) = File
            .ReadAllLines("../../../../AdventOfCode24.Solutions/Day01/task01.txt")
            .Select(inputLine => inputLine.Split(' '))
            .Select(array => (int.Parse(array[0]), int.Parse(array[^1])))
            .Aggregate((new List<int>(), new Dictionary<int, int>()), (result, tuple) =>
            {
                result.Item1.Add(tuple.Item1);

                if (result.Item2.TryAdd(tuple.Item2, 1) is false)
                {
                    result.Item2[tuple.Item2]++;
                }

                return result;
            });

        int finalSum = firstList
            .Select(number => secondListFrequencyMap.TryGetValue(number, out int frequency) ? frequency * number : 0)
            .Sum();

        Console.WriteLine(finalSum);
    }
}
