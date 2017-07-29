using System;
using System.Collections.Generic;

namespace Battleship
{
    class Game
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Battleships!");

            // Games ships
            List<Ship> Ships = new List<Ship>()
            {
                new Ship("Aircraft Carrier", 5),
                new Ship("Battleship", 4),
                new Ship("Cruiser", 3),
                new Ship("Destroyer", 2),
                new Ship("Destroyer", 2)
            };

            // Create a game board of an equal width and height
            const int boardSize = 10;
            Board defensiveBoard = new Board(boardSize, Ships);
            Board offensiveBoard = new Board(boardSize, Ships);

            // Place ships on board
            defensiveBoard.PlaceShips();
            defensiveBoard.DisplayBoard();
        }
    }
}