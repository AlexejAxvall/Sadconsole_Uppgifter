namespace SadConsoleGame;

internal class GameObject
{
    private ColoredGlyph _mapAppearance = new ColoredGlyph();
    public Point Position { get; private set; }

    public ColoredGlyph Appearance { get; set; }

    private void DrawGameObject(IScreenSurface screenSurface)
    {
        Appearance.CopyAppearanceTo(screenSurface.Surface[Position]);
        screenSurface.IsDirty = true;
    }

    public bool Move(Point newPosition, Map map)
    {
        int minX = 0, maxX = 22, minY = 0, maxY = 22;

        if (newPosition.X < minX) newPosition = new Point(maxX, newPosition.Y);
        else if (newPosition.X > maxX) newPosition = new Point(minX, newPosition.Y);
        if (newPosition.Y < minY) newPosition = new Point(newPosition.X, maxY);
        else if (newPosition.Y > maxY) newPosition = new Point(newPosition.X, minY);


        // Check if new position is valid
        if (!map.SurfaceObject.IsValidCell(newPosition.X, newPosition.Y))
        {
            return false;
        }


        // Check if other object is there and if it we can move through it
        if (map.TryGetMapObject(newPosition, out GameObject? foundObject) && !foundObject.Touched(this, map))
        { 
        return false;
        }
        
        // Restore the old cell
        _mapAppearance.CopyAppearanceTo(map.SurfaceObject.Surface[Position]);

        // Store the map cell of the new position
        map.SurfaceObject.Surface[newPosition].CopyAppearanceTo(_mapAppearance);

        Position = newPosition;
        DrawGameObject(map.SurfaceObject);

        return true;
    }

    public GameObject(ColoredGlyph appearance, Point position, IScreenSurface hostingSurface)
    {
        Appearance = appearance;
        Position = position;

        // Store the map cell
        hostingSurface.Surface[position].CopyAppearanceTo(_mapAppearance);

        // draw the object
        DrawGameObject(hostingSurface);
    }

    public virtual bool Touched(GameObject source, Map map)
    {
        return false;
    }

    public void RestoreMap(Map map) =>
    _mapAppearance.CopyAppearanceTo(map.SurfaceObject.Surface[Position]);
}