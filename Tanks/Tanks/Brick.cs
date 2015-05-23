using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class Brick
    {
        public int x;
        public int y;
        public char body = '#';
        public Brick(int x, int y, char body)
        {
            this.x = x;
            this.y = y;
            this.body = body;
        }

        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(body);
        }
    }
}
