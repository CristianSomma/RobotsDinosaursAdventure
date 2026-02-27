using RobotsDinosaursAdventure.Interfaces;
using RobotsDinosaursAdventure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsDinosaursAdventure.Managers
{
    public class MainManager
    {
        private readonly CancellationTokenSource _tokenSource;
        private readonly ILogger? _logger;
        public MainManager(ILogger? logger = default)
        {
            _tokenSource = new CancellationTokenSource();
            _logger = logger;
        }
        
        public async Task Simulate(uint numberOfRobots, uint numberOfDinosaurs)
        {
            var componentsQueue = new SharedQueue<Component>(_tokenSource.Token);
            var portalStack = new SharedStack<Component>(_tokenSource.Token);

            _ = Task.Run(async () =>
            {
                for(int i = 0; i < numberOfRobots; i++)
                {
                    Robot newRobot = new Robot(_logger);
                    newRobot.
                }
            }, _tokenSource.Token);
        }
    }
}
