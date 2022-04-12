using MVCLogin.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCLogin.Models.Context
{
    public class AppDbContext:DbContext
    {
        public AppDbContext()
        {
            Database.Connection.ConnectionString = "server=FATIH\\SQLEXPRESS;database=MVCLogin;uid=sa;pwd=123";
        }

        public DbSet<AppUser> Users { get; set; }
    }
}