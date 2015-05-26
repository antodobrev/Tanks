using System;

namespace Tanks
{
    class Brick
    {
        private int x;
        private int y;

        private const string symbol = "@";

        private const ConsoleColor color = ConsoleColor.DarkMagenta;

        private bool ruined;

        public Brick()
        {
            this.x = X;
            this.y = Y;
            this.ruined = false;
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
    }
}

