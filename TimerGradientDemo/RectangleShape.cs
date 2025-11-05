using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    public class RectangleShape : Shape
    {
        public RectangleShape(int x, int y, int width, int height, Color color)
            : base(x, y, width, height, color) { }

        public override void Draw(Graphics g, Control drawPanel)
        {
            using (var brush = new SolidBrush(color))
            {
                g.FillRectangle(brush, x, y, width, height);
            }
        }
    }
}