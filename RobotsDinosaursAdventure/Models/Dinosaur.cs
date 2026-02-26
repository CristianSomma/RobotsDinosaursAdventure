using RobotsDinosaursAdventure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsDinosaursAdventure.Models
{
    public class Dinosaur : Entity
    {
        private CancellationToken _token;

        public Dinosaur(CancellationToken token, ILogger? logger = default)
            : base("Dinosaur", 5500u, logger)
        {
            _token = token;
        }

        public async Task Build(SharedQueue<Component> componentsQueue)
        {
            while (!_token.IsCancellationRequested)
            {
                Component component = await componentsQueue.Dequeue();


            }
        }
    }
}
