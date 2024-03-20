namespace SadConsoleGame;

internal class Ghost : GameObject
{
    public Ghost(Point position, IScreenSurface hostingSurface) : base(new ColoredGlyph(Color.Red, Color.Black, 'M'), position, hostingSurface)
    {
        for (int i = 0; i < 10; i++)
        {
        
        }
    }
    public override bool Touched(GameObject source, Map map)
    {
        return base.Touched(source, map);
    }

    public override bool Update(TimeSpan delta, Map map)
    {
        System.Console.WriteLine("Booh");
        var player_position = map.UserControlledObject.Position;
        var direction = Position - player_position;
    
        Move(Position + direction, map);
    
        return true;
    }
}