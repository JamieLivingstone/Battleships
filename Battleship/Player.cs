using System;

namespace Battleship
{
    internal class Player
    {
        private readonly Board _board;
        private readonly Random _random = new Random();

        public Player(Board board)
        {
            _board = board;
        }

        public string Name { get; set; }
        public int TotalHits { get; set; }

        public string SetName()
        {
            while (true)
            {
                Console.Write("Please enter your name: ");
                Name = Console.ReadLine();

                if (Name.Length == 0)
                {
                    Console.WriteLine("Invalid name! Try again");
                    continue;
                }

                break;
            }
            
            return Name;
        }

        // Attack ship (Return true if coordinate is valid (does not matter if it is a hit or not))!
        public bool Attack(int x, int y, Board board)
        {
            if (!board.IsValidCoordinate(x, y) || board.IsPlotHit(x, y))
                return false;

            var fire = board.Fire(x, y);

            if (fire)
            {
                Console.WriteLine($"{Name} has hit this ship with the coordinates - X: {x}, Y: {y}");
                TotalHits++;
            }
            else
            {
                Console.WriteLine($"{Name} missed!");
            }

            Console.WriteLine();
            board.DrawHits();
            Console.ReadLine();
            Console.Clear();

            return true;
        }

        public void AutoAttack(Board board)
        {
            while (true)
            {
                var x = _random.Next(0, 9);
                var y = _random.Next(0, 9);

                var valid = Attack(x, y, board);

                if (valid)
                    break;
            }
        }
    }
}