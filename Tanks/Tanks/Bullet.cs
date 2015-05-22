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
        public bool isVisible = true;

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
        public void Remove()
        {
            if ((x == boundaryX) || (x == 0))
            {
                isVisible = false;
            }
            else if (y == boundaryY || y == 0)
            {
                isVisible = false;
            }
        }
        public void MoveBullet()
        {

            switch (Direction)
            {
                case "up":
                    y -= 1;
                    break;
                case "down":
                    y += 1;
                    break;
                case "left":
                    x -= 1;
                    break;
                case "right":
                    x += 1;
                    break;
                default:
                    break;
            }
            Remove();
        }

        public void Draw(Tank tank)
        {
            x = tank.X;
            y = tank.Y;
            if (tank.Direction == "up" && tank.Y != 0)
            {
                y -= 1;
                this.Direction = "up";
            }
            else if (tank.Direction == "down" && tank.Y != boundaryY - 1)
            {
                y += 1;
                this.Direction = "down";
            }
            else if (tank.Direction == "left" && tank.X != 0)
            {
                x -= 1;
                this.Direction = "left";
            }
            else if (tank.Direction == "right" && tank.X != boundaryX - 1)
            {
                x += 1;
                this.Direction = "right";
            }
            Console.SetCursorPosition(x, y);
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
