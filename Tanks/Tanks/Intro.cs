using System;
using System.IO;
using System.Text;

namespace Tanks
{
    public static class Intro
    {
        public static void SecondIntro()
        {
            Console.OutputEncoding = Encoding.Unicode; 
            StreamReader reader = new StreamReader("tanks_intro.txt");
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
