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
        private const ConsoleColor color = ConsoleColor.White;
        public int X
        {
            get { return this.X; }
            set { this.X = value; }
        }

        public int Y
        {
            get { return this.Y; }
            set { this.Y = value; }
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
        public void Draw(Tank tank)
        {
            int bulletInitX = tank.X;
            int bulletInitY = tank.Y;
            switch (tank.Direction)
            {
                case "up":
                    bulletInitY -= 1;
                    break;
                case "down":
                    bulletInitY += 1;
                    break;
                case "left":
                    bulletInitX -= 1;
                    break;
                case "right":
                    bulletInitX += 1;
                    break;
                default:
                    break;
            }

            Console.SetCursorPosition(bulletInitX, bulletInitY);
            Console.Write(body);
        }
        public void Draw() 
        {

        }
    }
}
