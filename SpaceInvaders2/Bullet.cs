using System.Drawing;

namespace SpaceInvaders2
{
    class Bullet
    {
        /* The bullet needs to be fired from the centre of the players x and on the players local y at 0.
         * Once fired, the bullet will travel up the form until it reaches the top and is out of bounds, whereby it’ll be repositioned out of scope of the form until the fire button is pressed
         * When the shoot event is fired, the bullet will move to the players current position and then move upwards.*/
        
        // --- Properties ---
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }

        // --- Internals ---
        int x, y; //  x and y location
        int width, height; // player horizontal and vertical dimension
        Image bulletImage; // graphic
        int speed; // how fast the bullet will move
        bool isActive; // if bullet is active, draw it and make it move


        // --- Constructor ---
        public Bullet(Image _bulletImage)
        {
            bulletImage = _bulletImage;
            width = bulletImage.Width;
            height = bulletImage.Height;
            speed = 5;
            ResestPosition();
        }

        // --- Methods ---
        void GetBulletStartPosition(Player _player)
        {
            x = _player.X + (_player.Width / 3);
            y = _player.Y;
        }
        public void FireBullet(Player _player)
        {
            if (isActive == false)
            {
                GetBulletStartPosition(_player);
                isActive = true;
            }
        }
        public void ResestPosition()
        {
            isActive = false;
            x = -100; y = 200;// move off screen on both x and y
        }
        public void Move()
        {
            if (y + height < 0) // gone out of bounds
                ResestPosition();
            else
                y -= speed; // just send it up towards the top of the screen
        }
        public void Draw(Graphics _gfx) // gets fed the graphics event object and uses that to draw
        {
            if (isActive)
                _gfx.DrawImage(bulletImage, X, Y);
        }
    }
}
