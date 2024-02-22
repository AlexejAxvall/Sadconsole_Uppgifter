namespace SadConsoleGame;

internal class Walls : GameObject
{
    public Walls(Point position, IScreenSurface hostingSurface) : base(new ColoredGlyph(Color.Blue, Color.Blue, '_'), position, hostingSurface)
    {
        for (int i = 0; i < 10; i++)
        {

        }
    }
    public override bool Touched(GameObject source, Map map)
    {
        return base.Touched(source, map);
    }
}