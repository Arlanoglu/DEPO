using MVCMailSender.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCMailSender.Models.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
            Database.Connection.ConnectionString = "server=FATIH\\SQLEXPRESS;database=MVCRegisterWithEmail;uid=sa;pwd=123";
        }

        public DbSet<AppUser> Users { get; set; }
    }
}