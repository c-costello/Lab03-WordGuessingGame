using System;
using System.IO;

namespace GuessingGameApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "../../../../../words.txt";

            CreateFile(path);
            Menu(path);

            //AppendToFile(path, "CAT");
            //ReadFile(path);
            //DeleteFile(path);
        }

        static void Menu(string path)
        {
            Console.WriteLine("Play HangMan?");
            Console.WriteLine("1. Begin Game");
            Console.WriteLine("2. Admin");
            Console.WriteLine("3. Exit");
            string pickStr = Console.ReadLine();

            int pick = pickHandler(pickStr);

            switch (pick)
            {
                case 1:
                    StartGame(path);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                default:
                    break;
            }

        }

        static int pickHandler(string pickStr)
        {
            int pick = Convert.ToInt32(pickStr);
            return pick;
        }


        //game functions
        static void StartGame(string path)
        {
            Console.WriteLine(GetRandomWord(path));
        }

        static string GetRandomWord(string path)
        {
            string[] words = File.ReadAllLines(path);
            int wordsLength = words.Length;
            Random r = new Random();
            int whichWord = r.Next(wordsLength);
            string word = words[whichWord];
            return word;

        } 
        //System.IO functions
        static void CreateFile(string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine("DOG");
                streamWriter.WriteLine("Cat");
                streamWriter.WriteLine("Silly");
                streamWriter.WriteLine("Funny");
            }

        }
        static void ReadFile(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string[] words = File.ReadAllLines(path);
                foreach (string word in words)
                {
                    Console.WriteLine(word);
                }

            }

        }
        static void AppendToFile(string path, string word)
        {
            using (StreamWriter streamWriter = File.AppendText(path))
            {
                streamWriter.WriteLine(word);
            }

        }
        static void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
