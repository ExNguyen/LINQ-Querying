using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using LINQLab.Models;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using MySqlX.XDevAPI.Common;
using Org.BouncyCastle.Asn1.Pkcs;
using System.Numerics;

namespace LINQLab
{
    class Problems
    {
        private EcommerceContext _context;

        public Problems()
        {
            _context = new EcommerceContext();
        }
        public void RunLINQQueries()
        {
            //// <><><><><><><><> R Actions (Read) <><><><><><><><><>
            RDemoOne();
            RProblemOne();
            RDemoTwo();
            RProblemTwo();
            RProblemThree();
            RProblemFour();
            RProblemFive();

            // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>
            RDemoThree();
            RProblemSix();
            RProblemSeven();
            RProblemEight();

            // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

            // <><> C Actions (Create) <><>
            CDemoOne();
            CProblemOne();
            CDemoTwo();
            CProblemTwo();

            //// <><> U Actions (Update) <><>
            UDemoOne();
            UProblemOne();
            UProblemTwo();

            //// <><> D Actions (Delete) <><>
            DDemoOne();
            DProblemOne();
            DProblemTwo();
            BonusOne();
            BonusTwo();


        }

        // <><><><><><><><> R Actions (Read) <><><><><><><><><>
        private void RDemoOne()
        {
            // This LINQ query will return all the users from the User table.
            var users = _context.Users.ToList();

            Console.WriteLine("RDemoOne: Emails of All users");
            foreach (User user in users)
            {
                Console.WriteLine(user.Email);
            }

        }

        private void RProblemOne()
        {
            // Print the COUNT of all the users from the User table.
            var users = _context.Users.Count();
            Console.WriteLine("\nRProblemOne: amount of users");
            Console.WriteLine(users);
        }

        /*
       Expected Result:
       User Count: 5
        */

        public void RDemoTwo()
        {
            // This LINQ query will get each product whose price is greater than $150.
            var productsOver150 = _context.Products.Where(p => p.Price > 150);
            Console.WriteLine("\nRDemoTwo: Products greater than $150");
            foreach (Product product in productsOver150)
            {
                Console.WriteLine($"{product.Name} - ${product.Price}");
            }
        }

        public void RProblemTwo()
        {
            // Write a LINQ query that gets each product whose price is less than or equal to $100.
            // Print the name and price of all products
            var productUnder100 = _context.Products.Where(p => p.Price < 100);
            Console.WriteLine("\nRProblemTwo: Products and price");
            foreach(Product product in productUnder100)
            {
                Console.WriteLine($"{product.Name} - ${product.Price}");
            }
        }

        /*
            Expected Result:

            Name: Freedom from the Known - Jiddu Krishnamurti
            Price: $14.99

            Name: Ball Mason Jar-32 oz.
            Price: $8.85

            Name: Catan The Board Game
            Price: $43.67
         */

        public void RProblemThree()
        {
            // Write a LINQ query that gets each product whose name that CONTAINS an "s".
            var productsThatContainS = _context.Products.Where(p => p.Name.Contains("s"));
            Console.WriteLine("\nRProblemThree: Products that contain the letter s");
            foreach (Product product in productsThatContainS)
            {
                Console.WriteLine($"Name: {product.Name}");
            }
        }
        /*
            Expected Result:

            Name: Freedom from the Known - Jiddu Krishnamurti

            Name: Ball Mason Jar-32 oz.

            Name: Stellar Basic Flute Key of G - Native American Style Flute

            Name: Apple Watch Series 3

            Name: Nintendo Switch
         */

        public void RProblemFour()
        {
            // Write a LINQ query that gets all the users who registered BEFORE 2016.
            // Then print each user's email and registration date to the console.
            var usersRegisteredBefore2016 = _context.Users.Where(u => u.RegistrationDate < new DateTime(2016, 1, 1));
            Console.WriteLine("\nRPoblemFour: User who registered BEFORE 2016");
            foreach (var user in usersRegisteredBefore2016)
            {
                Console.WriteLine($"User Email: {user.Email},\n Registration Date: {user.RegistrationDate}");
            }
        }
        /*
            Expected Result:

            Email: janett@gmail.com
            Registration Date: 10/15/2015 12:00:00 AM
            Email: gary@gmail.com
            Registration Date: 10/15/2012 12:00:00 AM
         */

