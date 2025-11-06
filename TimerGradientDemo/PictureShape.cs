using System;
using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    public class PictureShape : Shape
    {
        private readonly PictureBox pictureBox;

        public PictureShape(Form parent, int x, int y, int width, int height, Image image)
            : base(x, y, width, height, Color.Transparent) // base color not used
        {
            pictureBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Width = width,
                Height = height,
                Left = x,
                Top = y
            };

            parent.Controls.Add(pictureBox);

            // Initialize bounding box
            bx = x;
            by = y;
            bwidth = width;
            bheight = height;
        }

        // Only required abstract method from Shape
        public override void Draw(Graphics g, Control drawPanel)
        {
            // PictureBox renders itself, so nothing to draw manually.
            // But keep bounding box synced in case collisions are checked.
            bx = x;
            by = y;
            bwidth = width;
            bheight = height;

            pictureBox.Left = x;
            pictureBox.Top = y;
        }

        // Optional: override Move to keep PictureBox in sync
        public new void Move()
        {
            base.Move(); // update x, y, bounding box
            pictureBox.Left = x;
            pictureBox.Top = y;
        }

        // Optional: override DetectEdge to clamp PictureBox too
        public new void DetectEdge(int panelWidth, int panelHeight)
        {
            base.DetectEdge(panelWidth, panelHeight);
            pictureBox.Left = x;
            pictureBox.Top = y;
        }
    }
}