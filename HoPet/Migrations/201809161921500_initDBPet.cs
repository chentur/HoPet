namespace HoPet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initDBPet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdoptionRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsOpen = c.Boolean(nullable: false),
                        Organization_Id = c.Int(),
                        Pet_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .ForeignKey("dbo.Pets", t => t.Pet_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Organization_Id)
                .Index(t => t.Pet_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Age = c.Double(nullable: false),
                        IsAdopted = c.Boolean(nullable: false),
                        AnimalType = c.Int(nullable: false),
                        Description = c.String(),
                        Organization_Id = c.Int(),
                        Organization_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id1)
                .Index(t => t.Organization_Id1);
            
            CreateTable(
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        Area = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Email = c.String(),
                        ContactInfo = c.String(),
                        Password = c.String(nullable: false),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PetRelated = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AdoptionRequests", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AdoptionRequests", "Pet_Id", "dbo.Pets");
            DropForeignKey("dbo.Pets", "Organization_Id1", "dbo.Organizations");
            DropForeignKey("dbo.AdoptionRequests", "Organization_Id", "dbo.Organizations");
            DropIndex("dbo.Pets", new[] { "Organization_Id1" });
            DropIndex("dbo.AdoptionRequests", new[] { "User_Id" });
            DropIndex("dbo.AdoptionRequests", new[] { "Pet_Id" });
            DropIndex("dbo.AdoptionRequests", new[] { "Organization_Id" });
            DropTable("dbo.Products");
            DropTable("dbo.Users");
            DropTable("dbo.Organizations");
            DropTable("dbo.Pets");
            DropTable("dbo.AdoptionRequests");
        }
    }
}
