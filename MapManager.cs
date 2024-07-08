namespace SerpentConsole.MapManagement;

public class MapManager
{
    public int MapX { get; }
    public int MapY { get; }

    public MapManager(int sizeX, int sizeY)
    {
        MapX = sizeX;
        MapY = sizeY;
    }
}