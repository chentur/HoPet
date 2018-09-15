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

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<AdoptionRequest> AdoptionRequests { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}