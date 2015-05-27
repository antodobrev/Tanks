using System;

namespace Tanks
{
    class Brick
    {
        private int x;
        private int y;

        private const string symbol = "@";
        private const ConsoleColor color = ConsoleColor.DarkMagenta;

        private bool ruined = false;

        public Brick()
        {
            this.x = X;
            this.y = Y;
        }

        public ConsoleColor Color
        {
            get
            {
                return color;
            }
        }

        public string Symbol
        {
            get
            {
                return symbol;
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

        public bool Ruined
        {
            get
            {
                return ruined;
            }
            set
            {
                this.ruined = value;
            }
        }

        public void DrawBricks()
        {
                if (this.ruined == false)
                {
                    PrintOnPosition(this.x, this.y, symbol, color);
                }
        }

        static void PrintOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.DarkBlue)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }
    }
}

