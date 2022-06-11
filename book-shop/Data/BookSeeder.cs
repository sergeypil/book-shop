using book_shop.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
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
        private readonly BookContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public BookSeeder(BookContext ctx, IHostingEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            _ctx.Database.EnsureCreated();

            var user = await _userManager.FindByEmailAsync("eakan@dutchtreat.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Test",
                    LastName = "Test",
                    UserName = "test@mail.com",
                    Email = "test@mail.com"
                };
                var result = await _userManager.CreateAsync(user, "Test123");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user!");
                }
            }

            if (!_ctx.Products.Any())
            {
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/book.json");
                var json = File.ReadAllText(filepath);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);

                _ctx.Products.AddRange(products);

                var order = new Order()
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    User = user,
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
                _ctx.Orders.Add(order);
                _ctx.SaveChanges();
            }
        }
    }
}