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
        protected static int _entityId = 0;

        protected Entity(uint waitingThreshold = 1000u)
        {
            _waitingThreshold = (int)waitingThreshold;
            _entityId++;
        }

        protected async Task Wait()
        {
            int timeToWait = Random.Shared.Next(0, _waitingThreshold);

            await Task.Delay(timeToWait);
        }
    }
}
