using RegisterAssemblyTypesConsole.Interfaces;
using System;

namespace RegisterAssemblyTypesConsole.Implementations
{
    class Messenger : IMessenger
    {
        public void SendMessage()
        {
            Console.WriteLine("SendMessage is working!");
        }
    }
}