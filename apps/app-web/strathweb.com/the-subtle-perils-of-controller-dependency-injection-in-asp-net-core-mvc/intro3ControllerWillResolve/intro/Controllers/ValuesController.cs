/*
 * https://www.strathweb.com/2016/03/the-subtle-perils-of-controller-dependency-injection-in-asp-net-core-mvc/
 * http://localhost:63425/api/values
 */
using System;
using intro.IntroClasses;
using Microsoft.AspNetCore.Mvc;

namespace intro.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        public IFooService FooService { get; set; }

        [HttpGet]
        public IActionResult Get()
        {
            // use FooService here
            if (FooService == null)
            {
                throw new InvalidOperationException("the instance of the controller itself (and its disposal too) is created and owned by the framework, not by the container!");
            }
            else
            {
                Console.WriteLine("FooService is resolved.");
            }
            return View();
        }
    }
}