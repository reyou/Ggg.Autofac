using System;

namespace intro.IntroClasses
{
    public class CarRepository : ICarRepository
    {
        public string Title { get; set; }
        public CarRepository(DateTime time)
        {
            Title = GetType().Name + " " + Guid.NewGuid() + " " + time.ToString("G");
        }
    }
}