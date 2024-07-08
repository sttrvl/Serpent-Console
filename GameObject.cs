using SerpentConsole.Characters;

namespace SerpentConsole.GameObjects;

public class Food
{
    public Point Position;

    public Food(Point point)
    {
        Position = point;
    }

    public Food UpdateFoodPosition(Serpent serpent, int mapX, int mapY)
    {
        Point newPosition = Point.NewPosition(mapX, mapY);
                
        while (serpent.Segments.Any(segment => newPosition.X == segment.X && newPosition.Y == segment.Y))
        {
            newPosition = Point.NewPosition(mapX, mapY);
        }

        return new Food(newPosition);
    }

    public bool FoodAten(Serpent serpent) => Position.X == serpent.Segments[0].X && Position.Y == serpent.Segments[0].Y;
}