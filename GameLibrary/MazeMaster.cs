namespace GameLibrary
{
    using System;
    using Blocks;
    using Game;
    /// <summary>
    /// x - down , y - up
    /// </summary>
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

        public void MovePlayer(Direction direction)
        {
            if ((direction.IsUp() && direction.IsDown()) || (direction.IsLeft() && direction.IsRight()))
            {
                return;
            }
            var moved = TryMovePlayer(direction);
            if (!moved)
            {
                TryMoveWall(direction);
            }

        }


        public void ShowMaze()
        {
            displayer.ShowMaze(maze);
        }
        #region block moving
        private void TryMoveWall(Direction direction)
        {
            if (direction.IsDown() && CanMoveBlockDown())
            {
                DoMoveBlockDown();
            }
            if (direction.IsUp() && CanMoveBlockUp())
            {
                DoMoveBlockUp();
            }
            if (direction.IsLeft() && CanMoveBlockLeft())
            {
                DoMoveBlockLeft();
            }
            if (direction.IsRight() && CanMoveBlockRight())
            {
                DoMoveBlockRight();
            }
        }

        private void DoMoveBlock(int x, int y, int deltaX, int deltaY)
        {
            displayer.MoveBlock(x, y, x + deltaX, y + deltaY, maze[x + deltaX, y + deltaY]);
            DoMoveBlock(x, y, x + deltaX, y + deltaY, maze[x, y]);
        }
        private void DoMoveBlockRight()
        {
            DoMoveBlock(PlayerPosition.Item1, PlayerPosition.Item2 + 1, 0, 1);
            DoMovePlayerRight();
        }

        private void DoMoveBlockLeft()
        {
            DoMoveBlock(PlayerPosition.Item1, PlayerPosition.Item2 - 1, 0, -1);
            DoMovePlayerLeft();
        }

        private void DoMoveBlockUp()
        {
            DoMoveBlock(PlayerPosition.Item1 - 1, PlayerPosition.Item2, -1, 0);
            DoMovePlayerUp();
        }

        private void DoMoveBlockDown()
        {
            DoMoveBlock(PlayerPosition.Item1 + 1, PlayerPosition.Item2, 1, 0);
            DoMovePlayerDown();
        }

        private bool IsNextBlockPossibleToMove(int nextX, int nextY, int farX, int farY)
        {
            return maze[nextX, nextY] is Wall && maze[farX, farY] is Empty;
        }
        private bool CanMoveBlock(int currentX, int currentY, int deltaX, int deltaY)
        {
            var farX = currentX + 2 * deltaX;
            var farY = currentY + 2 * deltaY;
            return farX > 0 && farY > 0 && farY < maze.Y - 1 && farX < maze.X - 1
                && IsNextBlockPossibleToMove(currentX + deltaX, currentY + deltaY, farX, farY);
        }
        private bool CanMoveBlockUp()
        {
            return CanMoveBlock(PlayerPosition.Item1, PlayerPosition.Item2, -1, 0);
        }
        private bool CanMoveBlockDown()
        {
            return CanMoveBlock(PlayerPosition.Item1, PlayerPosition.Item2, 1, 0);
        }
        private bool CanMoveBlockRight()
        {
            return CanMoveBlock(PlayerPosition.Item1, PlayerPosition.Item2, 0, 1);
        }
        private bool CanMoveBlockLeft()
        {
            return CanMoveBlock(PlayerPosition.Item1, PlayerPosition.Item2, 0, -1);
        }
        private void DoMoveBlock(int lastX, int lastY, int nextX, int nextY, Block block)
        {
            maze[PlayerPosition.Item1, PlayerPosition.Item2] = new Empty();
            maze[nextX, nextY] = block;
        }
        #endregion
        #region player moving
        private bool TryMovePlayer(Direction direction)
        {
            if (direction.IsDown() && CanMovePlayerDown())
            {
                DoMovePlayerDown();
                return true;
            }
            if (direction.IsUp() && CanMovePlayerUp())
            {
                DoMovePlayerUp();
                return true;
            }
            if (direction.IsLeft() && CanMovePlayerLeft())
            {
                DoMovePlayerLeft();
                return true;
            }
            if (direction.IsRight() && CanMovePlayerRight())
            {
                DoMovePlayerRight();
                return true;
            }
            return false;
        }
        private bool IsNextBlockPossibleToStandOn(int x, int y)
        {
            return maze[x, y] is Empty || maze[x, y] is Exit;
        }
        private bool CanMovePlayerUp()
        {
            int nextX = PlayerPosition.Item1 - 1;
            return nextX >= 0 && IsNextBlockPossibleToStandOn(nextX, PlayerPosition.Item2);
        }
        private bool CanMovePlayerRight()
        {
            int nextY = PlayerPosition.Item2 + 1;
            return nextY < maze.Y && IsNextBlockPossibleToStandOn(PlayerPosition.Item1, nextY);
        }
        private bool CanMovePlayerLeft()
        {
            int nextY = PlayerPosition.Item2 - 1;
            return nextY >= 0 && IsNextBlockPossibleToStandOn(PlayerPosition.Item1, nextY);
        }
        private bool CanMovePlayerDown()
        {
            int nextX = PlayerPosition.Item1 + 1;
            return nextX < maze.X && IsNextBlockPossibleToStandOn(nextX, PlayerPosition.Item2);
        }
        private void DoMovePlayerUp()
        {
            int nextX = PlayerPosition.Item1 - 1;
            int nextY = PlayerPosition.Item2;
            MovePlayer(nextX, nextY);
            DoMovePlayer(Direction.Up);
        }
        private void DoMovePlayerDown()
        {
            int nextX = PlayerPosition.Item1 + 1;
            int nextY = PlayerPosition.Item2;
            MovePlayer(nextX, nextY);
            DoMovePlayer(Direction.Down);
        }

        private void DoMovePlayerRight()
        {
            int nextX = PlayerPosition.Item1;
            int nextY = PlayerPosition.Item2 + 1;
            MovePlayer(nextX, nextY);
            DoMovePlayer(Direction.Right);
        }

        private void DoMovePlayerLeft()
        {
            int nextX = PlayerPosition.Item1;
            int nextY = PlayerPosition.Item2 - 1;
            MovePlayer(nextX, nextY);
            DoMovePlayer(Direction.Left);
        }

        #endregion
        private Player GetCurrentPlayer()
        {
            return maze[PlayerPosition.Item1, PlayerPosition.Item2] as Player;
        }
        public void MovePlayer(int nextX, int nextY)
        {
            var player = GetCurrentPlayer();
            if (maze[nextX, nextY] is Exit)
            {
                DoMovePlayer(nextX, nextY, player);
                displayer.ShowEndMessage(GameEndException.GameEndReason.ExitFound);
                throw new GameEndException("", GameEndException.GameEndReason.ExitFound);
            }
            DoMovePlayer(nextX, nextY, player);
        }
        private void DoMovePlayer(Direction dir)
        {
            displayer.MovePlayer(dir);
        }
        private void DoMovePlayer(int nextX, int nextY, Player player)
        {
            maze[PlayerPosition.Item1, PlayerPosition.Item2] = new Empty();
            maze[nextX, nextY] = player;
            PlayerPosition = new Tuple<int, int>(nextX, nextY);
        }


        public void DisplayDebug(string data)
        {
            displayer.DisplayDebug(data);
        }
    }
}
