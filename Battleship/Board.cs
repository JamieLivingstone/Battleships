using System;
using System.Collections.Generic;
using static System.String;

namespace Battleship
{
    internal class Board
    {
        private readonly string[,] _board = new string[10, 10];
        public int TotalPlots { get; set; }

        // Plot a coordinate on the board
        public void Plot(int x, int y)
        {
            _board[x, y] = "S";
            TotalPlots++;
        }

        // Validate coordinate
        public bool IsValidCoordinate(int x, int y)
        {
            return x <= 9 && x >= 0 && y >= 0 && y <= 9;
        }

        // Return if plot is free or taken
        public bool IsPlotAvailable(int x, int y)
        {
            return IsValidCoordinate(x, y) && IsNullOrEmpty(_board[x, y]);
        }

        // Return if plot has already been hit
        public bool IsPlotHit(int x, int y)
        {
            string plot = _board[x, y];
            return plot == "H" || plot == "M";
        }

        // Attack the board
        public bool Fire(int x, int y)
        {
            string plot = _board[x, y];

            if (plot == "S")
            {
                _board[x, y] = "H";
                return true;
            }
            else
            {
                _board[x, y] = "M";
                return false;
            }
        }

        // Draw a visual representation of the board to the console
        public void DrawBoard(bool onlyShowHits = false)
        {
            Console.WriteLine();
            Console.WriteLine("  | 0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("--|--------------------");

            for (var y = 0; y <= 9; y++)
            {
                Console.Write($"{y} |");

                for (var x = 0; x <= 9; x++)
                {
                    var plot = _board[x, y];

                    if (IsNullOrEmpty(plot))
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        if (onlyShowHits && plot == "H" || plot == "M")
                            Console.Write(" " + plot);

                        else if(onlyShowHits == false)
                            Console.Write(" " + plot);

                        else
                            Console.Write("  ");
                    }
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        // Draw a visual representation of the hist on the board to the console
        public void DrawHits()
        {
            DrawBoard(true);
        }

        // Allow user to hand plot ships
        public void ManuallyPlotShips(List<Ship> ships)
        {
            // Display empty board
            DrawBoard();

            // Iterate over ships and request placement
            foreach (var ship in ships)
                while (true)
                {
                    Console.WriteLine($"Plot your {ship.Name}");

                    // Request inputs
                    Console.Write("Enter your coordinate X: ");
                    var xInput = Console.ReadLine();

                    Console.Write("Enter your coordinate Y: ");
                    var yInput = Console.ReadLine();

                    Console.Write("Enter direction h / v (Horizonal / Vertical): ");
                    var direction = Console.ReadLine().ToLower();

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

                    // Check direction input is valid
                    if (direction != "v" && direction != "h")
                    {
                        Console.WriteLine(
                            "Direction not valid please try again! Enter \"v\" for vertical or \"h\" for horizontal.");
                        Console.WriteLine();
                        continue;
                    }

                    // Validate coordinate is in range and the plot is available
                    var haShipPlot = PlotShip(x, y, direction, ship.Size);

                    if (haShipPlot)
                    {
                        Console.WriteLine($"Plotted your {ship.Name}");
                        break;
                    }

                    Console.Clear();
                    Console.WriteLine($"The {ship.Name} could not be placed here try again!");
                }
        }

        // Let the computer plot the ships automatically
        public void AutoPlotShips(List<Ship> ships)
        {
            var random = new Random((int) DateTime.Now.Ticks);

            foreach (var ship in ships)
                while (true)
                {
                    var x = random.Next(0, 10);
                    var y = random.Next(0, 10);
                    var direction = random.Next(0, 10) > 4 ? "h" : "v";

                    var haShipPlot = PlotShip(x, y, direction, ship.Size);

                    if (haShipPlot)
                        break;
                }
        }

        // Validate ship can be placed and return coordinates if it can or an empty list if not
        public bool PlotShip(int x, int y, string direction, int shipSize)
        {
            var plotsList = new List<List<int>>();

            for (var size = 0; size < shipSize; size++)
            {
                var shipX = x;
                var shipY = y;

                // Increment ship coordinate based on direction
                if (direction == "h")
                    shipX += size;
                else
                    shipY += size;

                // Check if plot is in bounds and free
                if (!IsPlotAvailable(shipX, shipY))
                    break;

                plotsList.Add(new List<int> {shipX, shipY});
            }

            if (plotsList.Count != shipSize)
                return false;

            foreach (var plot in plotsList)
                Plot(plot[0], plot[1]);

            return true;
        }
    }
}