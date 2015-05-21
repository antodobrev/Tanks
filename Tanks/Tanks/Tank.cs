using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class Tank : GameObject
    {
        private int x;
        private int y;

        private int boundaryX;
        private int boundaryY;

        private const string symbols = "O";
        private const ConsoleColor color = ConsoleColor.Yellow;

        private bool striked;

        int livesLeft;

        public string Direction = "up";

        public Tank(int BoundaryX, int BoundaryY)
        {
            this.livesLeft = 5;
            this.striked = false;
            this.boundaryX = BoundaryX;
            this.boundaryX = BoundaryY;
            this.x = BoundaryX / 2;
            this.y = BoundaryY - 1;
            //this.Direction = "up";                            
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
                this.y=value;
            }
        }

        public bool Collided
        {
            get
            {
                return striked;
            }
            set
            {
                striked = value;
                if (striked == true)
                {
                    if (livesLeft > 0)
                    {
                        livesLeft--;
                    }
                }
            }
        }

        public int LivesLeft
        {
            get
            {
                return livesLeft;
            }
        }
        public void MoveRight()
        {
            if (x + 1 < boundaryX - 1)
            {
                x++;
            }
        }
        public void MoveLeft()
        {
            if (x - 1 > 0)
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
            if (y + 1 < boundaryY - 4)
            {
                x++;
            }
        }
        public void Draw()
        {
            if (!striked)
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(x - 1, y);
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
