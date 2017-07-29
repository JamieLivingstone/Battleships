using System;
using System.Collections.Generic;

namespace Battleship
{
    internal class Game
    {
        private readonly Player _computer;
        private readonly Board _computerBoard;
        private readonly Player _player;
        private readonly Board _playerBoard;

        // Games ships
        private readonly List<Ship> _ships = new List<Ship>
        {
            new Ship("Aircraft Carrier", 5),
            new Ship("Battleship", 4),
            new Ship("Cruiser", 3),
            new Ship("Destroyer", 2),
            new Ship("Destroyer", 2)
        };

        public Game()
        {
            _playerBoard = new Board();
            _computerBoard = new Board();

            _player = new Player(_playerBoard);
            _computer = new Player(_computerBoard) {Name = "Computer"};
        }

        public void Start()
        {
            // Set players name
            _player.SetName();

            // Show ship setup
            InitializeShips();

            // Start the game
            Console.WriteLine("Press enter to start the game");
            Console.ReadLine();
            Console.Clear();

            while (_player.TotalHits < _computerBoard.TotalPlots && _computer.TotalHits < _playerBoard.TotalPlots)
            {
                _computerBoard.DrawHits();

                Console.Write("Enter enemy coordinate X: ");
                var xInput = Console.ReadLine();

                Console.Write("Enter enemy coordinate Y: ");
                var yInput = Console.ReadLine();

                // Validate inputs
                int x, y;

                // Validate x, y coordinates are valid integers
                try
                {
                    x = int.Parse(xInput);
                    y = int.Parse(yInput);
                }
                catch (Exception)
                {
                    Console.WriteLine("X and Y coordinates are not valid numbers! Try again.");
                    Console.WriteLine();
                    continue;
                }

                // Verify plot is valid (Not already hit / off the board)
                Console.Clear();
                var validAttack = _player.Attack(x, y, _computerBoard);

                if (!validAttack)
                {
                    Console.WriteLine("Already hit or invalid coordinate! Please try again.");
                    continue;
                }

                // Computer attack
                _computer.AutoAttack(_playerBoard);
            }

            // Display who won the game
            PrintWinner();
        }

        public void InitializeShips()
        {
            Console.WriteLine($"Hi there {_player.Name}, firstly you need to plot your ships!");
            Console.WriteLine(
                "Press ENTER to manually enter ships or type \"A\" and press enter to automatically plot your ships.");
            var input = Console.ReadLine();

            if (input.ToLower() == "a")
            {
                Console.WriteLine("Automatically plotting your ships...");
                _playerBoard.AutoPlotShips(_ships);
                _playerBoard.DrawBoard();
                Console.WriteLine();
            }
            else
            {
                _playerBoard.ManuallyPlotShips(_ships);
                Console.WriteLine();
                _playerBoard.DrawBoard();
                Console.WriteLine();
            }

            _computerBoard.AutoPlotShips(_ships);
        }

        public void PrintWinner()
        {
            Console.Clear();
            var winnerName = _player.TotalHits > _computer.TotalHits ? _player.Name : _computer.Name;
            Console.WriteLine($"The winner is {winnerName}!");
        }
    }
}