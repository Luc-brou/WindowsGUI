using System;
using System.Drawing;
using System.Windows.Forms;
using BouncyBall2023;

namespace TimerGradientDemo
{
    public partial class Form1 : Form
    {
        private readonly Timer myTimer = new Timer();
        private Ball ball;

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;

            // Create a ball inside the form’s client area
            ball = new Ball(this.ClientRectangle);
            ball.SetVelocity(-5, 0);

            myTimer.Interval = 30; // ~33 FPS
            myTimer.Tick += TimerEventProcessor;
            myTimer.Start();

            this.DoubleBuffered = true;

            // Update canvas bounds whenever the form is resized
            this.Resize += Form1_Resize;
        }

        private void TimerEventProcessor(object sender, EventArgs e)
        {
            ball.Move();
            Invalidate();
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

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (ball != null)
            {
                ball.UpdateCanvas(this.ClientRectangle);
            }
        }
    }
}