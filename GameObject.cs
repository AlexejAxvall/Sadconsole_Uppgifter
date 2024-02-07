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

    public bool Move(Point newPosition, IScreenSurface screenSurface)
    {
        // Check new position is valid
        if (!screenSurface.Surface.IsValidCell(newPosition.X, newPosition.Y)) return false;

        // Restore the old cell
        _mapAppearance.CopyAppearanceTo(screenSurface.Surface[Position]);

        // Store the map cell of the new position
        screenSurface.Surface[newPosition].CopyAppearanceTo(_mapAppearance);

        Position = newPosition;
        DrawGameObject(screenSurface);

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
}