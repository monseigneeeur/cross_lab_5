using IdentityClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LabWorksLib
;
namespace IdentityClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Labs()
        {
            return View();
        }

		//http post action method to add a new lab
		//http post method for labs with parameters
		[HttpPost]
		[Authorize]

		public IActionResult Labs(string labName)
		{
			var result = "";
            if (labName == "1")
            {
				
                LabOne labOne = new LabOne();
				result = labOne.execute().ToString();
			}
			else if (labName == "2")
			{
				LabTwo labTwo = new LabTwo();
				result = labTwo.Execute();
			}
			else if (labName == "3")
            {
				LabThree labThree = new LabThree();
				labThree.execute();
				result = "Lab 3 executed see the output file";
			}
			ViewData["result"] = result;
			return View();
			
		}

		public IActionResult Privacy()
        {
            return View();
        }
        
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
