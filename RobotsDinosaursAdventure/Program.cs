using RobotsDinosaursAdventure.Managers;
using RobotsDinosaursAdventure.Models;

namespace RobotsDinosaursAdventure
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Clear();

            int numberOfRobots, numberOfDinosaurs, portalSize;   
            Console.WriteLine("WELCOME!");
            Console.Write("Enter the number of robots:\n> ");
            Parse(out numberOfRobots, num => num < 0);

            Console.Write("Enter the number of dinosaurs:\n> ");
            Parse(out numberOfDinosaurs, num => num < 0);

            Console.Write("Enter the components needed for portal construction:\n> ");
            Parse(out portalSize, num => num < 0);

            Console.WriteLine("SIMULATION STARTED:");
            await new MainManager(new ConsoleLogger())
                .Simulate(
                numberOfRobots, 
                numberOfDinosaurs, 
                portalSize);
        }

        static void Parse(out int variable, Predicate<int>? predicate = null)
        {
            while(!int.TryParse(Console.ReadLine(), out variable) 
                || (predicate?.Invoke(variable) ?? false))
            {
                Console.WriteLine("ERROR: Enter a valid number...");
                Console.Write("> ");
            }
        }
    }
}