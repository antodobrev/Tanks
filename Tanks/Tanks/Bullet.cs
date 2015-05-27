using System;

namespace Tanks
{
    class Bullet : GameObject
    {
        public static char body = '*';
        public string Direction = string.Empty;
        private const ConsoleColor color = ConsoleColor.Red;
        private int x;
        private int y;
        private int prevX;
        private int prevY;
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
        public void Shoot(Tank tank)
        {
            this.x = tank.X;
            this.y = tank.Y;
            this.boundaryX = tank.boundaryX;
            this.boundaryY = tank.boundaryY;
            if (tank.Direction == "up" && tank.Y != 0)
            {
                this.y -= 1;
                this.Direction = "up";
            }
            else if (tank.Direction == "down" && tank.Y != boundaryY - 1)
            {
                this.y += 1;
                this.Direction = "down";
            }
            else if (tank.Direction == "left" && tank.X != 0)
            {
                this.x -= 1;
                this.Direction = "left";
            }
            else if (tank.Direction == "right" && tank.X != boundaryX - 1)
            {
                this.x += 1;
                this.Direction = "right";
            }
            Console.SetCursorPosition(x, y);
            Console.Write(body);
        }
        public void Shoot(Enemy tank)
        {
            this.x = tank.X;
            this.y = tank.Y;
            this.boundaryX = tank.boundaryX;
            this.boundaryY = tank.boundaryY;
            if (tank.Direction == "up" && tank.Y != 0)
            {
                this.y -= 1;
                this.Direction = "up";
            }
            else if (tank.Direction == "down" && tank.Y != boundaryY - 1)
            {
                this.y += 1;
                this.Direction = "down";
            }
            else if (tank.Direction == "left" && tank.X != 0)
            {
                this.x -= 1;
                this.Direction = "left";
            }
            else if (tank.Direction == "right" && tank.X != boundaryX - 1)
            {
                this.x += 1;
                this.Direction = "right";
            }
            if (this.Direction != "")
            {
                Console.SetCursorPosition(x, y);
                Console.Write(body);
            }
        }
        public void MoveBullet()
        {

            switch (Direction)
            {
                case "up":
                    if (y - 1 >= 0)
                    {
                        prevX = x;
                        prevY = y;
                        y--;
                    }
                    break;
                case "down":
                    if (y + 1 < boundaryY)
                    {
                        prevX = x;
                        prevY = y;
                        y++;
                    }
                    break;
                case "left":
                    if (x - 1 >= 0)
                    {
                        prevX = x;
                        prevY = y;
                        x--;
                    }
                    break;
                case "right":
                    if (x + 1 < boundaryX)
                    {
                        prevX = x;
                        prevY = y;
                        x++;
                    }
                    break;
                default:
                    break;
            }
            Remove();
        }
        public void Remove()
        {
            if (((x == boundaryX - 1) && Direction != "up" && Direction != "down") || (x == 0 && Direction != "up" && Direction != "down"))
            {
                isVisible = false;
            }
            else if (((y == boundaryY - 1) && Direction != "left" && Direction != "right") || (y == 0 && Direction != "left" && Direction != "right"))
            {
                isVisible = false;
            }
        }
        public void Draw()
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(this.x, this.y);
            Console.Write(body);
            Console.SetCursorPosition(this.prevX, this.prevY);
            Console.Write(' ');
        }
    }
}
