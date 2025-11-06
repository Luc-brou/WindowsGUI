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
        int cycle = (Environment.TickCount / 10) % 40; // 0–39ms cycle
        int growth = cycle <= 20 ? cycle : 40 - cycle; // grow then shrink cycle

        //below is all the logic to draw the triangle and make it grow and shrink

        int newWidth = width + growth;
        int newHeight = height + growth;

        var p1 = new Point(x + newWidth / 2, y);
        var p2 = new Point(x, y + newHeight);
        var p3 = new Point(x + newWidth, y + newHeight);
        var pts = new[] { p1, p2, p3 };

        using var brush = new SolidBrush(color); //colors the triangle
        g.FillPolygon(brush, pts);

        // sync bounding box (hitbox) to new size
        bx = x;
        by = y;
        bwidth = newWidth;
        bheight = newHeight;
    }
}