using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_Connect4
{
    internal class Program
    {
        class Player
        {
            public string Name { get; set; } //Current player name
            public char Tile { get; set; } //The current tile of the player
            public int Score { get; set; } //How many wins the player has obtained in the session
            public bool Win { get; set; } //If the player has won

            public virtual int chooseColumn() //The player enters a value of a column to place their tile and that value gets returned
            {
                Console.Write($"\n{Name}, where would you like to place your tile? ");
                int value = int.Parse(Console.ReadLine());
                return value;
            }
        }
        class AI : Player
        {
            public override int chooseColumn() //AI will pick a value at random between 1-7 to place their tile
            {
                Random number = new Random();
                int tileAI = number.Next() % 7 + 1;
                return tileAI;
            }
        }
        static class Board
        {
            static public int turnCounter = 1;
            static public char[,] cleanBoard = { { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { ' ', '1', '2', '3', '4', '5', '6', '7', ' ' } };
            static public char[,] board;
            static public void NewBoard()
            {
                board = cleanBoard;    
            }
            static public void Display()
            {
                Console.WriteLine();
                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        Console.Write(board[i, j] + " ");
                    }
                    Console.WriteLine();
                }
            }
        }
        class Initializer
        {
            public int NumOfUsers { get; set; } //Current number of players
            public string Player1Name { get; set; } //Name of player 1
            public string Player2Name { get; set; } //Name of player 2, "AI" if only one player is chosen

            public void NewGame() //To be called in order to start a new game
            {
                Board.NewBoard();
                Console.WriteLine("Welcome user! How many people will be playing?");
                while (NumOfUsers != 1 && NumOfUsers != 2) //Wait for correct number of players
                {
                    string input = Console.ReadLine();
                    int value = 0;
                    if (int.TryParse(input, out value)) //Ensure text will not crash the program
                    {
                        if (value != 1 && value != 2)
                        {
                            Console.WriteLine("\nSorry! 1 or 2 players only!"); //Tell how many players are allowed
                        }
                    }
                    else Console.WriteLine("\nPlease enter a number!"); //Tell only numbers
                    NumOfUsers = value; //Assign value to number of users for the class
                }
                switch (NumOfUsers) //Based on number of users get names for either 1 or 2 players and set them accordingly
                {
                    case 1:
                        Console.WriteLine("\n1 player, great!");
                        Console.Write("Name: ");
                        Player1Name = Console.ReadLine();
                        Player2Name = "AI"; //With only 1 player, player 2 is AI
                        break;
                    default:
                        Console.WriteLine("\n2 players, good luck!");
                        Console.Write("Player 1 Name: ");
                        Player1Name = Console.ReadLine();
                        Console.Write("Player 2 Name: ");
                        Player2Name = Console.ReadLine();
                        break;
                }
            }
        }
        static void Main(string[] args)
        {
            Initializer game = new Initializer(); //Create instance of game
            game.NewGame(); //Create new game and get player and game information
            Player p1 = new Player { Name = game.Player1Name, Score = 0, Tile = 'x', Win = false }; //Add player information to player 1
            Player p2 = new Player { Name = game.Player2Name, Score = 0, Tile = 'o', Win = false }; //Add player information to player 2
            if (game.NumOfUsers == 1) //For if it is only 1 player, make an AI instance instead of player instance
            {
                p2 = new AI { Name = game.Player2Name, Score = 0, Tile = 'o', Win = false }; //Add AI information to player 2
            }
            Console.Clear();
            Board.Display();
            Console.Read();
        }
    }
}
