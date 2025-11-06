using System;
using System.Drawing;
using System.Windows.Forms;

namespace BouncyShapes
{
    public abstract class Shape
    {
        // position and size
        protected int x, y, width, height;

        protected Color color;

        // velocity initialized to random velocity
        protected int[] velocity = new int[2];

        // bounding box for collision detection
        protected int bx, by, bwidth, bheight;

        public Shape(int x, int y, int width, int height, Color color)
        {
            //itializes shape position, size, and color
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            this.color = color;

            InitializeVelocity();
            SyncBounds();
        }

     
        public abstract void Draw(Graphics g, Control drawPanel); //method to draw shape

        public virtual void Move() //method to move shape based on velocity
        {
            x += velocity[0];
            y += velocity[1];
            SyncBounds(); //updates bounding box (aka hitbox) after moving
        }

        public bool CollidesWith(Shape other)
        {
            //handles collision detetction based on other shapes hitbox width and height
            return bx < other.bx + other.bwidth &&
                   bx + bwidth > other.bx &&
                   by < other.by + other.bheight &&
                   by + bheight > other.by;
        }

        public virtual void ShapeCollide(Shape other)
        {
            if (this != other && CollidesWith(other))
            {
                //reverses velocity of both shapes when they collide
                velocity[0] = -velocity[0];
                velocity[1] = -velocity[1];
                other.velocity[0] = -other.velocity[0];
                other.velocity[1] = -other.velocity[1];

                SeparateFrom(other);
                SyncBounds();
                other.SyncBounds();
            }
        }

        private void InitializeVelocity() //method to initialize velocity to be random
        {
            var random = new Random();
            do { velocity[0] = random.Next(-3, 4); } while (velocity[0] == 0);
            do { velocity[1] = random.Next(-3, 4); } while (velocity[1] == 0);
        }

        public virtual void DetectEdge(int panelWidth, int panelHeight) //method to detect edges of window
        {
            if (x <= 0 || x + width >= panelWidth) velocity[0] *= -1;
            if (y <= 0 || y + height >= panelHeight) velocity[1] *= -1;
            SyncBounds();
        }

        public void ClampToBounds(int panelWidth, int panelHeight) //method to keep shapes within current window bounds
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x + width > panelWidth) x = panelWidth - width;
            if (y + height > panelHeight) y = panelHeight - height;
            SyncBounds();
        }

        protected void SyncBounds() // updates this shape's bounding box (hitbox) 
        {                           //to match its current position and size
            bx = x;
            by = y;
            bwidth = width;
            bheight = height;
        }

        protected void SeparateFrom(Shape other) //method to separate shapes after collision
        {
            //reverses their velocity a little bit on collision to "bump" them apart
            //stops the shapes from getting stuck together as much
            //(still happens if you place a shape directly on top of another shape though)
            x += Math.Sign(velocity[0]);
            y += Math.Sign(velocity[1]);
            other.x += Math.Sign(other.velocity[0]);
            other.y += Math.Sign(other.velocity[1]);
        }
    }
}