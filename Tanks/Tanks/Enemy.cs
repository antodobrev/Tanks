using System;
using System.Collections.Generic;

namespace Tanks
{
    class Enemy : GameObject
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
          ConsoleColor.DarkBlue,
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
    }
}
