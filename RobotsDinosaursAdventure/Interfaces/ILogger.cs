using System;
using System.Collections.Generic;
using System.Text;

namespace RobotsDinosaursAdventure.Interfaces
{
    public interface ILogger
    {
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}
