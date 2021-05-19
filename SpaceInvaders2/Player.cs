using System.Drawing;

namespace SpaceInvaders2
{
    class Player
    {
        /* Player needs to be placed at the bottom of the form in the middle.
         * Player can only move horizontally (-x or x) according to its SPEED (SPEED = DISTANCE/TIME).
         * Player can only shoot a single bullet at a time.
         * The players width and height will match the dimensions of the image it is attached to, this will be used to determine boundaries for enemy collisions and the boundaries of the form.
         * The player will have a set amount of lives that when depleted will move the player to its start position.
         * The player will keep a record of its score which will be determined by incrementing by 1 for each enemy killed.
         * The player will have a set amount of lives that when depleted will move the player to its start position.
         * The player will keep a record of its score which will be determined by incrementing by 1 for each enemy killed.*/
        
        // --- Properties ---
        public int X { get { return x; } }
        public int Y { get { return y; } }
        public bool Left { get; set; }
        public bool Right { get; set; }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public int Score { get { return score; } }
        public int Lives { get { return lives; } }


        // --- Internals ---
        // --- Player Movement ---
        int x, y; // player x and player y location
        const int Y_OFFSET = 10;
        int formWidth, formHeight; // the total dimension size of the form
        int width, height; // player horizontal and vertical dimension
        Image playerImage; // player graphic
        int speed; // how fast the player will move
        // --- Player Statistics ---
        int score = 0; // player score
        const int MAX_LIVES = 3;
        int lives; // total lives

        // --- Constructor ---
        public Player(int _formWidth, int _formHeight, Image _playerImage)
        {
            formWidth = _formWidth;
            formHeight = _formHeight;
            playerImage = _playerImage;

            width = playerImage.Width;
            height = playerImage.Height;
            x = formWidth / 2 - width;
            y = formHeight - (height + Y_OFFSET);

            Left = false;
            Right = false;
            lives = MAX_LIVES;
            speed = 5;
        }

        // --- Methods ---
        public void MoveLeft() // moves the player left
        {
            if (Left)
            {
                if (x <= 0) // reached the boundary on the left hand side
                    x = 0; // keep it stuck on the right side of the ship
                else
                    x -= speed; // move left

            }
        }
        public void MoveRight() // moves the player right
        {
            if (Right)
            {
                if (x >= formWidth - width) // reached the boundary on the left hand side
                    x = formWidth - width; // keep it stuck on the left side of the ship
                else
                    x += speed; // move right
            }
        }
        public void ResetPosition() // after losing a life, the players position gets reset to the starting position
        {
            x = formWidth / 2 - width;
            y = formHeight - (height + Y_OFFSET);
        }
        public void IncreaseScore() // increments the players score by one for each enemy killed
        {
            score++;
        }
        public void DecreaseLives() // decreases the players live by one for each enemy that gets past the player or crashes into the player
        {
            lives--;
            ResetPosition();
        }
        public void Draw(Graphics _gfx) // gets fed the graphics event object and uses that to draw
        {
            _gfx.DrawImage(playerImage, X, Y);
        }
    }
}
