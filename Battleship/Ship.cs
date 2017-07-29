using System;

namespace Battleship
{
    internal class Ship
    {
        public readonly string Name;
        public readonly int Size;

        public Ship(string name, int size)
        {
            // Ships must be at least 1 cell in size
            if (size < 1)
                throw new ArgumentException("A ships size can not be less than one!");

            Name = name;
            Size = size;
        }
    }
}