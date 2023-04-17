using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WEB_API.Models;

namespace WEB_API.Data
{
    public class CarInventoryDbContext : DbContext
    {
        public CarInventoryDbContext() : base("CrudApiDatabase")
        {

        }
       
        public DbSet<Cars> cars { get; set; }
        public DbSet<Signup> signups { get; set; }
    }
}