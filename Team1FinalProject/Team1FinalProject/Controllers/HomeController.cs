using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team1FinalProject.DAL;
using Team1FinalProject.Models;

namespace Team1FinalProject.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _db;

        public HomeController(AppDbContext context)
        {
            _db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _db.Coupons.ToListAsync());
        }
        //public IActionResult Index()
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var coupon = await _context.Coupons
        //        .FirstOrDefaultAsync(m => m.CouponID == id);
        //    if (coupon == null)
        //    {
        //        return NotFound();
        //    }
        //    //List<Coupon> AllCoupons = new List<Coupon>();
        //    ////var query = from c in _db.Coupons select c;

        //    //if (_db.Coupons.Count > 0 )
        //    //{
        //    //    AllCoupons = _db.Coupons.Include(c => c.CouponCode).ToList();

        //    //}



        //    //return View("Index", "AllCoupons");
         
        public IActionResult Books()
        {
            return RedirectToAction("Index", "Books");
        }
    }
}