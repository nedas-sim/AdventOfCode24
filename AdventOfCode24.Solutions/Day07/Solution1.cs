namespace AdventOfCode24.Solutions.Day07;

public class Solution1
{
    public static void SolveExercise1()
    {
        string[] lines = File.ReadAllLines("../../../../AdventOfCode24.Solutions/Day07/task.txt");

        List<Equation> equations = lines
            .Select(line => line.Split(": "))
            .Select(parts => new
            {
                Result = long.Parse(parts[0]),
                Numbers = parts[1].Split(" ").Select(long.Parse).ToList(),
            })
            .Select(parsed => new Equation(parsed.Result, parsed.Numbers))
            .ToList();

        long result = equations
            .Where(equation => equation.OperationsExist(0, equation.Result))
            .Sum(equation => equation.Result);

        Console.WriteLine(result);
    }

    public static void SolveExercise2()
    {

    }

    private class Equation(long result, List<long> numbers)
    {
        public long Result { get; } = result;
        private List<long> Numbers { get; set; } = numbers.Reverse<long>().ToList();

        public bool OperationsExist(int index, long leftFromPreviousIteration)
        {
            if (index == Numbers.Count - 1)
            {
                return Numbers[index] == leftFromPreviousIteration;
            }

            if (leftFromPreviousIteration < 0)
            {
                return false;
            }

            long subtracted = leftFromPreviousIteration - Numbers[index];

            if (leftFromPreviousIteration % Numbers[index] == 0)
            {
                return OperationsExist(index + 1, leftFromPreviousIteration / Numbers[index])
                    || OperationsExist(index + 1, subtracted);
            }

            return OperationsExist(index + 1, subtracted);
        }
    }
}
