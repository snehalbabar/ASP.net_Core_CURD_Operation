using System;
using ASPNetMvcCRUD.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ASPNetMvcCRUD.Data
{
    public class MvcDemoDbContext : DbContext
    {
        public MvcDemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employees> MyProperty { get; set; }

    }
}

