using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimerGradientDemo
{
    public partial class Form1 : Form
    {
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        private int x = 0;
        public Form1()
        {
            InitializeComponent();
            myTimer.Tick += new EventHandler(TimerEventProcessor);
            myTimer.Interval = 800;
            //myTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void TimerEventProcessor(Object myObject,
                                            EventArgs myEventArgs)
        {
            Console.WriteLine("Tick");

            if (this.pbFrogFish.Visible == false)
            {
                this.pbFrogFish.Visible = true;
            }
            else
            {
                this.pbFrogFish.Visible = false;
            }

        }

        private void DrawEllipse()
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();//often passed in
            formGraphics.DrawEllipse(myPen, new Rectangle(x, 0, 100, 200));
            myPen.Dispose();
            formGraphics.Dispose();
        }

        private void FillEllipse()
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.FillEllipse(myBrush, new Rectangle(x + 100, 0, 100, 200));
            myBrush.Dispose();
            formGraphics.Dispose();
        }

        private void DrawGradient()
        {
            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            Rectangle rect = new Rectangle(250, 50, 200, 100);

            Color startColor = Color.Red;
            Color endColor = Color.Blue;

            using (LinearGradientBrush brush = new LinearGradientBrush(
                rect,
                startColor,
                endColor,
                LinearGradientMode.Horizontal))
            {
                formGraphics.FillRectangle(brush, rect);
            }
            formGraphics.Dispose();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Refresh();
            DrawEllipse();
            FillEllipse();
            DrawGradient();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            myTimer.Start();
        }
    }
}
