using System;

namespace RegistrationConcepts.Types
{
    internal class UserSession
    {
        public UserSession(DateTime addMinutes)
        {
            Console.WriteLine("UserSession addMinutes: " + addMinutes);
        }
    }
}