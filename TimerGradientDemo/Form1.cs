using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    public partial class Form1 : Form
    {
        private readonly Timer myTimer = new Timer();
        private readonly List<Shape> shapes = new List<Shape>();
        private int clickCount = 0;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            // Add an initial circle so you see something right away
            shapes.Add(new CircleShape(100, 100, 50, 50, Color.Blue));

            myTimer.Interval = 30; // ~33 FPS
            myTimer.Tick += TimerEventProcessor;
            myTimer.Start();

            this.MouseClick += Form1_MouseClick;
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            foreach (var shape in shapes)
            {
                shape.Move();
                shape.DetectEdge(this.ClientSize.Width, this.ClientSize.Height);
            }

            for (int i = 0; i < shapes.Count; i++)
                for (int j = i + 1; j < shapes.Count; j++)
                    shapes[i].ShapeCollide(shapes[j]);

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var shape in shapes)
                shape.Draw(e.Graphics, this);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            // Add another circle at the click location for now
            shapes.Add(new CircleShape(e.X, e.Y, 50, 50, Color.Red));
            clickCount++;
        }
    }
}