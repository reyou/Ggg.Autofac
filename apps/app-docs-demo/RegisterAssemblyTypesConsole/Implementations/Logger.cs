using RegisterAssemblyTypesConsole.Interfaces;
using System;

namespace RegisterAssemblyTypesConsole.Implementations
{
    class Logger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Logger is working!");
        }
    }
}