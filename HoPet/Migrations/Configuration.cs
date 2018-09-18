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
                PhoneNumber = "099500281",
                CoordX = 51.50632,
                CoordY = -0.12714
            });
            context.Organizations.Add(new Organization()
            {
                Id = 2,
                Name = "Hope for Paws",
                Description = "USA",
                Area = Area.EAST,
                PhoneNumber = "0544788199",
                CoordX = 31.954758,
                CoordY = 34.842255
            });
            context.Organizations.Add(new Organization()
            {
                Id = 3,
                Name = "Sweet Homes",
                Description = "We love pets!",
                Area = Area.SOUTH,
                PhoneNumber = "0544456899",
                CoordX = 31.954758,
                CoordY = 34.842255
            });

            context.SaveChanges();

            // pet
            Organization org1 = context.Organizations.Find(1);
            Console.WriteLine(org1.Id);
            Organization org2 = context.Organizations.Find(2);
            Console.WriteLine(org2.Id);
            Organization org3 = context.Organizations.Find(3);
            Console.WriteLine(org3.Id);

            context.Pets.Add(new Pet() { Id = 1, AnimalType = AnimalType.DOG, Name = "Shuki", Description = "black dog", IsAdopted = false, Age = 4, Organization = org1, Organization_Id = org1.Id});
            context.Pets.Add(new Pet() { Id = 2, AnimalType = AnimalType.FISH, Name = "Gutte", Description = "black fish", IsAdopted = false, Age = 5, Organization = org1, Organization_Id = org1.Id });
            context.Pets.Add(new Pet() { Id = 3, AnimalType = AnimalType.HAMSTER, Name = "Jasper", Description = "black hamster", IsAdopted = true, Age = 1, Organization = org2, Organization_Id = org2.Id });
            context.Pets.Add(new Pet() { Id = 4, AnimalType = AnimalType.RAT, Name = "Pinky", Description = "black rat", IsAdopted = false, Age = 6.5, Organization = org2, Organization_Id = org2.Id });
            context.Pets.Add(new Pet() { Id = 5, AnimalType = AnimalType.DOG, Name = "Kamila", Description = "white dog", IsAdopted = false, Age = 2, Organization = org2, Organization_Id = org2.Id });
            context.Pets.Add(new Pet() { Id = 6, AnimalType = AnimalType.DOG, Name = "Peggy", Description = "blue dog", IsAdopted = true, Age = 10, Organization = org2, Organization_Id = org2.Id });
            context.Pets.Add(new Pet() { Id = 7, AnimalType = AnimalType.HAMSTER, Name = "Ross", Description = "brown hamster", IsAdopted = false, Age = 0.5, Organization = org3, Organization_Id = org3.Id });
            context.Pets.Add(new Pet() { Id = 8, AnimalType = AnimalType.FISH, Name = "Bloop", Description = "nice fish", IsAdopted = false, Age = 3, Organization = org3, Organization_Id = org3.Id });
            context.Pets.Add(new Pet() { Id = 9, AnimalType = AnimalType.PARROT, Name = "Tooki", Description = "colorful parrot", IsAdopted = false, Age = 4, Organization = org3, Organization_Id = org3.Id });

            // Users
            context.Users.Add(new User() { Id = 1, Username = "yardenl", IsAdmin = false, Password = "123456", Email = "yardenl@gmail.com", ContactInfo = "099555281" });
            context.Users.Add(new User() { Id = 2, Username = "chentur", IsAdmin = true, Password = "123456", Email = "chentur@gmail.com", ContactInfo = "0544688199" });
            context.Users.Add(new User() { Id = 3, Username = "OscarGoodBoy", IsAdmin = false, Password = "123456", Email = "goodboy@gmail.com", ContactInfo = "0544788193" });

            // Adoption Requests
            User user1 = context.Users.Find(1);
            Console.WriteLine(user1.Id);
            User user2 = context.Users.Find(2);
            Console.WriteLine(user2.Id);
            User user3 = context.Users.Find(3);
            Console.WriteLine(user3.Id);

            Pet pet1 = context.Pets.Find(1);
            Console.WriteLine(pet1.Id);
            Pet pet2 = context.Pets.Find(2);
            Console.WriteLine(pet2.Id);
            Pet pet3 = context.Pets.Find(3);
            Console.WriteLine(pet3.Id);
            Pet pet4 = context.Pets.Find(4);
            Console.WriteLine(pet4.Id);
            Pet pet5 = context.Pets.Find(5);
            Console.WriteLine(pet5.Id);
            Pet pet6 = context.Pets.Find(6);
            Console.WriteLine(pet6.Id);
            Pet pet7 = context.Pets.Find(7);
            Console.WriteLine(pet7.Id);
            Pet pet8 = context.Pets.Find(8);
            Console.WriteLine(pet8.Id);
            Pet pet9 = context.Pets.Find(9);
            Console.WriteLine(pet9.Id);

            context.AdoptionRequests.Add(new AdoptionRequest() { Id = 1, IsOpen = true, User = user1, User_Id = user1.Id, Pet = pet1, Pet_Id = pet1.Id});
            context.AdoptionRequests.Add(new AdoptionRequest() { Id = 2, IsOpen = true, User = user2, User_Id = user2.Id , Pet = pet5, Pet_Id = pet5.Id });
            context.AdoptionRequests.Add(new AdoptionRequest() { Id = 3, IsOpen = true, User = user3, User_Id = user3.Id, Pet = pet4, Pet_Id = pet4.Id });
            context.AdoptionRequests.Add(new AdoptionRequest() { Id = 4, IsOpen = true, User = user1, User_Id = user1.Id, Pet = pet7, Pet_Id = pet7.Id });

            // Connect Pets and Adoption Requests to Organizations

            Organization updateOrg1 = context.Organizations.Find(1);
            updateOrg1.Pets = new List<Pet> { context.Pets.Find(1), context.Pets.Find(2) };
            updateOrg1.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(1) };
            context.Organizations.AddOrUpdate(updateOrg1);

            Organization updateOrg2 = context.Organizations.Find(2);
            updateOrg2.Pets = new List<Pet> { context.Pets.Find(3), context.Pets.Find(4), context.Pets.Find(5), context.Pets.Find(6) };
            updateOrg2.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(2), context.AdoptionRequests.Find(3) };
            context.Organizations.AddOrUpdate(updateOrg2);

            Organization updateOrg3 = context.Organizations.Find(3);
            updateOrg3.Pets = new List<Pet> { context.Pets.Find(7), context.Pets.Find(8), context.Pets.Find(9) };
            updateOrg3.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(4) };
            context.Organizations.AddOrUpdate(updateOrg3);

            // Connect Adoption Requests to Pets

            Pet updatePet1 = context.Pets.Find(1);
            updatePet1.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(1) };
            context.Pets.AddOrUpdate(updatePet1);

            Pet updatePet5 = context.Pets.Find(5);
            updatePet5.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(2) };
            context.Pets.AddOrUpdate(updatePet5);

            Pet updatePet4 = context.Pets.Find(4);
            updatePet4.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(3) };
            context.Pets.AddOrUpdate(updatePet4);

            Pet updatePet7 = context.Pets.Find(7);
            updatePet7.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(4) };
            context.Pets.AddOrUpdate(updatePet7);

            // Connect Adoption Requests to Users

            User updateUser1 = context.Users.Find(1);
            updateUser1.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(1), context.AdoptionRequests.Find(4) };
            context.Users.AddOrUpdate(updateUser1);

            User updateUser2 = context.Users.Find(2);
            updateUser2.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(2) };
            context.Users.AddOrUpdate(updateUser2);

            User updateUser3 = context.Users.Find(3);
            updateUser3.AdoptionRequests = new List<AdoptionRequest> { context.AdoptionRequests.Find(3) };
            context.Users.AddOrUpdate(updateUser3);

            context.SaveChanges();
        }
    }
}
