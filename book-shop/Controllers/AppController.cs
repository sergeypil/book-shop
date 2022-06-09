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
        private readonly BookContext bookContext;

        public AppController(BookContext bookContext)
        {
            this.bookContext = bookContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contac")]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost("contac")]
        public IActionResult Contact(LoginModel loginModel)
        {
            return View();
        }

        public IActionResult Shop()
        {
            var results = bookContext.Products.ToList();
            return View(results);
        }
    }
}
