namespace HoPet.Migrations
{
    using HoPet.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HoPet.Models.ProjectDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(HoPet.Models.ProjectDBContext context)
        {
            // Cleanup

            // Products
            var products = (from product in context.Products select product);
            context.Products.RemoveRange(products);
            // Pet
            var pets = (from pet in context.Pets select pet);
            context.Pets.RemoveRange(pets);
            // Adoption Requests
            var adoptionRequests = (from adoptionRequest in context.adoptionRequests select adoptionRequest);
            context.adoptionRequests.RemoveRange(adoptionRequests);
            // Organizations
            var organizations = (from organization in context.Organizations select organization);
            context.Organizations.RemoveRange(organizations);
            // Users
            var users = (from user in context.Users select user);
            context.Users.RemoveRange(users);

            context.SaveChanges();

            // Data insertion

            // Products
            context.Products.Add(new Product() { Id = 1, Name = "Ball", Description = "Red ball", PetRelated = AnimalType.DOG, Price = 15.99, Quantity = 100 });
            context.Products.Add(new Product() { Id = 2, Name = "Food", Description = "Dry food", PetRelated = AnimalType.CAT, Price = 60, Quantity = 50 });
            context.Products.Add(new Product() { Id = 3, Name = "Aquarium", Description = "Fish tank", PetRelated = AnimalType.FISH, Price = 84, Quantity = 23 });
            context.Products.Add(new Product() { Id = 4, Name = "Bed", Description = "Big bed", PetRelated = AnimalType.DOG, Price = 250, Quantity = 10 });

            // pet
            context.Pets.Add(new Pet() { Id = 1, AdoptionDate = new System.DateTime(2019, 7, 17), AnimalType = AnimalType.DOG, Name = "Shuki", Description = "black dog", IsAdopted = false, Birthdate = new System.DateTime(2017, 07, 17) });
            context.Pets.Add(new Pet() { Id = 2, AdoptionDate = new System.DateTime(2018, 7, 17), AnimalType = AnimalType.FISH, Name = "Gutte", Description = "black fish", IsAdopted = false, Birthdate = new System.DateTime(2017, 07, 17) });
            context.Pets.Add(new Pet() { Id = 3, AdoptionDate = new System.DateTime(2018, 9, 1), AnimalType = AnimalType.HAMSTER, Name = "Jasper", Description = "black hamster", IsAdopted = true, Birthdate = new System.DateTime(2017, 07, 17) });
            context.Pets.Add(new Pet() { Id = 4, AdoptionDate = new System.DateTime(2017, 07, 17), AnimalType = AnimalType.RAT, Name = "Pinky", Description = "black rat", IsAdopted = false, Birthdate = new System.DateTime(2017, 07, 17) });
            context.Pets.Add(new Pet() { Id = 5, AdoptionDate = new System.DateTime(2017, 02, 17), AnimalType = AnimalType.DOG, Name = "Kamila", Description = "white dog", IsAdopted = true, Birthdate = new System.DateTime(2017, 07, 17) });

            // Adoption Requests
            context.adoptionRequests.Add(new AdoptionRequest() { Id = 1, IsOpen = true, User = context.Users.Find(1), Pet = context.Pets.Find(1) });
            context.adoptionRequests.Add(new AdoptionRequest() { Id = 2, IsOpen = true, User = context.Users.Find(2), Pet = context.Pets.Find(5) });

            // Organizations
            context.Organizations.Add(new Organization()
            {
                Id = 1,
                Name = "SOS",
                Description = "SOS Pets",
                Area = Area.NORTH,
                PhoneNumber = "099500281",
                Pets = new List<Pet> { context.Pets.Find(1), context.Pets.Find(2) },
                AdoptionRequests = new List<AdoptionRequest> { context.adoptionRequests.Find(1) }
            });
            context.Organizations.Add(new Organization()
            {
                Id = 2,
                Name = "Hope for Paws",
                Description = "USA",
                Area = Area.EAST,
                PhoneNumber = "0544788199",
                Pets = new List<Pet> { context.Pets.Find(3), context.Pets.Find(4), context.Pets.Find(5) },
                AdoptionRequests = new List<AdoptionRequest> { context.adoptionRequests.Find(2) }
            });

            // Users
            context.Users.Add(new User() { Id = 1, Username = "yardenl", IsAdmin = false, Password = "123456", Email = "yardenl@gmail.com", ContactInfo = "099555281" });
            context.Users.Add(new User() { Id = 2, Username = "chentur", IsAdmin = true, Password = "123456", Email = "chentur@gmail.com", ContactInfo = "0544688199" });
            context.Users.Add(new User() { Id = 3, Username = "OscarGoodBoy", IsAdmin = false, Password = "123456", Email = "goodboy@gmail.com", ContactInfo = "0544788193" });


            context.SaveChanges();
        }
    }
}
