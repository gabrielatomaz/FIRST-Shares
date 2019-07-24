using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FIRSTShares.Controllers
{
    public class ResourcesController : Controller
    {
        public IActionResult Tutorials()
        {
            return View();
        }
    }
}