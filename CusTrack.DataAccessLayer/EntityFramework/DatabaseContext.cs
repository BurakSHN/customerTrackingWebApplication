using CusTrack.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CusTrack.DataAccessLayer.EntityFramework
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Seans> Seansing { get; set; }
        public DbSet<User> Users { get; set; }

        //Bu bir Constrac'tır !!!
        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }
    }
}
