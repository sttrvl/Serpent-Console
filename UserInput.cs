using SerpentConsole.Characters;
using SerpentConsole.DisplayGame;

namespace SerpentConsole.ManageInput;
public class UserInput
{
    public int MenuChoice(Display display)
    {
        display.DisplayMapSizeOptions();

        int? input = null;
        while (input == null || input <= 0 || input > 3)
        {
            try
            {
                if (input <= 0 || input > 3)
                {
                    ClearLineAt(0, 1, 20);
                    WriteAndPositionCursor("Invalid option.");
                }

                input = InputNumber("Choose an option");
            }
            catch (FormatException)
            {
                ClearLineAt(0, 1, 20);
                WriteAndPositionCursor("Not a number.");
            }
        }

        return (int)input;
    }

    private void WriteAndPositionCursor(string text)
    {
        Console.Write($"{text}");
        Console.SetCursorPosition(text.Length + 1, Console.CursorTop);
    }

    private void ClearLineAt(int column, int row, int lenght)
    {
        Console.SetCursorPosition(column, row);
        Console.WriteLine(new string(' ', lenght));
        Console.SetCursorPosition(column, row);
    }

    private int InputNumber(string text)
    {
        Console.Write(text + ": ");
        return Convert.ToInt32(Console.ReadLine());
    }

    public (int, int) ChooseMapSize(Display display)
    {
        return MenuChoice(display) switch
        {
            1 => (40, 20),
            2 => (60, 20),
            3 => (100, 20),
            _ => throw new NotImplementedException()
        };
    }

    public Direction CheckForKeys(Character character)
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;
            return key switch
            {
                ConsoleKey.W when character.SerpentDirection != Direction.Down => Direction.Up,
                ConsoleKey.S when character.SerpentDirection != Direction.Up => Direction.Down,
                ConsoleKey.A when character.SerpentDirection != Direction.Right => Direction.Left,
                ConsoleKey.D when character.SerpentDirection != Direction.Left => Direction.Right,
                _ => character.SerpentDirection
            };
        }

        return character.SerpentDirection;
    }
}