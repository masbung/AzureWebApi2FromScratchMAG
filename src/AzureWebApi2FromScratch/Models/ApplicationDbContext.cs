using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AzureWebApi2FromScratch.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection") {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, AzureWebApi2FromScratch.Migrations.Configuration>("DefaultConnection"));
        }

        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<Product> Products { get; set; }
    }
}