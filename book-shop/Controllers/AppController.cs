using book_shop.Data;
using book_shop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_shop.Controllers
{

    public class AppController : Controller
    { 

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Views/App/Index.cshtml");
        }
    }
}
