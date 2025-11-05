using System;
using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    public abstract class Shape
    {
        // Basic properties
        protected int x, y, width, height;
        protected Color color;

        protected int[] velocity = new int[2];

        // Bounding box properties
        protected int bx, by, bwidth, bheight;

        // Constructor
        public Shape(int x, int y, int width, int height, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;

            // speed
            initializeVelocity();
        }

        public abstract void Draw(Graphics g, Control drawPanel);

        public void Move()
        {
            // match Java: update position and bounding box
            y += velocity[1];
            x += velocity[0];

            bx = x;
            by = y;
            bwidth = width;
            bheight = height;
        }

        public bool CollidesWith(Shape other)
        {
            return this.bx < other.bx + other.bwidth &&
                   this.bx + this.bwidth > other.bx &&
                   this.by < other.by + other.bheight &&
                   this.by + this.bheight > other.by;
        }

        public void ShapeCollide(Shape other)
        {
            if (this != other && this.CollidesWith(other))
            {
                this.velocity[0] = -this.velocity[0];
                this.velocity[1] = -this.velocity[1];
                other.velocity[0] = -other.velocity[0];
                other.velocity[1] = -other.velocity[1];
            }
        }

        public void initializeVelocity()
        {
            var random = new Random();

            // Ensure X velocity is never zero
            do
            {
                velocity[0] = random.Next(-3, 4); // -3..3 inclusive
            } while (velocity[0] == 0);

            // Ensure Y velocity is never zero
            do
            {
                velocity[1] = random.Next(-3, 4);
            } while (velocity[1] == 0);
        }

        public void DetectEdge(int panelWidth, int panelHeight)
        {
            if (x <= 0 || x + width >= panelWidth) velocity[0] *= -1;
            if (y <= 0 || y + height >= panelHeight) velocity[1] *= -1;
        }

        public void ClampToBounds(int panelWidth, int panelHeight)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x + width > panelWidth) x = panelWidth - width;
            if (y + height > panelHeight) y = panelHeight - height;

            // Update bounding box too
            bx = x;
            by = y;
            bwidth = width;
            bheight = height;
        }


    }
}