using BouncyShapes;
using System;
using System.Drawing;
using System.Windows.Forms;

public class RectangleShape : Shape
{
    public RectangleShape(int x, int y, int width, int height, Color color)
        : base(x, y, width, height, color) { }

    public override void Draw(Graphics g, Control surface)
    {
        // Cycle alpha between 0 and 255
        int cycle = (Environment.TickCount / 10) % 510; // 0–509
        int alpha = cycle <= 255 ? cycle : 510 - cycle; // fade in then out

        using var brush = new SolidBrush(Color.FromArgb(alpha, color));
        g.FillRectangle(brush, x, y, width, height);

        // Sync bounding box for collisions
        bx = x;
        by = y;
        bwidth = width;
        bheight = height;
    }
}