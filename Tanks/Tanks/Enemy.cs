﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks
{
    class Enemy : GameObject
    {
        private int x;
        private int y;

        public int boundaryX = Tanks.WindowWidth - Tanks.GameMenuWidth;
        public int boundaryY = Tanks.WindowHeight;

        private const string symbols = "O";
        public string Direction = "up";

        public static List<ConsoleColor> PossibleColors = new List<ConsoleColor>
        {
          ConsoleColor.Blue,
          ConsoleColor.DarkBlue,
          ConsoleColor.DarkCyan,
          ConsoleColor.DarkGreen,
          ConsoleColor.Green
        };
        public static List<string> PossibleDirections = new List<string>
        {
            "up",
            "down",
            "left",
            "right"
        };
        private ConsoleColor color = PossibleColors[random.Next(0, PossibleColors.Count)];
        private string direction = PossibleDirections[random.Next(0, PossibleDirections.Count)];
        public static Random random = new Random();

        private bool striked;

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
                this.y = value;
            }
        }

        public Enemy(int initX, int initY)
        {
            this.striked = false;
            //this.boundaryX = BoundaryX;
            //this.boundaryY = BoundaryY;
            this.x = initX;
            this.y = initY;
        }

        public void MoveRight()
        {
            if (x + 1 < boundaryX)
            {
                x++;
            }
        }
        public void MoveLeft()
        {
            if (x - 1 >= 0)
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
            if (y + 1 < boundaryY)
            {
                x++;
            }
        }

        public void Draw()
        {
            if (!striked)
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(x, y);
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
