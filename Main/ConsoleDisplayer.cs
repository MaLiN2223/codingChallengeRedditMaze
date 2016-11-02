namespace Main
{
    using System;
    using System.Diagnostics;

    public class ConsoleDisplayer
    {
        public void ShowEndMessage(GameEndException.GameEndReason reason)
        {
            if (reason == GameEndException.GameEndReason.ExitFound)
                Console.WriteLine("GAME HAS ENDED");
        }
        public void ShowMaze(Maze maze)
        {
            for (int i = 0; i < maze.X; ++i)
            {
                for (int j = 0; j < maze.Y; ++j)
                {
                    Console.Write(GetValue(maze[i, j]));
                }
                Console.WriteLine("");
            }
        }
        public void MovePlayer(int currentX, int currentY, int nextX, int nextY, Block player)
        {
            Clear(currentX, currentY); 
            WriteOnPosition(nextX, nextY, player);
            Debug.WriteLine($"On console {currentX},{currentY} -> {nextX},{nextY}");
        }

        public void WriteOnPosition(int x, int y, Block c)
        {
            WriteOnPosition(x, y, GetValue(c));
        }

        public void DisplayDebug(string value)
        {
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            Console.Write(value);
            Console.SetCursorPosition(left, top);
        }
        public void WriteOnPosition(int x, int y, char c)
        {
            var left = Console.CursorLeft;
            var top = Console.CursorTop;
            Console.SetCursorPosition(y, x);
            Console.Write(c);
            Console.SetCursorPosition(left, top);
        }
        public void Clear(int x, int y)
        {
            WriteOnPosition(x, y, Empty);
        }

        private static char Empty = ' ';
        private static char GetValue(Block b)
        {
            if (b is Player)
                return 'O';
            if (b is Exit)
                return 'X';
            if (b is Empty)
                return Empty;
            if (b is Wall)
                return '#';
            return Empty;
        }
    }
}