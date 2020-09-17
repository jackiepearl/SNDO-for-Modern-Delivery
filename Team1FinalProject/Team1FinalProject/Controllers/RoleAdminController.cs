using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Team1FinalProject.DAL;
using Team1FinalProject.Models;

namespace Team1FinalProject.Controllers
{

    //TODO: Uncomment this line once you have roles working correctly
    //[Authorize(Roles = "Manager")]
    [Authorize(Roles="Manager,Employee")]
    public class RoleAdminController : Controller
    {
        private AppDbContext _db;
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        
   


        public RoleAdminController(AppDbContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }


        // GET: /RoleAdmin/
        public async Task<ActionResult> Index()
        {
            List<RoleEditModel> roles = new List<RoleEditModel>();
            
            foreach (IdentityRole role in _roleManager.Roles)
            {
                List<AppUser> members = new List<AppUser>();
                List<AppUser> nonMembers = new List<AppUser>();
                foreach (AppUser user in _userManager.Users)
                {
                    var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                    list.Add(user);
                }
                RoleEditModel re = new RoleEditModel();
                re.Role = role;
                re.Members = members;
                re.NonMembers = nonMembers;
                roles.Add(re);
            }
            return View(roles);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }

            //if code gets this far, we need to show an error
            return View(name);
        }

        public async Task<ActionResult> Edit(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEditModel { Role = role, Members = members, NonMembers = nonMembers });
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    user.AccountActive = true;
                    result = await _userManager.AddToRoleAsync(user, model.RoleName);
                 
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }

                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    AppUser user = await _userManager.FindByIdAsync(userId);
                    user.AccountActive = false;
                    result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                   
                    if (!result.Succeeded)
                    {
                        return View("Error", result.Errors);
                    }
                }
                return RedirectToAction("Index");
            }
            return View("Error", new string[] { "Role Not Found" });
        }
    
        [Authorize(Roles = "Employee")]
        public ActionResult CreateCustomer()
        {
            return View();
        }

       
        [HttpPost]
        [AllowAnonymous]
        [Authorize(Roles = "Employee")]
        [ValidateAntiForgeryToken]
        //TODO: This is the method where you create a new user
        public async Task<ActionResult> CreateCustomer(RegisterViewModel model)
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
                    await _userManager.AddToRoleAsync(user, "Customer");
                   
                    return RedirectToAction("Index","RoleAdmin");
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

        public IActionResult CreateEmployee()
        {
            return View("CreateCustomer");
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        //TODO: This is the method where you create a new user
        public async Task<ActionResult> CreateEmployee(RegisterViewModel model)
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

                    await _userManager.AddToRoleAsync(user, "Employee");
                                    
                    return RedirectToAction("Index","RoleAdmin");
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

        public async Task<ActionResult> ManagerEdit()
        {
            List<AppUser> members = await GetAllCustomer();
            return View(new CustomerEditModel { Members = members });
        }

        [HttpPost]
        public async Task<ActionResult> ManagerEdit(CustomerEditModel model)
        {
            List<AppUser> ap = new List<AppUser>();
            AppUser user = new AppUser();
            foreach (string userId in model.IdsToEdit ?? new string[] { })
            {
                user = await _userManager.FindByIdAsync(userId);
                ap.Add(user);

            }

            
            return View("EditCustomers",user);
        }

        [HttpPost]
        public async Task<ActionResult> EditCustomers(string Email,string FirstName,string LastName,string PhoneNumber,string StAddress,string City,string State,string ZipCode)    
        {
            AppUser user = _db.Users.FirstOrDefault(u => u.UserName == Email);

            if(ModelState.IsValid)
            {
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.PhoneNumber = PhoneNumber;
                user.StAddress = StAddress;
                user.City = City;
                user.State = State;
                user.ZipCode = ZipCode;

                _db.Update(user);
                _db.SaveChanges();
            }

            return View("CustomerUpdated");
            
        }

        public async Task<ActionResult> ManagerEdit2()
        {
            List<AppUser> members = await GetAllEmployees();
            return View(new CustomerEditModel { Members = members });
        }

        [HttpPost]
        public async Task<ActionResult> ManagerEdit2(CustomerEditModel model)
        {
            List<AppUser> ap = new List<AppUser>();
            AppUser user = new AppUser();
            foreach (string userId in model.IdsToEdit ?? new string[] { })
            {
                user = await _userManager.FindByIdAsync(userId);
                ap.Add(user);

            }


            return View("EditEmployees", user);
        }

        [HttpPost]
        public async Task<ActionResult> EditCustomers2(string Email, string FirstName, string LastName, string PhoneNumber, string StAddress, string City, string State, string ZipCode)
        {
            AppUser user = _db.Users.FirstOrDefault(u => u.UserName == Email);

            if (ModelState.IsValid)
            {
                user.FirstName = FirstName;
                user.LastName = LastName;
                user.PhoneNumber = PhoneNumber;
                user.StAddress = StAddress;
                user.City = City;
                user.State = State;
                user.ZipCode = ZipCode;

               _db.Update(user);
               _db.SaveChanges();
            }

            return View("EmployeeUpdated");

        }


        private void AddErrorsFromResult(IdentityResult result)
        { 
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        public async Task<List<AppUser>> GetAllCustomer()
        {
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, "Customer") ? members : nonMembers;
                list.Add(user);
            }

            

            return members;
        }

        public async Task<List<AppUser>> GetAllEmployees()
        {
            List<AppUser> members = new List<AppUser>();
            List<AppUser> nonMembers = new List<AppUser>();
            foreach (AppUser user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, "Employee") ? members : nonMembers;
                list.Add(user);
            }

            return members;
        }


    }
}