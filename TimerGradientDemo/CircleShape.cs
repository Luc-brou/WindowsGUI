using System;
using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    public class CircleShape : Shape
    {
        private Timer colorChangeTimer;
        private Color currentColor;

        public CircleShape(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color)
        {
            currentColor = color;

            // Timer to change color every 500ms
            colorChangeTimer = new Timer();
            colorChangeTimer.Interval = 500;
            colorChangeTimer.Tick += (s, e) => ChangeColor();
            colorChangeTimer.Start();
        }

        private void ChangeColor()
        {
            var random = new Random();
            currentColor = Color.FromArgb(
                random.Next(256),
                random.Next(256),
                random.Next(256));
        }

        public override void Draw(Graphics g, Control drawPanel)
        {
            using (var brush = new SolidBrush(currentColor))
            {
                g.FillEllipse(brush, x, y, width, height);
            }
        }
    }
}