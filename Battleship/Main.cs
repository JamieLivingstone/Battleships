using System;
using System.Collections.Generic;

namespace Battleship
{
    internal class Battleships
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Welcome to battleships!");
            Console.WriteLine();

            // Change console title
            Console.Title = "Battleships";

            // Start the game
            new Game().Start();
        }
    }
}