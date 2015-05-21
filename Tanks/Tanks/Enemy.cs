using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class Enemy:GameObject
    {
        private int x;
        private int y;

        private int boundaryX;
        private int boundaryY;

        private const string symbols = "O";
        public static List<ConsoleColor> PossibleColors = new List<ConsoleColor>
            {
                ConsoleColor.Red,
                ConsoleColor.Blue,
                ConsoleColor.DarkBlue,
                ConsoleColor.DarkYellow,
                ConsoleColor.DarkGreen
            };
        private ConsoleColor color = PossibleColors[random.Next(0, PossibleColors.Count)];
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
        public Enemy(int BoundaryX, int BoundaryY)
        {
            this.striked = false;
            this.boundaryX = BoundaryX;
            this.boundaryX = BoundaryY;
            this.x = boundaryX;
            this.y = boundaryY;
        }
        public void MoveRight()
        {
            if (x + 1 < boundaryX)
            {
                x++;
            }
        }
        public void MoveLeft()
        {
            if (x - 1 >= 0)
            {
                x--;
            }
        }
        public void MoveUp()
        {
            if (y - 1 > 0)
            {
                x--;
            }
        }
        public void MoveDown()
        {
            if (y + 1 < boundaryY)
            {
                x++;
            }
        }
        public void Draw()
        {
            if (!striked)
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(x, y);
                Console.Write(symbols);
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
