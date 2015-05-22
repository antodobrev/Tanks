using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Tanks
{

    class Tanks
    {
        const int GameMenuWidth = 20;
        const int WindowHeight = 35;
        const int WindowWidth = 71;
        const int MaximumEnemies = 10;

        static void Main()
        {
            Console.BufferHeight = Console.WindowHeight = WindowHeight;
            Console.BufferWidth = Console.WindowWidth = WindowWidth;
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;

            int boundaryX = WindowWidth - GameMenuWidth;
            int boundaryY = WindowHeight;

            Random random = new Random();
            Tank ourTank = new Tank(boundaryX, boundaryY);
            Enemy[] enemies = new Enemy[MaximumEnemies];
            for (int i = 0; i < enemies.Length; i++)
            {
                int enemyPosition = random.Next(0, 3);
                switch (enemyPosition)
                {
                    case 0: enemies[i] = new Enemy(0, 0); break;
                    case 1: enemies[i] = new Enemy(boundaryX / 2, 0); break;
                    case 2: enemies[i] = new Enemy(boundaryX - 1, 0); break;
                    default:break;
                }
            }

            //Intro.FirstIntro();
            //Intro.SecondIntro();
            //DrawGameMenu();
            List<Bullet> playerBullets = new List<Bullet>();
            List<Bullet> enemiesBullets = new List<Bullet>();
            //int reloadingTime = 0;
            while (true)
            {
                DrawGameField();
                for (int i = 0; i < boundaryY; i++)
                {
                    PrintOnPosition(boundaryX, i, "|", ConsoleColor.White);
                }
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        ourTank.Direction = "left";
                        if (ourTank.X - 1 >= 0)
                        {
                            ourTank.X = ourTank.X - 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        ourTank.Direction = "right";
                        if (ourTank.X + 1 < boundaryX)
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
                        if (ourTank.Y + 1 < boundaryY)
                        {
                            ourTank.Y = ourTank.Y + 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.Spacebar)
                    {
                        Bullet bullet = new Bullet();
                        bullet.Draw(ourTank);
                        playerBullets.Add(bullet);
                    }
                }
                for (int i = 0; i < playerBullets.Count; i++)
                {
                    if (playerBullets[i].isVisible)
                    {
                        playerBullets[i].MoveBullet();
                        playerBullets[i].Draw();
                    }
                    else
                    {
                        playerBullets.Remove(playerBullets[i]);
                    }
                }

                ourTank.Draw();
                Thread.Sleep(70);
                Console.Clear();
            }
        }
        static void PrintOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.DarkBlue) // for strings
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }
        public static void DrawGameField()
        {
            Console.OutputEncoding = Encoding.Unicode;
            StreamReader reader = new StreamReader(@"..\..\res\playfield.txt");
            using (reader)
            {
                int line = 0;
                while (true)
                {
                    string fieldLine = reader.ReadLine();
                    if (fieldLine == null)
                    {
                        break;
                    }
                   PrintOnPosition(0, line, fieldLine, ConsoleColor.DarkRed);
                   line++;
                }
            }

            Console.WriteLine();
        }
        private static void DrawGameMenu()
        {
        }
    }
}
