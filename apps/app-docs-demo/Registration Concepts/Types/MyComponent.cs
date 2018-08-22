using RegistrationConcepts.Interfaces;
using System;

namespace RegistrationConcepts.Types
{
    public class MyComponent
    {
        public MyComponent()
        {
            Console.WriteLine("MyComponent default constructor.");
        }

        public MyComponent(ILogger logger)
        {
            Console.WriteLine("MyComponent ILogger logger constructor.");
        }

        public MyComponent(ILogger logger, IConfigReader reader)
        {
            Console.WriteLine("MyComponent ILogger logger, IConfigReader reader constructor.");
        }
    }


}
