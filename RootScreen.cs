using System.Diagnostics;
using SadConsole.Input;
using SadConsole.Components;

namespace SadConsoleGame;

internal class RootScreen : ScreenObject
{
    private Map _map;

    public RootScreen()
    {
        _map = new Map(Game.Instance.ScreenCellsX, Game.Instance.ScreenCellsY);
        Children.Add(_map.SurfaceObject);
    }

    //bool newMove = true;
    bool going_Up= false;
    bool going_Down = false;
    bool going_Left = false;
    bool going_Right = false;

    bool next_direction_up = false;
    bool next_direction_down = false;
    bool next_direction_left = false;
    bool next_direction_right = false;
    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        bool handled = false;
        Player player = (Player)_map.UserControlledObject;
        
        if (keyboard.IsKeyPressed(Keys.Up) || going_Up || next_direction_up)
        {
            if (keyboard.IsKeyPressed(Keys.Up))
            {
                next_direction_up = true;
                next_direction_down = false;
                next_direction_left = false;
                next_direction_right = false;
            }
            player.dir = new Point(0, -1);
            handled = true;

            bool move_up_possible = player.Move(_map.UserControlledObject.Position + Direction.Up, _map);

            if (move_up_possible)
            {
                //newMove = false;
                going_Up = true;
                going_Down = false;
                going_Left = false;
                going_Right = false;
                
                Point playerPosition = _map.UserControlledObject.Position;
                Debug.WriteLine(playerPosition);
            }
            
        }
        if (keyboard.IsKeyPressed(Keys.Down) || going_Down || next_direction_down)
        {
            if (keyboard.IsKeyPressed(Keys.Down))
            {
                next_direction_up = false;
                next_direction_down = true;
                next_direction_left = false;
                next_direction_right = false;
            }

            player.dir = new Point(0, 1);
            handled = true;

            bool move_down_possible = player.Move(_map.UserControlledObject.Position + Direction.Down, _map);

            if (move_down_possible)
            {
                //newMove = false;
                going_Up = false;
                going_Down = true;
                going_Left = false;
                going_Right = false;
            }
        }

        if (keyboard.IsKeyPressed(Keys.Left) || going_Left || next_direction_left)
        {
            if (keyboard.IsKeyPressed(Keys.Left))
            {
                next_direction_up = false;
                next_direction_down = false;
                next_direction_left = true;
                next_direction_right = false;
            }
            player.dir = new Point(-1, 0);
            handled = true;

            bool move_left_possible = player.Move(_map.UserControlledObject.Position + Direction.Left, _map);

            if (move_left_possible)
            {
                //newMove = false;
                going_Up = false;
                going_Down = false;
                going_Left = true;
                going_Right = false;
            }

            
        }
        if (keyboard.IsKeyPressed(Keys.Right) || going_Right || next_direction_right)
        {
            if (keyboard.IsKeyPressed(Keys.Right))
            {
                next_direction_up = false;
                next_direction_down = false;
                next_direction_left = false;
                next_direction_right = true;
            }
            player.dir = new Point(1, 0);
            handled = true;

            bool move_right_possible = player.Move(_map.UserControlledObject.Position + Direction.Right, _map);

            if (move_right_possible)
            {
                //newMove = false;
                going_Up = false;
                going_Down = false;
                going_Left = false;
                going_Right = true;
            }
        }

        //newMove = true;
        return handled;
    }

    /*public override void Update(TimeSpan delta)
    {
        _map.Update(delta);
    }
    */
}