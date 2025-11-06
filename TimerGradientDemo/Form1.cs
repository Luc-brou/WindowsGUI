using BouncyShapes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media; 

namespace BouncyShapes
{
    public partial class Form1 : Form
    {
        private readonly Timer timer;
        private readonly List<Shape> shapes = new List<Shape>();
        private int clickCount = 0; //initializes click counter

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true; //reduces "flickering"

            timer = new Timer(); //initializes timer
            timer.Interval = 30; // ~33 FPS
            timer.Tick += Timer_Tick; //incrments timer
            timer.Start(); //starts timer

            this.MouseClick += Form1_MouseClick; //handles mouse clicks
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            foreach (var shape in shapes)
            {
                shape.Move(); //moves shape based on velocity
                shape.DetectEdge(this.ClientSize.Width, this.ClientSize.Height); //detects edge collision
            }

            for (int i = 0; i < shapes.Count; i++)
            {
                for (int j = i + 1; j < shapes.Count; j++)
                {
                    shapes[i].ShapeCollide(shapes[j]); //handles shape-shape collision
                }
            }

            Invalidate(); // repaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            foreach (var shape in shapes)
                shape.Draw(e.Graphics, this); //draws each shape
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) // on mouse click event
        {
            Shape s;
            Random rnd = new Random();

            // generates a random color
            Color randomColor = Color.FromArgb( // creates random color from rgb values
                rnd.Next(256), // R
                rnd.Next(256), // G
                rnd.Next(256)  // B
            );

            switch (clickCount % 5) // switch case statement "% 5" because there are 5 shapes
            {
                //passes in that random color to each shape's color paramter
                case 0:
                    s = new CircleShape(e.X, e.Y, 50, 50, randomColor); 
                    break;
                case 1:
                    s = new RectangleShape(e.X, e.Y, 60, 40, randomColor);
                    break;
                case 2:
                    s = new TriangleShape(e.X, e.Y, 50, 50, randomColor);
                    break;
                case 3:
                    s = new PolygonShape(e.X, e.Y, 60, 60, randomColor);
                    break;
                default: //this is the image shape, it has a sound effect on collision
                    s = new PictureShape(this, e.X, e.Y, 60, 60, Properties.Resources.imageshape);
                    var player = new SoundPlayer(Properties.Resources.collide); //this sound plays on collision
                    player.Play(); //this plays sound on creation
                    break;
            }

            shapes.Add(s); //adds shape to list
            clickCount++; //increments click counter
        }

        private const int WM_SIZING = 0x0214; // communicates window resizing
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m); //processes window messages

            if (m.Msg == WM_SIZING) //if window is being resized
            {
                foreach (var shape in shapes)
                {
                    shape.ClampToBounds(this.ClientSize.Width, this.ClientSize.Height); //ensures shapes stay within bounds
                }
                Invalidate(); // redraw immediately
            }
        }

    }
}