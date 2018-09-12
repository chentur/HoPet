using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace HoPet.Models
{
    public class ProjectDBContext : System.Data.Entity.DbContext
    {
        public ProjectDBContext() : base("ProjectDBContext")
        {
            Database.SetInitializer<ProjectDBContext>(null);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<AdoptionRequest> AdoptionRequests { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}