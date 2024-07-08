using SerpentConsole.Characters;
using SerpentConsole.DisplayGame;
using SerpentConsole.GameObjects;
using SerpentConsole.ManageInput;
using SerpentConsole.MapManagement;

namespace SerpentConsole;
public class Game
{
    int Score { get; set; } = 0;

    public bool gameLost = false;

    public void Run()
    {
        UserInput input = new UserInput();
        Display display = new Display();
        
        (int sizeX, int sizeY) = input.ChooseMapSize(display);
        MapManager map = new MapManager(sizeX, sizeY); // max 119, 29

        display.DisplayCursor(false);

        Food food = new Food(Point.NewPosition(map.MapX, map.MapY));
        Serpent serpent = new Serpent(new Point { X = map.MapX / 2 - 4, Y = map.MapY / 2 });

        while (!gameLost)
        {
            display.DisplayGameState(Score, map.MapX, map.MapY, food, serpent);
            serpent.UpdatePosition(input.CheckForKeys(serpent));

            if (food.FoodAten(serpent))
            {
                food = food.UpdateFoodPosition(serpent, map.MapX, map.MapY);
                serpent.Grow();
                UpdateScore();
            }

            if (GameLost(serpent, map.MapX, map.MapY)) EndGame(map.MapX, map.MapY);

            Thread.Sleep(60);
        }
    }

    void UpdateScore() => Score++;

    bool GameLost(Serpent serpent, int mapX, int mapY)
    {
        if (serpent.Segments[0].X >= mapX || serpent.Segments[0].X <= 0) return true;
        if (serpent.Segments[0].Y >= mapY || serpent.Segments[0].Y <= 0) return true;
        if (serpent.CheckSelfCollision()) return true;

        return false;
    }

    public void EndGame(int mapX, int mapY)
    {
        Console.SetCursorPosition(mapX / 2 - 4, mapY / 2);
        Console.WriteLine("Game Over");
        Console.SetCursorPosition(0, mapY + 1);
        gameLost = true;
    }
}