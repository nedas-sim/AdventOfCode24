namespace AdventOfCode24.Solutions.Day09;

public class Solution1
{
    public static void SolveExercise1()
    {
        string diskMap = File.ReadAllText("../../../../AdventOfCode24.Solutions/Day08/task.txt");

        List<DiskSpace> diskSpaces = [];
        
        foreach ((int Index, char Item) diskMapDigit in diskMap.Index())
        {
            if (diskMapDigit.Index % 2 == 0)
            {
                diskSpaces.Add(new BlockFile(diskMapDigit.Item - '0', diskMapDigit.Index / 2));
            }
            else
            {
                diskSpaces.Add(new FreeSpace(diskMapDigit.Item - '0'));
            }
        }
    }
}

public abstract record DiskSpace;
public record BlockFile(int AmountOfBlocks, int ID) : DiskSpace;
public record FreeSpace(int Amount) : DiskSpace;
