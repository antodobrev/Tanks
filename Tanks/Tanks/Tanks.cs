using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Tanks
{

    class Tanks
    {
        public const int GameMenuWidth = 20;
        public const int WindowHeight = 35;
        public const int WindowWidth = 71;
        public const int MaximumEnemies = 10;

        static void Main()
        {
            Console.BufferHeight = Console.WindowHeight = WindowHeight;
            Console.BufferWidth = Console.WindowWidth = WindowWidth;
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;

            int boundaryX = WindowWidth - GameMenuWidth;
            int boundaryY = WindowHeight;

            Random random = new Random();
            Tank playerTank = new Tank(boundaryX, boundaryY);
            Enemy[] enemies = new Enemy[MaximumEnemies];
            for (int i = 0; i < enemies.Length; i++)
            {
                int enemyPosition = random.Next(0, 3);
                switch (enemyPosition)
                {
                    case 0: enemies[i] = new Enemy(0, 0); enemies[i].Direction=Enemy.PossibleDirections[random.Next(0, Enemy.PossibleDirections.Count)]; break;
                    case 1: enemies[i] = new Enemy(boundaryX / 2, 0); break;
                    case 2: enemies[i] = new Enemy(boundaryX - 1, 0); break;
                    default:break;
                }
            }

            //Intro.FirstIntro();
            //Intro.SecondIntro();
            List<Bullet> playerBullets = new List<Bullet>();
            List<Bullet> enemiesBullets = new List<Bullet>();
            //int reloadingTime = 0;
            while (true)
            {
                DrawGameField();
                DrawGameMenu();
                DrawBorder(boundaryX, boundaryY);
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        playerTank.Direction = "left";
                        if (playerTank.X - 1 >= 0)
                        {
                            playerTank.X = playerTank.X - 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        playerTank.Direction = "right";
                        if (playerTank.X + 1 < boundaryX)
                        {
                            playerTank.X = playerTank.X + 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.UpArrow)
                    {
                        playerTank.Direction = "up";
                        if (playerTank.Y - 1 >= 0)
                        {
                            playerTank.Y = playerTank.Y - 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.DownArrow)
                    {
                        playerTank.Direction = "down";
                        if (playerTank.Y + 1 < boundaryY)
                        {
                            playerTank.Y = playerTank.Y + 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.Spacebar)
                    {
                        Bullet bullet = new Bullet();
                        bullet.Shoot(playerTank);
                        playerBullets.Add(bullet);
                    }
                }
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (random.Next(0, 2) != 0)
                    {
                        enemies[i].Direction = Enemy.PossibleDirections[random.Next(0, Enemy.PossibleDirections.Count)];
                    }
                    if (enemies[i].Direction == "left")
                    {
                        if (enemies[i].X - 1 >= 0)
                        {
                            enemies[i].X = enemies[i].X - 1;
                        }
                    }
                    else if (enemies[i].Direction == "right")
                    {
                        if (enemies[i].X + 1 < boundaryX)
                        {
                            enemies[i].X = enemies[i].X + 1;
                        }
                    }
                    else if (enemies[i].Direction == "up")
                    {
                        if (enemies[i].Y - 1 >= 0)
                        {
                            enemies[i].Y = enemies[i].Y - 1;
                        }
                    }
                    else if (enemies[i].Direction == "down")
                    {
                        if (enemies[i].Y + 1 < boundaryY)
                        {
                            enemies[i].Y = enemies[i].Y + 1;
                        }
                    }
                    if (random.Next(0, 3) != 0 && enemiesBullets.Count < 10)
                    {
                        Bullet bullet = new Bullet();
                        bullet.Shoot(enemies[i]);
                        if (bullet.Direction != "")
                        {
                            enemiesBullets.Add(bullet);
                        }
                        
                    }
                }

                playerTank.Draw();
                for (int i = 0; i < enemies.Length; i++)
                {
                    enemies[i].Draw();
                }
                MoveBulletInField(playerBullets);
                MoveBulletInField(enemiesBullets);
                Thread.Sleep(70);
                Console.Clear();
            }
        }

        private static void MoveBulletInField(List<Bullet> playerBullets)
        {
            for (int i = 0; i < playerBullets.Count; i++)
            {
                if (playerBullets[i].isVisible)
                {
                    playerBullets[i].MoveBullet();
                    playerBullets[i].Draw();
                }
                if (!playerBullets[i].isVisible)
                {
                    playerBullets.Remove(playerBullets[i]);
                }
            }
        }

        private static void DrawBorder(int boundaryX, int boundaryY)
        {
            for (int i = 0; i < boundaryY; i++)
            {
                PrintOnPosition(boundaryX, i, "|", ConsoleColor.White);
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
            PrintOnPosition(53, 5, "Score:", ConsoleColor.Magenta);
        }
    }
}
