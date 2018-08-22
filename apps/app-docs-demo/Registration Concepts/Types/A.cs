using System;

namespace RegistrationConcepts.Types
{
    internal class A
    {
        public A(B bInstance)
        {
            Console.WriteLine("A B bInstance constructor.");
        }

        public A()
        {
            Console.WriteLine("A constructor.");
        }

        public B MyB { get; set; }
        public B B { get; set; }
        public int PropertyName { get; set; }
    }
}