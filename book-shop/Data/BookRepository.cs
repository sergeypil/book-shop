using book_shop.Data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_shop.Data
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _ctx;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(BookContext ctx, ILogger<BookRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public void AddOrder(Order newOrder)
        {
            foreach (var item in newOrder.Items)
            {
                item.Product = _ctx.Products.Find(item.ProductId);
            }

            AddEntity(newOrder);
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (includeItems)
            {
                return _ctx.Orders
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .ToList();
            }
            else
            {
                return _ctx.Orders
                .ToList();
            }
        }

        public IEnumerable<Order> GetAllOrdersByUser(string userName, bool includeItem)
        {
            if (includeItem)
            {
                return _ctx.Orders
                  .Where(o => o.User.UserName == userName)
                .Include(o => o.Items)
                .ThenInclude(oi => oi.Product)
                .ToList();
            }
            else
            {
                return _ctx.Orders
                  .Where(o => o.User.UserName == userName)
                .ToList();
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            _logger.LogInformation("GetAllProducts was called");
            return _ctx.Products.OrderBy(p => p.Title).ToList();
        }

        public Order GetOrderById(string userName, int id)
        {
            return _ctx.Orders
              .Include(o => o.Items)
              .ThenInclude(oi => oi.Product)
              .Where(o => o.Id == id && o.User.UserName == userName)
              .FirstOrDefault();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products.Where(p => p.Category == category).ToList();
        }

        public bool SaveAll()
        {
            _ctx.SaveChanges();
            return true;
        }
    }
}
