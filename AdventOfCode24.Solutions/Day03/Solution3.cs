namespace AdventOfCode24.Solutions.Day03;

public class Solution3
{
    const string MUL = "mul(";
    const string DO = "do()";
    const string DONT = "don't()";

    public static void SolveExercise1()
    {
        ReadOnlySpan<char> inputText = File.ReadAllText("../../../../AdventOfCode24.Solutions/Day03/task.txt").AsSpan();

        int indexToCheck;
        int total = 0;

        while ((indexToCheck = inputText.IndexOf(MUL)) > -1)
        {
            if (GetTotal(inputText, ref indexToCheck) is int result)
            {
                total += result;
            }

            inputText = inputText[indexToCheck..];
        }

        Console.WriteLine(total);
    }

    public static void SolveExercise2()
    {
        ReadOnlySpan<char> originalInputText = File.ReadAllText("../../../../AdventOfCode24.Solutions/Day03/task.txt").AsSpan();

        List<uint> actions = [];

        ReadOnlySpan<char> inputText = originalInputText;

        while (true)
        {
            uint mulIndex = (uint)inputText.IndexOf(MUL);
            uint doIndex = (uint)inputText.IndexOf(DO);
            uint dontIndex = (uint)inputText.IndexOf(DONT);
            
            (uint indexToCheck, uint whichIsFirst) = MinWithIndex(doIndex, dontIndex, mulIndex);

            // -1 index overflows for uint
            if (indexToCheck == uint.MaxValue)
            {
                break;
            }

            actions.Add(whichIsFirst);

            indexToCheck++;

            inputText = inputText[(int)indexToCheck..];
        }

        inputText = originalInputText;
        bool isEnabled = true;
        int total = 0;
        int indexToCheck1;

        foreach (uint action in actions)
        {
            switch (action)
            {
                case 0:
                    inputText = inputText[(inputText.IndexOf(DO) + 1)..];
                    isEnabled = true;
                    break;
                case 1:
                    inputText = inputText[(inputText.IndexOf(DONT) + 1)..];
                    isEnabled = false;
                    break;
                case 2:
                    if (isEnabled)
                    {
                        indexToCheck1 = inputText.IndexOf(MUL);

                        if (GetTotal(inputText, ref indexToCheck1) is int result)
                        {
                            total += result;
                        }
                        if (indexToCheck1 >= inputText.Length)
                        {
                            break;
                        }
                        inputText = inputText[indexToCheck1..];
                    }
                    break;
            }
        }

        Console.WriteLine(total);
    }

    private static uint Min(uint a, uint b, uint c) => Math.Min(a, Math.Min(b, c));
    private static (uint Min, uint Index) MinWithIndex(uint a, uint b, uint c) => 
        (Min(a, b, c), 
            a == Min(a, b, c) ? (uint)0 :
            b == Min(a, b, c) ? (uint)1 :
            (uint)2);

    private static int? GetTotal(ReadOnlySpan<char> inputText, ref int indexToCheck)
    {
        indexToCheck += MUL.Length;

        if (indexToCheck >= inputText.Length)
        {
            return null;
        }

        int num1 = 0, num2 = 0;
        bool isCorrupted = true;

        while (inputText[indexToCheck] >= '0' && inputText[indexToCheck] <= '9')
        {
            int numToAppend = inputText[indexToCheck] - '0';
            num1 = num1 * 10 + numToAppend;
            indexToCheck++;

            if (inputText[indexToCheck] == ',')
            {
                isCorrupted = false;
                break;
            }
        }

        indexToCheck++;

        if (isCorrupted)
        {
            return null;
        }

        isCorrupted = true;

        while (inputText[indexToCheck] >= '0' && inputText[indexToCheck] <= '9')
        {
            int numToAppend = inputText[indexToCheck] - '0';
            num2 = num2 * 10 + numToAppend;
            indexToCheck++;

            if (inputText[indexToCheck] == ')')
            {
                isCorrupted = false;
                break;
            }
        }

        if (isCorrupted)
        {
            return null;
        }

        return num1 * num2;
    }
}
