using System;
using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    //inherits from base shape class
    public class CircleShape : Shape
    {
        private Timer colorChangeTimer;
        private Color currentColor;

        public CircleShape(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color)
        {
            currentColor = color;
            colorChangeTimer = new Timer(); // Initializes the timer to change color
            colorChangeTimer.Interval = 500;
            colorChangeTimer.Tick += (s, e) => ChangeColor(); //this unique visual effect changes color every 500 milliseconds
            colorChangeTimer.Start();
        }

        private void ChangeColor()
        {
            var random = new Random();
            currentColor = Color.FromArgb( //picks random rgb value, highest value is 256 hence the "random.next(256)"
                random.Next(256), //r
                random.Next(256), //g
                random.Next(256)); //b
        }

        public override void Draw(Graphics g, Control drawPanel)
        {
            using (var brush = new SolidBrush(currentColor))
            {
                g.FillEllipse(brush, x, y, width, height); //fills elipse (circle) with color
            }
        }
    }
}