using book_shop.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace book_shop.Data
{
    public class BookSeeder
    {
        private readonly BookContext _bookContext;
        private readonly IWebHostEnvironment _env;

        public BookSeeder(BookContext bookContext, IWebHostEnvironment env)
        {
            _bookContext = bookContext;
            _env = env;
        }

        public void Seed()
        {
            _bookContext.Database.EnsureCreated();
            if (!_bookContext.Products.Any())
            {
                var filePath = Path.Combine(_env.ContentRootPath, "Data/book.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);
                _bookContext.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    Items = new List<OrderItem>()
          {
            new OrderItem()
            {
                Product = products.First(),
              Quantity = 5,
              UnitPrice = products.First().Price
            }
          }
                };
                _bookContext.Orders.Add(order);
                _bookContext.SaveChanges();
            }
        }
    }
}
