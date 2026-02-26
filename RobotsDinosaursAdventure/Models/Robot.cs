using RobotsDinosaursAdventure.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace RobotsDinosaursAdventure.Models
{
    public class Robot : Entity
    {
        public Robot(ILogger? logger = default) 
            : base("Robot", 3000u, logger) { }

        public async Task ProduceComponents(SharedQueue<Component> queue, CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Wait(token);
                Component newComponent = new Component();
                _logger?.Log($"{Name} has just collected the component");

                await queue.Enqueue(newComponent);
            }
        }
    }
}
