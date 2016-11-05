using System;

namespace ConsoleGame
{
    using System.Diagnostics;
    using GameLibrary;
    using GameLibrary.Game;

    public class ConsoleGame : AbstractGame
    { 
        public ConsoleGame(MazeMaster master) : base(master)
        {
            this.master = master;
        }

        public override void Start()
        {
            master.ShowMaze();
            try
            {
                while (true)
                {
                    var key = Console.ReadKey(true).Key;
                    MakeAction(key);
                }
            }
            catch (GameEndException)
            {
                Console.ReadKey();
            }
        }

        private void MakeAction(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    master.MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    master.MoveDown();
                    break;
                case ConsoleKey.RightArrow:
                    master.MoveRight();
                    break;
                case ConsoleKey.LeftArrow:
                    master.MoveLeft();
                    break;
                default:
                    return;
            }
            Debug.WriteLine($"On maze {master.PlayerPosition.Item1},{master.PlayerPosition.Item2}");
        }

        public override void DisplayDebug(string data)
        {
            master.DisplayDebug(data);
        }

    }
}
