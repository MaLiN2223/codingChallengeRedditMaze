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

        public void MovePlayer(Direction direction)
        {
            if ((direction.IsUp() && direction.IsDown()) || (direction.IsLeft() && direction.IsRight()))
            {
                return;
            }
            bool moved = false;
            moved = TryMovePlayer(direction);
            if (!moved)
            {
                TryMoveWall();
            }


        }

        private void TryMoveWall()
        {
            //TODO : Moving wall
        }
        private bool TryMovePlayer(Direction direction)
        {
            if (direction.IsDown() && CanMovePlayerDown())
            {
                DoMoveDown();
                return true;
            }
            if (direction.IsUp() && CanMovePlayerUp())
            {
                DoMovePlayerUp();
                return true;
            }
            if (direction.IsLeft() && CanMovePlayerLeft())
            {
                DoMoveLeft();
                return true;
            }
            if (direction.IsRight() && CanMovePlayerRight())
            {
                DoMoveRight();
                return true;
            }
            return moved;
        }

        public void ShowMaze()
        {
            displayer.ShowMaze(maze);
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
        private void DoMoveDown()
        {
            int nextX = PlayerPosition.Item1 + 1;
            int nextY = PlayerPosition.Item2;
            MovePlayer(nextX, nextY);
            DoMovePlayer(Direction.Down);
        }

        private void DoMoveRight()
        {
            int nextX = PlayerPosition.Item1;
            int nextY = PlayerPosition.Item2 + 1;
            MovePlayer(nextX, nextY);
            DoMovePlayer(Direction.Right);
        }

        private void DoMoveLeft()
        {
            int nextX = PlayerPosition.Item1;
            int nextY = PlayerPosition.Item2 - 1;
            MovePlayer(nextX, nextY);
            DoMovePlayer(Direction.Left);
        }


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
