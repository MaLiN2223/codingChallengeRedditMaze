using System;

namespace Main
{

    public class Program
    {
        private static void Main()
        { 
            var game = new ConsoleGame(new MazeMaster("maze1.txt"));
            game.Start();

        }
    }
}
