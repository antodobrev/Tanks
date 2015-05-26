using System;
using System.Collections.Generic;
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
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.Clear();
            Console.CursorVisible = false;

            int boundaryX = WindowWidth - GameMenuWidth;
            int boundaryY = WindowHeight;

            Random random = new Random();
            Tank playerTank = new Tank(boundaryX, boundaryY);
            List<Enemy> enemies = new List<Enemy>();
            for (int i = 0; i < MaximumEnemies; i++)
            {
                int enemyPosition = random.Next(0, 3);
                switch (enemyPosition)
                {
                    case 0: enemies.Add(new Enemy(0, 0)); break;
                    case 1: enemies.Add(new Enemy(boundaryX / 2, 0)); break;
                    case 2: enemies.Add(new Enemy(boundaryX - 1, 0)); break;
                    default: break;
                }
            }

            //Intro.FirstIntro();
            //Intro.SecondIntro();
            List<Bullet> playerBullets = new List<Bullet>();
            List<Bullet> enemiesBullets = new List<Bullet>();
            List<Brick> bricks = BricksPositions();
            DrawBorder(boundaryX, boundaryY);
            DrawBricks(bricks);
            //int reloadingTime = 0;
            while (true)
            {
                DrawGameMenu();
                //Test for ruined brick
                //takes x and y, where the bullet hits the brick, and makes brick.Ruined true.
                //bricks = FindRuinedBrick(bricks, 10, 12);

                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        playerTank.Direction = "left";
                        playerTank.MoveLeft();
                    }
                    else if (pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        playerTank.Direction = "right";
                        playerTank.MoveRight();
                    }
                    else if (pressedKey.Key == ConsoleKey.UpArrow)
                    {
                        playerTank.Direction = "up";
                        playerTank.MoveUp();
                    }
                    else if (pressedKey.Key == ConsoleKey.DownArrow)
                    {
                        playerTank.Direction = "down";
                        playerTank.MoveDown();
                    }
                    else if (pressedKey.Key == ConsoleKey.Spacebar)
                    {
                        Bullet bullet = new Bullet();
                        bullet.Shoot(playerTank);
                        playerBullets.Add(bullet);
                    }
                }
                for (int i = 0; i < enemies.Count; i++)
                {
                    if (random.Next(0, 2) != 0)
                    {
                        enemies[i].Direction = Enemy.PossibleDirections[random.Next(0, Enemy.PossibleDirections.Count)];
                    }
                    switch (enemies[i].Direction)
                    {
                        case "left": enemies[i].MoveLeft(); break;
                        case "right": enemies[i].MoveRight(); break;
                        case "up": enemies[i].MoveUp(); break;
                        case "down": enemies[i].MoveDown(); break;
                        default: break;
                    }

                    if (random.Next(0, 20) == 0 && enemiesBullets.Count < 10)
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
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].Draw();
                }
                DetectCollitionWithBullet(enemiesBullets, bricks);
                DetectCollitionWithBullet(playerBullets, bricks);
                MoveBulletInField(playerBullets);
                MoveBulletInField(enemiesBullets);
                playerTank.CheckIfHit(enemiesBullets);
                for (int i = 0; i < enemies.Count; i++)
                {
                    enemies[i].CheckIfHit(playerBullets);
                    if (enemies[i].Striked)
                    {
                        enemies.Remove(enemies[i]);
                    }
                }

                RemoveRuinedBrick(bricks);
                if (Tank.LivesLeft < 0)
                {
                    Console.Clear();
                    Console.Beep(625, 225);
                    PrintOnPosition(31, boundaryY/2, "GAME OVER", ConsoleColor.Red);
                    Console.ReadLine();
                        Environment.Exit(0);
                }
                Thread.Sleep(100);
                //Console.Clear();
            }
        }

        public static void DetectCollitionWithBullet(List<Bullet> enemiesBullets, List<Brick> bricks)
        {
            foreach (var brick in bricks)
            {
                foreach (var bullet in enemiesBullets)
                {
                    if (brick.X == bullet.X && brick.Y == bullet.Y)
                    {
                        brick.Ruined = true;
                        bullet.isVisible = false;
                    }
                }
            }
        }

        private static void MoveBulletInField(List<Bullet> bullets)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                if (bullets[i].isVisible)
                {
                    bullets[i].MoveBullet();
                    bullets[i].Draw();
                }
                else
                {
                    Console.SetCursorPosition(bullets[i].X, bullets[i].Y);
                    Console.Write(' ');
                    bullets.Remove(bullets[i]);
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

        static void PrintOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.DarkBlue)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        private static void DrawGameMenu()
        {
            PrintOnPosition(53, 5, "Score:", ConsoleColor.Magenta);
            PrintOnPosition(53, 7, string.Format("Lives: {0}", Tank.LivesLeft), ConsoleColor.Magenta);
        }

        private static void DrawBricks(List<Brick> bricks)
        {
            foreach (Brick brick in bricks)
            {
                if (brick.Ruined == false)
                {
                    PrintOnPosition(brick.X, brick.Y, brick.Symbol, brick.Color);
                }
            }
        }

        public static List<Brick> BricksPositions()
        {

            List<Brick> bricksPositions = new List<Brick>();

            for (int i = 7; i < 45; i++)
            {
                Brick brick = new Brick
                {
                    X = i,
                    Y = 12
                };
                bricksPositions.Add(brick);
            }

            for (int i = 8; i < 28; i++)
            {
                Brick brick = new Brick
                {
                    X = 25,
                    Y = i
                };
                bricksPositions.Add(brick);
            }

            for (int i = 1; i < 15; i++)
            {
                Brick brick = new Brick
                {
                    X = i,
                    Y = 7
                };
                bricksPositions.Add(brick);
            }

            for (int i = 2; i < 11; i++)
            {
                Brick brick = new Brick
                {
                    X = 15,
                    Y = i
                };
                bricksPositions.Add(brick);
            }

            for (int i = 6; i < 39; i++)
            {
                Brick brick = new Brick
                {
                    X = i,
                    Y = 30
                };
                bricksPositions.Add(brick);
            }

            for (int i = 27; i < 49; i++)
            {
                Brick brick = new Brick
                {
                    X = i,
                    Y = 25
                };
                bricksPositions.Add(brick);
            }

            return bricksPositions;
        }

        public static List<Brick> RemoveRuinedBrick(List<Brick> bricks)
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].Ruined)
                {
                    Console.SetCursorPosition(bricks[i].X, bricks[i].Y);
                    Console.Write(' ');
                    bricks.Remove(bricks[i]);
                }
            }
            return bricks;
        }
    }
}
