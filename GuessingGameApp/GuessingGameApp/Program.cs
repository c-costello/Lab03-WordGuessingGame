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
            AppendToFile(path, "CAT");
            ReadFile(path);
        }

        static void CreateFile(string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine("DOG");
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
