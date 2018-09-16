namespace HoPet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdoptionRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsOpen = c.Boolean(nullable: false),
                        Pet_Id = c.Int(),
                        User_Id = c.Int(),
                        Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pets", t => t.Pet_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .Index(t => t.Pet_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Organization_Id);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        AdoptionDate = c.DateTime(nullable: false),
                        IsAdopted = c.Boolean(nullable: false),
                        AnimalType = c.Int(nullable: false),
                        Description = c.String(),
                        Organization_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Organizations", t => t.Organization_Id)
                .Index(t => t.Organization_Id);
            
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
                "dbo.Organizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        PhoneNumber = c.String(nullable: false),
                        Area = c.Int(nullable: false),
                        CoordX = c.Double(nullable: false),
                        CoordY = c.Double(nullable: false),
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
            DropForeignKey("dbo.Pets", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.AdoptionRequests", "Organization_Id", "dbo.Organizations");
            DropForeignKey("dbo.AdoptionRequests", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AdoptionRequests", "Pet_Id", "dbo.Pets");
            DropIndex("dbo.Pets", new[] { "Organization_Id" });
            DropIndex("dbo.AdoptionRequests", new[] { "Organization_Id" });
            DropIndex("dbo.AdoptionRequests", new[] { "User_Id" });
            DropIndex("dbo.AdoptionRequests", new[] { "Pet_Id" });
            DropTable("dbo.Products");
            DropTable("dbo.Organizations");
            DropTable("dbo.Users");
            DropTable("dbo.Pets");
            DropTable("dbo.AdoptionRequests");
        }
    }
}
