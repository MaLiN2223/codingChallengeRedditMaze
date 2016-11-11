using System;

namespace WPFGame
{
    using System.Windows;
    using System.Windows.Input;
    using GameLibrary;
    using GameLibrary.Game;
    class WpfGame : AbstractGame
    {
        public WpfGame(MazeMaster master) : base(master)
        {
        }

        public override void DisplayDebug(string data)
        {
            MessageBox.Show(data);
        }

        public override void Start()
        {
            master.ShowMaze();
            IsPlaying = true;
        }

        private bool IsPlaying = false;
        public void MakeAction(KeyEventArgs e)
        {
            if (!IsPlaying)
                return;
            try
            {
                switch (e.Key)
                {
                    case Key.Up:
                        master.MovePlayer(Direction.Up);
                        break;
                    case Key.Down:
                        master.MovePlayer(Direction.Down);
                        break;
                    case Key.Right:
                        master.MovePlayer(Direction.Right);
                        break;
                    case Key.Left:
                        master.MovePlayer(Direction.Left);
                        break;
                    default:
                        return;
                }
            }
            catch (GameEndException)
            {
                var data = MessageBox.Show("Do you want to try again?", "Again?", MessageBoxButton.YesNo);
                if (data.HasFlag(MessageBoxResult.OK))
                    Start();
                if (data.HasFlag(MessageBoxResult.No))
                    IsPlaying = false;
            }
        }
    }
}
