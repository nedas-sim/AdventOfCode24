namespace AdventOfCode24.Solutions.Day05;

public class Solution1
{
    public static void SolveExercise1()
    {
        string[] lines = File.ReadAllLines("../../../../AdventOfCode24.Solutions/Day05/task.txt");

        bool readingSection1 = true;

        List<PageOrderingRule> rules = [];
        List<Update> updates = [];

        foreach (string line in lines)
        {
            if (readingSection1 && string.IsNullOrEmpty(line))
            {
                readingSection1 = false;
                continue;
            }

            if (readingSection1)
            {
                uint[] numbersSplitByPipe = line.Split('|')
                    .Select(uint.Parse)
                    .ToArray();

                rules.Add(new(numbersSplitByPipe[0], numbersSplitByPipe[1]));
                continue;
            }

            List<uint> numbersSplitByComma = line.Split(',')
                .Select(uint.Parse)
                .ToList();

            updates.Add(new(numbersSplitByComma));
        }

        foreach (PageOrderingRule rule in rules)
        {
            updates = updates
                .Where(update => update.DoesRulePass(rule))
                .ToList();
        }

        long sumOfMid = updates.Sum(update => update.MiddleNumber);

        Console.WriteLine(sumOfMid);
    }

    private record struct PageOrderingRule(uint Before, uint After);
    private record struct Update(List<uint> PageNumbers)
    {
        public readonly bool DoesRulePass(PageOrderingRule rule)
        {
            bool foundAfter = false;

            foreach (uint number in PageNumbers)
            {
                if (number == rule.After)
                {
                    foundAfter = true;
                    continue;
                }

                if (number == rule.Before && foundAfter)
                {
                    return false;
                }
            }

            return true;
        }

        public readonly uint MiddleNumber => PageNumbers[PageNumbers.Count / 2];
    }
}
