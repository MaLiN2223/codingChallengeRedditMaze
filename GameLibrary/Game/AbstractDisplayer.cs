using System;

namespace GameLibrary.Game
{
    using Blocks;

    public abstract class AbstractDisplayer : IDisplayer
    {
        protected abstract void ShowText(string data);
        public void ShowEndMessage(GameEndException.GameEndReason reason)
        {
            if (reason == GameEndException.GameEndReason.ExitFound)
                ShowText("You just won!");
        }

        protected abstract void ClearMaze();
        public void ShowMaze(Maze maze)
        {
            ClearMaze();
            for (int i = 0; i < maze.X; ++i)
            {
                for (int j = 0; j < maze.Y; ++j)
                {
                    WriteOnPosition(i, j, maze[i, j]);
                }
            }
        }
        public abstract void MoveBlock(int currentX, int currentY, int nextX, int nextY, Block block);

        public abstract void WriteOnPosition(int x, int y, Block c);
        public abstract void DisplayDebug(string value);
        public abstract void MovePlayer(Direction dir);
        public void Clear(int x, int y)
        {
            WriteOnPosition(x, y, new Empty());
        }
    }
}
