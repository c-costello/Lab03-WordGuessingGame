using System;
using System.IO;

namespace GuessingGameApp
{
    public class Program
    {
        public static string wordPath = "../../../../../words.txt";
        public static string guessPath = "../../../../../guesses.txt";
        public static bool isCorrect = false;
        static void Main(string[] args)
        {
            CreateFile();
            Menu();
        }

        public static void Menu()
        {
            Console.WriteLine("Play HangMan?");
            Console.WriteLine("1. Begin Game");
            Console.WriteLine("2. Admin");
            Console.WriteLine("3. Exit");
            string pickStr = Console.ReadLine();

            int pick = PickHandler(pickStr);

            switch (pick)
            {
                case 1:
                    
                    StartGame();
                    break;
                case 2:
                    AdminMenu();
                    break;
                case 3:
                    break;
                case 0:
                    Console.WriteLine("Please enter a valid number");
                    Menu();
                    break;
                default:
                    break;
            }

        }

        public static int PickHandler(string pickStr)
        {
            int pick;
            try
            {
                int check = Convert.ToInt32(pickStr);
            }
            catch (FormatException)
            {
                pick = 0;
                return pick;
                throw;
            }
            pick = Convert.ToInt32(pickStr);
            return pick;
        }

        //admin functions
        public static void AdminMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. View Words");
            Console.WriteLine("2. Edit Words");
            Console.WriteLine("3. Return to menu");
            string pickStr = Console.ReadLine();
            int pick = PickHandler(pickStr);
            switch (pick)
            {
                case 1:
                    ViewWords();
                    break;
                case 2:
                    AddAWord();                    
                    break;
                case 3:
                    Menu();
                    break;
                default:
                    break;
            }
        }
        public static void AddAWord()
        {
            ReadFile(wordPath);
            Console.WriteLine("What word would you like to add?");
            string addedWord = Console.ReadLine();
            AppendToFile(wordPath, addedWord);
            Console.WriteLine();
            AdminMenu();
        }
        public static void ViewWords()
        {
            ReadFile(wordPath);
            Console.WriteLine();
            AdminMenu();
        }


        //game functions
        public static void StartGame()
        {
            CreateFile();
            CreateFileGuesses();
            string guessThisWord = GetRandomWord(wordPath);
            while (isCorrect == false)
            {
                //Console.WriteLine("Your Word: " + guessThisWord);
                Console.WriteLine(String.Join(' ', WordChecker(guessThisWord)));
                ReadFile(guessPath);
                GuessChecker(LetterPrompt(), guessThisWord);
                Console.WriteLine();
            }
            Console.WriteLine(String.Join(' ', WordChecker(guessThisWord)));
            Console.WriteLine("You Won!");
            Console.Clear();
            isCorrect = false;
            DeleteFile(guessPath);
            DeleteFile(wordPath);
            Menu();

            
        }
        public static void GuessChecker(string letterInput, string word)
        {
            char letter = Convert.ToChar(letterInput);
            AppendToFile(guessPath, letterInput);
            char[] checkedWord = WordChecker(word);
            string checkedWordStr = new string(checkedWord);
            if (checkedWordStr == word)
            {
                isCorrect = true;
            }
        }
        public static string LetterPrompt()
        {
            Console.Write("Guess a Letter: ");
            string choice = Console.ReadLine();
            string choiceStr = choice.ToUpper();
            return choiceStr;
        }
        public static char[] WordChecker(string word)
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
        public static string GetRandomWord(string path)
        {
            string[] words = File.ReadAllLines(path);
            int wordsLength = words.Length;
            Random r = new Random();
            int whichWord = r.Next(wordsLength);
            string word = words[whichWord];
            return word;

        } 
        


        //System.IO functions
        public static void CreateFile()
        {
            using (StreamWriter streamWriter = new StreamWriter(wordPath))
            {
                streamWriter.WriteLine("DOG");
                streamWriter.WriteLine("CAT");
                streamWriter.WriteLine("SILLY");
                streamWriter.WriteLine("FUNNY");
            }

        }
        public static void CreateFileGuesses()
        {
            using (StreamWriter streamWriter = new StreamWriter(guessPath))
            {
                streamWriter.WriteLine("GUESSES");
            }
        }
        public static string[] ReadFile(string path)
        {
            using (StreamReader streamReader = new StreamReader(path))
            {
                string[] words = File.ReadAllLines(path);
                foreach (string word in words)
                {
                    Console.WriteLine(word);
                }

                return words;
            }

        }
        public static string[] AppendToFile(string path, string input)
        {
            string inputUpper = input.ToUpper();
            using (StreamWriter streamWriter = File.AppendText(path))
            {
                streamWriter.WriteLine(inputUpper);
            }
            return ReadFile(path);

            


        }
        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}
