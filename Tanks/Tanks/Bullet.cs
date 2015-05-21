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
        public void Kill()
        {
            if (x == 35 || x == 0)
            {
                isVisible = false;
            }
            else if (y == 34 || y == 0)
            {
                isVisible = false;
            }
        }
        public void MoveBullet()
        {
            
            switch (Direction)
            {
                case "up": y -= 1;
                    break;
                case "down": y += 1;
                    break;
                case "left": x -= 1;
                    break;
                case "right": x += 1;
                    break;
                default:
                    break;
            }
            Kill();
        }

        public void Draw(Tank tank)
        {
            x = tank.X;
            y = tank.Y;
            switch (tank.Direction)
            {
                case "up":
                    y -= 1;
                    this.Direction = "up";
                    break;
                case "down":
                    y += 1;
                    this.Direction = "down";
                    break;
                case "left":
                    x -= 1;
                    this.Direction = "left";
                    break;
                case "right":
                    x += 1;
                    this.Direction = "right";
                    break;
                default:
                    break;
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
