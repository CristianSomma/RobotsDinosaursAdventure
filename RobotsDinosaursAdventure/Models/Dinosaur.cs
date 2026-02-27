using RobotsDinosaursAdventure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsDinosaursAdventure.Models
{
    public class Dinosaur : Entity
    {
        private readonly int _portalSizeTarget;
        private readonly CancellationTokenSource _tokenSource;
        private readonly CancellationToken _token;

        public Dinosaur(
            uint portalSizeTarget, 
            CancellationTokenSource tokenSource, 
            CancellationToken token, 
            ILogger? logger = default)
            
            : base("Dinosaur", 5500u, logger)
        {
            _portalSizeTarget = (int)portalSizeTarget;
            _tokenSource = tokenSource;
            _token = token;
        }

        public async Task Build(
            SharedQueue<Component> componentsQueue, 
            SharedStack<Component> portalStack)
        {
            while (!_token.IsCancellationRequested)
            {
                Component component = await componentsQueue.Dequeue();

                await portalStack.Push(component);

                if(await portalStack.TryClear(length =>
                {
                    return length >= _portalSizeTarget;
                }))
                    _tokenSource.Cancel();
            }
        }
    }
}
