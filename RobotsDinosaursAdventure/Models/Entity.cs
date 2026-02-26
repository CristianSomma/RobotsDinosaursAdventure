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
        private static int _entityId = 0;
        protected readonly ILogger? _logger;

        protected Entity(string name, uint waitingThreshold = 1000u, ILogger? logger = default)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
            _entityId++;
            _name = $"{name} {_entityId}".ToUpper();
            _waitingThreshold = (int)waitingThreshold;
            _logger = logger;
        }

        public string Name => _name;

        protected async Task Wait(CancellationToken token)
        {
            int timeToWait = Random.Shared.Next(0, _waitingThreshold);

            await Task.Delay(timeToWait, token);
        }
    }
}
