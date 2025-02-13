﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Team1FinalProject.DAL;
using Team1FinalProject.Models;




namespace Team1FinalProject.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private SignInManager<AppUser> _signInManager;
        private UserManager<AppUser> _userManager;
        private PasswordValidator<AppUser> _passwordValidator;
        private AppDbContext _db;

        public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signIn)
        {
            _db = context;
            _userManager = userManager;
            _signInManager = signIn;
           
            //user manager only has one password validator
            _passwordValidator = (PasswordValidator<AppUser>)userManager.PasswordValidators.FirstOrDefault();
        }

        
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) //user has been redirected here from a page they're not authorized to see
            {
                return View("Error", new string[] { "Access Denied" });
            }
            _signInManager.SignOutAsync(); //this removes any old cookies hanging around
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser user = _userManager.Users.FirstOrDefault(u => u.Email == model.Email);

            if(user == null)
            {
                return View("Error", new string[] { "User are not in the database. Please register!" });
            }
            if (user.AccountActive == false)
            {
                return View("Error", new string[] { "Your account is disabled!" });
            }



            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");                
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
        }

       
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult RegisterCustomer()
        {
            return View();
        }

        //TODO: Create RegisterEmployee Method

        //
        // POST: /Account/RegisterCustomer
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //TODO: This is the method where you create a new user
        public async Task<ActionResult> RegisterCustomer(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    StAddress = model.Street,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    AccountActive = true,
                    

                    //TODO: You will need to add all of the properties for your User model here
                    //Make sure that you have included ALL of the properties and that they match
                    //the model class EXACTLY!!
                    //FirstName = model.FirstName,
                    //LastName = model.LastName,

                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {                
                    await _userManager.AddToRoleAsync(user, "Customer");

                    Microsoft.AspNetCore.Identity.SignInResult result2 = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                    string subjectline = "You have created an account at Bevo Books!";
                    string body = "Congratulations. You have created a Bevo Books account!";
                    Utilities.SendEmail.Send_Email(user.Email, subjectline, body);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        //TODO: This is the method where you create a new user
        public async Task<ActionResult> RegisterEmployee(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    StAddress = model.Street,
                    City = model.City,
                    State = model.State,
                    ZipCode = model.ZipCode,
                    AccountActive = true,


                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //TODO: Add user to desired role
                    //This will not work until you have seeded Identity OR added the "Customer" role 
                    //by navigating to the RoleAdmin controller and manually added the "Customer" role

                    await _userManager.AddToRoleAsync(user, "Employee");
                    //another example
                    //await _userManager.AddToRoleAsync(user, "Manager");

                    Microsoft.AspNetCore.Identity.SignInResult result2 = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        //GET: Account/Index
        public ActionResult Index()
        {
            IndexViewModel ivm = new IndexViewModel();

            //get user info
            String id = User.Identity.Name;
            AppUser user = _db.Users.FirstOrDefault(u => u.UserName == id);

            //populate the view model
            ivm.Email = user.Email;
            ivm.UserName = user.UserName;
            ivm.HasPassword = true;
            ivm.UserID = user.Id;
            ivm.FirstName = user.FirstName;
            ivm.LastName = user.LastName;
            ivm.PhoneNumber = user.PhoneNumber;
            ivm.Address = user.StAddress + " , " + user.City + ", " + user.State + " " + user.ZipCode;



            return View(ivm);
           
        }


        //Logic for change password
        // GET: /Account/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
            var result = await _userManager.ChangePasswordAsync(userLoggedIn, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {

                await _signInManager.SignInAsync(userLoggedIn, isPersistent: false);
                string subjectline = "Your Password has been changed!";
                string body = "Your new passsword should be working!";
                Utilities.SendEmail.Send_Email(userLoggedIn.Email, subjectline, body);
                return RedirectToAction("Index", "Home");
            }
            AddErrors(result);
            return View(model);
        }

        [Authorize]
        public ActionResult ChangeFirstName()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeFirstName(String NewFirstName,[Bind("NewFirstName")] ChangeFirstNameModel model)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
            userLoggedIn.FirstName = NewFirstName;
            _db.SaveChanges();

            IndexViewModel ivm = new IndexViewModel();

            //populate the view model
            ivm.Email = userLoggedIn.Email;
            ivm.UserName = userLoggedIn.UserName;
            ivm.HasPassword = true;
            ivm.UserID = userLoggedIn.Id;
            ivm.FirstName = userLoggedIn.FirstName;
            ivm.LastName = userLoggedIn.LastName;
            ivm.PhoneNumber = userLoggedIn.PhoneNumber;
            ivm.Address = userLoggedIn.StAddress + " , " + userLoggedIn.City + ", " + userLoggedIn.State + " " + userLoggedIn.ZipCode;


            return View("Index",ivm);
        }

        [Authorize]
        public ActionResult ChangeLastName()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeLastName (String NewLastName,[Bind("NewLastName")] ChangeLastNameModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
            userLoggedIn.LastName = NewLastName;
            _db.SaveChanges();

            IndexViewModel ivm = new IndexViewModel();

            //populate the view model
            ivm.Email = userLoggedIn.Email;
            ivm.UserName = userLoggedIn.UserName;
            ivm.HasPassword = true;
            ivm.UserID = userLoggedIn.Id;
            ivm.FirstName = userLoggedIn.FirstName;
            ivm.LastName = userLoggedIn.LastName;
            ivm.PhoneNumber = userLoggedIn.PhoneNumber;
            ivm.Address = userLoggedIn.StAddress + " , " + userLoggedIn.City + ", " + userLoggedIn.State + " " + userLoggedIn.ZipCode;


            return View("Index", ivm);
        }

        [Authorize]
        public ActionResult ChangeEmail()
        {
            return View();
        }

       
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangeEmail(String NewEmail,[Bind("NewEmail")] ChangeEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
            userLoggedIn.Email = NewEmail;
            _db.SaveChanges();

            IndexViewModel ivm = new IndexViewModel();

            //populate the view model
            ivm.Email = userLoggedIn.Email;
            ivm.UserName = userLoggedIn.UserName;
            ivm.HasPassword = true;
            ivm.UserID = userLoggedIn.Id;
            ivm.FirstName = userLoggedIn.FirstName;
            ivm.LastName = userLoggedIn.LastName;
            ivm.PhoneNumber = userLoggedIn.PhoneNumber;
            ivm.Address = userLoggedIn.StAddress + " , " + userLoggedIn.City + ", " + userLoggedIn.State + " " + userLoggedIn.ZipCode;


            return View("Index", ivm);
        }


        public ActionResult ChangeAddress()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ChangeAddress(String NewStreet, String NewCity,String NewState,String NewZip)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
            userLoggedIn.StAddress = NewStreet;
            userLoggedIn.City = NewCity;
            userLoggedIn.State = NewState;
            userLoggedIn.ZipCode = NewZip;
            _db.SaveChanges();

            IndexViewModel ivm = new IndexViewModel();

            //populate the view model
            ivm.Email = userLoggedIn.Email;
            ivm.UserName = userLoggedIn.UserName;
            ivm.HasPassword = true;
            ivm.UserID = userLoggedIn.Id;
            ivm.FirstName = userLoggedIn.FirstName;
            ivm.LastName = userLoggedIn.LastName;
            ivm.PhoneNumber = userLoggedIn.PhoneNumber;
            ivm.Address = userLoggedIn.StAddress + " , " + userLoggedIn.City + ", " + userLoggedIn.State + " " + userLoggedIn.ZipCode;


            return View("Index", ivm);
        }

        public ActionResult ChangePhoneNumber()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<ActionResult> ChangePhoneNumber(String NewPhoneNumber, [Bind("NewPhoneNumber")] ChangePhoneNumberModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            AppUser userLoggedIn = await _userManager.FindByNameAsync(User.Identity.Name);
            userLoggedIn.PhoneNumber = NewPhoneNumber;
            _db.SaveChanges();

            IndexViewModel ivm = new IndexViewModel();

            //populate the view model
            ivm.Email = userLoggedIn.Email;
            ivm.UserName = userLoggedIn.UserName;
            ivm.HasPassword = true;
            ivm.UserID = userLoggedIn.Id;
            ivm.FirstName = userLoggedIn.FirstName;
            ivm.LastName = userLoggedIn.LastName;
            ivm.PhoneNumber = userLoggedIn.PhoneNumber;
            ivm.Address = userLoggedIn.StAddress + " , " + userLoggedIn.City + ", " + userLoggedIn.State + " " + userLoggedIn.ZipCode;


            return View("Index", ivm);
        }
        //GET:/Account/AccessDenied
        public ActionResult AccessDenied(String ReturnURL)
        {
            return View("Error", new string[] { "Access is denied" });
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
           

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}