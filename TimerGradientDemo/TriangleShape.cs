using BouncyShapes;
using System;
using System.Drawing;
using System.Windows.Forms;

public class TriangleShape : Shape
{
    public TriangleShape(int x, int y, int width, int height, Color color)
        : base(x, y, width, height, color) { }

    public override void Draw(Graphics g, Control surface)
    {
        // Oscillate growth between 0 and 20 pixels
        int cycle = (Environment.TickCount / 10) % 40; // 0–39
        int growth = cycle <= 20 ? cycle : 40 - cycle; // grow then shrink

        int newW = width + growth;
        int newH = height + growth;

        var p1 = new Point(x + newW / 2, y);
        var p2 = new Point(x, y + newH);
        var p3 = new Point(x + newW, y + newH);
        var pts = new[] { p1, p2, p3 };

        using var brush = new SolidBrush(color);
        g.FillPolygon(brush, pts);

        // Sync bounding box to new size
        bx = x;
        by = y;
        bwidth = newW;
        bheight = newH;
    }
}