        public void RProblemFive()
        {
            // Write a LINQ query that gets all of the users who registered AFTER 2016 and BEFORE 2018.
            // Then print each user's email and registration date to the console.
            var usersRegisteredBefore2016 = _context.Users.Where(u => u.RegistrationDate > new DateTime(2016, 1, 1) && u.RegistrationDate < new DateTime(2018, 1, 1));
            Console.WriteLine("\nRPoblemFour: User who registered AFTER 2016 and BEFORE 2018");
            foreach (var user in usersRegisteredBefore2016)
            {
                Console.WriteLine($"User Email: {user.Email},\n Registration Date: {user.RegistrationDate}");
            }
        }
        /*
            Expected Result:

            Email: bibi@gmail.com
            Registration Date: 4/6/2017 12:00:00 AM
         */

        // <><><><><><><><> R Actions (Read) with Foreign Keys <><><><><><><><><>

        private void RDemoThree()
        {
            // This LINQ query will retreive all of the users who are assigned to the role of Customer.
            var customerUsers = _context.UserRoles.Include(ur => ur.Role).Include(ur => ur.User).Where(ur => ur.Role.RoleName == "Customer");
            Console.WriteLine("\nRDemoThree: Customer Users");
            foreach (UserRole userrole in customerUsers)
            {
                Console.WriteLine($"Email: {userrole.User.Email} Role: {userrole.Role.RoleName}");
            }
        }
        public void RProblemSix()
        {
            // Write a LINQ query that retrieves all of the products in the shopping cart of the user who has the email "afton@gmail.com".
            // Then print the product's name, price, and quantity to the console.
            var userShoppingCart = _context.ShoppingCartItems.Include(usc => usc.User).Include(usc => usc.Product).Where(usc => usc.User.Email == "afton@gmail.com");
            Console.WriteLine("\nRProblemSix: afton@gmail.com shopping cart");
            foreach (ShoppingCartItem shoppingCartItem in userShoppingCart)
            {
                Console.WriteLine($"Name: {shoppingCartItem.Product.Name} \nPrice: ${shoppingCartItem.Product.Price} \nQuantity: {shoppingCartItem.Quantity}");
                Console.WriteLine();
            }

        }
        /*
            Expected Result:
            Name: Freedom from the Known - Jiddu Krishnamurti
            Price: $14.99
            Quantity: 1

            Name: Ball Mason Jar-32 oz.
            Price: $8.85
            Quantity: 10

            Name: Catan The Board Game
            Price: $43.67
            Quantity: 1

            Name: Nintendo Switch
            Price: $299.00
            Quantity: 1
        */

        public void RProblemSeven()
        {
            // Write a LINQ query that retrieves all of the products in the shopping cart of the user who has the email "oda@gmail.com" and returns the sum of all of the products prices.
            // HINT: End of query will be: .Select(sc => sc.Product.Price).Sum();
            // Print the total of the shopping cart to the console.
            // Remember to break the problem down and take it one step at a time!
            var userShoppingCart = _context.ShoppingCartItems.Include(usc => usc.User).Include(usc => usc.Product).Where(usc => usc.User.Email == "oda@gmail.com").Select(sc => sc.Product.Price).Sum();
            Console.WriteLine("\nRProblemSeven: total sum of oda@gmail.com shopping cart");
            Console.WriteLine($"Total: ${userShoppingCart}");
        }
        /*
         Total: $715.34
         */

        public void RProblemEight()
        {
            // Write a query that retrieves all of the products in the shopping cart of users who have the role of "Employee".
            // Then print the product's name, price, and quantity to the console along with the email of the user that has it in their cart.
            var employeeShoppingCart = _context.Users.Include(ur => ur.UserRoles)
                .ThenInclude(r => r.Role).Include(user => user.ShoppingCartItems)
                .ThenInclude(item => item.Product)
                .Where(user => user.UserRoles.Any(ur => ur.Role.RoleName == "Employee"));

            Console.WriteLine("\nRProblemEight: Retrieve employees shopping cart");

            foreach (var userCart in employeeShoppingCart)
            {
                Console.WriteLine($"User's email: {userCart.Email}");
                Console.WriteLine("-----------");

                foreach (var cartItem in userCart.ShoppingCartItems)
                {
                    Console.WriteLine($"Product name: {cartItem.Product.Name}");
                    Console.WriteLine($"Price: ${cartItem.Product.Price}");
                    Console.WriteLine($"Quantity: {cartItem.Quantity}");
                    Console.WriteLine();
                }
            }

        }
        /*
            Expected Result

            User's email: bibi@gmail.com
            -----------
            Product name: Apple Watch Series 3
            Price: 169.00
            Quantity:5



            User's email: janett@gmail.com
            -----------
            Product name: Freedom from the Known - Jiddu Krishnamurti
            Price: 14.99
            Quantity:1

            Product name: Catan The Board Game
            Price: 43.67
            Quantity:1
         */

