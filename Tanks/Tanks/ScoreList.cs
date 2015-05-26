using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Tanks
{
    public static class ScoreList
    {
        //filling the dictionary with all the stats of score.text
        //Tanks class calls this method first.
        public static void Score(int score)
        {
            StreamReader reader = new StreamReader(@"..\..\score.txt");
            Dictionary<string, int> scoreList = new Dictionary<string, int>();
            Thread.Sleep(2000);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            //current player name
            Console.WriteLine();
            Console.Write(@"                        Enter your name: ");
            string name = Console.ReadLine().ToUpper();
            Console.Clear();
            string[] scorePair = { "pesho", "0" };
            using (reader)
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    scorePair = line.Split();
                    scoreList.Add(scorePair[0], int.Parse(scorePair[1]));
                }
                //adding cuurent player name and score
                if (!scoreList.ContainsKey(name))
                {
                    scoreList.Add(name, score);
                }
                else
                {
                    //adding only the best score
                    if (scoreList[name] < score)
                    {
                        scoreList[name] = score;
                    }
                }
            }
            //sorting by descending order all the scores
            var sortedScoreList = scoreList.OrderByDescending(points => points.Value).ToDictionary(k => k.Key, v => v.Value);
            //print the score on the console
            PrintOnTheConsole(sortedScoreList, name);
            //write the score int the score.txt
            WriteScore(sortedScoreList);
        }

        //write the score in the .txt file to have it for a long time :)
        public static void WriteScore(Dictionary<string, int> sortedScoreList)
        {
            StreamWriter writeScore = new StreamWriter(@"..\..\score.txt");
            using (writeScore)
            {
                foreach (var pair in sortedScoreList)
                {
                    writeScore.WriteLine("{0} {1}", pair.Key, pair.Value);
                }
            }
        }
        //printing the updated score on the console.
        //this is the last method which will be runned
        public static void PrintOnTheConsole(Dictionary<string, int> scoreList, string name)
        {
            Console.WriteLine("                            BEST SCORES: ");
            Console.WriteLine();
            int counter = 1;
            foreach (var pair in scoreList)
            {
                //current score is colored in red.
                if (pair.Key == name)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("                    {0}. {1} - {2} POINTS!", counter, pair.Key, pair.Value);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("                    {0}. {1} - {2} POINTS!", counter, pair.Key, pair.Value);
                }
                Console.ForegroundColor = ConsoleColor.White;
                counter++;
            }
            Console.WriteLine();
            Console.WriteLine(@"                           Play again? Y\N");
            //add restart mode.

            
            //"Press any key to continue goes black"
            Console.ForegroundColor = ConsoleColor.Black;
        }   
    }
}

