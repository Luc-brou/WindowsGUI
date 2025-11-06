using System;
using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    public class PolygonShape : Shape
    {
        private double angle = 0; //inital angle set to 0 degrees

        public PolygonShape(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color) { }

        public override void Move()
        {
            base.Move();
            angle += 0.05; //angle rotation based on move method
        }

        public override void Draw(Graphics g, Control surface)
        {
            var center = new Point(x + width / 2, y + height / 2); //center point of polygon

            var pts = new[]
            {
            RotatePoint(center, new Point(x, y), angle), //top-left
            RotatePoint(center, new Point(x + width, y), angle), //top-right
            RotatePoint(center, new Point(x + width, y + height), angle), //bottom-right
            RotatePoint(center, new Point(x, y + height), angle) //bottom-left
        };

            g.FillPolygon(new SolidBrush(color), pts); //fill polygon with color
            g.DrawPolygon(Pens.Black, pts);

            bx = x; by = y; bwidth = width; bheight = height; //update bounding box
        }

        private Point RotatePoint(Point center, Point p, double angle)
        {
            //this is all the math to rotate a point around a center point
            double dx = p.X - center.X;
            double dy = p.Y - center.Y;
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            return new Point( // returns new point after rotation
                (int)(center.X + dx * cos - dy * sin),
                (int)(center.Y + dx * sin + dy * cos));
        }
    }
}