using System;
using System.Windows.Forms;

namespace SpaceInvaders2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DateTime currentTime;
            DateTime pastTime;
            TimeSpan deltaTime;
            _ = DateTime.Now;
            pastTime = DateTime.Now;


            SpaceInvaders form = new SpaceInvaders();
            form.Show();

            while (form.Created == true)
            {
                currentTime = DateTime.Now;
                deltaTime = currentTime - pastTime;
                if (deltaTime.TotalMilliseconds > 10)
                {
                    Application.DoEvents();
                    form.UpdateGame();
                    form.Refresh();
                    pastTime = DateTime.Now;
                }
            }
        }
    }
}
