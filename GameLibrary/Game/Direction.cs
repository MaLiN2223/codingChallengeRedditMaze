using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Game
{

    [Flags]
    public enum Direction
    {
        Up = 1, Down = 2, Right = 4, Left = 8
    }

    public static class DirectionExtensions
    {

        public static bool IsUp(this Direction dir)
        {
            return (dir & Direction.Up) != 0;
        }
        public static bool IsDown(this Direction dir)
        {
            return (dir & Direction.Down) != 0;
        }
        public static bool IsRight(this Direction dir)
        {
            return (dir & Direction.Right) != 0;
        }
        public static bool IsLeft(this Direction dir)
        {
            return (dir & Direction.Left) != 0;
        }
    }
}
