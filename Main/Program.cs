using System;

namespace ConsoleGame
{
    using GameLibrary;

    public class Program
    {
        private static void Main()
        {
            var game = new ConsoleGame(new MazeMaster("maze1.txt", new ConsoleDisplayer()));
            game.Start();
        }
    }
}
