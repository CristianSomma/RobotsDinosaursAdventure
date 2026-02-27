using RobotsDinosaursAdventure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsDinosaursAdventure.Models
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void LogError(string message)
        {
            var oldForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {message}");
            Console.ForegroundColor = oldForegroundColor;
        }

        public void LogWarning(string message)
        {
            var oldForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[WARNING] {message}");
            Console.ForegroundColor = oldForegroundColor;
        }
    }
}
