using System;
using System.Drawing;
using System.Windows.Forms;
using BouncyBall2023; // reference your Ball class namespace

namespace TimerGradientDemo
{
    public partial class Form1 : Form
    {
        private readonly Timer myTimer = new Timer();
        private Ball ball;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true; // reduce flicker

            // Create a ball inside the form’s client area
            ball = new Ball(this.ClientRectangle);

            // Force velocity to always go left
            ball.SetVelocity(-5, 0);

            myTimer.Interval = 30; // ~33 FPS
            myTimer.Tick += TimerEventProcessor;
            myTimer.Start();
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            ball.Move();
            Invalidate(); // trigger repaint
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            ball.Draw(e.Graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // nothing needed here
        }
    }
}