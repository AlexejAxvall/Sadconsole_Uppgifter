﻿namespace SadConsoleGame;

internal class Player : GameObject
{
    public int score;

    public Player(Point position, IScreenSurface hostingSurface)
    : base(new ColoredGlyph(Color.White, Color.Black, 2), position, hostingSurface)
    {
        score = 0;
    }

    public override bool Touched(GameObject source, Map map)
    {
        return base.Touched(source, map);
    }
}
