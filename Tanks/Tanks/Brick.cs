using System;

namespace Tanks
{
    class Brick
    {
        private int x;
        private int y;

        private char symbol = '@';
        private ConsoleColor color = ConsoleColor.DarkMagenta;

        private bool ruined = false;

        private bool solid = false;

        public Brick()
        {
            this.x = X;
            this.y = Y;
            this.color = Color;
            this.Solid = solid;
        }

        public ConsoleColor Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        public bool Solid
        {
            get { return solid; }
            set { solid = value; }
        }

        public char Symbol
        {
            get
            {
                return symbol;
            }
            set
            {
                symbol = value;
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

        static void PrintOnPosition(int x, int y, char str, ConsoleColor color = ConsoleColor.DarkBlue)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }
    }
}

