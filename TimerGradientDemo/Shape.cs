using System;
using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    /// <summary>
    /// Abstract base class for all shapes in the screensaver.
    /// Handles position, velocity, bounding box, movement, edge detection,
    /// and collision logic. Subclasses override Draw() to render themselves.
    /// </summary>
    public abstract class Shape
    {
        // Position and size
        protected int x, y, width, height;

        // Visual color
        protected Color color;

        // Velocity vector [vx, vy]
        protected int[] velocity = new int[2];

        // Bounding box for collision detection
        protected int bx, by, bwidth, bheight;

        public Shape(int x, int y, int width, int height, Color color)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;

            InitializeVelocity();
            SyncBounds();
        }

        /// <summary>
        /// Draw the shape. Must be implemented by subclasses.
        /// </summary>
        public abstract void Draw(Graphics g, Control drawPanel);

        /// <summary>
        /// Move the shape according to its velocity.
        /// </summary>
        public virtual void Move()
        {
            x += velocity[0];
            y += velocity[1];
            SyncBounds();
        }

        /// <summary>
        /// Check if this shape’s bounding box overlaps another’s.
        /// </summary>
        public bool CollidesWith(Shape other)
        {
            return bx < other.bx + other.bwidth &&
                   bx + bwidth > other.bx &&
                   by < other.by + other.bheight &&
                   by + bheight > other.by;
        }

        /// <summary>
        /// Handle collision with another shape.
        /// Default behavior: flip both velocities and separate slightly.
        /// </summary>
        public virtual void ShapeCollide(Shape other)
        {
            if (this != other && CollidesWith(other))
            {
                velocity[0] = -velocity[0];
                velocity[1] = -velocity[1];
                other.velocity[0] = -other.velocity[0];
                other.velocity[1] = -other.velocity[1];

                SeparateFrom(other);
                SyncBounds();
                other.SyncBounds();
            }
        }

        /// <summary>
        /// Initialize velocity with non‑zero random values.
        /// </summary>
        private void InitializeVelocity()
        {
            var random = new Random();
            do { velocity[0] = random.Next(-3, 4); } while (velocity[0] == 0);
            do { velocity[1] = random.Next(-3, 4); } while (velocity[1] == 0);
        }

        /// <summary>
        /// Bounce off edges of the panel.
        /// </summary>
        public virtual void DetectEdge(int panelWidth, int panelHeight)
        {
            if (x <= 0 || x + width >= panelWidth) velocity[0] *= -1;
            if (y <= 0 || y + height >= panelHeight) velocity[1] *= -1;
            SyncBounds();
        }

        /// <summary>
        /// Clamp shape inside panel bounds (used on resize).
        /// </summary>
        public void ClampToBounds(int panelWidth, int panelHeight)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x + width > panelWidth) x = panelWidth - width;
            if (y + height > panelHeight) y = panelHeight - height;
            SyncBounds();
        }

        /// <summary>
        /// Update bounding box to current position/size.
        /// </summary>
        protected void SyncBounds()
        {
            bx = x;
            by = y;
            bwidth = width;
            bheight = height;
        }

        /// <summary>
        /// Push shapes slightly apart after collision to prevent overlap.
        /// </summary>
        protected void SeparateFrom(Shape other)
        {
            x += Math.Sign(velocity[0]);
            y += Math.Sign(velocity[1]);
            other.x += Math.Sign(other.velocity[0]);
            other.y += Math.Sign(other.velocity[1]);
        }
    }
}