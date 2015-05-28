using System;
using System.Collections.Generic;

namespace Tanks
{
    class Enemy
    {
        private int x;
        private int y;

        private int prevX;
        private int prevY;

        public int boundaryX = Tanks.WindowWidth - Tanks.GameMenuWidth;
        public int boundaryY = Tanks.WindowHeight;

        private const string symbols = "O";
        public string Direction = "up";

        public static List<ConsoleColor> PossibleColors = new List<ConsoleColor>
        {
          ConsoleColor.Blue,
          ConsoleColor.Cyan,
          ConsoleColor.DarkCyan,
          ConsoleColor.DarkGreen,
          ConsoleColor.Green
        };
        public static List<string> PossibleDirections = new List<string>
        {
            "up",
            "down",
            "left",
            "right"
        };
        private ConsoleColor color = PossibleColors[random.Next(0, PossibleColors.Count)];
        private string direction = PossibleDirections[random.Next(0, PossibleDirections.Count)];
        public static Random random = new Random();

        private bool striked;

        public ConsoleColor Color
        {
            get
            {
                return color;
            }
        }

        public int X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }
        public int Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        public int PrevX
        {
            get
            {
                return this.prevX;
            }
            set
            {
                this.prevX = value;
            }
        }
        public int PrevY
        {
            get
            {
                return this.prevY;
            }
            set
            {
                this.prevY = value;
            }
        }

        public bool Striked
        {
            get
            {
                return this.striked;
            }
            set
            {
                this.striked = value;
            }
        }

        public Enemy(int initX, int initY)
        {
            this.striked = false;
            this.x = initX;
            this.y = initY;
        }

        public void MoveRight()
        {
            if (x + 1 < boundaryX)
            {
                prevX = x;
                prevY = y;
                x++;
            }
        }
        public void MoveLeft()
        {
            if (x - 1 >= 0)
            {
                prevX = x;
                prevY = y;
                x--;
            }
        }
        public void MoveUp()
        {
            if (y - 1 > 0)
            {
                prevX = x;
                prevY = y;
                y--;
            }
        }
        public void MoveDown()
        {
            if (y + 1 < boundaryY)
            {
                prevX = x;
                prevY = y;
                y++;
            }
        }

        public void CheckIfHit(List<Bullet> bullets)
        {
            foreach (var bullet in bullets)
            {
                if (bullet.X == this.X && bullet.Y == this.Y)
                {
                    this.striked = true;
                    bullets.Remove(bullet);
                    Console.SetCursorPosition(bullet.X,bullet.Y);
                    Console.WriteLine(" ");
                    break;
                }
            }
        }

        public void Draw()
        {
            if (!striked)
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(x, y);
                Console.Write(symbols);
                Console.SetCursorPosition(prevX, prevY);
                Console.Write(' ');
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(x, y);
                Console.Write("X");
            }
        }

        public bool CheckLeftCell(List<Brick> bricks,Tank playerTank,List<Enemy> enemies)
        {
            bool isEmpty = true;
            foreach (var brick in bricks)
            {
                if (x - 1 == brick.X && y == brick.Y)
                {
                    isEmpty = false;
                    break;
                }
            }

            if (x - 1 == playerTank.X && y == playerTank.Y)
            {
                isEmpty = false;
            }

            foreach (var enemy in enemies)
            {
                if (x - 1 == enemy.X && y == enemy.Y)
                {
                    isEmpty = false;
                    break;
                }
            }
            return isEmpty;
        }
        public bool CheckRightCell(List<Brick> bricks,Tank playerTank, List<Enemy> enemies)
        {
            bool isEmpty = true;
            foreach (var brick in bricks)
            {
                if (x + 1 == brick.X && y == brick.Y)
                {
                    isEmpty = false;
                    break;
                }
            }

            if (x + 1 == playerTank.X && y == playerTank.Y)
            {
                isEmpty = false;
            }

            foreach (var enemy in enemies)
            {
                if (x + 1 == enemy.X && y == enemy.Y)
                {
                    isEmpty = false;
                    break;
                }
            }
            return isEmpty;
        }
        public bool CheckDownCell(List<Brick> bricks,Tank playerTank, List<Enemy> enemies)
        {
            bool isEmpty = true;
            foreach (var brick in bricks)
            {
                if (x == brick.X && y + 1 == brick.Y)
                {
                    isEmpty = false;
                    break;
                }
            }

            if (x == playerTank.X && y + 1 == playerTank.Y)
            {
                isEmpty = false;
            }

            foreach (var enemy in enemies)
            {
                if (x == enemy.X && y + 1 == enemy.Y)
                {
                    isEmpty = false;
                    break;
                }
            }
            return isEmpty;
        }
        public bool CheckUpCell(List<Brick> bricks,Tank playerTank,List<Enemy> enemies)
        {
            bool isEmpty = true;
            foreach (var brick in bricks)
            {
                if (x == brick.X && y - 1 == brick.Y)
                {
                    isEmpty = false;
                    break;
                }
            }
            if (x == playerTank.X && y - 1 == playerTank.Y)
            {
                isEmpty = false;
            }
            foreach (var enemy in enemies)
            {
                if (x == enemy.X && y - 1 == enemy.Y)
                {
                    isEmpty = false;
                    break;
                }
            }
            return isEmpty;
        }
    }
}
