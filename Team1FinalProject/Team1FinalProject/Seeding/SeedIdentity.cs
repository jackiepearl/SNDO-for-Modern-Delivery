using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Team1FinalProject.DAL;
using Team1FinalProject.Models;

//TODO: Change this namespace to match your project
namespace Team1FinalProject.Seeding
{
    //add identity data
    public static class SeedIdentity
    {
        public static async Task AddAdmin(IServiceProvider serviceProvider)
        {
            AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //TODO: Add the needed roles
            //if role doesn't exist, add it
            if (await _roleManager.RoleExistsAsync("Manager") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            if (await _roleManager.RoleExistsAsync("Customer") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (await _roleManager.RoleExistsAsync("Employee") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Employee"));
            }

            //check to see if the manager has been added
            AppUser manager = _db.Users.FirstOrDefault(u => u.Email == "admin@example.com");

            //if manager hasn't been created, then add them
            if (manager == null)
            {
                manager = new AppUser();
                manager.UserName = "admin@example.com";
                manager.Email = "admin@example.com";
                manager.PhoneNumber = "(512)555-5555";
                manager.FirstName = "Admin";
                manager.LastName = "Admin";
                manager.StAddress = "123 Guadalupe St.";
                manager.City = "Austin";
                manager.State = "TX";
                manager.ZipCode = "78705";
                manager.AccountActive = true;
                //TODO: Add any other fields for your app user class here
                //manager.LastName = "Example";
                //manager.DateAdded = DateTime.Today;

                //NOTE: Ask the user manager to create the new user
                //The second parameter for .CreateAsync is the user's password
                var result = await _userManager.CreateAsync(manager, "Abc123!");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }
                _db.SaveChanges();
                manager = _db.Users.FirstOrDefault(u => u.UserName == "admin@example.com");
            }

            //make sure user is in role
            if (await _userManager.IsInRoleAsync(manager, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(manager, "Manager");
            }

            //save changes
            _db.SaveChanges();
        }

        public static async Task AddEmployees(IServiceProvider serviceProvider)
        {
            AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //TODO: Add the needed roles
            //if role doesn't exist, add it
            if (await _roleManager.RoleExistsAsync("Manager") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            if (await _roleManager.RoleExistsAsync("Customer") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (await _roleManager.RoleExistsAsync("Employee") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Employee"));
            }



            AppUser e1 = _db.Users.FirstOrDefault(u => u.Email == "c.baker@bevosbooks.com");
            if (e1 == null)
            {
                e1 = new AppUser();
                e1.UserName = "c.baker@bevosbooks.com";
                e1.Email = "c.baker@bevosbooks.com";
                e1.PhoneNumber = "3395325649";
                e1.FirstName = "Christopher";
                e1.LastName = "Baker";
                e1.StAddress = "1245 Lake Libris Dr.";
                e1.City = "Cedar Park";
                e1.State = "TX";
                e1.ZipCode = "78613";
                
                e1.AccountActive = true;

                var result = await _userManager.CreateAsync(e1, "dewey4");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e1 = _db.Users.FirstOrDefault(u => u.UserName == "c.baker@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e1, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(e1, "Manager");
            }

            AppUser e2 = _db.Users.FirstOrDefault(u => u.Email == "s.barnes@bevosbooks.com");
            if (e2 == null)
            {
                e2 = new AppUser();
                e2.UserName = "s.barnes@bevosbooks.com";
                e2.Email = "s.barnes@bevosbooks.com";
                e2.PhoneNumber = "9636389416";
                e2.FirstName = "Susan";
                e2.LastName = "Barnes";
                e2.StAddress = "888 S. Main";
                e2.City = "Kyle";
                e2.State = "TX";
                e2.ZipCode = "78640";
                
                e2.AccountActive = true;

                var result = await _userManager.CreateAsync(e2, "smitty");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e2 = _db.Users.FirstOrDefault(u => u.UserName == "s.barnes@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e2, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e2, "Employee");
            }

            AppUser e3 = _db.Users.FirstOrDefault(u => u.Email == "h.garcia@bevosbooks.com");
            if (e3 == null)
            {
                e3 = new AppUser();
                e3.UserName = "h.garcia@bevosbooks.com";
                e3.Email = "h.garcia@bevosbooks.com";
                e3.PhoneNumber = "4547135738";
                e3.FirstName = "Hector";
                e3.LastName = "Garcia";
                e3.StAddress = "777 PBR Drive";
                e3.City = "Austin";
                e3.State = "TX";
                e3.ZipCode = "78712";
                
                e3.AccountActive = true;

                var result = await _userManager.CreateAsync(e3, "squirrel");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e3 = _db.Users.FirstOrDefault(u => u.UserName == "h.garcia@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e3, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e3, "Employee");
            }

            AppUser e4 = _db.Users.FirstOrDefault(u => u.Email == "b.ingram@bevosbooks.com");
            if (e4 == null)
            {
                e4 = new AppUser();
                e4.UserName = "b.ingram@bevosbooks.com";
                e4.Email = "b.ingram@bevosbooks.com";
                e4.PhoneNumber = "5817343315";
                e4.FirstName = "Brad";
                e4.LastName = "Ingram";
                e4.StAddress = "6548 La Posada Ct.";
                e4.City = "Austin";
                e4.State = "TX";
                e4.ZipCode = "78705";
                
                e4.AccountActive = true;

                var result = await _userManager.CreateAsync(e4, "changalang");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e4 = _db.Users.FirstOrDefault(u => u.UserName == "b.ingram@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e4, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e4, "Employee");
            }

            AppUser e5 = _db.Users.FirstOrDefault(u => u.Email == "j.jackson@bevosbooks.com");
            if (e5 == null)
            {
                e5 = new AppUser();
                e5.UserName = "j.jackson@bevosbooks.com";
                e5.Email = "j.jackson@bevosbooks.com";
                e5.PhoneNumber = "8241915317";
                e5.FirstName = "Jack";
                e5.LastName = "Jackson";
                e5.StAddress = "222 Main";
                e5.City = "Austin";
                e5.State = "TX";
                e5.ZipCode = "78760";
                
                e5.AccountActive = true;

                var result = await _userManager.CreateAsync(e5, "rhythm");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e5 = _db.Users.FirstOrDefault(u => u.UserName == "j.jackson@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e5, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e5, "Employee");
            }

            AppUser e6 = _db.Users.FirstOrDefault(u => u.Email == "t.jacobs@bevosbooks.com");
            if (e6 == null)
            {
                e6 = new AppUser();
                e6.UserName = "t.jacobs@bevosbooks.com";
                e6.Email = "t.jacobs@bevosbooks.com";
                e6.PhoneNumber = "2477822475";
                e6.FirstName = "Todd";
                e6.LastName = "Jacobs";
                e6.StAddress = "4564 Elm St.";
                e6.City = "Georgetown";
                e6.State = "TX";
                e6.ZipCode = "78628";
               
                e6.AccountActive = true;

                var result = await _userManager.CreateAsync(e6, "approval");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e6 = _db.Users.FirstOrDefault(u => u.UserName == "t.jacobs@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e6, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e6, "Employee");
            }

            AppUser e7 = _db.Users.FirstOrDefault(u => u.Email == "l.jones@bevosbooks.com");
            if (e7 == null)
            {
                e7 = new AppUser();
                e7.UserName = "l.jones@bevosbooks.com";
                e7.Email = "l.jones@bevosbooks.com";
                e7.PhoneNumber = "4764966462";
                e7.FirstName = "Lester";
                e7.LastName = "Jones";
                e7.StAddress = "999 LeBlat";
                e7.City = "Austin";
                e7.State = "TX";
                e7.ZipCode = "78747";
                
                e7.AccountActive = true;

                var result = await _userManager.CreateAsync(e7, "society");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e7 = _db.Users.FirstOrDefault(u => u.UserName == "l.jones@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e7, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e7, "Employee");
            }

            AppUser e8 = _db.Users.FirstOrDefault(u => u.Email == "b.larson@bevosbooks.com");
            if (e8 == null)
            {
                e8 = new AppUser();
                e8.UserName = "b.larson@bevosbooks.com";
                e8.Email = "b.larson@bevosbooks.com";
                e8.PhoneNumber = "3355258855";
                e8.FirstName = "Bill";
                e8.LastName = "Larson";
                e8.StAddress = "1212 N. First Ave";
                e8.City = "Round Rock";
                e8.State = "TX";
                e8.ZipCode = "78665";
               
                e8.AccountActive = true;

                var result = await _userManager.CreateAsync(e8, "tanman");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e8 = _db.Users.FirstOrDefault(u => u.UserName == "b.larson@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e8, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e8, "Employee");
            }

            AppUser e9 = _db.Users.FirstOrDefault(u => u.Email == "v.lawrence@bevosbooks.com");
            if (e9 == null)
            {
                e9 = new AppUser();
                e9.UserName = "v.lawrence@bevosbooks.com";
                e9.Email = "v.lawrence@bevosbooks.com";
                e9.PhoneNumber = "7511273054";
                e9.FirstName = "Victoria";
                e9.LastName = "Lawrence";
                e9.StAddress = "6639 Bookworm Ln.";
                e9.City = "Austin";
                e9.State = "TX";
                e9.ZipCode = "78712";
               
                e9.AccountActive = true;

                var result = await _userManager.CreateAsync(e9, "longhorns");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e9 = _db.Users.FirstOrDefault(u => u.UserName == "v.lawrence@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e9, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e9, "Employee");
            }

            AppUser e10 = _db.Users.FirstOrDefault(u => u.Email == "m.lopez@bevosbooks.com");
            if (e10 == null)
            {
                e10 = new AppUser();
                e10.UserName = "m.lopez@bevosbooks.com";
                e10.Email = "m.lopez@bevosbooks.com";
                e10.PhoneNumber = "7477907070";
                e10.FirstName = "Marshall";
                e10.LastName = "Lopez";
                e10.StAddress = "90 SW North St";
                e10.City = "Austin";
                e10.State = "TX";
                e10.ZipCode = "78729";
                
                e10.AccountActive = true;

                var result = await _userManager.CreateAsync(e10, "swansong");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e10 = _db.Users.FirstOrDefault(u => u.UserName == "m.lopez@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e10, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e10, "Employee");
            }

            AppUser e11 = _db.Users.FirstOrDefault(u => u.Email == "j.macleod@bevosbooks.com");
            if (e11 == null)
            {
                e11 = new AppUser();
                e11.UserName = "j.macleod@bevosbooks.com";
                e11.Email = "j.macleod@bevosbooks.com";
                e11.PhoneNumber = "2621216845";
                e11.FirstName = "Jennifer";
                e11.LastName = "MacLeod";
                e11.StAddress = "2504 Far West Blvd.";
                e11.City = "Austin";
                e11.State = "TX";
                e11.ZipCode = "78705";
                
                e11.AccountActive = true;

                var result = await _userManager.CreateAsync(e11, "fungus");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e11 = _db.Users.FirstOrDefault(u => u.UserName == "j.macleod@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e11, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e11, "Employee");
            }

            AppUser e12 = _db.Users.FirstOrDefault(u => u.Email == "e.markham@bevosbooks.com");
            if (e12 == null)
            {
                e12 = new AppUser();
                e12.UserName = "e.markham@bevosbooks.com";
                e12.Email = "e.markham@bevosbooks.com";
                e12.PhoneNumber = "5028075807";
                e12.FirstName = "Elizabeth";
                e12.LastName = "Markham";
                e12.StAddress = "7861 Chevy Chase";
                e12.City = "Austin";
                e12.State = "TX";
                e12.ZipCode = "78785";
                
                e12.AccountActive = true;

                var result = await _userManager.CreateAsync(e12, "median");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e12 = _db.Users.FirstOrDefault(u => u.UserName == "e.markham@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e12, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e12, "Employee");
            }

            AppUser e13 = _db.Users.FirstOrDefault(u => u.Email == "g.martinez@bevosbooks.com");
            if (e13 == null)
            {
                e13 = new AppUser();
                e13.UserName = "g.martinez@bevosbooks.com";
                e13.Email = "g.martinez@bevosbooks.com";
                e13.PhoneNumber = "1994708542";
                e13.FirstName = "Gregory";
                e13.LastName = "Martinez";
                e13.StAddress = "8295 Sunset Blvd.";
                e13.City = "Austin";
                e13.State = "TX";
                e13.ZipCode = "78712";
                
                e13.AccountActive = true;

                var result = await _userManager.CreateAsync(e13, "decorate");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e13 = _db.Users.FirstOrDefault(u => u.UserName == "g.martinez@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e13, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e13, "Employee");
            }

            AppUser e14 = _db.Users.FirstOrDefault(u => u.Email == "j.mason@bevosbooks.com");
            if (e14 == null)
            {
                e14 = new AppUser();
                e14.UserName = "j.mason@bevosbooks.com";
                e14.Email = "j.mason@bevosbooks.com";
                e14.PhoneNumber = "1748136441";
                e14.FirstName = "Jack";
                e14.LastName = "Mason";
                e14.StAddress = "444 45th St";
                e14.City = "Austin";
                e14.State = "TX";
                e14.ZipCode = "78701";
                
                e14.AccountActive = true;

                var result = await _userManager.CreateAsync(e14, "rankmary");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e14 = _db.Users.FirstOrDefault(u => u.UserName == "j.mason@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e14, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e14, "Employee");
            }

            AppUser e15 = _db.Users.FirstOrDefault(u => u.Email == "c.miller@bevosbooks.com");
            if (e15 == null)
            {
                e15 = new AppUser();
                e15.UserName = "c.miller@bevosbooks.com";
                e15.Email = "c.miller@bevosbooks.com";
                e15.PhoneNumber = "8999319585";
                e15.FirstName = "Charles";
                e15.LastName = "Miller";
                e15.StAddress = "8962 Main St.";
                e15.City = "Austin";
                e15.State = "TX";
                e15.ZipCode = "78709";
                
                e15.AccountActive = true;

                var result = await _userManager.CreateAsync(e15, "kindly");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e15 = _db.Users.FirstOrDefault(u => u.UserName == "c.miller@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e15, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e15, "Employee");
            }

            AppUser e16 = _db.Users.FirstOrDefault(u => u.Email == "m.nguyen@bevosbooks.com");
            if (e16 == null)
            {
                e16 = new AppUser();
                e16.UserName = "m.nguyen@bevosbooks.com";
                e16.Email = "m.nguyen@bevosbooks.com";
                e16.PhoneNumber = "8716746381";
                e16.FirstName = "Mary";
                e16.LastName = "Nguyen";
                e16.StAddress = "465 N. Bear Cub";
                e16.City = "Austin";
                e16.State = "TX";
                e16.ZipCode = "78734";
                
                e16.AccountActive = true;

                var result = await _userManager.CreateAsync(e16, "ricearoni");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e16 = _db.Users.FirstOrDefault(u => u.UserName == "m.nguyen@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e16, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e16, "Employee");
            }

            AppUser e17 = _db.Users.FirstOrDefault(u => u.Email == "s.rankin@bevosbooks.com");
            if (e17 == null)
            {
                e17 = new AppUser();
                e17.UserName = "s.rankin@bevosbooks.com";
                e17.Email = "s.rankin@bevosbooks.com";
                e17.PhoneNumber = "5239029525";
                e17.FirstName = "Suzie";
                e17.LastName = "Rankin";
                e17.StAddress = "23 Dewey Road";
                e17.City = "Austin";
                e17.State = "TX";
                e17.ZipCode = "78712";
                
                e17.AccountActive = true;

                var result = await _userManager.CreateAsync(e17, "walkamile");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e17 = _db.Users.FirstOrDefault(u => u.UserName == "s.rankin@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e17, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e17, "Employee");
            }

            AppUser e18 = _db.Users.FirstOrDefault(u => u.Email == "m.rhodes@bevosbooks.com");
            if (e18 == null)
            {
                e18 = new AppUser();
                e18.UserName = "m.rhodes@bevosbooks.com";
                e18.Email = "m.rhodes@bevosbooks.com";
                e18.PhoneNumber = "1232139514";
                e18.FirstName = "Megan";
                e18.LastName = "Rhodes";
                e18.StAddress = "4587 Enfield Rd.";
                e18.City = "Austin";
                e18.State = "TX";
                e18.ZipCode = "78729";
                
                e18.AccountActive = true;

                var result = await _userManager.CreateAsync(e18, "ingram45");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e18 = _db.Users.FirstOrDefault(u => u.UserName == "m.rhodes@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e18, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e18, "Employee");
            }

            AppUser e19 = _db.Users.FirstOrDefault(u => u.Email == "e.rice@bevosbooks.com");
            if (e19 == null)
            {
                e19 = new AppUser();
                e19.UserName = "e.rice@bevosbooks.com";
                e19.Email = "e.rice@bevosbooks.com";
                e19.PhoneNumber = "2706602803";
                e19.FirstName = "Eryn";
                e19.LastName = "Rice";
                e19.StAddress = "3405 Rio Grande";
                e19.City = "Austin";
                e19.State = "TX";
                e19.ZipCode = "78746";
                
                e19.AccountActive = true;

                var result = await _userManager.CreateAsync(e19, "arched");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e19 = _db.Users.FirstOrDefault(u => u.UserName == "e.rice@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e19, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(e19, "Manager");
            }

            AppUser e20 = _db.Users.FirstOrDefault(u => u.Email == "a.rogers@bevosbooks.com");
            if (e20 == null)
            {
                e20 = new AppUser();
                e20.UserName = "a.rogers@bevosbooks.com";
                e20.Email = "a.rogers@bevosbooks.com";
                e20.PhoneNumber = "4139645586";
                e20.FirstName = "Allen";
                e20.LastName = "Rogers";
                e20.StAddress = "4965 Oak Hill";
                e20.City = "Austin";
                e20.State = "TX";
                e20.ZipCode = "78705";
                
                e20.AccountActive = true;

                var result = await _userManager.CreateAsync(e20, "lottery");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e20 = _db.Users.FirstOrDefault(u => u.UserName == "a.rogers@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e20, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(e20, "Manager");
            }

            AppUser e21 = _db.Users.FirstOrDefault(u => u.Email == "s.saunders@bevosbooks.com");
            if (e21 == null)
            {
                e21 = new AppUser();
                e21.UserName = "s.saunders@bevosbooks.com";
                e21.Email = "s.saunders@bevosbooks.com";
                e21.PhoneNumber = "9036349587";
                e21.FirstName = "Sarah";
                e21.LastName = "Saunders";
                e21.StAddress = "332 Avenue C";
                e21.City = "Austin";
                e21.State = "TX";
                e21.ZipCode = "78733";
                
                e21.AccountActive = true;

                var result = await _userManager.CreateAsync(e21, "nostalgic");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e21 = _db.Users.FirstOrDefault(u => u.UserName == "s.saunders@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e21, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e21, "Employee");
            }

            AppUser e22 = _db.Users.FirstOrDefault(u => u.Email == "w.sewell@bevosbooks.com");
            if (e22 == null)
            {
                e22 = new AppUser();
                e22.UserName = "w.sewell@bevosbooks.com";
                e22.Email = "w.sewell@bevosbooks.com";
                e22.PhoneNumber = "7224308314";
                e22.FirstName = "William";
                e22.LastName = "Sewell";
                e22.StAddress = "2365 51st St.";
                e22.City = "Austin";
                e22.State = "TX";
                e22.ZipCode = "78755";
                
                e22.AccountActive = true;

                var result = await _userManager.CreateAsync(e22, "offbeat");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e22 = _db.Users.FirstOrDefault(u => u.UserName == "w.sewell@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e22, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(e22, "Manager");
            }

            AppUser e23 = _db.Users.FirstOrDefault(u => u.Email == "m.sheffield@bevosbooks.com");
            if (e23 == null)
            {
                e23 = new AppUser();
                e23.UserName = "m.sheffield@bevosbooks.com";
                e23.Email = "m.sheffield@bevosbooks.com";
                e23.PhoneNumber = "9349192978";
                e23.FirstName = "Martin";
                e23.LastName = "Sheffield";
                e23.StAddress = "3886 Avenue A";
                e23.City = "San Marcos";
                e23.State = "TX";
                e23.ZipCode = "78666";
                
                e23.AccountActive = true;

                var result = await _userManager.CreateAsync(e23, "evanescent");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e23 = _db.Users.FirstOrDefault(u => u.UserName == "m.sheffield@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e23, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e23, "Employee");
            }

            AppUser e24 = _db.Users.FirstOrDefault(u => u.Email == "c.silva@bevosbooks.com");
            if (e24 == null)
            {
                e24 = new AppUser();
                e24.UserName = "c.silva@bevosbooks.com";
                e24.Email = "c.silva@bevosbooks.com";
                e24.PhoneNumber = "4874328170";
                e24.FirstName = "Cindy";
                e24.LastName = "Silva";
                e24.StAddress = "900 4th St";
                e24.City = "Austin";
                e24.State = "TX";
                e24.ZipCode = "78758";
                
                e24.AccountActive = true;

                var result = await _userManager.CreateAsync(e24, "stewboy");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e24 = _db.Users.FirstOrDefault(u => u.UserName == "c.silva@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e24, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e24, "Employee");
            }

            AppUser e25 = _db.Users.FirstOrDefault(u => u.Email == "e.stuart@bevosbooks.com");
            if (e25 == null)
            {
                e25 = new AppUser();
                e25.UserName = "e.stuart@bevosbooks.com";
                e25.Email = "e.stuart@bevosbooks.com";
                e25.PhoneNumber = "1967846827";
                e25.FirstName = "Eric";
                e25.LastName = "Stuart";
                e25.StAddress = "5576 Toro Ring";
                e25.City = "Austin";
                e25.State = "TX";
                e25.ZipCode = "78758";
                
                e25.AccountActive = true;

                var result = await _userManager.CreateAsync(e25, "instrument");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e25 = _db.Users.FirstOrDefault(u => u.UserName == "e.stuart@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e25, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e25, "Employee");
            }

            AppUser e26 = _db.Users.FirstOrDefault(u => u.Email == "j.tanner@bevosbooks.com");
            if (e26 == null)
            {
                e26 = new AppUser();
                e26.UserName = "j.tanner@bevosbooks.com";
                e26.Email = "j.tanner@bevosbooks.com";
                e26.PhoneNumber = "5923026779";
                e26.FirstName = "Jeremy";
                e26.LastName = "Tanner";
                e26.StAddress = "4347 Almstead";
                e26.City = "Austin";
                e26.State = "TX";
                e26.ZipCode = "78712";
                
                e26.AccountActive = true;

                var result = await _userManager.CreateAsync(e26, "hecktour");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e26 = _db.Users.FirstOrDefault(u => u.UserName == "j.tanner@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e26, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e26, "Employee");
            }

            AppUser e27 = _db.Users.FirstOrDefault(u => u.Email == "a.taylor@bevosbooks.com");
            if (e27 == null)
            {
                e27 = new AppUser();
                e27.UserName = "a.taylor@bevosbooks.com";
                e27.Email = "a.taylor@bevosbooks.com";
                e27.PhoneNumber = "7246195827";
                e27.FirstName = "Allison";
                e27.LastName = "Taylor";
                e27.StAddress = "467 Nueces St.";
                e27.City = "Austin";
                e27.State = "TX";
                e27.ZipCode = "78727";
                
                e27.AccountActive = true;

                var result = await _userManager.CreateAsync(e27, "countryrhodes");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e27 = _db.Users.FirstOrDefault(u => u.UserName == "a.taylor@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e27, "Employee") == false)
            {
                await _userManager.AddToRoleAsync(e27, "Employee");
            }

            AppUser e28 = _db.Users.FirstOrDefault(u => u.Email == "r.taylor@bevosbooks.com");
            if (e28 == null)
            {
                e28 = new AppUser();
                e28.UserName = "r.taylor@bevosbooks.com";
                e28.Email = "r.taylor@bevosbooks.com";
                e28.PhoneNumber = "9071236087";
                e28.FirstName = "Rachel";
                e28.LastName = "Taylor";
                e28.StAddress = "345 Longview Dr.";
                e28.City = "Austin";
                e28.State = "TX";
                e28.ZipCode = "78746";
                
                e28.AccountActive = true;

                var result = await _userManager.CreateAsync(e28, "landus");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                e28 = _db.Users.FirstOrDefault(u => u.UserName == "r.taylor@bevosbooks.com");
            }
            if (await _userManager.IsInRoleAsync(e28, "Manager") == false)
            {
                await _userManager.AddToRoleAsync(e28, "Manager");
            }


            //save changes
            _db.SaveChanges();
        }

        public static async Task AddCustomers(IServiceProvider serviceProvider)
        {
            AppDbContext _db = serviceProvider.GetRequiredService<AppDbContext>();
            UserManager<AppUser> _userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            RoleManager<IdentityRole> _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //TODO: Add the needed roles
            //if role doesn't exist, add it
            if (await _roleManager.RoleExistsAsync("Manager") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            if (await _roleManager.RoleExistsAsync("Customer") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            if (await _roleManager.RoleExistsAsync("Employee") == false)
            {
                await _roleManager.CreateAsync(new IdentityRole("Employee"));
            }

            AppUser c1 = _db.Users.FirstOrDefault(u => u.Email == "cbaker@example.com");
            if (c1 == null)
            {
                c1 = new AppUser();
                c1.UserName = "cbaker@example.com";
                c1.Email = "cbaker@example.com";
                c1.PhoneNumber = "5725458641";
                c1.FirstName = "Christopher";
                c1.LastName = "Baker";
                c1.StAddress = "1898 Schurz Alley";
                c1.City = "Austin";
                c1.State = "TX";
                c1.ZipCode = "78705";
                
                c1.AccountActive = true;

                var result = await _userManager.CreateAsync(c1, "bookworm");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c1 = _db.Users.FirstOrDefault(u => u.UserName == "cbaker@example.com");
            }
            if (await _userManager.IsInRoleAsync(c1, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c1, "Customer");
            }

            AppUser c2 = _db.Users.FirstOrDefault(u => u.Email == "banker@longhorn.net");
            if (c2 == null)
            {
                c2 = new AppUser();
                c2.UserName = "banker@longhorn.net";
                c2.Email = "banker@longhorn.net";
                c2.PhoneNumber = "9867048435";
                c2.FirstName = "Michelle";
                c2.LastName = "Banks";
                c2.StAddress = "97 Elmside Pass";
                c2.City = "Austin";
                c2.State = "TX";
                c2.ZipCode = "78712";
                
                c2.AccountActive = true;

                var result = await _userManager.CreateAsync(c2, "potato");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c2 = _db.Users.FirstOrDefault(u => u.UserName == "banker@longhorn.net");
            }
            if (await _userManager.IsInRoleAsync(c2, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c2, "Customer");
            }

            AppUser c3 = _db.Users.FirstOrDefault(u => u.Email == "franco@example.com");
            if (c3 == null)
            {
                c3 = new AppUser();
                c3.UserName = "franco@example.com";
                c3.Email = "franco@example.com";
                c3.PhoneNumber = "6836109514";
                c3.FirstName = "Franco";
                c3.LastName = "Broccolo";
                c3.StAddress = "88 Crowley Circle";
                c3.City = "Austin";
                c3.State = "TX";
                c3.ZipCode = "78786";
                
                c3.AccountActive = true;

                var result = await _userManager.CreateAsync(c3, "painting");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c3 = _db.Users.FirstOrDefault(u => u.UserName == "franco@example.com");
            }
            if (await _userManager.IsInRoleAsync(c3, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c3, "Customer");
            }

            AppUser c4 = _db.Users.FirstOrDefault(u => u.Email == "wchang@example.com");
            if (c4 == null)
            {
                c4 = new AppUser();
                c4.UserName = "wchang@example.com";
                c4.Email = "wchang@example.com";
                c4.PhoneNumber = "7070911071";
                c4.FirstName = "Wendy";
                c4.LastName = "Chang";
                c4.StAddress = "56560 Sage Junction";
                c4.City = "Eagle Pass";
                c4.State = "TX";
                c4.ZipCode = "78852";
                
                c4.AccountActive = true;

                var result = await _userManager.CreateAsync(c4, "texas1");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c4 = _db.Users.FirstOrDefault(u => u.UserName == "wchang@example.com");
            }
            if (await _userManager.IsInRoleAsync(c4, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c4, "Customer");
            }

            AppUser c5 = _db.Users.FirstOrDefault(u => u.Email == "limchou@gogle.com");
            if (c5 == null)
            {
                c5 = new AppUser();
                c5.UserName = "limchou@gogle.com";
                c5.Email = "limchou@gogle.com";
                c5.PhoneNumber = "1488907687";
                c5.FirstName = "Lim";
                c5.LastName = "Chou";
                c5.StAddress = "60 Lunder Point";
                c5.City = "Austin";
                c5.State = "TX";
                c5.ZipCode = "78729";
                
                c5.AccountActive = true;

                var result = await _userManager.CreateAsync(c5, "Anchorage");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c5 = _db.Users.FirstOrDefault(u => u.UserName == "limchou@gogle.com");
            }
            if (await _userManager.IsInRoleAsync(c5, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c5, "Customer");
            }

            AppUser c6 = _db.Users.FirstOrDefault(u => u.Email == "shdixon@aoll.com");
            if (c6 == null)
            {
                c6 = new AppUser();
                c6.UserName = "shdixon@aoll.com";
                c6.Email = "shdixon@aoll.com";
                c6.PhoneNumber = "6899701824";
                c6.FirstName = "Shan";
                c6.LastName = "Dixon";
                c6.StAddress = "9448 Pleasure Avenue";
                c6.City = "Georgetown";
                c6.State = "TX";
                c6.ZipCode = "78628";
                
                c6.AccountActive = true;

                var result = await _userManager.CreateAsync(c6, "aggies");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c6 = _db.Users.FirstOrDefault(u => u.UserName == "shdixon@aoll.com");
            }
            if (await _userManager.IsInRoleAsync(c6, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c6, "Customer");
            }

            AppUser c7 = _db.Users.FirstOrDefault(u => u.Email == "j.b.evans@aheca.org");
            if (c7 == null)
            {
                c7 = new AppUser();
                c7.UserName = "j.b.evans@aheca.org";
                c7.Email = "j.b.evans@aheca.org";
                c7.PhoneNumber = "9986825917";
                c7.FirstName = "Jim Bob";
                c7.LastName = "Evans";
                c7.StAddress = "51 Emmet Parkway";
                c7.City = "Austin";
                c7.State = "TX";
                c7.ZipCode = "78705";
                
                c7.AccountActive = true;

                var result = await _userManager.CreateAsync(c7, "hampton1");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c7 = _db.Users.FirstOrDefault(u => u.UserName == "j.b.evans@aheca.org");
            }
            if (await _userManager.IsInRoleAsync(c7, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c7, "Customer");
            }

            AppUser c8 = _db.Users.FirstOrDefault(u => u.Email == "feeley@penguin.org");
            if (c8 == null)
            {
                c8 = new AppUser();
                c8.UserName = "feeley@penguin.org";
                c8.Email = "feeley@penguin.org";
                c8.PhoneNumber = "3464121966";
                c8.FirstName = "Lou Ann";
                c8.LastName = "Feeley";
                c8.StAddress = "65 Darwin Crossing";
                c8.City = "Austin";
                c8.State = "TX";
                c8.ZipCode = "78704";
                
                c8.AccountActive = true;

                var result = await _userManager.CreateAsync(c8, "longhorns");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c8 = _db.Users.FirstOrDefault(u => u.UserName == "feeley@penguin.org");
            }
            if (await _userManager.IsInRoleAsync(c8, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c8, "Customer");
            }

            AppUser c9 = _db.Users.FirstOrDefault(u => u.Email == "tfreeley@minnetonka.ci.us");
            if (c9 == null)
            {
                c9 = new AppUser();
                c9.UserName = "tfreeley@minnetonka.ci.us";
                c9.Email = "tfreeley@minnetonka.ci.us";
                c9.PhoneNumber = "6581357270";
                c9.FirstName = "Tesa";
                c9.LastName = "Freeley";
                c9.StAddress = "7352 Loftsgordon Court";
                c9.City = "College Station";
                c9.State = "TX";
                c9.ZipCode = "77840";
                
                c9.AccountActive = true;

                var result = await _userManager.CreateAsync(c9, "mustangs");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c9 = _db.Users.FirstOrDefault(u => u.UserName == "tfreeley@minnetonka.ci.us");
            }
            if (await _userManager.IsInRoleAsync(c9, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c9, "Customer");
            }

            AppUser c10 = _db.Users.FirstOrDefault(u => u.Email == "mgarcia@gogle.com");
            if (c10 == null)
            {
                c10 = new AppUser();
                c10.UserName = "mgarcia@gogle.com";
                c10.Email = "mgarcia@gogle.com";
                c10.PhoneNumber = "3767347949";
                c10.FirstName = "Margaret";
                c10.LastName = "Garcia";
                c10.StAddress = "7 International Road";
                c10.City = "Austin";
                c10.State = "TX";
                c10.ZipCode = "78756";
                
                c10.AccountActive = true;

                var result = await _userManager.CreateAsync(c10, "onetime");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c10 = _db.Users.FirstOrDefault(u => u.UserName == "mgarcia@gogle.com");
            }
            if (await _userManager.IsInRoleAsync(c10, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c10, "Customer");
            }

            AppUser c11 = _db.Users.FirstOrDefault(u => u.Email == "chaley@thug.com");
            if (c11 == null)
            {
                c11 = new AppUser();
                c11.UserName = "chaley@thug.com";
                c11.Email = "chaley@thug.com";
                c11.PhoneNumber = "2198604221";
                c11.FirstName = "Charles";
                c11.LastName = "Haley";
                c11.StAddress = "8 Warrior Trail";
                c11.City = "Austin";
                c11.State = "TX";
                c11.ZipCode = "78746";
                
                c11.AccountActive = true;

                var result = await _userManager.CreateAsync(c11, "pepperoni");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c11 = _db.Users.FirstOrDefault(u => u.UserName == "chaley@thug.com");
            }
            if (await _userManager.IsInRoleAsync(c11, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c11, "Customer");
            }

            AppUser c12 = _db.Users.FirstOrDefault(u => u.Email == "jeffh@sonic.com");
            if (c12 == null)
            {
                c12 = new AppUser();
                c12.UserName = "jeffh@sonic.com";
                c12.Email = "jeffh@sonic.com";
                c12.PhoneNumber = "1222185888";
                c12.FirstName = "Jeffrey";
                c12.LastName = "Hampton";
                c12.StAddress = "9107 Lighthouse Bay Road";
                c12.City = "Austin";
                c12.State = "TX";
                c12.ZipCode = "78756";
                
                c12.AccountActive = true;

                var result = await _userManager.CreateAsync(c12, "raiders");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c12 = _db.Users.FirstOrDefault(u => u.UserName == "jeffh@sonic.com");
            }
            if (await _userManager.IsInRoleAsync(c12, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c12, "Customer");
            }

            AppUser c13 = _db.Users.FirstOrDefault(u => u.Email == "wjhearniii@umich.org");
            if (c13 == null)
            {
                c13 = new AppUser();
                c13.UserName = "wjhearniii@umich.org";
                c13.Email = "wjhearniii@umich.org";
                c13.PhoneNumber = "5123071976";
                c13.FirstName = "John";
                c13.LastName = "Hearn";
                c13.StAddress = "59784 Pierstorff Center";
                c13.City = "Liberty";
                c13.State = "TX";
                c13.ZipCode = "77575";
                
                c13.AccountActive = true;

                var result = await _userManager.CreateAsync(c13, "jhearn22");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c13 = _db.Users.FirstOrDefault(u => u.UserName == "wjhearniii@umich.org");
            }
            if (await _userManager.IsInRoleAsync(c13, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c13, "Customer");
            }

            AppUser c14 = _db.Users.FirstOrDefault(u => u.Email == "ahick@yaho.com");
            if (c14 == null)
            {
                c14 = new AppUser();
                c14.UserName = "ahick@yaho.com";
                c14.Email = "ahick@yaho.com";
                c14.PhoneNumber = "1211949601";
                c14.FirstName = "Anthony";
                c14.LastName = "Hicks";
                c14.StAddress = "932 Monica Way";
                c14.City = "San Antonio";
                c14.State = "TX";
                c14.ZipCode = "78203";
                
                c14.AccountActive = true;

                var result = await _userManager.CreateAsync(c14, "hickhickup");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c14 = _db.Users.FirstOrDefault(u => u.UserName == "ahick@yaho.com");
            }
            if (await _userManager.IsInRoleAsync(c14, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c14, "Customer");
            }

            AppUser c15 = _db.Users.FirstOrDefault(u => u.Email == "ingram@jack.com");
            if (c15 == null)
            {
                c15 = new AppUser();
                c15.UserName = "ingram@jack.com";
                c15.Email = "ingram@jack.com";
                c15.PhoneNumber = "1372121569";
                c15.FirstName = "Brad";
                c15.LastName = "Ingram";
                c15.StAddress = "4 Lukken Court";
                c15.City = "New Braunfels";
                c15.State = "TX";
                c15.ZipCode = "78132";
                
                c15.AccountActive = true;

                var result = await _userManager.CreateAsync(c15, "ingram2015");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c15 = _db.Users.FirstOrDefault(u => u.UserName == "ingram@jack.com");
            }
            if (await _userManager.IsInRoleAsync(c15, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c15, "Customer");
            }

            AppUser c16 = _db.Users.FirstOrDefault(u => u.Email == "toddj@yourmom.com");
            if (c16 == null)
            {
                c16 = new AppUser();
                c16.UserName = "toddj@yourmom.com";
                c16.Email = "toddj@yourmom.com";
                c16.PhoneNumber = "8543163836";
                c16.FirstName = "Todd";
                c16.LastName = "Jacobs";
                c16.StAddress = "7 Susan Junction";
                c16.City = "New York";
                c16.State = "NY";
                c16.ZipCode = "10101";
                
                c16.AccountActive = true;

                var result = await _userManager.CreateAsync(c16, "toddy25");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c16 = _db.Users.FirstOrDefault(u => u.UserName == "toddj@yourmom.com");
            }
            if (await _userManager.IsInRoleAsync(c16, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c16, "Customer");
            }

            AppUser c17 = _db.Users.FirstOrDefault(u => u.Email == "thequeen@aska.net");
            if (c17 == null)
            {
                c17 = new AppUser();
                c17.UserName = "thequeen@aska.net";
                c17.Email = "thequeen@aska.net";
                c17.PhoneNumber = "3214163359";
                c17.FirstName = "Victoria";
                c17.LastName = "Lawrence";
                c17.StAddress = "669 Oak Junction";
                c17.City = "Lockhart";
                c17.State = "TX";
                c17.ZipCode = "78644";
                
                c17.AccountActive = true;

                var result = await _userManager.CreateAsync(c17, "something");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c17 = _db.Users.FirstOrDefault(u => u.UserName == "thequeen@aska.net");
            }
            if (await _userManager.IsInRoleAsync(c17, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c17, "Customer");
            }

            AppUser c18 = _db.Users.FirstOrDefault(u => u.Email == "linebacker@gogle.com");
            if (c18 == null)
            {
                c18 = new AppUser();
                c18.UserName = "linebacker@gogle.com";
                c18.Email = "linebacker@gogle.com";
                c18.PhoneNumber = "2505265350";
                c18.FirstName = "Erik";
                c18.LastName = "Lineback";
                c18.StAddress = "099 Luster Point";
                c18.City = "Kingwood";
                c18.State = "TX";
                c18.ZipCode = "77325";
                
                c18.AccountActive = true;

                var result = await _userManager.CreateAsync(c18, "Password1");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c18 = _db.Users.FirstOrDefault(u => u.UserName == "linebacker@gogle.com");
            }
            if (await _userManager.IsInRoleAsync(c18, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c18, "Customer");
            }

            AppUser c19 = _db.Users.FirstOrDefault(u => u.Email == "elowe@netscare.net");
            if (c19 == null)
            {
                c19 = new AppUser();
                c19.UserName = "elowe@netscare.net";
                c19.Email = "elowe@netscare.net";
                c19.PhoneNumber = "4070619503";
                c19.FirstName = "Ernest";
                c19.LastName = "Lowe";
                c19.StAddress = "35473 Hansons Hill";
                c19.City = "Beverly Hills";
                c19.State = "CA";
                c19.ZipCode = "90210";
                
                c19.AccountActive = true;

                var result = await _userManager.CreateAsync(c19, "aclfest2017");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c19 = _db.Users.FirstOrDefault(u => u.UserName == "elowe@netscare.net");
            }
            if (await _userManager.IsInRoleAsync(c19, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c19, "Customer");
            }

            AppUser c20 = _db.Users.FirstOrDefault(u => u.Email == "cluce@gogle.com");
            if (c20 == null)
            {
                c20 = new AppUser();
                c20.UserName = "cluce@gogle.com";
                c20.Email = "cluce@gogle.com";
                c20.PhoneNumber = "7358436110";
                c20.FirstName = "Chuck";
                c20.LastName = "Luce";
                c20.StAddress = "4 Emmet Junction";
                c20.City = "Navasota";
                c20.State = "TX";
                c20.ZipCode = "77868";
                
                c20.AccountActive = true;

                var result = await _userManager.CreateAsync(c20, "nothinggood");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c20 = _db.Users.FirstOrDefault(u => u.UserName == "cluce@gogle.com");
            }
            if (await _userManager.IsInRoleAsync(c20, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c20, "Customer");
            }

            AppUser c21 = _db.Users.FirstOrDefault(u => u.Email == "mackcloud@george.com");
            if (c21 == null)
            {
                c21 = new AppUser();
                c21.UserName = "mackcloud@george.com";
                c21.Email = "mackcloud@george.com";
                c21.PhoneNumber = "7240178229";
                c21.FirstName = "Jennifer";
                c21.LastName = "MacLeod";
                c21.StAddress = "3 Orin Road";
                c21.City = "Austin";
                c21.State = "TX";
                c21.ZipCode = "78712";
                
                c21.AccountActive = true;

                var result = await _userManager.CreateAsync(c21, "whatever");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c21 = _db.Users.FirstOrDefault(u => u.UserName == "mackcloud@george.com");
            }
            if (await _userManager.IsInRoleAsync(c21, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c21, "Customer");
            }

            AppUser c22 = _db.Users.FirstOrDefault(u => u.Email == "cmartin@beets.com");
            if (c22 == null)
            {
                c22 = new AppUser();
                c22.UserName = "cmartin@beets.com";
                c22.Email = "cmartin@beets.com";
                c22.PhoneNumber = "2495200223";
                c22.FirstName = "Elizabeth";
                c22.LastName = "Markham";
                c22.StAddress = "8171 Commercial Crossing";
                c22.City = "Austin";
                c22.State = "TX";
                c22.ZipCode = "78712";
               
                c22.AccountActive = true;

                var result = await _userManager.CreateAsync(c22, "snowsnow");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c22 = _db.Users.FirstOrDefault(u => u.UserName == "cmartin@beets.com");
            }
            if (await _userManager.IsInRoleAsync(c22, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c22, "Customer");
            }

            AppUser c23 = _db.Users.FirstOrDefault(u => u.Email == "clarence@yoho.com");
            if (c23 == null)
            {
                c23 = new AppUser();
                c23.UserName = "clarence@yoho.com";
                c23.Email = "clarence@yoho.com";
                c23.PhoneNumber = "4086179161";
                c23.FirstName = "Clarence";
                c23.LastName = "Martin";
                c23.StAddress = "96 Anthes Place";
                c23.City = "Schenectady";
                c23.State = "NY";
                c23.ZipCode = "12345";
                
                c23.AccountActive = true;

                var result = await _userManager.CreateAsync(c23, "whocares");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c23 = _db.Users.FirstOrDefault(u => u.UserName == "clarence@yoho.com");
            }
            if (await _userManager.IsInRoleAsync(c23, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c23, "Customer");
            }

            AppUser c24 = _db.Users.FirstOrDefault(u => u.Email == "gregmartinez@drdre.com");
            if (c24 == null)
            {
                c24 = new AppUser();
                c24.UserName = "gregmartinez@drdre.com";
                c24.Email = "gregmartinez@drdre.com";
                c24.PhoneNumber = "9371927523";
                c24.FirstName = "Gregory";
                c24.LastName = "Martinez";
                c24.StAddress = "10 Northridge Plaza";
                c24.City = "Austin";
                c24.State = "TX";
                c24.ZipCode = "78717";
               
                c24.AccountActive = true;

                var result = await _userManager.CreateAsync(c24, "xcellent");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c24 = _db.Users.FirstOrDefault(u => u.UserName == "gregmartinez@drdre.com");
            }
            if (await _userManager.IsInRoleAsync(c24, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c24, "Customer");
            }

            AppUser c25 = _db.Users.FirstOrDefault(u => u.Email == "cmiller@bob.com");
            if (c25 == null)
            {
                c25 = new AppUser();
                c25.UserName = "cmiller@bob.com";
                c25.Email = "cmiller@bob.com";
                c25.PhoneNumber = "5954063857";
                c25.FirstName = "Charles";
                c25.LastName = "Miller";
                c25.StAddress = "87683 Schmedeman Circle";
                c25.City = "Austin";
                c25.State = "TX";
                c25.ZipCode = "78727";
                
                c25.AccountActive = true;

                var result = await _userManager.CreateAsync(c25, "mydogspot");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c25 = _db.Users.FirstOrDefault(u => u.UserName == "cmiller@bob.com");
            }
            if (await _userManager.IsInRoleAsync(c25, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c25, "Customer");
            }

            AppUser c26 = _db.Users.FirstOrDefault(u => u.Email == "knelson@aoll.com");
            if (c26 == null)
            {
                c26 = new AppUser();
                c26.UserName = "knelson@aoll.com";
                c26.Email = "knelson@aoll.com";
                c26.PhoneNumber = "8929209512";
                c26.FirstName = "Kelly";
                c26.LastName = "Nelson";
                c26.StAddress = "3244 Ludington Court";
                c26.City = "Beaumont";
                c26.State = "TX";
                c26.ZipCode = "77720";
                
                c26.AccountActive = true;

                var result = await _userManager.CreateAsync(c26, "spotmydog");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c26 = _db.Users.FirstOrDefault(u => u.UserName == "knelson@aoll.com");
            }
            if (await _userManager.IsInRoleAsync(c26, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c26, "Customer");
            }

            AppUser c27 = _db.Users.FirstOrDefault(u => u.Email == "joewin@xfactor.com");
            if (c27 == null)
            {
                c27 = new AppUser();
                c27.UserName = "joewin@xfactor.com";
                c27.Email = "joewin@xfactor.com";
                c27.PhoneNumber = "9226301774";
                c27.FirstName = "Joe";
                c27.LastName = "Nguyen";
                c27.StAddress = "4780 Talisman Court";
                c27.City = "San Marcos";
                c27.State = "TX";
                c27.ZipCode = "78667";
                
                c27.AccountActive = true;

                var result = await _userManager.CreateAsync(c27, "joejoejoe");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c27 = _db.Users.FirstOrDefault(u => u.UserName == "joewin@xfactor.com");
            }
            if (await _userManager.IsInRoleAsync(c27, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c27, "Customer");
            }

            AppUser c28 = _db.Users.FirstOrDefault(u => u.Email == "orielly@foxnews.cnn");
            if (c28 == null)
            {
                c28 = new AppUser();
                c28.UserName = "orielly@foxnews.cnn";
                c28.Email = "orielly@foxnews.cnn";
                c28.PhoneNumber = "2537646912";
                c28.FirstName = "Bill";
                c28.LastName = "O'Reilly";
                c28.StAddress = "4154 Delladonna Plaza";
                c28.City = "Bergheim";
                c28.State = "TX";
                c28.ZipCode = "78004";
                
                c28.AccountActive = true;

                var result = await _userManager.CreateAsync(c28, "billyboy");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c28 = _db.Users.FirstOrDefault(u => u.UserName == "orielly@foxnews.cnn");
            }
            if (await _userManager.IsInRoleAsync(c28, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c28, "Customer");
            }

            AppUser c29 = _db.Users.FirstOrDefault(u => u.Email == "ankaisrad@gogle.com");
            if (c29 == null)
            {
                c29 = new AppUser();
                c29.UserName = "ankaisrad@gogle.com";
                c29.Email = "ankaisrad@gogle.com";
                c29.PhoneNumber = "2182889379";
                c29.FirstName = "Anka";
                c29.LastName = "Radkovich";
                c29.StAddress = "72361 Bayside Drive";
                c29.City = "Austin";
                c29.State = "TX";
                c29.ZipCode = "78789";
                
                c29.AccountActive = true;

                var result = await _userManager.CreateAsync(c29, "radgirl");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c29 = _db.Users.FirstOrDefault(u => u.UserName == "ankaisrad@gogle.com");
            }
            if (await _userManager.IsInRoleAsync(c29, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c29, "Customer");
            }

            AppUser c30 = _db.Users.FirstOrDefault(u => u.Email == "megrhodes@freserve.co.uk");
            if (c30 == null)
            {
                c30 = new AppUser();
                c30.UserName = "megrhodes@freserve.co.uk";
                c30.Email = "megrhodes@freserve.co.uk";
                c30.PhoneNumber = "9532396075";
                c30.FirstName = "Megan";
                c30.LastName = "Rhodes";
                c30.StAddress = "76875 Hoffman Point";
                c30.City = "Orlando";
                c30.State = "FL";
                c30.ZipCode = "32830";
                
                c30.AccountActive = true;

                var result = await _userManager.CreateAsync(c30, "meganr34");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c30 = _db.Users.FirstOrDefault(u => u.UserName == "megrhodes@freserve.co.uk");
            }
            if (await _userManager.IsInRoleAsync(c30, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c30, "Customer");
            }

            AppUser c31 = _db.Users.FirstOrDefault(u => u.Email == "erynrice@aoll.com");
            if (c31 == null)
            {
                c31 = new AppUser();
                c31.UserName = "erynrice@aoll.com";
                c31.Email = "erynrice@aoll.com";
                c31.PhoneNumber = "7303815953";
                c31.FirstName = "Eryn";
                c31.LastName = "Rice";
                c31.StAddress = "048 Elmside Park";
                c31.City = "South Padre Island";
                c31.State = "TX";
                c31.ZipCode = "78597";
                
                c31.AccountActive = true;

                var result = await _userManager.CreateAsync(c31, "ricearoni");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c31 = _db.Users.FirstOrDefault(u => u.UserName == "erynrice@aoll.com");
            }
            if (await _userManager.IsInRoleAsync(c31, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c31, "Customer");
            }

            AppUser c32 = _db.Users.FirstOrDefault(u => u.Email == "jorge@noclue.com");
            if (c32 == null)
            {
                c32 = new AppUser();
                c32.UserName = "jorge@noclue.com";
                c32.Email = "jorge@noclue.com";
                c32.PhoneNumber = "3677322422";
                c32.FirstName = "Jorge";
                c32.LastName = "Rodriguez";
                c32.StAddress = "01 Browning Pass";
                c32.City = "Austin";
                c32.State = "TX";
                c32.ZipCode = "78744";
                
                c32.AccountActive = true;

                var result = await _userManager.CreateAsync(c32, "alaskaboy");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c32 = _db.Users.FirstOrDefault(u => u.UserName == "jorge@noclue.com");
            }
            if (await _userManager.IsInRoleAsync(c32, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c32, "Customer");
            }

            AppUser c33 = _db.Users.FirstOrDefault(u => u.Email == "mrrogers@lovelyday.com");
            if (c33 == null)
            {
                c33 = new AppUser();
                c33.UserName = "mrrogers@lovelyday.com";
                c33.Email = "mrrogers@lovelyday.com";
                c33.PhoneNumber = "3911705385";
                c33.FirstName = "Allen";
                c33.LastName = "Rogers";
                c33.StAddress = "844 Anderson Alley";
                c33.City = "Canyon Lake";
                c33.State = "TX";
                c33.ZipCode = "78133";
                
                c33.AccountActive = true;

                var result = await _userManager.CreateAsync(c33, "bunnyhop");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c33 = _db.Users.FirstOrDefault(u => u.UserName == "mrrogers@lovelyday.com");
            }
            if (await _userManager.IsInRoleAsync(c33, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c33, "Customer");
            }

            AppUser c34 = _db.Users.FirstOrDefault(u => u.Email == "stjean@athome.com");
            if (c34 == null)
            {
                c34 = new AppUser();
                c34.UserName = "stjean@athome.com";
                c34.Email = "stjean@athome.com";
                c34.PhoneNumber = "7351610920";
                c34.FirstName = "Olivier";
                c34.LastName = "Saint-Jean";
                c34.StAddress = "1891 Docker Point";
                c34.City = "Austin";
                c34.State = "TX";
                c34.ZipCode = "78779";
                
                c34.AccountActive = true;

                var result = await _userManager.CreateAsync(c34, "dustydusty");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c34 = _db.Users.FirstOrDefault(u => u.UserName == "stjean@athome.com");
            }
            if (await _userManager.IsInRoleAsync(c34, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c34, "Customer");
            }

            AppUser c35 = _db.Users.FirstOrDefault(u => u.Email == "saunders@pen.com");
            if (c35 == null)
            {
                c35 = new AppUser();
                c35.UserName = "saunders@pen.com";
                c35.Email = "saunders@pen.com";
                c35.PhoneNumber = "5269661692";
                c35.FirstName = "Sarah";
                c35.LastName = "Saunders";
                c35.StAddress = "1469 Upham Road";
                c35.City = "Austin";
                c35.State = "TX";
                c35.ZipCode = "78720";
                
                c35.AccountActive = true;

                var result = await _userManager.CreateAsync(c35, "jrod2017");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c35 = _db.Users.FirstOrDefault(u => u.UserName == "saunders@pen.com");
            }
            if (await _userManager.IsInRoleAsync(c35, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c35, "Customer");
            }

            AppUser c36 = _db.Users.FirstOrDefault(u => u.Email == "willsheff@email.com");
            if (c36 == null)
            {
                c36 = new AppUser();
                c36.UserName = "willsheff@email.com";
                c36.Email = "willsheff@email.com";
                c36.PhoneNumber = "1875727246";
                c36.FirstName = "William";
                c36.LastName = "Sewell";
                c36.StAddress = "1672 Oak Valley Circle";
                c36.City = "Austin";
                c36.State = "TX";
                c36.ZipCode = "78705";
                
                c36.AccountActive = true;

                var result = await _userManager.CreateAsync(c36, "martin1234");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c36 = _db.Users.FirstOrDefault(u => u.UserName == "willsheff@email.com");
            }
            if (await _userManager.IsInRoleAsync(c36, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c36, "Customer");
            }

            AppUser c37 = _db.Users.FirstOrDefault(u => u.Email == "sheffiled@gogle.com");
            if (c37 == null)
            {
                c37 = new AppUser();
                c37.UserName = "sheffiled@gogle.com";
                c37.Email = "sheffiled@gogle.com";
                c37.PhoneNumber = "1394323615";
                c37.FirstName = "Martin";
                c37.LastName = "Sheffield";
                c37.StAddress = "816 Kennedy Place";
                c37.City = "Round Rock";
                c37.State = "TX";
                c37.ZipCode = "78680";
                
                c37.AccountActive = true;

                var result = await _userManager.CreateAsync(c37, "penguin12");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c37 = _db.Users.FirstOrDefault(u => u.UserName == "sheffiled@gogle.com");
            }
            if (await _userManager.IsInRoleAsync(c37, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c37, "Customer");
            }

            AppUser c38 = _db.Users.FirstOrDefault(u => u.Email == "johnsmith187@aoll.com");
            if (c38 == null)
            {
                c38 = new AppUser();
                c38.UserName = "johnsmith187@aoll.com";
                c38.Email = "johnsmith187@aoll.com";
                c38.PhoneNumber = "6645937874";
                c38.FirstName = "John";
                c38.LastName = "Smith";
                c38.StAddress = "0745 Golf Road";
                c38.City = "Austin";
                c38.State = "TX";
                c38.ZipCode = "78760";
                
                c38.AccountActive = true;

                var result = await _userManager.CreateAsync(c38, "rogerthat");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c38 = _db.Users.FirstOrDefault(u => u.UserName == "johnsmith187@aoll.com");
            }
            if (await _userManager.IsInRoleAsync(c38, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c38, "Customer");
            }

            AppUser c39 = _db.Users.FirstOrDefault(u => u.Email == "dustroud@mail.com");
            if (c39 == null)
            {
                c39 = new AppUser();
                c39.UserName = "dustroud@mail.com";
                c39.Email = "dustroud@mail.com";
                c39.PhoneNumber = "6470254680";
                c39.FirstName = "Dustin";
                c39.LastName = "Stroud";
                c39.StAddress = "505 Dexter Plaza";
                c39.City = "Sweet Home";
                c39.State = "TX";
                c39.ZipCode = "77987";
                
                c39.AccountActive = true;

                var result = await _userManager.CreateAsync(c39, "smitty444");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c39 = _db.Users.FirstOrDefault(u => u.UserName == "dustroud@mail.com");
            }
            if (await _userManager.IsInRoleAsync(c39, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c39, "Customer");
            }

            AppUser c40 = _db.Users.FirstOrDefault(u => u.Email == "estuart@anchor.net");
            if (c40 == null)
            {
                c40 = new AppUser();
                c40.UserName = "estuart@anchor.net";
                c40.Email = "estuart@anchor.net";
                c40.PhoneNumber = "7701621022";
                c40.FirstName = "Eric";
                c40.LastName = "Stuart";
                c40.StAddress = "585 Claremont Drive";
                c40.City = "Corpus Christi";
                c40.State = "TX";
                c40.ZipCode = "78412";
                
                c40.AccountActive = true;

                var result = await _userManager.CreateAsync(c40, "stewball");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c40 = _db.Users.FirstOrDefault(u => u.UserName == "estuart@anchor.net");
            }
            if (await _userManager.IsInRoleAsync(c40, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c40, "Customer");
            }

            AppUser c41 = _db.Users.FirstOrDefault(u => u.Email == "peterstump@noclue.com");
            if (c41 == null)
            {
                c41 = new AppUser();
                c41.UserName = "peterstump@noclue.com";
                c41.Email = "peterstump@noclue.com";
                c41.PhoneNumber = "2181960061";
                c41.FirstName = "Peter";
                c41.LastName = "Stump";
                c41.StAddress = "89035 Welch Circle";
                c41.City = "Pflugerville";
                c41.State = "TX";
                c41.ZipCode = "78660";
                
                c41.AccountActive = true;

                var result = await _userManager.CreateAsync(c41, "slowwind");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c41 = _db.Users.FirstOrDefault(u => u.UserName == "peterstump@noclue.com");
            }
            if (await _userManager.IsInRoleAsync(c41, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c41, "Customer");
            }

            AppUser c42 = _db.Users.FirstOrDefault(u => u.Email == "jtanner@mustang.net");
            if (c42 == null)
            {
                c42 = new AppUser();
                c42.UserName = "jtanner@mustang.net";
                c42.Email = "jtanner@mustang.net";
                c42.PhoneNumber = "9908469499";
                c42.FirstName = "Jeremy";
                c42.LastName = "Tanner";
                c42.StAddress = "4 Stang Trail";
                c42.City = "Austin";
                c42.State = "TX";
                c42.ZipCode = "78702";
                
                c42.AccountActive = true;

                var result = await _userManager.CreateAsync(c42, "tanner5454");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c42 = _db.Users.FirstOrDefault(u => u.UserName == "jtanner@mustang.net");
            }
            if (await _userManager.IsInRoleAsync(c42, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c42, "Customer");
            }

            AppUser c43 = _db.Users.FirstOrDefault(u => u.Email == "taylordjay@aoll.com");
            if (c43 == null)
            {
                c43 = new AppUser();
                c43.UserName = "taylordjay@aoll.com";
                c43.Email = "taylordjay@aoll.com";
                c43.PhoneNumber = "7011918647";
                c43.FirstName = "Allison";
                c43.LastName = "Taylor";
                c43.StAddress = "726 Twin Pines Avenue";
                c43.City = "Austin";
                c43.State = "TX";
                c43.ZipCode = "78713";
                
                c43.AccountActive = true;

                var result = await _userManager.CreateAsync(c43, "allyrally");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c43 = _db.Users.FirstOrDefault(u => u.UserName == "taylordjay@aoll.com");
            }
            if (await _userManager.IsInRoleAsync(c43, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c43, "Customer");
            }

            AppUser c44 = _db.Users.FirstOrDefault(u => u.Email == "rtaylor@gogle.com");
            if (c44 == null)
            {
                c44 = new AppUser();
                c44.UserName = "rtaylor@gogle.com";
                c44.Email = "rtaylor@gogle.com";
                c44.PhoneNumber = "8937910053";
                c44.FirstName = "Rachel";
                c44.LastName = "Taylor";
                c44.StAddress = "06605 Sugar Drive";
                c44.City = "Austin";
                c44.State = "TX";
                c44.ZipCode = "78712";
                
                c44.AccountActive = true;

                var result = await _userManager.CreateAsync(c44, "taylorbaylor");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c44 = _db.Users.FirstOrDefault(u => u.UserName == "rtaylor@gogle.com");
            }
            if (await _userManager.IsInRoleAsync(c44, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c44, "Customer");
            }

            AppUser c45 = _db.Users.FirstOrDefault(u => u.Email == "teefrank@noclue.com");
            if (c45 == null)
            {
                c45 = new AppUser();
                c45.UserName = "teefrank@noclue.com";
                c45.Email = "teefrank@noclue.com";
                c45.PhoneNumber = "6394568913";
                c45.FirstName = "Frank";
                c45.LastName = "Tee";
                c45.StAddress = "3567 Dawn Plaza";
                c45.City = "Austin";
                c45.State = "TX";
                c45.ZipCode = "78786";
                
                c45.AccountActive = true;

                var result = await _userManager.CreateAsync(c45, "teeoff22");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c45 = _db.Users.FirstOrDefault(u => u.UserName == "teefrank@noclue.com");
            }
            if (await _userManager.IsInRoleAsync(c45, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c45, "Customer");
            }

            AppUser c46 = _db.Users.FirstOrDefault(u => u.Email == "ctucker@alphabet.co.uk");
            if (c46 == null)
            {
                c46 = new AppUser();
                c46.UserName = "ctucker@alphabet.co.uk";
                c46.Email = "ctucker@alphabet.co.uk";
                c46.PhoneNumber = "2676838676";
                c46.FirstName = "Clent";
                c46.LastName = "Tucker";
                c46.StAddress = "704 Northland Alley";
                c46.City = "San Antonio";
                c46.State = "TX";
                c46.ZipCode = "78279";
                
                c46.AccountActive = true;

                var result = await _userManager.CreateAsync(c46, "tucksack1");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c46 = _db.Users.FirstOrDefault(u => u.UserName == "ctucker@alphabet.co.uk");
            }
            if (await _userManager.IsInRoleAsync(c46, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c46, "Customer");
            }

            AppUser c47 = _db.Users.FirstOrDefault(u => u.Email == "avelasco@yoho.com");
            if (c47 == null)
            {
                c47 = new AppUser();
                c47.UserName = "avelasco@yoho.com";
                c47.Email = "avelasco@yoho.com";
                c47.PhoneNumber = "3452909754";
                c47.FirstName = "Allen";
                c47.LastName = "Velasco";
                c47.StAddress = "72 Harbort Point";
                c47.City = "Navasota";
                c47.State = "TX";
                c47.ZipCode = "77868";
                
                c47.AccountActive = true;

                var result = await _userManager.CreateAsync(c47, "meow88");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c47 = _db.Users.FirstOrDefault(u => u.UserName == "avelasco@yoho.com");
            }
            if (await _userManager.IsInRoleAsync(c47, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c47, "Customer");
            }

            AppUser c48 = _db.Users.FirstOrDefault(u => u.Email == "vinovino@grapes.com");
            if (c48 == null)
            {
                c48 = new AppUser();
                c48.UserName = "vinovino@grapes.com";
                c48.Email = "vinovino@grapes.com";
                c48.PhoneNumber = "8567089194";
                c48.FirstName = "Janet";
                c48.LastName = "Vino";
                c48.StAddress = "1 Oak Valley Place";
                c48.City = "Boston";
                c48.State = "MA";
                c48.ZipCode = "02114";
                
                c48.AccountActive = true;

                var result = await _userManager.CreateAsync(c48, "vinovino");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c48 = _db.Users.FirstOrDefault(u => u.UserName == "vinovino@grapes.com");
            }
            if (await _userManager.IsInRoleAsync(c48, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c48, "Customer");
            }

            AppUser c49 = _db.Users.FirstOrDefault(u => u.Email == "westj@pioneer.net");
            if (c49 == null)
            {
                c49 = new AppUser();
                c49.UserName = "westj@pioneer.net";
                c49.Email = "westj@pioneer.net";
                c49.PhoneNumber = "6260784394";
                c49.FirstName = "Jake";
                c49.LastName = "West";
                c49.StAddress = "48743 Banding Parkway";
                c49.City = "Marble Falls";
                c49.State = "TX";
                c49.ZipCode = "78654";
                
                c49.AccountActive = true;

                var result = await _userManager.CreateAsync(c49, "gowest");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c49 = _db.Users.FirstOrDefault(u => u.UserName == "westj@pioneer.net");
            }
            if (await _userManager.IsInRoleAsync(c49, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c49, "Customer");
            }

            AppUser c50 = _db.Users.FirstOrDefault(u => u.Email == "winner@hootmail.com");
            if (c50 == null)
            {
                c50 = new AppUser();
                c50.UserName = "winner@hootmail.com";
                c50.Email = "winner@hootmail.com";
                c50.PhoneNumber = "3733971174";
                c50.FirstName = "Louis";
                c50.LastName = "Winthorpe";
                c50.StAddress = "96850 Summit Crossing";
                c50.City = "Austin";
                c50.State = "TX";
                c50.ZipCode = "78730";
                
                c50.AccountActive = true;

                var result = await _userManager.CreateAsync(c50, "louielouie");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c50 = _db.Users.FirstOrDefault(u => u.UserName == "winner@hootmail.com");
            }
            if (await _userManager.IsInRoleAsync(c50, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c50, "Customer");
            }

            AppUser c51 = _db.Users.FirstOrDefault(u => u.Email == "rwood@voyager.net");
            if (c51 == null)
            {
                c51 = new AppUser();
                c51.UserName = "rwood@voyager.net";
                c51.Email = "rwood@voyager.net";
                c51.PhoneNumber = "8433359800";
                c51.FirstName = "Reagan";
                c51.LastName = "Wood";
                c51.StAddress = "18354 Bluejay Street";
                c51.City = "Austin";
                c51.State = "TX";
                c51.ZipCode = "78712";
                
                c51.AccountActive = true;

                var result = await _userManager.CreateAsync(c51, "woodyman1");
                if (result.Succeeded == false)
                {
                    throw new Exception("This user can't be added - " + result.ToString());
                }

                _db.SaveChanges();
                c51 = _db.Users.FirstOrDefault(u => u.UserName == "rwood@voyager.net");
            }
            if (await _userManager.IsInRoleAsync(c51, "Customer") == false)
            {
                await _userManager.AddToRoleAsync(c51, "Customer");
            }
            _db.SaveChanges();

        }

    }
}