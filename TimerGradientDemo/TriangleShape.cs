using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    public class TriangleShape : Shape
    {
        public TriangleShape(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color) { }

        public override void Draw(Graphics g, Control drawPanel)
        {
            using (var brush = new SolidBrush(color))
            {
                Point[] pts =
                {
                    new Point(x, y + height),          // bottom-left
                    new Point(x + width / 2, y),       // top
                    new Point(x + width, y + height)   // bottom-right
                };
                g.FillPolygon(brush, pts);
            }
        }
    }
}