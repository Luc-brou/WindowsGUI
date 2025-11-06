using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace BouncyShapes
{
    public class PictureShape : Shape
    {
        private readonly PictureBox pictureBox;

        private readonly byte[] soundBytes;

        [DllImport("winmm.dll", SetLastError = true)] // tells to use PlaySound multimedia DLL from windows
        private static extern bool PlaySound(byte[] pszSound, IntPtr hmod, uint fdwSound); 

        // flags
        private const uint SND_ASYNC = 0x0001; // play asynchronously
        private const uint SND_MEMORY = 0x0004;
        private const uint SND_NODEFAULT = 0x0002; // avoid system default sound
        private const uint SND_NOSTOP = 0x0010;    // don’t stop currently playing sounds

        public PictureShape(Form parent, int x, int y, int width, int height, Image image)
            : base(x, y, width, height, Color.Transparent)
        {
            pictureBox = new PictureBox // PictureBox to hold image
            {
                //initializes PictureBox properties
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = width,
                Height = height,
                Left = x,
                Top = y
            };
            parent.Controls.Add(pictureBox);

            bx = x; by = y; bwidth = width; bheight = height;

            using (var s = Properties.Resources.collide)
            {
                //copies sound effect into memory to load slightly faster
                soundBytes = new byte[s.Length];
                s.Read(soundBytes, 0, soundBytes.Length);
            }
        }

        private void PlaySFX()
        {
            //plays sound from memory with specified flags
            PlaySound(soundBytes, IntPtr.Zero, SND_ASYNC | SND_MEMORY | SND_NODEFAULT | SND_NOSTOP);
        }

        public override void Draw(Graphics g, Control surface)
        {
            // keeps PictureBox position synced
            pictureBox.Left = x;
            pictureBox.Top = y;

            // blinks border every ~300 ms
            bool on = ((Environment.TickCount / 300) % 2) == 1;

            if (on) //if the border is active, draw it
            {
                using (Graphics pg = pictureBox.CreateGraphics()) // draws on PictureBox directly
                    using (Pen redPen = new Pen(Color.Red, 3)) 
                {
                    pg.DrawRectangle(redPen, 0, 0, pictureBox.Width - 1, pictureBox.Height - 1);
                }
            }
            else
            {
                // clears previous border by invalidating the PictureBox
                pictureBox.Invalidate();
            }

            // manually syncs bounding box for collision checks
            bx = x;
            by = y;
            bwidth = width;
            bheight = height;
        }

        public override void Move() // method to move shape
        {
            base.Move();
            pictureBox.Left = x;
            pictureBox.Top = y;
        }

        public override void DetectEdge(int panelWidth, int panelHeight) // method to detect edge collision
        {
            int oldVX = velocity[0]; //old velocity x axis
            int oldVY = velocity[1]; //old velocity y axis

            base.DetectEdge(panelWidth, panelHeight); //detects edge of window panel

            if (velocity[0] != oldVX || velocity[1] != oldVY)
            {
                //plays sound effect on edge collision
                PlaySFX();
            }

            pictureBox.Left = x;
            pictureBox.Top = y;
        }

        public override void ShapeCollide(Shape other)
        {
            bool collided = this.CollidesWith(other);
            base.ShapeCollide(other);

            if (collided)
            {
                //plays sound effect on shape-shape collision
                PlaySFX();
            }
        }
    }
}