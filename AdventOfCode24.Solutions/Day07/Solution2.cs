namespace AdventOfCode24.Solutions.Day07;

public static class Solution2
{
    public static void SolveExercise2()
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

        Console.WriteLine($"Total equations: {equations.Count}");

        long result = equations
            .Index()
            .AsParallel() // <-- definitely faster
            .Where(equation =>
            {
                if (equation.Index % 10 == 0)
                {
                    Console.WriteLine($"Processing equation {equation.Index}");
                }
                return equation.Item.OperationsExist();
            })
            .Sum(equation => equation.Item.Result);

        Console.WriteLine(result);
    }

    #region Operations
    private abstract record Operation
    {
        public abstract long Apply(long left, long right);
    }

    private record Addition : Operation
    {
        public override long Apply(long left, long right) => left + right;

        private static Addition _instance = new();
        public static Addition Instance => _instance;
    }

    private record Multiplication : Operation
    {
        public override long Apply(long left, long right) => left * right;

        private static Multiplication _instance = new();
        public static Multiplication Instance => _instance;
    }

    private record Concatenation : Operation
    {
        public override long Apply(long left, long right) => long.Parse(left.ToString() + right.ToString());

        private static Concatenation _instance = new();
        public static Concatenation Instance => _instance;
    }
    #endregion

    private class Equation(long result, List<long> numbers)
    {
        public long Result { get; } = result;
        private List<long> Numbers { get; set; } = numbers;

        private IEnumerable<List<Operation>> GetOperations(int index)
        {
            if (index == Numbers.Count - 2)
            {
                yield return [Addition.Instance];
                yield return [Multiplication.Instance];
                yield return [Concatenation.Instance];

                yield break;
            }

            List<Operation> ops = [Addition.Instance, Multiplication.Instance, Concatenation.Instance];
            foreach (Operation operation in ops)
            {
                foreach (List<Operation> operations in GetOperations(index + 1))
                {
                    yield return [operation, .. operations];
                }
            }
        }

        public bool OperationsExist()
        {
            foreach (List<Operation> operations in GetOperations(0))
            {
                long result = Numbers[0];

                foreach ((int Index, Operation Item) op in operations.Index())
                {
                    result = op.Item.Apply(result, Numbers[op.Index + 1]);
                    if (result > Result)
                    {
                        break;
                    }
                }

                if (result == Result)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
