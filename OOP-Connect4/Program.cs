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
        static void Main(string[] args)
        {
            Board.Display();
            Console.Read();
        }
    }
}
