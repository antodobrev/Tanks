using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Tanks
{

    class Tanks
    {
        const int GameMenuWidth = 30;
        const int WindowHeight = 40;
        const int WindowWidth = 71;
        const int MaximumEnemies = 10;
        static void Main()
        {
            Console.BufferHeight = Console.WindowHeight = 35;
            Console.BufferWidth = Console.WindowWidth = 71;
            Console.OutputEncoding = Encoding.Unicode;
            int boundaryX = 71;
            int boundaryY = Console.BufferHeight;
            Console.CursorVisible = false;
            Tank ourTank = new Tank(40, boundaryY);
            Enemy newEnemy = new Enemy(0, 1);
            Intro.FirstIntro();
            Intro.SecondIntro();
            //DrawGameMenu();
            List<Bullet> bullets = new List<Bullet>();
            while (true)
            {
                Console.Clear();
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        ourTank.Direction = "left";
                        if (ourTank.X - 1 > 0)
                        {
                            ourTank.X = ourTank.X - 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        ourTank.Direction = "right";
                        if (ourTank.X + 1 < 35)
                        {
                            ourTank.X = ourTank.X + 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.UpArrow)
                    {
                        ourTank.Direction = "up";
                        if (ourTank.Y - 1 >= 0)
                        {
                            ourTank.Y = ourTank.Y - 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.DownArrow)
                    {
                        ourTank.Direction = "down";
                        if (ourTank.Y + 1 < 35)
                        {
                            ourTank.Y = ourTank.Y + 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.Spacebar)
                    {
                        Bullet bullet = new Bullet();
                        bullet.Draw(ourTank);
                        bullets.Add(bullet);
                    }
                }
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].isVisible)
                    {
                        bullets[i].MoveBullet();
                        bullets[i].Draw();
                    }
                    else
                    {
                        bullets.Remove(bullets[i]);
                    }
                }
                newEnemy.Draw();
                ourTank.Draw();
                Thread.Sleep(70);
            }
        }
        private static void DrawGameMenu()
        {
        }
        static void PrintOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.DarkBlue) // for strings
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }
    }
}
