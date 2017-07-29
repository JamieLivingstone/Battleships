using System;
using System.Collections.Generic;
using System.Text;

namespace Battleship
{
    class Ship
    {
        private readonly string _name;
        private readonly int _size;

        public Ship(string name, int size)
        {
            _name = name;
            _size = size;
        }
    }
}
