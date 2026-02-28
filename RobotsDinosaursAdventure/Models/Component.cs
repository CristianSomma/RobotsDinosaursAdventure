using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsDinosaursAdventure.Models
{
    public struct Component
    {
        private readonly string _name;
        private static int _idCounter = 0;

        public Component()
        {
            Interlocked.Increment(ref _idCounter);
            _name = $"Component {_idCounter}";
        }

        public string Name => _name.ToUpper();
    }
}
