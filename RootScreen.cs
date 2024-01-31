internal class RootScreen : ScreenObject
{
    private ScreenSurface _map;

    public RootScreen()
    {
        _map = new ScreenSurface(Game.Instance.ScreenCellsX, Game.Instance.ScreenCellsY - 5);
        _map.UseMouse = false;

        Children.Add(_map);
    }
}