using SerpentConsole.Characters;
using SerpentConsole.GameObjects;

namespace SerpentConsole.DisplayGame;
public class Display
{
    public void DisplayGameState(int score, int mapX, int mapY, Food food, Serpent serpent)
    {
        DisplayScore(score, mapY);
        DisplayBorders(mapX, mapY);
        DisplayGameObjects(serpent, food, mapX, mapY);
    }
    public void DisplayGameObjects(Serpent serpent, Food food, int mapY, int mapX)
    {
        DisplayMapEmptySpaces(mapX, mapY, food);
        DisplaySerpent(serpent);
        DisplayFood(food);
    }

    private void DisplayMapEmptySpaces(int mapY, int mapX, Food food) // I thought about another way
    {                                                                 // Maybe assigning every space a value
        for (int index = 1; index < mapX; index++)                    // And drawing everything according to that value
        { 
            for (int index2 = 1; index2 < mapY; index2++)
            {
                if (index != food.Position.X || index2 != food.Position.Y)
                {
                    Console.SetCursorPosition(index, index2);
                    Console.Write(" ");
                }
            }
        }
    }

    public void DisplayFood(Food food)
    {
        Console.SetCursorPosition(food.Position.X,  food.Position.Y);
        Console.Write("X");
    }

    public void DisplaySerpent(Serpent serpent)
    {
        for (int index = 0; index < serpent.Segments.Count; index++)
        {
            if (index == 0) Console.ForegroundColor = ConsoleColor.Red;

            Console.SetCursorPosition(serpent.Segments[index].X, serpent.Segments[index].Y);
            Console.Write("O");
            Console.ResetColor();
        }
    }

    public void DisplayBorders(int x, int y)
    {     
        DrawHorizontal(y, x);
        DrawHorizontal(0, x);
        DrawVertical(x, y + 1);
        DrawVertical(0, y + 1);
        Console.SetCursorPosition(0, 0);
    }

    private void DrawHorizontal(int y, int Lenght)
    {
        for (int i = 0; i < Lenght; i++)
        {
            Console.SetCursorPosition(i, y);
            Console.WriteLine("-");
        }
    }

    private void DrawVertical(int x, int Lenght)
    {
        for (int i = 0; i < Lenght; i++)
        {
            Console.SetCursorPosition(x, i);
            Console.WriteLine("|");
        }
    }

    public void DisplayScore(int score, int yPosition)
    {
        Console.SetCursorPosition(1, yPosition + 1);
        Console.WriteLine($"Score: {score}");
    }

    public void DisplayMapSizeOptions() => Console.WriteLine($"1 - Small | 2 - Medium | 3 - Big");

    public void DisplayCursor(bool isOn) => Console.CursorVisible = isOn;
}