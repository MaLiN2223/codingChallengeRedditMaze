using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Ink;

namespace WPFGame
{
    using System.Globalization;
    using System.Windows;
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

        private Shape player;
        private readonly Canvas canvas;
        private readonly double heightPerBlock;
        private readonly double widthPerBlock;
        private const double epsilon = 0.0;
        private Rectangle wall => new Rectangle
        {
            Width = widthPerBlock,
            Height = heightPerBlock,
            Fill = Brushes.Black
        };

        private Rectangle exit => new Rectangle
        {
            Width = widthPerBlock,
            Height = heightPerBlock,
            Fill = Brushes.Green
        };
        private Shape GetShape(Block b)
        {
            Shape shape = null;
            if (b is Empty)
            {
                shape = null;
            }
            else if (b is Wall)
            {
                shape = wall;
            }
            else if (b is Player)
            {
                shape = player = GetPlayer();
            }
            else if (b is Exit)
            {
                shape = exit;
            }
            return shape;
        }

        private Shape GetPlayer()
        {
            if (player != null)
                return player;
            player = new Ellipse
            {
                Width = widthPerBlock,
                Height = heightPerBlock,
                Fill = Brushes.Red
            };

            return player;
        }

        protected override void ShowText(string data)
        {
            MessageBox.Show(data);
        }

        private Shape GetShape(double x, double y)
        {
            foreach (UIElement child in canvas.Children)
            {
                var shape = child as Shape;
                if (shape == null)
                    continue;
                if (Math.Abs(Canvas.GetLeft(shape) - y) <= 0.1 && Math.Abs(Canvas.GetTop(shape) - x) <= 0.1)
                {
                    return shape;
                }
            }
            return null;
        }
        public override void MoveBlock(int currentX, int currentY, int nextX, int nextY, Block block)
        {
            var currentRealX = RealX(currentX);
            var currentRealY = RealY(currentY);
            var nextRealX = RealX(nextX);
            var nextRealY = RealY(nextY);
            var shape = GetShape(currentRealX, currentRealY);
            Canvas.SetTop(shape, nextRealX);
            Canvas.SetLeft(shape, nextRealY);

        }

        public override void WriteOnPosition(int x, int y, Block c)
        {
            double realX = RealX(x);
            double realY = RealY(y);
            WriteOnPosition(realX, realY, GetShape(c));
        }

        private double RealY(int y)
        {
            return y * (heightPerBlock + epsilon);
        }

        private double RealX(int x)
        {
            return x * (widthPerBlock + epsilon);
        }

        public override void MovePlayer(Direction dir)
        {
            var left = Canvas.GetLeft(player);
            left = double.IsNaN(left) ? 0 : left;
            var top = Canvas.GetTop(player);
            top = double.IsNaN(top) ? 0 : top;

            switch (dir)
            {
                case Direction.Down:
                    top += heightPerBlock;
                    break;
                case Direction.Up:
                    top -= heightPerBlock;
                    break;
                case Direction.Right:
                    left += widthPerBlock;
                    break;
                case Direction.Left:
                    left -= widthPerBlock;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }

            Canvas.SetLeft(player, left);
            Canvas.SetTop(player, top);
        }
        private void WriteOnPosition(double x, double y, Shape shape)
        {
            if (shape == null)
                return;
            Canvas.SetLeft(shape, y);
            Canvas.SetTop(shape, x);
            canvas.Children.Add(shape);
        }

        public override void DisplayDebug(string value)
        {
            MessageBox.Show(value);
        }
    }
}
