using SadConsole;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace SadConsoleGame;

internal class Map
{
    private List<GameObject> _mapObjects;
    private ScreenSurface _mapSurface;

    public IReadOnlyList<GameObject> GameObjects => _mapObjects.AsReadOnly();
    public ScreenSurface SurfaceObject => _mapSurface;
    public GameObject UserControlledObject { get; set; }

    public Map(int mapWidth, int mapHeight)
    {
        _mapObjects = new List<GameObject>();
        _mapSurface = new ScreenSurface(mapWidth, mapHeight);
        _mapSurface.UseMouse = false;

        FillBackground();

        UserControlledObject = new Player(_mapSurface.Surface.Area.Center, _mapSurface);

        CreateWalls();
        CreatePellet();
        CreateGhost();
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
        new List<int> {},
        new List<int> {0, 23},
        new List<int> {0, 1, 4, 5, 18, 19, 22, 23},
        new List<int> {0, 1, 2, 3, 4, 5, 6, 11, 12, 17, 18, 19, 20, 21, 22, 23},
        new List<int> {0, 1, 8, 9, 14, 15, 22, 23},
        new List<int> {0, 3, 4, 5, 6, 7, 8, 9, 10, 13, 14, 15, 16, 17, 18, 19, 20, 23},
        new List<int> {0, 1, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 22, 23},
        new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23},
        new List<int> {0, 1, 4, 5, 6, 7, 16, 7, 16, 17, 18, 19, 22, 23},
        new List<int> {0, 5, 6, 7, 8, 15, 16, 17, 18, 23},
        new List<int> {0, 1, 6, 7, 16, 17, 22, 23},
        new List<int> {0, 1, 2, 7, 8, 11, 12, 15, 16, 21, 22, 23},
        new List<int> {8, 9, 14, 15},
        new List<int> {0, 1, 2, 7, 8, 11, 12, 15, 16, 21, 22, 23},
        new List<int> {0, 1, 6, 7, 16, 17, 22, 23},
        new List<int> {0, 5, 6, 7, 8, 15, 16, 17, 18, 23},
        new List<int> {0, 1, 4, 5, 6, 7, 16, 7, 16, 17, 18, 19, 22, 23},
        new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23},
        new List<int> {0, 1, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 22, 23},
        new List<int> {0, 3, 4, 5, 6, 7, 8, 9, 10, 13, 14, 15, 16, 17, 18, 19, 20, 23},
        new List<int> {0, 1, 8, 9, 14, 15, 22, 23},
        new List<int> {0, 1, 2, 3, 4, 5, 6, 11, 12, 17, 18, 19, 20, 21, 22, 23},
        new List<int> {0, 1, 4, 5, 18, 19, 22, 23},
        new List<int> {0, 23},
    };

    private void CreateWalls()
    {
        for (int y = 1; y < mapList.Count; y++)
        {
            for (int x = 0; x < mapList[y].Count; x += 2)
            {
                int low_Threshold = mapList[y][x];
                int high_Threshold = mapList[y][x + 1];

                for (int x_to_place_on = low_Threshold; x_to_place_on < high_Threshold; x_to_place_on++)
                {
                    Point Wallsposition = new Point(x_to_place_on, y);

                    Walls walls = new Walls(Wallsposition, _mapSurface);
                    _mapObjects.Add(walls);
                }
            }
        }
    }

    private void CreatePellet()
    {
        for (int y = 1; y < Game.Instance.ScreenCellsY; y++)
        {
            for (int x = 0; x < Game.Instance.ScreenCellsX; x++)
            {
                Point pelletPosition = new Point(x, y);
                if (!TryGetMapObject(pelletPosition, out _))
                {
                    Pellet pellet = new Pellet(pelletPosition, _mapSurface);
                    _mapObjects.Add(pellet);
                }
            }
        }
    }

    private void CreateGhost()
    {
        for (int i = 0; i < 3; i++)
        {
            Point ghostPosition = new Point(10 + i, 13);

            // Check if any object is already positioned there, repeat the loop if found
            bool foundObject = _mapObjects.Any(obj => obj.Position == ghostPosition);
            if (foundObject) continue;

            // If the code reaches here, we've got a good position, create the game object.
            Ghost ghost = new Ghost(ghostPosition, _mapSurface);
            _mapObjects.Add(ghost);
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

    public void Update(TimeSpan delta)
    {
        foreach (var go in _mapObjects)
        {
            Debug.WriteLine(delta);
            go.Update(delta, this);
        }
    }
}