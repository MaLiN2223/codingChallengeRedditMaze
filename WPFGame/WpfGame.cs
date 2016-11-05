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
        }
        public void MakeAction(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up:
                    master.MoveUp();
                    break;
                case Key.Down:
                    master.MoveDown();
                    break;
                case Key.Right:
                    master.MoveRight();
                    break;
                case Key.Left:
                    master.MoveLeft();
                    break;
                default:
                    return;
            } 
        }
    }
}
