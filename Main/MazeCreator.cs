using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    using System.IO;

    public static class MazeCreator
    {
        public static Tuple<Maze, Tuple<int, int>> MazeFromFile(string file)
        {
            var lines = File.ReadAllLines(file);
            int y = lines[0].Length;
            int x = lines.Length;
            var maze = new Maze(x, y);
            int a = 0;
            int b;
            Tuple<int, int> PlayerPosition = null;
            foreach (var line in lines)
            {
                b = 0;
                foreach (var c in line)
                {
                    maze[a, b] = GetBlock(c);
                    if (c.Equals(Player))
                    {
                        if (PlayerPosition != null)
                            throw new ArgumentException("Too much players");
                        PlayerPosition = new Tuple<int, int>(a, b);
                    }
                    b++;
                }
                a++;
            }
            return new Tuple<Maze, Tuple<int, int>>(maze, PlayerPosition);
        }

        private const char Player = 'O';
        private const char Empty = ' ';
        private const char Wall = '#';
        private const char Treasure = 'X';
        private static Block GetBlock(char c)
        {
            switch (c)
            {
                case Empty:
                    return new Empty();
                case Wall:
                    return new Wall();
                case Player:
                    return new Player();
                case Treasure:
                    return new Exit();
                default:
                    return new Empty();
            }
        }
    }
}
