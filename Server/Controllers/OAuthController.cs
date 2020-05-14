using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    public class OAuthController:Controller
    {
        [HttpPost]
        public IActionResult Authorize()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Authorize(string userName)
        {
            return View();
        }
        public IActionResult Token()
        {
            return View();
        }
    }
}
