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
            /*
             * -> Viene creato un nuovo componente
             * -> Il componente viene messo in coda
             */

            while (!token.IsCancellationRequested)
            {
                await Wait(token);
                Component newComponent = new Component();
                _logger?.Log($"{Name} has just collected the {newComponent.Name}");

                await queue.Enqueue(newComponent);
            }
        }
    }
}
