namespace GameLibrary
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Blocks;

    public class Maze : IEnumerable<Block>
    {
        public IEnumerator<Block> GetEnumerator()
        {
            return maze.Cast<Block>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public int X { get; private set; }
        public int Y { get; private set; }

        private readonly Block[,] maze;
        public Block this[int x, int y]
        {
            get { return maze[x, y]; }
            set { maze[x, y] = value; }
        }

        public Maze(int x, int y)
        {
            X = x;
            Y = y;
            maze = new Block[x, y];
        }
    }
}