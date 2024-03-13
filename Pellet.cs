namespace SadConsoleGame;

internal class Pellet : GameObject
{
    public Pellet(Point position, IScreenSurface hostingSurface)
    : base(new ColoredGlyph(Color.Yellow, Color.Black, '.'), position, hostingSurface)
    {

    }

    public override bool Touched(GameObject source, Map map)
    {
        // Is the player the one that touched us?
        if (source == map.UserControlledObject)
        {
            map.RemoveMapObject(this);

            return true;
        }

        return false;
    }
}