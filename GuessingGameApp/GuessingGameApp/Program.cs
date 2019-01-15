using System;
using System.IO;

namespace GuessingGameApp
{
    public class Program
    {
        public static string wordPath = "../../../../../words.txt";
        public static string guessPath = "../../../../../guesses.txt";
        public static bool isCorrect = false;
        public static string guessThisWord;
        static void Main(string[] args)
        {
            CreateFile();
            Menu();
        }

        /// <summary>
        /// Prompts user to select option from menu, handle response, and then call the appropriate function via switch statement
        /// </summary>
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
                    CreateFileGuesses();
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

        /// <summary>
        /// Handler that takes in the user input and converts input to integer
        /// has the try catch block for the menus
        /// 
        /// </summary>
        /// <param name="pickStr">pickStr, the user response from the menu prompt</param>
        /// <returns>the user input as an int</returns>
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
        /// <summary>
        /// Prompts user to select option from admin menu
        /// calls PickHandler to convert user input from string to integer
        /// uses a switch statement to call appropriate method
        /// </summary>
        public static void AdminMenu()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. View Words");
            Console.WriteLine("2. Edit Words");
            Console.WriteLine("3. Remove One Word");
            Console.WriteLine("4. Return to menu");
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
                    RemoveOneWord();
                    break;
                case 4:
                    Menu();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// prompts user to give input on what word they want to add
        /// converts the user input to uppercase
        /// and adds the user input to the words file
        /// </summary>
        public static void AddAWord()
        {
            ReadFile(wordPath);
            Console.WriteLine("What word would you like to add?");
            string addedWordInput = Console.ReadLine();
            string addedWord = addedWordInput.ToUpper();
            AppendToFile(wordPath, addedWord);
            Console.WriteLine();
            AdminMenu();
        }
        /// <summary>
        /// Prints out a list of all the words in the words file
        /// </summary>
        public static void ViewWords()
        {
            ReadFile(wordPath);
            Console.WriteLine();
            AdminMenu();
        }

        /// <summary>
        /// prompts user to enter a word they would like to delete
        /// checks word against words in file
        /// deletes file and re-writes file without the selected word
        /// </summary>
        public static void RemoveOneWord()
        {
            string[] words = File.ReadAllLines(wordPath);
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine("What word would you like to remove? Please enter exact word");
            string deletedWordInput = Console.ReadLine();
            string deletedWord = deletedWordInput.ToUpper();
            string[] newWords = new string[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                if (deletedWord != words[i])
                {
                    newWords[i] = words[i];
                }
                if (newWords == words)
                {
                    Console.WriteLine("That's not one of the words!");
                    AdminMenu();
                }
            }
            DeleteFile(wordPath);
            using (StreamWriter streamWriter = new StreamWriter(wordPath))
            {
                foreach (string word in newWords)
                {
                    streamWriter.WriteLine(word);
                }
            }
            Console.WriteLine("Your words now are: ");
            ViewWords();
            AdminMenu();
            

            
        }

        //game functions
        /// <summary>
        /// generates a random word and triggers the start of the game
        /// </summary>
        public static void StartGame()
        {

            guessThisWord = GetRandomWord(wordPath);
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
            isCorrect = false;
            DeleteFile(guessPath);
            Menu();

            
        }

        /// <summary>
        /// takes user's input guess and converts it from string to char
        /// handles any execptions caused by conversion
        /// appends user guess to the guesses file
        /// calls wordChecker
        /// </summary>
        /// <param name="letterInput"></param>
        /// <param name="word"></param>
        public static void GuessChecker(string letterInput, string word)
        {
            char letter;
            bool canConvert = false;
            while (canConvert == false)
            {
                try
                {
                    letter = Convert.ToChar(letterInput);

                }
                catch (Exception)
                {
                    Console.WriteLine("Only Enter One Letter!");
                    letterInput = LetterPrompt();

                }
                canConvert = true;
            }
            letter = Convert.ToChar(letterInput);
            AppendToFile(guessPath, letterInput);
            char[] checkedWord = WordChecker(word);
            string checkedWordStr = new string(checkedWord);
            if (checkedWordStr == word)
            {
                isCorrect = true;
            }
        }

        /// <summary>
        /// Prompts user to submit a guess
        /// </summary>
        /// <returns>Returns user input as a string</returns>
        public static string LetterPrompt()
        {
            Console.Write("Guess a Letter: ");
            string choice = Console.ReadLine();
            string choiceStr = choice.ToUpper();
            return choiceStr;
        }

        /// <summary>
        /// Creates word form (ex _ _ _)
        /// checks guesses against random word that is being guessed
        /// for every letter that has been guessed correctly, fill in the appropriate blank
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
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

        /// <summary>
        /// generate a random word from the words file
        /// </summary>
        /// <param name="path">file path wordPath</param>
        /// <returns>the targed guess word</returns>
        public static string GetRandomWord(string path)
        {
            string[] words = File.ReadAllLines(path);
            int wordsLength = words.Length;
            Random r = new Random();
            string word = "";
            while (word == "")
            {
                int whichWord = r.Next(wordsLength);
                word = words[whichWord];
            }
            return word;
        } 
        


        //System.IO functions
        /// <summary>
        /// creates a file
        /// </summary>
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

        /// <summary>
        /// creates the guesses file
        /// </summary>
        public static void CreateFileGuesses()
        {
            using (StreamWriter streamWriter = new StreamWriter(guessPath))
            {
                streamWriter.WriteLine("GUESSES");
            }
        }

        /// <summary>
        /// reads the file and prints the list of words
        /// </summary>
        /// <param name="path">wordPath</param>
        /// <returns> returns an array of all the words in the file</returns>
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

        /// <summary>
        /// adds a word to the file
        /// </summary>
        /// <param name="path">wordPath</param>
        /// <param name="input"> word to be added </param>
        /// <returns> a new array made up of the new words file</returns>
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
