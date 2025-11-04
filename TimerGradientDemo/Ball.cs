using System;
using System.Drawing;

namespace BouncyBall2023
{
    enum ShapeType
    {
        Circle,
        Square,
        Triangle
    }

    class Ball
    {
        public Rectangle ballBox;
        private Rectangle mainCanvas;
        private int size = 64;

        private int XVelocity;
        private int YVelocity;

        private Random random = new Random();
        private Color color;
        private ShapeType shape;

        private int tickCount = 0;

        public Ball(Rectangle mainCanvas)
        {
            this.mainCanvas = mainCanvas;

            ballBox.Height = size;
            ballBox.Width = size;

            ballBox.X = (mainCanvas.Width / 2) - (size / 2);
            ballBox.Y = (mainCanvas.Height / 2) - (size / 2);

            while (XVelocity > -3 && XVelocity < 3)
                XVelocity = random.Next(-15, 15);

            while (YVelocity > -3 && YVelocity < 3)
                YVelocity = random.Next(-15, 15);

            color = RandomColor();
            shape = RandomShape();
        }

        public void Move()
        {
            ballBox.X += XVelocity;
            ballBox.Y += YVelocity;

            if (ballBox.Left < mainCanvas.Left || ballBox.Right > mainCanvas.Right)
                FlipX();
            if (ballBox.Top < mainCanvas.Top || ballBox.Bottom > mainCanvas.Bottom)
                FlipY();

            tickCount++;
            if (tickCount >= 10)
            {
                tickCount = 0;
                shape = RandomShape();
                color = RandomColor();
            }
        }

        public void Draw(Graphics graphics)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                switch (shape)
                {
                    case ShapeType.Circle:
                        graphics.FillEllipse(brush, ballBox);
                        break;
                    case ShapeType.Square:
                        graphics.FillRectangle(brush, ballBox);
                        break;
                    case ShapeType.Triangle:
                        Point[] pts =
                        {
                            new Point(ballBox.Left + ballBox.Width/2, ballBox.Top),
                            new Point(ballBox.Left, ballBox.Bottom),
                            new Point(ballBox.Right, ballBox.Bottom)
                        };
                        graphics.FillPolygon(brush, pts);
                        break;
                }
            }
        }

        public void FlipX() => XVelocity *= -1;
        public void FlipY() => YVelocity *= -1;

        public void SetVelocity(int vx, int vy)
        {
            XVelocity = vx;
            YVelocity = vy;
        }

        public void UpdateCanvas(Rectangle newCanvas)
        {
            mainCanvas = newCanvas;
        }

        private Color RandomColor()
        {
            return Color.FromArgb(255,
                random.Next(256),
                random.Next(256),
                random.Next(256));
        }

        private ShapeType RandomShape()
        {
            Array values = Enum.GetValues(typeof(ShapeType));
            return (ShapeType)values.GetValue(random.Next(values.Length));
        }
    }
}