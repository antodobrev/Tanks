using System;
using System.Threading;

namespace Tanks
{

    class Tanks
    {
        const int GameMenuWidth = 30;
        const int WindowHeight = 40;
        const int WindowWidth = 71;
        const int MaximumEnemies = 10;
        static void Main()
        {
            Console.BufferHeight = Console.WindowHeight = 35;
            Console.BufferWidth = Console.WindowWidth = 71;
            int boundaryX = 71;
            int boundaryY = Console.BufferHeight;
            Console.CursorVisible = false;
            Tank ourTank = new Tank(boundaryX / 2, boundaryY);

            //DrawGameMenu();

            while (true)
            {
                Console.Clear();
                while (Console.KeyAvailable)
                {
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                    while (Console.KeyAvailable) Console.ReadKey(true);
                    if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        if (ourTank.X - 1 > 0)
                        {
                            ourTank.X = ourTank.X - 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        if (ourTank.X + 1 < 35)
                        {
                            ourTank.X = ourTank.X + 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.UpArrow)
                    {
                        if (ourTank.Y - 1 >= 0)
                        {
                            ourTank.Y = ourTank.Y - 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.DownArrow)
                    {
                        if (ourTank.Y + 1 < 35)
                        {
                            ourTank.Y = ourTank.Y + 1;
                        }
                    }
                    else if (pressedKey.Key == ConsoleKey.Spacebar)
                    {
                        //if (ourTank.Y + 2 < 35)
                        //{
                        //    ourTank.Y = ourTank.Y + 2;
                        //}
                    }
                }
                ourTank.Draw();
                Thread.Sleep(100);
            }
        }
        private static void DrawGameMenu()
        {
        }
        static void PrintOnPosition(int x, int y, string str, ConsoleColor color = ConsoleColor.DarkBlue) // for strings
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }
    }
}
