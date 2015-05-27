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
        public static int score = 0;
        public static int level = 1;

        static void Main()
        {
            Console.BufferHeight = Console.WindowHeight = WindowHeight;
            Console.BufferWidth = Console.WindowWidth = WindowWidth;
            Console.OutputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;

            int boundaryX = WindowWidth - GameMenuWidth;
            int boundaryY = WindowHeight;

            //Intro.FirstIntro();
            //Intro.SecondIntro();

            Random random = new Random();
            int MaximumEnemies = 10;
            Tank playerTank = new Tank(boundaryX, boundaryY);
            List<Enemy> enemies = new List<Enemy>();
            DrawBorder(boundaryX, boundaryY);
            //int reloadingTime = 0;
            while (true)
            {
                Console.Clear();
                PrintOnPosition(32, boundaryY / 2, string.Format("LEVEL {0}", level), ConsoleColor.Red);
                Thread.Sleep(700);
                Console.Clear();

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
                List<Bullet> playerBullets = new List<Bullet>();
                List<Bullet> enemiesBullets = new List<Bullet>();

                List<Brick> bricks = BricksPositions();
                foreach (Brick brick in bricks)
                {
                    brick.DrawBricks();
                }
                Thread.Sleep(500);
                DrawBorder(boundaryX, boundaryY);
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
                            if (playerTank.CheckLeftCell(bricks))
                                playerTank.MoveLeft();
                        }
                        else if (pressedKey.Key == ConsoleKey.RightArrow)
                        {
                            playerTank.Direction = "right";
                            if (playerTank.CheckRightCell(bricks))
                                playerTank.MoveRight();
                        }
                        else if (pressedKey.Key == ConsoleKey.UpArrow)
                        {
                            playerTank.Direction = "up";
                            if (playerTank.CheckUpCell(bricks))
                                playerTank.MoveUp();
                        }
                        else if (pressedKey.Key == ConsoleKey.DownArrow)
                        {
                            playerTank.Direction = "down";
                            if (playerTank.CheckDownCell(bricks))
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
                            case "left":
                                if (enemies[i].CheckLeftCell(bricks,playerTank,enemies))
                                    enemies[i].MoveLeft(); break;
                            case "right":
                                if (enemies[i].CheckRightCell(bricks,playerTank,enemies))
                                    enemies[i].MoveRight(); break;
                            case "up":
                                if (enemies[i].CheckUpCell(bricks,playerTank,enemies))
                                    enemies[i].MoveUp(); break;
                            case "down":
                                if (enemies[i].CheckDownCell(bricks,playerTank,enemies))
                                    enemies[i].MoveDown(); break;
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
                            switch (enemies[i].Color)
                            {
                                case ConsoleColor.Blue: score += 200; break;
                                case ConsoleColor.Cyan: score += 250; break;
                                case ConsoleColor.DarkCyan: score += 300; break;
                                case ConsoleColor.DarkGreen: score += 350; break;
                                case ConsoleColor.Green: score += 400; break;
                                default: break;
                            }
                            enemies.Remove(enemies[i]);
                        }
                    }

                    RemoveRuinedBrick(bricks);
                    PrintSolidBricks(bricks);
                    if (enemies.Count == 0)
                    {
                        Console.Clear();
                        //Console.Beep(625, 225);
                        PrintOnPosition(28, boundaryY / 2, "LEVEL COMPLETED", ConsoleColor.Red);
                        level++;
                        MaximumEnemies += 6;
                        playerTank.X = boundaryX / 2;
                        playerTank.Y = boundaryY - 1;
                        Thread.Sleep(700);
                        break;

                    }
                    if (Tank.LivesLeft < 0)
                    {
                        Console.Clear();
                        Console.Beep(625, 225);
                        PrintOnPosition(31, boundaryY / 2, "GAME OVER", ConsoleColor.Red);
                        ScoreList.Score(score);
                        if (Console.ReadKey().Key == ConsoleKey.Y)
                        {
                            score = 0;
                            Tank.LivesLeft = 5;
                            MaximumEnemies = 10;
                            break;
                        }
                        else
                        {
                            Environment.Exit(0);
                        }
                        Console.ReadLine();
                        break;
                    }
                    Thread.Sleep(100);
                }
                Console.Clear();
            }
        }

        public static void DetectCollitionWithBullet(List<Bullet> bullets, List<Brick> bricks)
        {
            foreach (var brick in bricks)
            {
                foreach (var bullet in bullets)
                {
                    if (brick.X == bullet.X && brick.Y == bullet.Y)
                    {
                        brick.Ruined = true;
                        bullet.isVisible = false;
                    }
                    else if (brick.X == bullet.X && brick.Y == bullet.Y)
                    {
                        bullet.isVisible = false;
                    }
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
            PrintOnPosition(53, 03, string.Format("Level: {0}",level),ConsoleColor.Magenta);
            PrintOnPosition(53, 5, string.Format("Score: {0}", score), ConsoleColor.Magenta);
            PrintOnPosition(53, 7, string.Format("Lives: {0}", Tank.LivesLeft), ConsoleColor.Magenta);
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

            for (int i = 1; i < 17; i++)
            {
                Brick brick = new Brick
                {
                    X = 4,
                    Y = i
                };
                brick.Color = ConsoleColor.DarkGray;
                brick.Solid = true;
                bricksPositions.Add(brick);
            }

            for (int i = 18; i < 34; i++)
            {
                Brick brick = new Brick
                {
                    X = 46,
                    Y = i
                };
                brick.Color = ConsoleColor.DarkGray;
                brick.Solid = true;
                bricksPositions.Add(brick);
            }

            for (int i = 7; i < 45; i++)
            {
                Brick brick = new Brick
                {
                    X = i,
                    Y = 13
                };
                brick.Color = ConsoleColor.DarkGray;
                brick.Solid = true;
                bricksPositions.Add(brick);
            }

            return bricksPositions;
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

        public static List<Brick> RemoveRuinedBrick(List<Brick> bricks)
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].Ruined && bricks[i].Solid == false)
                {
                    Console.SetCursorPosition(bricks[i].X, bricks[i].Y);
                    Console.Write(' ');
                    bricks.Remove(bricks[i]);
                }
            }
            return bricks;
        }

        public static void PrintSolidBricks(List<Brick> bricks)
        {
            for (int i = 0; i < bricks.Count; i++)
            {
                if (bricks[i].Ruined && bricks[i].Solid)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.SetCursorPosition(bricks[i].X, bricks[i].Y);
                    Console.Write(bricks[i].Symbol);
                }
            }
        }
    }
}
