using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    public class PolygonShape : Shape
    {
        public PolygonShape(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color) { }

        public override void Draw(Graphics g, Control drawPanel)
        {
            using (var brush = new SolidBrush(color))
            {
                Point[] pts =
                {
                    new Point(x + width / 4, y),               // top-left
                    new Point(x + (3 * width) / 4, y),         // top-right
                    new Point(x + width, y + height / 2),      // right
                    new Point(x + (3 * width) / 4, y + height),// bottom-right
                    new Point(x + width / 4, y + height),      // bottom-left
                    new Point(x, y + height / 2)               // left
                };
                g.FillPolygon(brush, pts);
            }
        }
    }
}