using SadConsole.Configuration;

Settings.WindowTitle = "My SadConsole Game";

Builder configuration = new Builder()
    .SetScreenSize(90, 30)
    .OnStart(Startup)
    ;

Game.Create(configuration);
Game.Instance.Run();
Game.Instance.Dispose();

static void Startup(object? sender, GameHost host)
{
    ScreenObject container = new ScreenObject();
    Game.Instance.Screen = container;

    Console background_console = new(90, 30);

    for (int x = 0; x < background_console.Width; x++)
    {
        for (int y = 0; y < background_console.Height; y++)
        {
            // Set the background color of the cell at (x, y).
            background_console.SetBackground(x, y, Color.Blue);
        }
    }

    container.Children.Add(background_console);

    // First console
    Console console1 = new(60, 14);
    console1.Position = (3, 2);
    console1.Surface.DefaultBackground = Color.AnsiCyan;
    console1.Clear();
    console1.Print(1, 1, "Type on me!");
    console1.Cursor.Position = (1, 2);
    console1.Cursor.IsEnabled = true;
    console1.Cursor.IsVisible = true;
    console1.Cursor.MouseClickReposition = true;
    console1.IsFocused = true;
    console1.MoveToFrontOnMouseClick = true;
    console1.FocusOnMouseClick = true;

    // Add a child surface
    ScreenSurface surfaceObject = new(5, 3);
    surfaceObject.Surface.FillWithRandomGarbage(surfaceObject.Font);
    surfaceObject.Position = console1.Surface.Area.Center - (surfaceObject.Surface.Area.Size / 2);
    surfaceObject.UseMouse = false;

    console1.Children.Add(surfaceObject);

    // Second console
    Console console2 = new Console(58, 12);
    console2.Position = new Point(19, 11);
    console2.Surface.DefaultBackground = Color.AnsiRed;
    console2.Clear();
    console2.Print(1, 1, "Type on me!");
    console2.Cursor.Position = new Point(1, 2);
    console2.Cursor.IsEnabled = true;
    console2.Cursor.IsVisible = true;
    console2.FocusOnMouseClick = true;
    console2.MoveToFrontOnMouseClick = true;

    container.Children.MoveToBottom(console2);

    container.Children.Add(console2);
    container.Children.Add(console1);
}