        //        // <><><><><><><><> CUD (Create, Update, Delete) Actions <><><><><><><><><>

        //        //IMPORTANT: The following methods will MODIFY your database. Even if you stop and restart the application, any changes made to the database will persist!
        //        //Calling a Create method more than once will result in duplicate data added to the table.
        //        //Calling an Update or Delete method more than once might cause an error. For instance, if you call a method that deletes the Nintendo Switch from the database, then try to call the method again, there will no longer be a Nintendo Switch to delete!
        //        //You may want to use Breakpoints or WriteLines to verify your LINQ Queries are finding the correct items before you add the .SaveChanges() to the method!

        // <><> C Actions (Create) <><>

        private void CDemoOne()
        {
            // This will create a new User object and add it to the Users table.
            //User newUser = new User()
            //{
            //    Email = "david@gmail.com",
            //    Password = "DavidsPass123"
            //};
            //_context.Users.Add(newUser);
            //_context.SaveChanges();
        }

        private void CProblemOne()
        {
            // Create a new Product object and add that product to the Products table. Choose any name and product info you like.
            //double newItemPrice = 214.99;
            //Product newProduct = new Product()
            //{
            //    Name = "Cooler Master V850 SFX",
            //    Description = "SFX Form Factor: High - quality PSU that's compatible with all SFX cases and suitable for mini-ITX system builds.",
            //    Price = (decimal)newItemPrice
            //};
            //_context.Products.Add(newProduct);
            //_context.SaveChanges();
        }

        public void CDemoTwo()
        {
            //// This will add the role of "Customer" to the user we created in CDemoOne by adding a new row to the Userroles junction table.
            //var roleId = _context.Roles.Where(r => r.RoleName == "Customer").Select(r => r.Id).SingleOrDefault();
            //var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            //UserRole newUserrole = new UserRole()
            //{
            //    UserId = userId,
            //    RoleId = roleId
            //};
            //_context.UserRoles.Add(newUserrole);
            //_context.SaveChanges();
            //// If you encounter problems running this method, it likely means you ran CDemoOne multiple times and have created duplicate customers with the email "david@gmail.com"
        }

        public void CProblemTwo()
        {
            // Create a new ShoppingCartItem to represent the new product you created in CProblemOne being added to the shopping cart of the user created in CDemoOne.
            // This will add a new row to ShoppingCart junction table.
            //var userId = _context.Users.Where(u => u.Email == "david@gmail.com").Select(u => u.Id).SingleOrDefault();
            //var productId = _context.Products.Where(p => p.Name == "Cooler Master V850 SFX").Select(p => p.Id).SingleOrDefault();
            //var quantity = 1;
            //ShoppingCartItem newShoppingCartItem = new ShoppingCartItem()
            //{
            //    UserId = userId,
            //    ProductId = productId,
            //    Quantity = quantity
            //};
            //_context.ShoppingCartItems.Add(newShoppingCartItem);
            //_context.SaveChanges();
        }


        // <><> U Actions (Update) <><>

        private void UDemoOne()
        {
            //// This will update the email of the user from CDemoOne to "dan@gmail.com"
            //// Remember that after this update occurs, there should be no more "david@gmail.com" on the database!
            //var user = _context.Users.Where(u => u.Email == "david@gmail.com").SingleOrDefault();
            //user.Email = "dan@gmail.com";
            //_context.Users.Update(user);
            //_context.SaveChanges();
        }

        private void UProblemOne()
        {
            //// Update the price of the product you created in CProblemOne to something different using LINQ.
            //double newPrice = 200.00;
            //var product = _context.Products.Where(p => p.Name == "Cooler Master V850 SFX").SingleOrDefault();
            //product.Price = (decimal)newPrice;
            //_context.Products.Update(product);
            //_context.SaveChanges();

        }

