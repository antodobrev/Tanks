using System;
using System.Collections.Generic;

namespace Tanks
{
    class Tank : GameObject
    {
        private int x;
        private int y;

        private int prevX;
        private int prevY;

        public int boundaryX = Tanks.WindowWidth - Tanks.GameMenuWidth;
        public int boundaryY = Tanks.WindowHeight;

        private const string symbols = "O";
        private const ConsoleColor color = ConsoleColor.Yellow;

        private bool striked;
        static int livesLeft;

        public string Direction = "up";

        public Tank(int initX, int initY)
        {
            livesLeft = 5;
            this.striked = false;
            this.x = initX / 2;
            this.y = initY - 1;
        }

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
                return striked;
            }
            set
            {
                this.striked = value;
                if (striked == true)
                {
                    if (livesLeft > 0)
                    {
                        livesLeft--;
                    }
                }
            }
        }
        public static int LivesLeft
        {
            get
            {
                return livesLeft;
            }
            set
            {
                livesLeft = value;
            }
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
            if (y - 1 >= 0)
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
            else if (striked)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(x, y);
                Console.Write("X");
                Console.SetCursorPosition(prevX, prevY);
                Console.Write(' ');
                
                striked = false;
                this.prevX = x;
                this.prevY = y;
                this.x = boundaryX / 2;
                this.y = boundaryY - 1;
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
                    livesLeft--;
                    break;
                }
            }
        }

        public bool CheckLeftCell(List<Brick> bricks)
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
            return isEmpty;
        }
        public bool CheckRightCell(List<Brick> bricks)
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
            return isEmpty;
        }
        public bool CheckDownCell(List<Brick> bricks)
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
            return isEmpty;
        }
        public bool CheckUpCell(List<Brick> bricks)
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
            return isEmpty;
        }
    }
}
