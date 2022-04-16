using System;

namespace OOP_Connect4 //Separated file since this error class was added as a late addition.
{
    public class Error : Exception //Error class created handling input exceptions.
    {
        public Error()
        {
            Console.Write("\nError Found: Invalid Input."); //Custom error message.
        }
    }
}
