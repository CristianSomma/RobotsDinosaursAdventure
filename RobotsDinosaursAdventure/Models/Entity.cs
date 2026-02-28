using RobotsDinosaursAdventure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotsDinosaursAdventure.Models
{
    public abstract class Entity
    {
        private readonly int _waitingThreshold;
        private readonly string _name;
        protected readonly ILogger? _logger;

        protected Entity(string name, uint waitingThreshold = 1000u, ILogger? logger = default)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(name);
            _name = name.ToUpper();
            _waitingThreshold = (int)waitingThreshold;
            _logger = logger;
        }

        public string Name => _name;

        protected async Task Wait(CancellationToken token = default)
        {
            int timeToWait = Random.Shared.Next(0, _waitingThreshold);
            await Task.Delay(timeToWait, token);
        }
    }
}
