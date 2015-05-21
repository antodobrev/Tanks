using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class Bullet : GameObject
    {
        public static char body = '*';
        public string Direction = string.Empty;
        private const ConsoleColor color = ConsoleColor.White;
        private int x;
        private int y;

        private int boundaryX;
        private int boundaryY;
        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
        public Bullet()
        {

        }
        public Bullet(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public ConsoleColor Color
        {
            get
            {
                return color;
            }
        }
        public void MoveBullet()
        {
            if (y + 1 < boundaryY)  //If we are not at the bottom of the game field, move down.
            {
                y++;
            }
            else                   //Otherwise the rock becomes invisible
            {
                //visible = false;
            }
        }

        public void Draw(Tank tank)
        {
            int bulletInitX = tank.X;
            int bulletInitY = tank.Y;
            switch (tank.Direction)
            {
                case "up":
                    bulletInitY -= 1;
                    this.Direction = "up";
                    break;
                case "down":
                    bulletInitY += 1;
                    this.Direction = "down";
                    break;
                case "left":
                    bulletInitX -= 1;
                    this.Direction = "left";
                    break;
                case "right":
                    bulletInitX += 1;
                    this.Direction = "right";
                    break;
                default:
                    break;
            }

            Console.SetCursorPosition(bulletInitX, bulletInitY);
            Console.Write(body);
        }

        public void Draw() 
        {

            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(body);
        }
    }
}
