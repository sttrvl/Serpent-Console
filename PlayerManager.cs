namespace SerpentConsole.Characters;

public struct Point
{
    public int Y { get; set; }
    public int X { get; set; }

    public static Point NewPosition(int mapX, int mapY) => new Point
    {
        X = new Random().Next(1, mapX - 1),
        Y = new Random().Next(1, mapY - 1)
    };
};

public abstract class Character
{
    public Direction SerpentDirection { get; set; }

    public Point Position { get; set; }
}

public class Serpent : Character
{
    public List<Point> Segments { get; set; } = new List<Point>();

    public Serpent(Point position)
    {
        Position = position;
        StartingSnake(Position);
        SerpentDirection = SetDirection();
    }

    private void StartingSnake(Point position)
    {
        Segments.Add(new Point { X = position.X, Y = position.Y });
        Grow();
        Grow();
    }

    public Direction SetDirection()
    {
        return new Random().Next(0, 4) switch
        {
            0 => Direction.Left,
            1 => Direction.Right,
            2 => Direction.Up,
            3 => Direction.Down,
            _ => throw new NotImplementedException()
        };
    }

    public void UpdatePosition(Direction direction)
    {
        Point head = Segments.First();
        int newX = head.X, newY = head.Y;

        switch (direction)
        {
            case Direction.Up when SerpentDirection != Direction.Down:
                newY--;
                SerpentDirection = direction;
                break;
            case Direction.Down when SerpentDirection != Direction.Up:
                newY++;
                SerpentDirection = direction;
                break;
            case Direction.Right when SerpentDirection != Direction.Left:
                newX++;
                SerpentDirection = direction;
                break;
            case Direction.Left when SerpentDirection != Direction.Right:
                SerpentDirection = direction;
                newX--;
                break;
        };

        UpdateSegments(newX, newY);
    }

    public void Grow()
    {
        Point tail = Segments[Segments.Count - 1];
        Segments.Add(new Point { X = tail.X, Y = tail.Y });
    }

    public void UpdateSegments(int newX, int newY) 
    {
        for (int i = Segments.Count - 1; i > 0; i--) // shift each segment to the position of the one in front of it
            Segments[i] = Segments[i - 1];

        Segments[0] = new Point { X = newX, Y = newY }; 
        // head has no segment in front of it, so it uses the newY and newX to update itself
    }

    public bool CheckSelfCollision()
    {
        var head = Segments[0];
        for (int i = 1; i < Segments.Count; i++)
            if (Segments[i].X == head.X && Segments[i].Y == head.Y) return true;

        return false;
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}