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
            public string Name { get; set; }
            public char Tile { get; set; }
            public int Score { get; set; }
            public bool Win { get; set; }

            public virtual int chooseColumn()
            {
                Console.Write($"\n{Name}, where would you like to place your tile? ");
                int value = int.Parse(Console.ReadLine());
                return value;
            }
        }
        class AI : Player
        {
            public override int chooseColumn()
            {
                Random number = new Random();
                int tileAI = number.Next() % 7 + 1;
                return tileAI;
            }
        }
        class Board
        {
            static public int turnCounter = 1;
            static public char[,] board = { { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                            { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                            { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                            { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                            { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                            { '|', '#', '#', '#', '#', '#', '#', '#', '|' },
                                            { ' ', '1', '2', '3', '4', '5', '6', '7', ' ' } };

            public void Display()
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
            Board board = new Board();
            board.Display();

            Console.Read();
        }
    }
}
