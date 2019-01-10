using System;
using System.IO;

namespace GuessingGameApp
{
    class Program
    {
        public static string path = "../../../../../words.txt";
        public static string guessPath = "../../../../../guesses.txt";
        public static bool isCorrect = false;
        static void Main(string[] args)
        {
            CreateFile(path);
            CreateFileGuesses(guessPath);
            Menu();
        }

        static void Menu()
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
            string guessThisWord = GetRandomWord(path);
            while (isCorrect == false)
            {
                Console.WriteLine("Your Word: " + guessThisWord);
                GuessChecker(LetterPrompt(), guessThisWord);
                Console.WriteLine();
            }
            Console.WriteLine("You Won!");
            Menu();

            
        }

        static void GuessChecker(string letterInput, string word)
        {
            char letter = Convert.ToChar(letterInput);
            AppendToFile(guessPath, letterInput);
            char[] checkedWord = WordChecker(word);
            string checkedWordStr = new string(checkedWord);
            Console.WriteLine("This is my word: " + checkedWordStr);
            ReadFile(guessPath);
            if (checkedWordStr == word)
            {
                isCorrect = true;
            }
        }

        static string LetterPrompt()
        {
            Console.WriteLine("Guess a Letter: ");
            string choice = Console.ReadLine();
            return choice;
        }

        static char[] WordChecker(string word)
        {
            int wordLength = word.Length;
            char[] wordForm = new char[wordLength];
            for (int i = 0; i < wordLength; i++)
            {
                wordForm[i] = '_';
            }
            
            string[] guessedLetters = File.ReadAllLines(guessPath);
            char[] guessedLettersChar = new char[guessedLetters.Length];
            for (int i = 1; i < guessedLetters.Length; i++)
            {
                guessedLettersChar[i-1] = Convert.ToChar(guessedLetters[i]);
            };

            for (int i = 0; i < guessedLettersChar.Length; i++)
            {
                for (int j = 0; j < word.Length; j++)                  
                {
                    if( word[j] == guessedLettersChar[i])
                    {
                        wordForm[j] = word[j];
                    }
                }

            }
            return wordForm;
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
        static void CreateFileGuesses(string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                streamWriter.WriteLine("GUESSES");
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
        static void AppendToFile(string path, string input)
        {
            using (StreamWriter streamWriter = File.AppendText(path))
            {
                streamWriter.WriteLine(input);
            }

        }
        static void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
