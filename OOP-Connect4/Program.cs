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
            public bool Win { get; set; } //If the player has won
            public int Score { get; set; } //Current score for the player

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
                board = cleanBoard.Clone() as char[,];
                turnCounter = 1;
                Controller.game = true;
            }
            static public void Display() //function for displaying the board.
            {
                Console.WriteLine("Turns: " + turnCounter); //debug line
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

            bool shouldRun = true; //variable to check if game is running.

            public bool WillRun()
            {
                return shouldRun;
            }

            public void StopRun()
            {
                shouldRun = false;
            }
            public void NewGame() //To be called in order to start a new game
            {
                NumOfUsers = 0; // makes sure there are no user when a game starts.
                Board.NewBoard();
                Console.Write("Welcome user! How many people will be playing?\nPlayers [1/2]: ");
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
                        Console.Write("\nName: ");
                        Player1Name = Console.ReadLine();
                        Player2Name = "AI"; //With only 1 player, player 2 is AI
                        break;
                    default:
                        Console.WriteLine("\n2 players, good luck!");
                        Console.Write("\nPlayer 1 Name: ");
                        Player1Name = Console.ReadLine();
                        Console.Write("Player 2 Name: ");
                        Player2Name = Console.ReadLine();
                        break;
                }
            }
        }
        static class Controller
        {
            public static bool game = true;
            static public bool PlaceTile(int column, Player player) //Places the tile in the first available slot based on players choice, if available
            {                                                       //If not available will return false so player knows to pick a different column
                for (int i = 5; i >= 0; i--)//Starts at the bottom of the board and goes to the top
                {
                    if (Board.board[i, column] == '#') //Checks if the current position is free
                    {
                        Board.board[i, column] = player.Tile; //Set current clear slot to current players tile
                        Board.turnCounter++; //Increase the turn counter so next player can go
                        Controller.CheckWinCondition(player); //checks winning conditions for player. //CHECK PLACETILE METHOD
                        return true; //Notify that a tile was placed
                    }
                }
                return false; //If a tile was unable to be placed return false showing that the column is currently full
            }
            static public void RandomizePlayers(ref Player p1, ref Player p2) //Passing by reference to ensure values change outside of the function
            {
                Random prandom = new Random();
                int goesFirst = prandom.Next() % 2 + 1;
                Player store = new Player();
                if (goesFirst == 1) //if result is 1, will change player order.
                {
                    store = p1;
                    p1 = p2;
                    p2 = store;
                }
                Console.WriteLine(p1.Name + " goes first."); //tells which player goes first.
            }

            static public void CheckWinCondition(Player p)
            {
                char s = p.Tile;
                //start of winning condition check:
                for (int i = 5; i >= 0; i--) //horizontal check.
                {
                    if (p.Win == true) break;
                    for (int j = 1; j <= 4; j++)
                    {
                        if (p.Win == true) break;
                        int count = 0;
                        if (Board.board[i, j] == s)
                        {
                            count++;
                            if (Board.board[i, j + 1] == s) count++;
                            if (Board.board[i, j + 2] == s) count++;
                            if (Board.board[i, j + 3] == s) count++;
                        }
                        if (count == 4)
                        {
                            p.Win = true; //declares the player a winner.
                        }
                    }
                }

                for (int i = 5; i >= 3; i--) //vertical check.
                {
                    if (p.Win == true) break;
                    for (int j = 1; j <= 7; j++)
                    {
                        if (p.Win == true) break;
                        int count = 0;
                        if (Board.board[i, j] == s)
                        {
                            count++;
                            if (Board.board[i - 1, j] == s) count++;
                            if (Board.board[i - 2, j] == s) count++;
                            if (Board.board[i - 3, j] == s) count++;
                        }
                        if (count == 4)
                        {
                            p.Win = true; //declares the player a winner.
                        }
                    }
                }

                for (int i = 5; i >= 3; i--) //ascending diagonal check.
                {
                    if (p.Win == true) break;
                    for (int j = 1; j <= 4; j++)
                    {
                        if (p.Win == true) break;
                        int count = 0;
                        if (Board.board[i, j] == s)
                        {
                            count++;
                            if (Board.board[i - 1, j + 1] == s) count++;
                            if (Board.board[i - 2, j + 2] == s) count++;
                            if (Board.board[i - 3, j + 3] == s) count++;
                        }
                        if (count == 4)
                        {
                            p.Win = true; //declares the player a winner.
                        }
                    }
                }

                for (int i = 0; i <= 2; i++) //descending diagonal check.
                {
                    if (p.Win == true) break;
                    for (int j = 1; j <= 4; j++)
                    {
                        if (p.Win == true) break;
                        int count = 0;
                        if (Board.board[i, j] == s)
                        {
                            count++;
                            if (Board.board[i + 1, j + 1] == s) count++;
                            if (Board.board[i + 2, j + 2] == s) count++;
                            if (Board.board[i + 3, j + 3] == s) count++;
                        }
                        if (count == 4)
                        {
                            p.Win = true; //declares the player a winner.
                        }
                    }
                }


            }
            static public bool EndGameCondition(Player p1, Player p2) //Checks for winning condition for a given player and add to their score
            {                                                         //Also returns true if won or end of game and false if game is still ongoing
                if (p1.Win && Controller.game == true)
                {
                    Console.WriteLine($"Game over, {p1.Name} wins.");
                    Controller.game = false;
                    p1.Score++; //Add to players score
                    return true;
                }

                if (p2.Win && Controller.game == true)
                {
                    Console.WriteLine($"Game over, {p2.Name} wins.");
                    Controller.game = false;
                    p2.Score++; //Add to players score
                    return true;
                }

                if (Board.turnCounter == 43 && Controller.game == true)
                {
                    Console.WriteLine("Game over, board is full. Nobody wins.");
                    Controller.game = false;
                    return true;
                }
                return false;
            }
        }
        static void Main(string[] args)
        {
            Initializer game = new Initializer(); //Create instance of game
            while (game.WillRun())
            {
                Console.Clear();
                game.NewGame(); //Create new game and get player and game information
                Player p1 = new Player { Name = game.Player1Name, Tile = 'x', Win = false }; //Add player information to player 1
                Player p2 = new Player { Name = game.Player2Name, Tile = 'o', Win = false }; //Add player information to player 2
                if (game.NumOfUsers == 1) //For if it is only 1 player, make an AI instance instead of player instance
                {
                    p2 = new AI { Name = "AI", Tile = 'o', Win = false }; //Add AI information to player 2
                }
                while (Controller.game) //Check if a player has won, if not continue the game (also checks if the game is running)
                {
                    Console.Clear(); //Clear the console to keep it clean and crisp and not full of information and needing to scroll
                    Board.Display(); //Display the board in its current state
                    if (Controller.EndGameCondition(p1, p2))
                    {
                        Console.WriteLine($"\nCurrent Score:\n{p1.Name}: {p1.Score} and {p2.Name}: {p2.Score}"); //Display current number of wins
                        Console.Write("\nDo you want to run another game? [Y/N] ");
                        while (game.WillRun())
                        {
                            string input = Console.ReadLine().ToLower(); //Using ToLower to ensure no matter how they input yes/no/y/n it will work
                            if (input == "yes" || input == "y")
                            {
                                p1.Win = false;
                                p2.Win = false;
                                Board.NewBoard(); //breaks while doing nothing, thus restarts the game.
                                Console.Clear(); //Clears for the next game
                                Board.Display(); //Displays new board for next game
                                break;
                            }
                            if (input == "no" || input == "n") game.StopRun(); //method to stop execution of the game
                            else
                            {
                                Console.Write("Invalid input, please try again. Another game? (Y/N) ");
                            }
                        }
                    }
                    if (Board.turnCounter == 1) //This is to make sure that randomizing only happens on turn 1
                    {
                        Controller.RandomizePlayers(ref p1, ref p2); //Calls randomizer and passes values by reference to switch player order
                    }

                    if (Board.turnCounter % 2 == 1 && Controller.game == true) //Allow player 1 to go (if game is running)
                    {
                        int placement = p1.chooseColumn(); //Ask for the column they would like to place
                        if (!Controller.PlaceTile(placement, p1)) //If it cannot be placed notify the user, then refresh
                        {
                            if (p1.Name != "AI")//added this if to make sure dumb AI won't flood the chat with message below.
                            {
                                Console.WriteLine("Sorry this column is full! Try again!");
                                Thread.Sleep(400);
                            }
                        }
                    }
                    else if (Board.turnCounter % 2 == 0 && Controller.game == true) //Allow player 2 to go (if game is running)
                    {
                        int placement = p2.chooseColumn(); //Ask for the column they would like to place
                        if (!Controller.PlaceTile(placement, p2)) //If it cannot be placed notify the user, then refresh
                        {
                            {
                                if (p2.Name != "AI")//added this if to make sure dumb AI won't flood the chat with message below.
                                {
                                    Console.WriteLine("Sorry this column is full! Try again!");
                                    Thread.Sleep(400);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
