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
        }

        static void CreateFile(string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine("This is my first Line");
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
        static void AppendToFile(string path)
        {

        }
        static void DeleteFile(string path)
        {

        }
    }
}
