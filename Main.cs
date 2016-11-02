using System;

namespace CodingChallenges
{
    public class Program
    {
        private static void Main()
        { 
            Console.ReadKey();
        }
 
    }
	
	public class Maze
	{
		private Block [,] maze;
		public Block this[int x, int y]
		{
			get{return maze[x,y];}
			set{maze[x,y] = value;}
		}
		public Maze(int x,int y)
		{
			maze = new Block[x,y];
		}
	}
	
	public class Block
	{
		public static char Wall = '#';
		public static char Exit = 'X';
	}
}