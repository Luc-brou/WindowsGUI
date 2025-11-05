using BouncyShapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    public partial class Form1 : Form
    {
        private readonly Timer timer;
        private readonly List<Shape> shapes = new List<Shape>();
        private int clickCount = 0;

        public Form1()
        {
            //InitializeComponent();
            DoubleBuffered = true;

            // Add a couple of shapes so collisions are visible
            shapes.Add(new CircleShape(100, 100, 50, 50, Color.Blue));

            timer = new Timer();
            timer.Interval = 30; // ~33 FPS
            timer.Tick += Timer_Tick;
            timer.Start();

            this.MouseClick += Form1_MouseClick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Move and bounce each shape
            foreach (var shape in shapes)
            {
                shape.Move();
                shape.DetectEdge(this.ClientSize.Width, this.ClientSize.Height);
            }

            // Collision detection between all pairs
            for (int i = 0; i < shapes.Count; i++)
            {
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    shapes[i].ShapeCollide(shapes[j]);
                }
            }

            Invalidate(); // repaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var shape in shapes)
                shape.Draw(e.Graphics, this);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            Shape s;
            switch (clickCount % 4)
            {
                case 0:
                    s = new CircleShape(e.X, e.Y, 50, 50, Color.Orange);
                    break;
                case 1:
                    s = new RectangleShape(e.X, e.Y, 60, 40, Color.Green);
                    break;
                case 2:
                    s = new TriangleShape(e.X, e.Y, 50, 50, Color.Pink);
                    break;
                default:
                    s = new PolygonShape(e.X, e.Y, 60, 60, Color.Purple);
                    break;
            }

            shapes.Add(s);
            clickCount++;
        }

        private const int WM_SIZING = 0x0214;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_SIZING)
            {
                foreach (var shape in shapes)
                {
                    shape.ClampToBounds(this.ClientSize.Width, this.ClientSize.Height);
                }
                Invalidate(); // redraw immediately
            }
        }

    }
}