        private void UProblemTwo()
        {
            // Change the role of the user we created to "Employee"
            // HINT: You need to delete the existing role relationship and then create a new Userrole object and add it to the Userroles table
            // See the DDemoOne below as an example of removing a role relationship
            //var userrole = _context.UserRoles.Where(ur => ur.User.Email == "dan@gmail.com").SingleOrDefault();
            //_context.UserRoles.Remove(userrole);
            //_context.SaveChanges();

            //var roleId = _context.Roles.Where(r => r.RoleName == "Employee").Select(r => r.Id).SingleOrDefault();
            //var userId = _context.Users.Where(u => u.Email == "dan@gmail.com").Select(u => u.Id).SingleOrDefault();
            //UserRole newUserRole = new UserRole()
            //{
            //    UserId = userId,
            //    RoleId = roleId
            //};
            //_context.UserRoles.Add(newUserRole);
            //_context.SaveChanges();
        }

        // <><> D Actions (Delete) <><>

        private void DDemoOne()
        {
            // Delete the role relationship from the user who has the email "oda@gmail.com" using LINQ.
            //var userrole = _context.UserRoles.Where(ur => ur.User.Email == "oda@gmail.com").SingleOrDefault();
            //_context.UserRoles.Remove(userrole);

            //_context.SaveChanges();

        }

        private void DProblemOne()
        {
            // Delete all of the product relationships to the user with the email "oda@gmail.com" in the ShoppingCart table using LINQ.
            // HINT: Use a Loop
            //var removeItems = _context.ShoppingCartItems.Where(u => u.User.Email == "oda@gmail.com").ToList();
            //foreach (var item in removeItems)
            //{
            //    _context.ShoppingCartItems.Remove(item);
            //};

            //_context.SaveChanges();
        }

        private void DProblemTwo()
        {
            // Delete the user with the email "oda@gmail.com" from the Users table using LINQ.
            //var removeUser = _context.Users.Where(u => u.Email == "oda@gmail.com").SingleOrDefault();
            //_context.Users.Remove(removeUser);
            //_context.SaveChanges();

        }

        // <><><><><><><><> BONUS PROBLEMS <><><><><><><><><>

        private void BonusOne()
        {
            // Prompt the user to enter in an email and password through the console.
            // Take the email and password and check if the there is a person that matches that combination.
            // Print "Signed In!" to the console if they exists and the values match otherwise print "Invalid Email or Password.".
            do
            {
                Console.WriteLine("Enter Email: ");
                string userEmail = Console.ReadLine();

                Console.WriteLine("Enter Password: ");
                string userPass = Console.ReadLine();

                var user = _context.Users.FirstOrDefault(u => u.Email == userEmail && u.Password == userPass);
                if (user != null)
                {
                    Console.WriteLine("Signed In!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Email or Password.");
                }
                Console.WriteLine();


            } while (true);
        }
           

        private void BonusTwo()
        {
            // Write a query that finds the total of every users shopping cart products using LINQ.
            // Display the total of each users shopping cart as well as the total of the totals to the console.
            var shoppingCartTotals = _context.Users
            .Where(user => user.ShoppingCartItems != null && user.ShoppingCartItems.Any())
            .Select(user => new
            {
                  UserEmail = user.Email,
                  Total = user.ShoppingCartItems.Sum(item => item.Product.Price * item.Quantity)
            })
            .ToList();

            Console.WriteLine("Bonus Two");
            foreach (var userTotal in shoppingCartTotals)
            {
                Console.WriteLine($"User's email: {userTotal.UserEmail}");
                Console.WriteLine($"User's shopping cart total: {userTotal.Total:C}");
                Console.WriteLine("-----------");
            }

        }
        // BIG ONE
        private void BonusThree()
        {
            // 1. Create functionality for a user to sign in via the console
            // 2. If the user succesfully signs in, give them a menu where they can perform the following actions within the console:
            // -View the products in their shopping cart
            // -View all products in the Products table
            // -Add a product to the shopping cart (incrementing quantity if that product is already in their shopping cart)
            // -Remove a product from their shopping cart
            // 3. If the user does not successfully sign in
            // -Display "Invalid Email or Password"
            // -Re-prompt the user for credentials

        }

    }
}
