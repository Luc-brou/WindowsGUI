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
        int cycle = (Environment.TickCount / 10) % 510; // cycle length = 510 steps of 10 ms = 5.1 seconds
        int alpha = cycle <= 255 ? cycle : 510 - cycle; // fade in to 255, then fade out to 0
                                                        // whole fade in/out cycle lasts ~5.1 seconds

        using var brush = new SolidBrush(Color.FromArgb(alpha, color));
        g.FillRectangle(brush, x, y, width, height);

        // sync bounding box for collisions
        bx = x;
        by = y;
        bwidth = width;
        bheight = height;
    }
}