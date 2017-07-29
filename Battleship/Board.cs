using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Board
    {
        private readonly int _boardSize;
        private List<Ship> _ships;
        private List<Coordinate> _coordinates;

        public Board(int boardSize, List<Ship> ships)
        {
            _boardSize = boardSize;
            _ships = ships;
        }

        // Dispaly a visual representation of the board with ships
        public void DisplayBoard()
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string outputBoard = "[  ]";

            // Append letter headers to board
            for (int i = 0; i < _boardSize; i++)
            {
                outputBoard += $"[{letters[i]}]";
            }

            outputBoard += "\n";

            // Append numbered rows to board
            for (int i = 1; i <= _boardSize; i++)
            {
                // Current row number
                outputBoard += $"[{i:00}]";

                // Boards plots with corresponding shape
                for (int cell = 0; cell <_boardSize; cell++)
                {
                    outputBoard += $"[ ]";
                }

                outputBoard += "\n";
            }

            // Draw board
            Console.WriteLine(outputBoard);
        }

        // Allow user to manually place ships
        public void PlaceShips()
        {
            Console.WriteLine("It's time to place your ships! Let's begin.");

            foreach (Ship ship in _ships)
            {
                
            }
        }
    }
}
