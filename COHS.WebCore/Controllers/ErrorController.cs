using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace COHS.WebCore.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Status()
        {
            return View();
        }

		public IActionResult Exception()
		{
			return View();
		}
	}
}