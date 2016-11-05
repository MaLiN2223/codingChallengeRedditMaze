using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFGame
{
    using System.Globalization;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using GameLibrary;
    using GameLibrary.Blocks;
    using GameLibrary.Game;

    public class WpfDisplayer : AbstractDisplayer
    {
        public WpfDisplayer(Canvas canvas)
        {
            this.canvas = canvas;
            widthPerBlock = 10;
            heightPerBlock = 10;
        } 
        private readonly Canvas canvas;
        private double heightPerBlock;
        private double widthPerBlock;
        private double epsilon = 0.0;
        private Rectangle wall => new Rectangle
        {
            Width = widthPerBlock,
            Height = heightPerBlock, 
            Fill = Brushes.Black
        };

        private Ellipse player => new Ellipse
        {
            Width = widthPerBlock,
            Height = heightPerBlock,  
            Fill = Brushes.Red
        };
        private Rectangle exit => new Rectangle
        {
            Width = widthPerBlock,
            Height = heightPerBlock, 
            Fill = Brushes.Green
        };
        private Rectangle empty => new Rectangle
        {
            Width = widthPerBlock,
            Height = heightPerBlock, 
            Fill = Brushes.White

        };
        private Shape GetShape(Block b)
        {

            Shape shape = null;
            if (b is Empty)
            {
                shape = empty;
            }
            else if (b is Wall)
            {
                shape = wall;
            }
            else if (b is Player)
            {
                shape = player;
            }
            else if (b is Exit)
            {
                shape = exit;
            }
            return shape;

        }

        public override void WriteOnPosition(int x, int y, Block c)
        {
            double realX = x * (heightPerBlock + epsilon);
            double realY = y * (heightPerBlock + epsilon);
            WriteOnPosition(realX, realY, GetShape(c));
        }

        private void WriteOnPosition(double x, double y, Shape shape)
        {
            Canvas.SetLeft(shape, y);
            Canvas.SetTop(shape, x);
            canvas.Children.Add(shape);

        }
        public override void DisplayDebug(string value)
        {
            throw new NotImplementedException();
        }
        public override void ShowEndMessage(GameEndException.GameEndReason reason)
        {
            throw new NotImplementedException();
        }
    }
}
