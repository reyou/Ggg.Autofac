using System;

namespace RegisterAssemblyTypesConsole.Interfaces
{
    class Logger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logger is working!");
        }
    }
}