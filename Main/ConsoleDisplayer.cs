namespace ConsoleGame
{
    using System;
    using System.Diagnostics;
    using GameLibrary;
    using GameLibrary.Blocks;
    using GameLibrary.Game;
    public class ConsoleDisplayer : AbstractDisplayer
    {
        protected override void ShowText(string data)
        {
            Console.WriteLine(data); 
        }

        public override void WriteOnPosition(int x, int y, Block c)
        {
            WriteOnPosition(x, y, GetValue(c));
        }

        public override void MovePlayer(Direction dir)
        {
            throw new NotImplementedException();
        }

        public override void DisplayDebug(string value)
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