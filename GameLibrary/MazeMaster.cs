namespace GameLibrary
{
    using System;
    using Blocks;
    using Game;

    public class MazeMaster
    {
        private Maze maze; 
        public Tuple<int, int> PlayerPosition;
        private readonly IDisplayer displayer;

        public MazeMaster(string configFile, IDisplayer displayer)
        {
            var mazeData = MazeCreator.MazeFromFile(configFile);
            PlayerPosition = mazeData.Item2;
            maze = mazeData.Item1;
            this.displayer = displayer;
        }

        public void ShowMaze()
        {
            displayer.ShowMaze(maze);
        }

        private bool IsNextBlockOkay(int x, int y)
        {
            return maze[x, y] is Empty || maze[x, y] is Exit;
        }
        private bool CanMoveUp()
        {
            int nextX = PlayerPosition.Item1 - 1;
            return nextX >= 0 && IsNextBlockOkay(nextX, PlayerPosition.Item2);
        }
        private bool CanMoveRight()
        {
            int nextY = PlayerPosition.Item2 + 1;
            return nextY < maze.Y && IsNextBlockOkay(PlayerPosition.Item1, nextY);
        }
        private bool CanMoveLeft()
        {
            int nextY = PlayerPosition.Item2 - 1;
            return nextY >= 0 && IsNextBlockOkay(PlayerPosition.Item1, nextY);
        }
        private bool CanMoveDown()
        {
            int nextX = PlayerPosition.Item1 + 1;
            return nextX < maze.X && IsNextBlockOkay(nextX, PlayerPosition.Item2);
        }
        public void MoveUp()
        {
            if (CanMoveUp())
            {
                DoMoveUp();
            }
        }
        public void MoveDown()
        {
            if (CanMoveDown())
            {
                DoMoveDown();
            }
        }
        public void MoveRight()
        {
            if (CanMoveRight())
            {
                DoMoveRight();
            }
        }
        public void MoveLeft()
        {
            if (CanMoveLeft())
            {
                DoMoveLeft();
            }
        }

        private void DoMoveUp()
        {
            int nextX = PlayerPosition.Item1 - 1;
            int nextY = PlayerPosition.Item2;
            MovePlayer(nextX, nextY);
        }
        private void DoMoveDown()
        {
            int nextX = PlayerPosition.Item1 + 1;
            int nextY = PlayerPosition.Item2;
            MovePlayer(nextX, nextY);
        }

        private void DoMoveRight()
        {
            int nextX = PlayerPosition.Item1;
            int nextY = PlayerPosition.Item2 + 1;
            MovePlayer(nextX, nextY);
        }

        private void DoMoveLeft()
        {

            int nextX = PlayerPosition.Item1;
            int nextY = PlayerPosition.Item2 - 1;
            MovePlayer(nextX, nextY);
        }


        private Block GetCurrentPlayer()
        {
            return maze[PlayerPosition.Item1, PlayerPosition.Item2];
        }
        public void MovePlayer(int nextX, int nextY)
        {
            var player = GetCurrentPlayer();
            if (maze[nextX, nextY] is Exit)
            {
                DoMove(nextX, nextY, player);
                displayer.ShowEndMessage(GameEndException.GameEndReason.ExitFound);
                throw new GameEndException("", GameEndException.GameEndReason.ExitFound);
            }
            DoMove(nextX, nextY, player);
        }

        private void DoMove(int nextX, int nextY, Block player)
        {
            maze[PlayerPosition.Item1, PlayerPosition.Item2] = new Empty();
            maze[nextX, nextY] = player;
            displayer.MoveBlock(PlayerPosition.Item1, PlayerPosition.Item2, nextX, nextY, player);
            PlayerPosition = new Tuple<int, int>(nextX, nextY);
        }

        public void DisplayDebug(string data)
        {
            displayer.DisplayDebug(data);
        }
    }
}
