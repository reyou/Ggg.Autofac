using System;

namespace intro.IntroClasses
{
    public class UserRepository : IUserRepository
    {
        public string Title { get; set; }
        public UserRepository()
        {
            Title = GetType().Name + " " + Guid.NewGuid().ToString();
        }
    }
}