using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Notflix.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index(string genre)
        {
            ViewData["genre"] = genre;
            return View();
        }
    }
}
