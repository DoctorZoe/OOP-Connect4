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
        static void Main(string[] args)
        {
        }
    }
}
