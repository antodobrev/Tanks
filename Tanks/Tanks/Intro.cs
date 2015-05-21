using System;
using System.IO;
using System.Text;
using System.Threading;

namespace Tanks
{
    class Intro
    {
        public static void FirstIntro()
        {
            using (StreamReader reader = new StreamReader(@"..\..\T.txt"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    line = reader.ReadLine();
                    Console.WriteLine(line);
                }
            }
            Thread.Sleep(1000);
            Console.Clear();
            using (StreamReader reader = new StreamReader(@"..\..\A.txt"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    line = reader.ReadLine();
                    Console.WriteLine(line);
                }
            }
            Thread.Sleep(1000);
            Console.Clear();
            using (StreamReader reader = new StreamReader(@"..\..\N.txt"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    line = reader.ReadLine();
                    Console.WriteLine(line);
                }
            }
            Thread.Sleep(1000);
            Console.Clear();
            using (StreamReader reader = new StreamReader(@"..\..\K.txt"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    line = reader.ReadLine();
                    Console.WriteLine(line);
                }
            }
            Thread.Sleep(1000);
            Console.Clear();
            using (StreamReader reader = new StreamReader(@"..\..\S.txt"))
            {
                string line = reader.ReadLine();
                while (line != null)
                {
                    line = reader.ReadLine();
                    Console.WriteLine(line);
                }
            }
            Thread.Sleep(1000);
            Console.Clear();
        }
        public static void SecondIntro()
        {
            Console.OutputEncoding = Encoding.Unicode;
            StreamReader reader = new StreamReader(@"../../tanks_intro.txt");
            using (reader)
            {
                while (true)
                {
                    string introLine = reader.ReadLine();
                    if (introLine == null)
                    {
                        break;
                    }

                    Console.WriteLine(introLine);
                }
            }

            Console.WriteLine();
            Console.WriteLine(@"                     Press Enter to start the game!");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
