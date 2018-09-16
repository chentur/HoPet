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
            AutomaticMigrationDataLossAllowed = false;
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
            // Organizations
            var organizations = (from organization in context.Organizations select organization);
            context.Organizations.RemoveRange(organizations);
            // Users
            var users = (from user in context.Users select user);
            context.Users.RemoveRange(users);
            // Adoption Requests
            var adoptionRequests = (from adoptionRequest in context.AdoptionRequests select adoptionRequest);
            context.AdoptionRequests.RemoveRange(adoptionRequests);

            context.SaveChanges();

            // Data insertion

            // Products
            context.Products.Add(new Product() { Id = 1, Name = "Ball", Description = "Red ball", PetRelated = AnimalType.DOG, Price = 15.99, Quantity = 100 });
            context.Products.Add(new Product() { Id = 2, Name = "Food", Description = "Dry food", PetRelated = AnimalType.CAT, Price = 60, Quantity = 50 });
            context.Products.Add(new Product() { Id = 3, Name = "Aquarium", Description = "Fish tank", PetRelated = AnimalType.FISH, Price = 84, Quantity = 23 });
            context.Products.Add(new Product() { Id = 4, Name = "Bed", Description = "Big bed", PetRelated = AnimalType.DOG, Price = 250, Quantity = 10 });

            // Organizations
            context.Organizations.Add(new Organization()
            {
                Id = 1,
                Name = "SOS",
                Description = "SOS Pets",
                Area = Area.NORTH,
                PhoneNumber = "099500281"
            });
            context.Organizations.Add(new Organization()
            {
                Id = 2,
                Name = "Hope for Paws",
                Description = "USA",
                Area = Area.EAST,
                PhoneNumber = "0544788199"
            });
            context.Organizations.Add(new Organization()
            {
                Id = 3,
                Name = "Sweet Homes",
                Description = "We love pets!",
                Area = Area.SOUTH,
                PhoneNumber = "0544456899"
            });

            context.SaveChanges();

            // pet
            Organization org1 = context.Organizations.Find(1);
            Console.WriteLine(org1.Id);
            Organization org2 = context.Organizations.Find(2);
            Console.WriteLine(org2.Id);
            Organization org3 = context.Organizations.Find(3);
            Console.WriteLine(org3.Id);

            context.Pets.Add(new Pet() { Id = 1, AnimalType = AnimalType.DOG, Name = "Shuki", Description = "black dog", IsAdopted = false, Age = 4, Organization = org1 });
            context.Pets.Add(new Pet() { Id = 2, AnimalType = AnimalType.FISH, Name = "Gutte", Description = "black fish", IsAdopted = false, Age = 5, Organization = org1 });
            context.Pets.Add(new Pet() { Id = 3, AnimalType = AnimalType.HAMSTER, Name = "Jasper", Description = "black hamster", IsAdopted = true, Age = 1, Organization = org2 });
            context.Pets.Add(new Pet() { Id = 4, AnimalType = AnimalType.RAT, Name = "Pinky", Description = "black rat", IsAdopted = false, Age = 6.5, Organization = org2 });
            context.Pets.Add(new Pet() { Id = 5, AnimalType = AnimalType.DOG, Name = "Kamila", Description = "white dog", IsAdopted = false, Age = 2, Organization = org2 });
            context.Pets.Add(new Pet() { Id = 6, AnimalType = AnimalType.DOG, Name = "Peggy", Description = "blue dog", IsAdopted = true, Age = 10, Organization = org2 });
            context.Pets.Add(new Pet() { Id = 7, AnimalType = AnimalType.HAMSTER, Name = "Ross", Description = "brown hamster", IsAdopted = false, Age = 0.5, Organization = org3 });
            context.Pets.Add(new Pet() { Id = 8, AnimalType = AnimalType.FISH, Name = "Bloop", Description = "nice fish", IsAdopted = false, Age = 3, Organization = org3 });
            context.Pets.Add(new Pet() { Id = 9, AnimalType = AnimalType.PARROT, Name = "Tooki", Description = "colorful parrot", IsAdopted = false, Age = 4, Organization = org3 });

            // Users
            context.Users.Add(new User() { Id = 1, Username = "yardenl", IsAdmin = false, Password = "123456", Email = "yardenl@gmail.com", ContactInfo = "099555281" });
            context.Users.Add(new User() { Id = 2, Username = "chentur", IsAdmin = true, Password = "123456", Email = "chentur@gmail.com", ContactInfo = "0544688199" });
            context.Users.Add(new User() { Id = 3, Username = "OscarGoodBoy", IsAdmin = false, Password = "123456", Email = "goodboy@gmail.com", ContactInfo = "0544788193" });

            // Adoption Requests
            context.AdoptionRequests.Add(new AdoptionRequest() { Id = 1, IsOpen = true, User = context.Users.Find(1), Pet = context.Pets.Find(1) });
            context.AdoptionRequests.Add(new AdoptionRequest() { Id = 2, IsOpen = true, User = context.Users.Find(2), Pet = context.Pets.Find(5) });
            context.AdoptionRequests.Add(new AdoptionRequest() { Id = 3, IsOpen = true, User = context.Users.Find(3), Pet = context.Pets.Find(4) });
            context.AdoptionRequests.Add(new AdoptionRequest() { Id = 4, IsOpen = true, User = context.Users.Find(1), Pet = context.Pets.Find(7) });

            // Connect Pets and Adoption Requests to Organizations

            Organization updateOrg1 = context.Organizations.Find(1);
            //context.Entry(updateOrg1).State = EntityState.Modified;
            updateOrg1.Pets = new List<Pet> { context.Pets.Find(1), context.Pets.Find(2) };
            updateOrg1.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(1) };
            context.Organizations.AddOrUpdate(updateOrg1);

            Organization updateOrg2 = context.Organizations.Find(2);
            //context.Entry(updateOrg2).State = EntityState.Modified;
            updateOrg2.Pets = new List<Pet> { context.Pets.Find(3), context.Pets.Find(4), context.Pets.Find(5), context.Pets.Find(6) };
            updateOrg2.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(2), context.AdoptionRequests.Find(3) };
            context.Organizations.AddOrUpdate(updateOrg2);

            Organization updateOrg3 = context.Organizations.Find(3);
            //context.Entry(updateOrg3).State = EntityState.Modified;
            updateOrg3.Pets = new List<Pet> { context.Pets.Find(7), context.Pets.Find(8), context.Pets.Find(9) };
            updateOrg3.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(4) };
            context.Organizations.AddOrUpdate(updateOrg3);

            context.SaveChanges();
        }
    }
}
