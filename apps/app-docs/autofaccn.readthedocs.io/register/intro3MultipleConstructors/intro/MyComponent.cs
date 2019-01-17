using System;

namespace intro
{
    public class MyComponent
    {
        public MyComponent()
        {
            Console.WriteLine("const1 called.");
        }

        public MyComponent(ILogger logger)
        {
            Console.WriteLine("const2 called.");
        }

        public MyComponent(ILogger logger, IConfigReader reader)
        {
            Console.WriteLine("const3 called.");
        }
    }
}