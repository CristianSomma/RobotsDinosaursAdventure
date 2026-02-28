using RobotsDinosaursAdventure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsDinosaursAdventure.Models
{
    public class Dinosaur : Entity
    {
        private readonly int _portalSizeTarget;

        public Dinosaur(
            string name,
            uint portalSizeTarget, 
            ILogger? logger = default)
            
            : base(name, 4500u, logger)
        {
            _portalSizeTarget = (int)portalSizeTarget;
        }

        public async Task Build(
            SharedQueue<Component> componentsQueue, 
            SharedStack<Component> portalStack,
            CancellationTokenSource? tokenSource = default)
        {
            /*
             * -> finché il token non ordina di fermare l'esecuzione del metodo
             * -> Ottiene il primo componente nella coda
             * -> Lo inserisce nello stack del portale
             * -> Se lo stack supera la dimensione massima, lo svuota
             */

            CancellationToken token = tokenSource is default(CancellationTokenSource)
                ? CancellationToken.None
                : tokenSource.Token;

            while (!token.IsCancellationRequested)
            {
                await Wait(token);
                Component component = await componentsQueue.Dequeue();

                await portalStack.Push(component);
                _logger?.Log($"{Name} used {component.Name} to assemble the portal.");

                if(await portalStack.TryClear(length =>
                {
                    return length >= _portalSizeTarget;
                }))
                {
                    _logger?.LogWarning("The portal is completed");
                    tokenSource?.Cancel();
                }
            }
        }
    }
}
