using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Entities;

namespace API.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<ClassName> TableName { get; set; } 
        //TableName to be created in database must be in plural while ClassName in singular
        public DbSet<Product> Products { get; set; }

        public DbSet<Basket> Baskets { get; set; }
    }
}