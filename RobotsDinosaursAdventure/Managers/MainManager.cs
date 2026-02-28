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
        
        public async Task Simulate(int numberOfRobots, int numberOfDinosaurs, int portalSize)
        {
            // queue e stack comuni
            var componentsQueue = new SharedQueue<Component>(_tokenSource.Token);
            var portalStack = new SharedStack<Component>(_tokenSource.Token);
            
            // lista dei task da aspettare prima di chiudere questa funzione
            List<Task> tasksRunning = new List<Task>();

            tasksRunning.Add(Task.Run(() => ListenForInput(_tokenSource.Token)));

            // genera il numero di robot definiti, e li avvia alla produzione
            // dei componenti
            for (int i = 0; i < numberOfRobots; i++)
            {
                Robot newRobot = new Robot($"Robot {i}", _logger);

                tasksRunning.Add(
                    newRobot.ProduceComponents(
                    componentsQueue,
                    _tokenSource.Token));
            }

            // genera il numero di dinosauri definiti e li avvia alla costruzione
            // del portale
            for (int i = 0; i < numberOfDinosaurs; i++)
            {
                Dinosaur newDinosaur = new Dinosaur(
                    $"Dinosaur {i}",
                    (uint)portalSize,
                    _logger);

                tasksRunning.Add(
                    newDinosaur.Build(
                        componentsQueue,
                        portalStack,
                        _tokenSource
                        ));
            }

            await Task.WhenAll(tasksRunning);
        }

        private void ListenForInput(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                ConsoleKeyInfo keyPressed = Console.ReadKey(intercept: true);

                if(keyPressed.Key == ConsoleKey.Escape)
                {
                    _logger?.LogWarning("SIMULATION TERMINATED");
                    _tokenSource.Cancel();
                    break;
                }
            }
        }
    }
}
