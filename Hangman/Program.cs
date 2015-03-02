using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hangman
{
    class Program
    {

        static string input = string.Empty;
        static List<string> lettersGuessed = new List<string>() { };
        static int numberOfLives = 8;
        static bool correctGuess = false;
        static bool wordGuessed = false;
        static int numberOfCorrectGuesses = 0;
        static List<string> wordBank = new List<string> { "watermelon", "papaya", "nectarine", "pomegranate", "kumquat" };
        static Random randomNumber = new Random();
        static int random = randomNumber.Next(0, wordBank.Count);
        static string magicWord = string.Empty;
        static string returnString = string.Empty;


        static void Main(string[] args)
        {
            Console.Clear();
            WelcomeScreen();
            userInfo();
        }
        /// <summary>
        /// Welcomes the user and explains how to play the game.
        /// </summary>
        static void WelcomeScreen()
        {
            Console.WriteLine(@"    
                                                                               
    _/                                                                         
   _/_/_/      _/_/_/  _/_/_/      _/_/_/  _/_/_/  _/_/      _/_/_/  _/_/_/    
  _/    _/  _/    _/  _/    _/  _/    _/  _/    _/    _/  _/    _/  _/    _/   
 _/    _/  _/    _/  _/    _/  _/    _/  _/    _/    _/  _/    _/  _/    _/    
_/    _/    _/_/_/  _/    _/    _/_/_/  _/    _/    _/    _/_/_/  _/    _/     
                                   _/                                          
                              _/_/            
               _____ 
               | |   
               | | O 
               |  /|\
               |  /\ 
               ------   
            ");
            TypeOut("Welcome grasshopper. What is your name?");
            string name = Console.ReadLine();
            Console.WriteLine("Thanks " + name + "! In order to win you must guess the correct word by entering a letter or word. If you do not guess the word in 8 turns the game is over Press any key to continue. ");
            numberOfLives = 8;
            Console.ReadKey();

        }

        /// <summary>
        /// Prints the word one by one
        /// </summary>
        /// <param name="input">welcome message and instructions</param>
        static void TypeOut(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                Console.Write(input[i]);
                System.Threading.Thread.Sleep(70);
            }
            Console.WriteLine();
        }

        static void userInfo()
        {
            Console.Clear();
            magicWord = wordBank[random];
            Console.Write(@"
                            LETTERS USED: ");
            foreach (var item in lettersGuessed)
            {
                Console.Write(item.ToUpper() + " ");
            }
            Console.WriteLine();
            numberOfCorrectGuesses = 0;
            Console.WriteLine("\n Lives left: " + numberOfLives);
            Console.WriteLine("\n Word: " + Mask(magicWord, lettersGuessed));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n \n Enter a letter or word to guess");
            Console.WriteLine();
            input = Console.ReadLine().ToString().ToLower();
            Add();
        }

        static void Add()
        {

            bool result = input.All(Char.IsLetter);
            if (lettersGuessed.Contains(input))
            {
                Console.WriteLine("\nYou already guessed that.");
                System.Threading.Thread.Sleep(1600);
                correctGuess = false;
                userInfo();
            }
            else if (result)
            {
                for (int i = 0; i < magicWord.Length; i++)
                {
                    if (input == magicWord[i].ToString())
                    {
                        correctGuess = true;
                    }
                    else if (input == magicWord)
                    {
                        wordGuessed = true;
                    }
                    else
                        wordGuessed = false;
                }
            }
            else 
            {
            Console.WriteLine("invalid input. try again");
                
            }


            if (correctGuess)
            {
                Console.WriteLine(@"

           /(|
          (  :
         __\  \  _____
       (____)  `|
      (____)|   |
       (____).__|
        (___)__.|_____

You guessed right! ");
                {
                    lettersGuessed.Add(input);
                    numberOfCorrectGuesses++;
                    if (wordGuessed || Mask(magicWord, lettersGuessed).Replace(" ", "").ToLower() == magicWord.ToLower())
                    {
                        Winner();
                    }
                    correctGuess = false;
                    System.Threading.Thread.Sleep(1600);
                    userInfo();
                }
            }
            else if (wordGuessed)
            {
                Winner();
            }
            else if (numberOfLives == 0)
            {
                GameOver();
            }
            else if (lettersGuessed.Contains(input))
            {
                Console.WriteLine("\nYou already guessed that.");
                System.Threading.Thread.Sleep(1600);
                correctGuess = false;
                userInfo();

            }
            else if (result)
            {
                Console.WriteLine(@"
  ______
 (( ____ \----
 (( _____
 ((_____ 
 ((____   ----
      /  /
     (_(( 

Womp! " + input + " is not in the word.");
                lettersGuessed.Add(input);
                numberOfLives--;
                System.Threading.Thread.Sleep(1600);
                userInfo();
            }
            else {
                correctGuess = false;
                System.Threading.Thread.Sleep(1600);
                userInfo();
            }

        

        }
    


        static string Mask(string word, List<string> guessedLetters)
        {
            string returnString = "";
            int i = 0;
            bool correctGuess = false;

            while (i < word.Length)
            {
                var letter = word[i].ToString();
                foreach (var item in guessedLetters)
                {
                    if (item == letter)
                    {
                        correctGuess = true;
                    }
                }
                if (returnString.Contains(magicWord))
                {
                    correctGuess = true;
                }
                if (correctGuess)
                {
                    returnString += letter.ToUpper() + " ";
                    correctGuess = false;
                }
                else
                {
                    returnString += "_ ";
                }
                i++;
            }
            return returnString;
        }


        //Winner Function
        static void Winner()
        {
            Console.Clear();
            Console.WriteLine(@"
                                                                        
    |  o                    |  o                    |  o               |
  __|      _  _    __,    __|      _  _    __,    __|      _  _    __, |
 /  |  |  / |/ |  /  |   /  |  |  / |/ |  /  |   /  |  |  / |/ |  /  | |
 \_/|_/|_/  |  |_/\_/|/  \_/|_/|_/  |  |_/\_/|/  \_/|_/|_/  |  |_/\_/|/o
                    /|                      /|                      /|  
                    \|                      \|                      \|  
Congratulations!!!!! You win!!!!");
            Console.WriteLine();
            System.Threading.Thread.Sleep(2600);
            Console.WriteLine("Word: " + magicWord.ToUpper());
            End();
        }

        //Loser Function
        static void GameOver()
        {
            Console.Clear();
            Console.WriteLine(@"
                                                                                        
                                                  
 .-.. .-.  .--.--.  .-.       .-. .    ._ .-. .--.
(   |(   ) |  |  | (.-'      (   ) \  /  (.-' |   
 `-`| `-'`-'  '  `- `--'      `-'   `'    `--''   
 ._.'                                             
                            
Womp womp! Sorry you did not win...");
            TypeOut("you did not win ");
            Console.WriteLine();
            TypeOut("Word: " + magicWord.ToUpper());
            End();
        }

        static void End()
        {
            TypeOut("Thanks for playing");
            System.Threading.Thread.Sleep(1900);

                System.Environment.Exit(-5);
            }
        
    }
}

    





