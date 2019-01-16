using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using intro.IntroClasses;
using Microsoft.AspNetCore.Mvc;

namespace intro.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IFooService _service;

        public ValuesController(IFooService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            // use _service here

            return View();
        }
    }
}