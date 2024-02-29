using SadConsole;
using System.Diagnostics.CodeAnalysis;

namespace SadConsoleGame;

internal class Map
{
    private List<GameObject> _mapObjects;
    private ScreenSurface _mapSurface;

    public ScreenSurface SurfaceObject => _mapSurface;
    public GameObject UserControlledObject { get; set; }

    public Map(int mapWidth, int mapHeight)
    {
        _mapObjects = new List<GameObject>();
        _mapSurface = new ScreenSurface(mapWidth, mapHeight);
        _mapSurface.UseMouse = false;

        FillBackground();

        UserControlledObject = new GameObject(new ColoredGlyph(Color.White, Color.Black, 2), _mapSurface.Surface.Area.Center, _mapSurface);

        CreateWalls();

        for (int i = 0; i < 10; i++)
        {
            CreatePellet();
            CreateGhost();
        }
    }


    private void FillBackground()
    {
        for (int y = 0; y < _mapSurface.Surface.Height; y++) 
        {
            for (int x = 0; x < _mapSurface.Surface.Width; x++) 
            {
                var cell = _mapSurface.Surface[x, y];

                cell.Background = SadRogue.Primitives.Color.Black;
            }
        }
        _mapSurface.IsDirty = true;
    }

    List<List<int>> mapList = new List<List<int>>
    {
        new List<int> { 2, 2, 2, 2, 2 },
        new List<int> { 1, 16, 1 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 },
        new List<int> { 20 }
    };

    private void CreateWalls()
    {
        int count = 0;
        int space = 0;
        for (int y = 0; y < (mapList.Count); y++)
        {
            for (int x = 0; x < (mapList[y].Count); x++)
            {
                int theX = mapList[y][x];

                for (int x2 = space; x2 < theX; x2++)
                {
                    Point Wallsposition = new Point(x2, y);

                    Walls walls = new Walls(Wallsposition, _mapSurface);
                    _mapObjects.Add(walls);

                    count = x2 + 1;
                    space = 0;
                }
                
                space = 1;
            }
            
            space = 0;
        }
    }

    private void CreatePellet()
    {
        // Try 1000 times to get an empty map position
        for (int i = 0; i < 1000; i++)
        {
            // Get a random position
            Point randomPosition = new Point(Game.Instance.Random.Next(0, _mapSurface.Surface.Width),
                                             Game.Instance.Random.Next(0, _mapSurface.Surface.Height));

            // Check if any object is already positioned there, repeat the loop if found
            bool foundObject = _mapObjects.Any(obj => obj.Position == randomPosition);
            if (foundObject) continue;

            // If the code reaches here, we've got a good position, create the game object.
            Pellet pellet = new Pellet(randomPosition, _mapSurface);
            _mapObjects.Add(pellet);
            break;
        }
    }

    private void CreateGhost()
    {
        // Try 1000 times to get an empty map position
        for (int i = 0; i < 1000; i++)
        {
            // Get a random position
            Point randomPosition = new Point(Game.Instance.Random.Next(0, _mapSurface.Surface.Width),
                                             Game.Instance.Random.Next(0, _mapSurface.Surface.Height));

            // Check if any object is already positioned there, repeat the loop if found
            bool foundObject = _mapObjects.Any(obj => obj.Position == randomPosition);
            if (foundObject) continue;

            // If the code reaches here, we've got a good position, create the game object.
            Ghost ghost = new Ghost(randomPosition, _mapSurface);
            _mapObjects.Add(ghost);
            break;
        }
    }

    public bool TryGetMapObject(Point position, [NotNullWhen(true)] out GameObject? gameObject)
    {
        // Try to find a map object at that position
        foreach (var otherGameObject in _mapObjects)
        {
            if (otherGameObject.Position == position)
            {
                gameObject = otherGameObject;
                return true;
            }
        }

        gameObject = null;
        return false;
    }

    public void RemoveMapObject(GameObject mapObject)
    {
        if (_mapObjects.Contains(mapObject))
        {
            _mapObjects.Remove(mapObject);
            mapObject.RestoreMap(this);
        }
    }
}