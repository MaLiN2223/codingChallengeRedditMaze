using System;

namespace GameLibrary.Game
{
    using Blocks;

    public interface IDisplayer
    {
        void ShowEndMessage(GameEndException.GameEndReason reason);
        void ShowMaze(Maze maze);
        void MoveBlock(int currentX, int currentY, int nextX, int nextY, Block block);
        void WriteOnPosition(int x, int y, Block c);
        void MovePlayer(Direction dir);
        void DisplayDebug(string value);
        void Clear(int x, int y);

    }
}
