namespace AdventOfCode24.Solutions.Day07;

public class Solution1
{
    public static void SolveExercise1()
    {
        string[] lines = File.ReadAllLines("../../../../AdventOfCode24.Solutions/Day07/task.txt");

        List<Equation1> equations = lines
            .Select(line => line.Split(": "))
            .Select(parts => new
            {
                Result = ulong.Parse(parts[0]),
                Numbers = parts[1].Split(" ").Select(ulong.Parse).ToList(),
            })
            .Select(parsed => new Equation1(parsed.Result, parsed.Numbers))
            .ToList();

        ulong result = 0;

        IEnumerable<ulong> toAdd = equations
            .Where(equation => equation.OperationsExist(0, equation.Result))
            .Select(equation => equation.Result);

        foreach (ulong val in toAdd)
        {
            result += val;
        }

        Console.WriteLine(result);
    }

    public static void SolveExercise2()
    {
        string[] lines = File.ReadAllLines("../../../../AdventOfCode24.Solutions/Day07/task.txt");

        List<Equation2> equations = lines
            .Select(line => line.Split(": "))
            .Select(parts => new
            {
                Result = ulong.Parse(parts[0]),
                Numbers = parts[1].Split(" ").Select(ulong.Parse).ToList(),
            })
            .Select(parsed => new Equation2(parsed.Result, parsed.Numbers))
            .ToList();

        ulong result = 0;

        IEnumerable<ulong> toAdd = equations
            .Where(equation => equation.OperationsExist(0, equation.Result))
            .Select(equation => equation.Result);

        foreach (ulong val in toAdd)
        {
            result += val;
        }

        Console.WriteLine(result);
    }

    private class Equation1(ulong result, List<ulong> numbers)
    {
        public ulong Result { get; } = result;
        private List<ulong> Numbers { get; set; } = numbers.Reverse<ulong>().ToList();

        public bool OperationsExist(int index, ulong leftFromPreviousIteration)
        {
            if (index == Numbers.Count - 1)
            {
                return Numbers[index] == leftFromPreviousIteration;
            }

            if (leftFromPreviousIteration < 0)
            {
                return false;
            }

            ulong subtracted = leftFromPreviousIteration - Numbers[index];

            if (leftFromPreviousIteration % Numbers[index] == 0)
            {
                return OperationsExist(index + 1, leftFromPreviousIteration / Numbers[index])
                    || OperationsExist(index + 1, subtracted);
            }

            return OperationsExist(index + 1, subtracted);
        }
    }

    private class Equation2(ulong result, List<ulong> numbers)
    {
        public ulong Result { get; } = result;
        private List<ulong> Numbers { get; set; } = numbers.Reverse<ulong>().ToList();

        public bool OperationsExist(int index, ulong leftFromPreviousIteration)
        {
            if (index == Numbers.Count - 1)
            {
                return Numbers[index] == leftFromPreviousIteration;
            }

            if (leftFromPreviousIteration < 0)
            {
                return false;
            }

            ulong subtracted = leftFromPreviousIteration - Numbers[index];
            bool canBeDivided = leftFromPreviousIteration % Numbers[index] == 0;
            bool canBeConcatenated = leftFromPreviousIteration.ToString().EndsWith(Numbers[index].ToString());

            return OperationsExist(index + 1, subtracted)
                || (canBeDivided && OperationsExist(index + 1, leftFromPreviousIteration / Numbers[index]))
                || (canBeConcatenated && OperationsExist(index + 1, GetBeforeConcatenation(leftFromPreviousIteration, Numbers[index])));

            //try
            //{
                
            //}
            //catch
            //{
            //    return true; // 222284499394090
            //}
        }

        private static ulong GetBeforeConcatenation(ulong leftFromPreviousIteration, ulong number)
        {
            string trimmed = (leftFromPreviousIteration - number).ToString().TrimEnd('0');
            if (trimmed == "")
            {
                return 0;
            }

            return ulong.Parse(trimmed);
        }
    }
}
