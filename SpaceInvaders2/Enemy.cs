using System.Drawing;

namespace SpaceInvaders2
{
    class Enemy
    {
        /* The enemy will spawn at the top of the form and slowly move towards the bottom of the screen.
         * Once a enemy is killed, they will be placed off screen until all of them are killed.*/
        
        // --- Properties ---
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public bool Alive { get { return alive; } }

        // --- Internals ---
        int x, y; // x and y location
        int formWidth, formHeight; // form Width and Height
        int width, height; // horizontal and vertical dimension
        Image enemyImage; // enemy graphic
        int speed; // how fast the enemy will move
        int pastY;
        int moveDownAmount;
        const int LEFT = -1; const int RIGHT = 1; const int DOWN = 0; // indicates which direction they moving in. They are 1 and -1 because they are used in math to calculate new direction after moving down
        int currentDirection;
        int pastDirection;
        bool alive;


        // --- Constructor ---
        public Enemy(int _formWidth, int _formHeight, int _position, Image _enemyImage)
        {
            formWidth = _formWidth;
            formHeight = _formHeight;
            enemyImage = _enemyImage;

            width = enemyImage.Width;
            height = enemyImage.Height;

            x = 0;
            y = _position * (-height * 2); // makes sure they don’t overlap, 

            speed = 2;
            pastY = 0;
            moveDownAmount = height + 2; // the amount it moves down once its reached either left or right
            currentDirection = DOWN;
            pastDirection = LEFT;
            alive = true;
        }

        // --- Methods ---
        public void Move()
        {
            if (alive)
            {
                // check the boundaries so it isn’t out of bounds
                if (y + height > formHeight)
                {
                    ResetPosition();
                    alive = false;
                }

                if (currentDirection == RIGHT)
                {
                    x += speed;// move the current direction right
                    if (x + width > formWidth)  // check if the direction needs to move down yet
                    {
                        pastY = y;
                        pastDirection = RIGHT;
                        currentDirection = DOWN;
                    }
                }
                else if (currentDirection == LEFT)
                {
                    x -= speed;// move the current direction right
                    if (x <= 0) // check if the direction needs to move down yet
                    {
                        pastY = y;
                        pastDirection = LEFT;
                        currentDirection = DOWN;
                    }
                }
                else if (currentDirection == DOWN)// check the current direction is down
                {
                    y += speed;
                    if (y >= pastY + moveDownAmount) // if its reached its max moved-down amount already then change it to move left or right
                        currentDirection = -pastDirection; // change the direction to the opposite direction it was before it moved down
                }
            }
        }
        void ResetPosition() // moves the enemy off screen and out of the way of the bullet.
        {
            x = 0;
            y = -height;
        }
        bool IsCollidingPlayer(Player _player)
        {
            // check for where the player isn’t in relation to the enemy
            if (_player.X + _player.Width <= x || _player.X >= x + width || //Checks if the players position on the x is either smaller or larger than the enemy
                _player.Y + _player.Height <= y || _player.Y >= y + height) // Checks if the players position on the y is either larger or smaller than the enemy
                return false; // No collisions happening
            else
                return true; // Collision has happened
        }
        bool IsCollidingBullet(Bullet _bullet)
        {
            // Checks for where the bullet isn't in relation to the enemy
            if (_bullet.X + _bullet.Width <= x || _bullet.X >= x + width || //Checks if the bullets position on the x is either smaller or larger than the enemy
                _bullet.Y + _bullet.Height <= y || _bullet.Y >= y + height) // Checks if the bullets position on the y is either larger or smaller than the enemy
                return false; // No collisions happening
            else
                return true; // Collision has happened
        }


        public void CheckCollisions(Player _player, Bullet _bullet)
        {
            if (IsCollidingPlayer(_player)) // dies, resets the enemyPos off screen, takes a live off the player, resets the players position
            {
                alive = false;
                ResetPosition();
                _player.DecreaseLives();
                _player.ResetPosition();
            }

            if (IsCollidingBullet(_bullet)) // dies, resets the enemy position off screen, adds to the players score, resets the bullets position
            {
                alive = false;
                ResetPosition();
                _player.IncreaseScore();
                _bullet.ResestPosition();
            }
        }

        public void Draw(Graphics _gfx) // gets fed the graphics event object and uses that to draw
        {
            if (alive)
            {
                _gfx.DrawImage(enemyImage, X, Y);
            }
        }
    }
}
