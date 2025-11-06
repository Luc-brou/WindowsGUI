using System;
using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    public class PolygonShape : Shape
    {
        private double angle = 0; // rotation angle in radians

        public PolygonShape(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color) { }

        public override void Move()
        {
            base.Move();
            angle += 0.05; // spin speed
        }

        public override void Draw(Graphics g, Control surface)
        {
            var center = new Point(x + width / 2, y + height / 2);

            // Define polygon vertices relative to bounding box
            var pts = new[]
            {
            RotatePoint(center, new Point(x, y), angle),
            RotatePoint(center, new Point(x + width, y), angle),
            RotatePoint(center, new Point(x + width, y + height), angle),
            RotatePoint(center, new Point(x, y + height), angle)
        };

            g.FillPolygon(new SolidBrush(color), pts);
            g.DrawPolygon(Pens.Black, pts);

            bx = x; by = y; bwidth = width; bheight = height;
        }

        private Point RotatePoint(Point center, Point p, double angle)
        {
            double dx = p.X - center.X;
            double dy = p.Y - center.Y;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new Point(
                (int)(center.X + dx * cos - dy * sin),
                (int)(center.Y + dx * sin + dy * cos));
        }
    }
}