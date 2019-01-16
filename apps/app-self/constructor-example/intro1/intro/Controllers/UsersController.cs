using intro.IntroClasses;
using Microsoft.AspNetCore.Mvc;

namespace intro.Controllers
{
    public class UsersController : Controller
    {
        public IUserRepository UserRepo { get; set; }
        public ICarRepository CarRepo { get; set; }
        public UsersController(IUserRepository userRepository, ICarRepository carRepository)
        {
            UserRepo = userRepository;
            CarRepo = carRepository;
        }

        public IActionResult Index()
        {
            return View("Index", UserRepo.Title + " " + CarRepo.Title);
        }
    }
}