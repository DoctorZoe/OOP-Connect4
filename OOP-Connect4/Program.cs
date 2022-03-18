using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

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
                int value = 0;
                while (value > 7 || value < 1) //Only allow values between 1 and 7
                {
                    string input = Console.ReadLine(); //Get column from user input
                    if (int.TryParse(input, out value)) //Ensure text will not crash the program, and we always get a integer value
                    {
                        if (value > 7 || value < 1) //Give error message if value outside of the appropriate range
                        {
                            Console.WriteLine("\nPlease enter a value from 1 to 7.");
                        }
                    }
                    else Console.WriteLine("\nPlease enter a number!"); //Give error message to give integer values
                }
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
            static public int turnCounter = 1; //counter for number of turns, starts at one for the first turn.
            static public char[,] cleanBoard = { { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                                 { ' ', '1', '2', '3', '4', '5', '6', '7', ' ' } }; //basic, clean board.
            static public char[,] board; //actual board being used.
            static public void NewBoard() //function for clearing the board.
            {
                board = cleanBoard;
            }
            static public void Display() //function for displaying the board.
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
        static class Controller
        {
            static public bool PlaceTile(int column, Player player) //Places the tile in the first available slot based on players choice, if available
            {                                                       //If not available will return false so player knows to pick a different column
                for (int i = 5; i >= 0; i--)//Starts at the bottom of the board and goes to the top
                {
                    if (Board.board[i, column] == '#') //Checks if the current position is free
                    {
                        Board.board[i, column] = player.Tile; //Set current clear slot to current players tile
                        Board.turnCounter++; //Increase the turn counter so next player can go
                        return true; //Notify that a tile was placed
                    }
                }
                return false; //If a tile was unable to be placed return false showing that the column is currently full
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
            while (!p1.Win && !p2.Win) //Check if a player has won, if not continue the game
            {
                Console.Clear(); //Clear the console to keep it clean and crisp and not full of information and needing to scroll
                Board.Display(); //Display the board in its current state
                if (Board.turnCounter % 2 == 1) //Allow player 1 to go
                {
                    int placement = p1.chooseColumn(); //Ask for the column they would like to place
                    if (!Controller.PlaceTile(placement, p1)) //If it cannot be placed notify the user, then refresh
                    {
                        Console.WriteLine("Sorry this column is full! Try again!");
                        Thread.Sleep(1500);
                    }
                }
                else if (Board.turnCounter % 2 == 0) //Allow player 2 to go
                {
                    int placement = p2.chooseColumn(); //Ask for the column they would like to place
                    if (!Controller.PlaceTile(placement, p2)) //If it cannot be placed notify the user, then refresh
                    {
                        {
                            Console.WriteLine("Sorry this column is full! Try again!");
                            Thread.Sleep(1500);
                        }
                    }
                }
            }
        }
    }
}
