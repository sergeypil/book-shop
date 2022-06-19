using book_shop.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace book_shop.Data
{
    public class BookContext : IdentityDbContext<StoreUser>
    {
        public BookContext(DbContextOptions<BookContext> dbContextOptions) : base(dbContextOptions)
        { }
        public Microsoft.EntityFrameworkCore.DbSet<Product> Products { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Order> Orders { get; set; }
    }
}
