using SadConsole.Configuration;

Settings.WindowTitle = "My SadConsole Game";

Game.Create(100, 50, Startup);
Game.Instance.Run();
Game.Instance.Dispose();

static void Startup(object? sender, GameHost host)
{
    Console startingConsole = Game.Instance.StartingConsole!;

    string welcome_text = "Hello from SadConsole";
    int[] welcome_text_array = {50, 5};

    startingConsole.FillWithRandomGarbage(startingConsole.Font);
    startingConsole.Fill(new Rectangle(3, 3, 23, 3), Color.Violet, Color.Black, 0, Mirror.None);
    startingConsole.Print(4, 4, "Hello from SadConsole");
}