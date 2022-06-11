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
        private readonly IBookRepository bookRepository;

        public AppController(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
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
        public IActionResult Contact(LoginViewModel loginModel)
        {
            return View();
        }

        public IActionResult Shop()
        {
            var results = bookRepository.GetAllProducts();
            return View(results);
        }
    }
}
