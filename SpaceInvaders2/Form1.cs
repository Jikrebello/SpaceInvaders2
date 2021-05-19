using System;
using System.Drawing;
using System.Windows.Forms;

namespace SpaceInvaders2
{
    public partial class SpaceInvaders : Form
    {
        // --- Internals ---
        Player player;
        Image playerImage = Image.FromFile("SpaceShip_2.png");
        Bullet bullet;
        Image bulletImage = Image.FromFile("Bullet.png");
        Enemy[] enemies = new Enemy[MAX_ENEMIES];
        Image enemyImage = Image.FromFile("EnemyAlien.png");
        bool gameOver;
        const int MAX_ENEMIES = 50;
        int numOfEnemies;
        const int START_LEVEL = 1;
        int currentLevel;


        // --- Constructor ---
        public SpaceInvaders()
        {
            InitializeComponent();
            StartUpGame();
        }

        // --- Methods ---
        void StartUpGame()
        {
            player = new Player(ClientRectangle.Width, ClientRectangle.Height, playerImage);
            bullet = new Bullet(bulletImage);
            numOfEnemies = 5;
            for (int i = 0; i < numOfEnemies; i++)
                enemies[i] = new Enemy(ClientRectangle.Width, ClientRectangle.Height, i, enemyImage);
            currentLevel = START_LEVEL;
            gameOver = false;
        }
        public void UpdateGame()
        {
            if (gameOver == false)
            {
                for (int i = 0; i < numOfEnemies; i++)
                    enemies[i].Move();

                bullet.Move();
                player.MoveLeft();
                player.MoveRight();
                CheckCollisions();
                CheckGameOver();
                CheckAllEnemiesDead();
            }
        }
        void CheckCollisions() // loop through all the enemies in the enemies[] and perform CheckCollisions() on them
        {
            for (int i = 0; i < numOfEnemies; i++)
                enemies[i].CheckCollisions(player, bullet);
        }
        void CheckGameOver()
        {
            if (player.Lives == 0)
            {
                gameOver = true;
                MessageBox.Show("Game Over!" + Environment.NewLine + "Score: " + player.Score);
            }
        }
        void CheckAllEnemiesDead() // check if all enemies are dead, if they are, reset their positions, increase the amount of them and the level
        {
            bool allEnemiesDead = true;
            for (int i = 0; i < numOfEnemies; i++)
            {
                if (enemies[i].Alive == true)
                {
                    allEnemiesDead = false;
                    break;
                }
                else
                    allEnemiesDead = true;
            }

            if (allEnemiesDead)
            {
                if (currentLevel <= 5) // add more enemies until level 5
                {
                    currentLevel++;
                    numOfEnemies += 5;
                }
                else if (currentLevel > 10) // after level 5 continue adding levels but don’t add more enemies
                    currentLevel++;

                for (int i = 0; i < numOfEnemies; i++)
                    enemies[i] = new Enemy(ClientRectangle.Width, ClientRectangle.Height, i, enemyImage);
            }
        }

        // --- Events ---

        // --- Events --
        private void SpaceInvaders_Paint(object sender, PaintEventArgs e)
        {
            Graphics gfx = e.Graphics;

            SolidBrush blackBrush = new SolidBrush(Color.Black);

            gfx.FillRectangle(blackBrush, ClientRectangle);

            if (gameOver == false)
            {
                bullet.Draw(gfx);
                player.Draw(gfx);

                for (int i = 0; i < numOfEnemies; i++)
                    enemies[i].Draw(gfx);
            }
        }
        private void SpaceInvaders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                player.Right = true;
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                player.Left = true;

            if (e.KeyCode == Keys.Space)
                bullet.FireBullet(player);
        }
        private void SpaceInvaders_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                player.Right = false;
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                player.Left = false;
        }
    }
}
