using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team1FinalProject.DAL;
using Team1FinalProject.Models;
using Team1FinalProject.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Team1FinalProject.Controllers
{
   
    public enum SortByA { Recent, PMasc, PMdesc, PriceAsc, PriceDesc, Pop }
    public enum SortByB { Recent, PMasc, PMdesc, PriceAsc, PriceDesc }
    public enum SortByC { PMasc, PMdesc, RevAsc, RevDesc }
    public enum SortByF { EmpID, AppAsc, AppDesc, RejAsc, RejDesc }
  
    public class ReportsController : Controller
    {
        private AppDbContext _db;
        private UserManager<AppUser> _userManager;


        public ReportsController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _db = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Manager")]
        public ActionResult Index()
        {

            return View("Index");
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ReportA()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult DisplayReportA(SortByA Sort)
        {
            List<OrderDet> orderDets = new List<OrderDet>();
            var query = from o in _db.OrderDets
                        select o;
            orderDets = query.Include(o => o.Book).Include(o => o.Order).Include(o => o.Order.AppUser).ToList();



            switch (Sort)
            {
                case SortByA.Recent:
                    return View("ReportAResults", orderDets.OrderByDescending(o => o.OrderDetID));
                case SortByA.PMasc:
                    return View("ReportAResults", orderDets.OrderBy(o => o.Margin));
                case SortByA.PMdesc:
                    return View("ReportAResults", orderDets.OrderByDescending(o => o.Margin));
                case SortByA.PriceAsc:
                    return View("ReportAResults", orderDets.OrderBy(o => o.BookPrice));
                case SortByA.PriceDesc:
                    return View("ReportAResults", orderDets.OrderByDescending(o => o.BookPrice));
                case SortByA.Pop:
                    return View("ReportAResults", orderDets.OrderByDescending(o => o.Quantity));
            }
            return View("ReportAResults", orderDets);


            //if (Time == Time.MostRecent)
            //{
            //    query = query.OrderByDescending(o => o.Order.OrderDate).ThenBy(o => o.Order.OrderDate.TimeOfDay);
            //}
            //else
            //{
            //    query = query.OrderBy(o => o.Order.OrderDate).ThenBy(o => o.Order.OrderDate.TimeOfDay);
            //}

            //if (PMOrder == Orderofresults.Ascending)
            //{
            //    query = query.OrderBy(o => o.Margin);
            //}
            //else
            //{
            //    query = query.OrderByDescending(o => o.Margin);
            //}

            //if (PriceOrder == Orderofresults.Ascending)
            //{
            //    query = query.OrderBy(o => o.BookPrice);
            //}
            //else
            //{
            //    query = query.OrderByDescending(o => o.BookPrice);
            //}

            //if (Popularity == Orderofresults.Ascending)
            //{
            //    query = query.OrderBy(o => o.Quantity);
            //}

            //orderDets = query.Include(o => o.Book).Include(o => o.Order).Include(o => o.Order.AppUser).ToList();

            //return View("ReportAResults", orderDets);
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ReportB()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult DisplayReportB(SortByB Sort)
        {
            List<Order> orders = new List<Order>();
            var query = from o in _db.Orders
                        where o.OrderComplete == true
                        select o;

            orders = query.Include(o => o.AppUser).ToList();

            List<Order> orderWnav = new List<Order>();
            foreach (Order ord in orders)
            {
                Order order = _db.Orders.Include(o => o.AppUser).Include(o => o.OrderDets).ThenInclude(o => o.Book).FirstOrDefault(o => o.OrderID == ord.OrderID);
                if (order.NumberOfBooks == 0)
                {
                    order.WACC = 0;
                }
                else
                {
                    order.WACC = order.OrderCost / order.NumberOfBooks;
                }
                orderWnav.Add(order);
            }

            switch (Sort)
            {
                case SortByB.Recent:
                    return View("ReportBResults", orderWnav.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderDate.TimeOfDay));
                case SortByB.PMasc:
                    return View("ReportBResults", orderWnav.OrderBy(o => o.OrderMargin));
                case SortByB.PMdesc:
                    return View("ReportBResults", orderWnav.OrderByDescending(o => o.OrderMargin));
                case SortByB.PriceAsc:
                    return View("ReportBResults", orderWnav.OrderBy(o => o.OrderSubtotal));
                case SortByB.PriceDesc:
                    return View("ReportBResults", orderWnav.OrderByDescending(o => o.OrderSubtotal));
            }
            return View("ReportBResults", orderWnav);




            //if (Time == Time.MostRecent)
            //{
            //    query = query.OrderByDescending(o => o.OrderDate).ThenBy(o => o.OrderDate.TimeOfDay);
            //}
            //else
            //{
            //    query = query.OrderBy(o => o.OrderDate).ThenBy(o => o.OrderDate.TimeOfDay);
            //}

            //if (PMOrder == Orderofresults.Ascending)
            //{
            //    query = query.OrderBy(o => o.OrderMargin);
            //}
            //else
            //{
            //    query = query.OrderByDescending(o => o.OrderMargin);
            //}

            //if (PriceOrder == Orderofresults.Ascending)
            //{
            //    query = query.OrderBy(o => o.OrderSubtotal);
            //}
            //else
            //{
            //    query = query.OrderByDescending(o => o.OrderSubtotal);
            //}

            //orders = query.Include(o => o.AppUser).ToList();

            //List<Order> orderWnav = new List<Order>();
            //foreach (Order ord in orders)
            //{
            //    Order order = _db.Orders.Include(o => o.AppUser).Include(o => o.OrderDets).ThenInclude(o => o.Book).FirstOrDefault(o => o.OrderID == ord.OrderID);
            //    order.WACC = order.OrderCost / order.NumberOfBooks;
            //    orderWnav.Add(order);
            //}
            //return View("ReportBResults", orderWnav);


        }

        [Authorize(Roles = "Manager")]
        public IActionResult ReportC()
        {
            return View();
        }


        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> DisplayReportC(SortByC Sort)
        {

            List<AppUser> users = await GetAllCustomer();
            List<AppUser> userwOrders = new List<AppUser>();

            foreach (AppUser ui in users)
            {
                AppUser a = _db.Users.Include(u => u.Orders).ThenInclude(u => u.OrderDets).ThenInclude(u => u.Book).FirstOrDefault(u => u.UserName == ui.UserName);

                if (a.Orders.Count() != 0)
                {
                    userwOrders.Add(a);
                }

            }

            var query = from u in userwOrders
                        select u;
            List<AppUser> users1 = query.ToList();

            switch (Sort)
            {
                case SortByC.PMasc:
                    return View("ReportCResults", users1.OrderBy(o => o.ProfitMargin));
                case SortByC.PMdesc:
                    return View("ReportCResults", users1.OrderByDescending(o => o.ProfitMargin));
                case SortByC.RevAsc:
                    return View("ReportCResults", users1.OrderBy(o => o.TotalRev));
                case SortByC.RevDesc:
                    return View("ReportCResults", users1.OrderByDescending(o => o.TotalRev));
            }

            return View("ReportCResults", users1);


            //if (PMOrder == Orderofresults.Ascending)
            //{
            //    query = query.OrderBy(o => o.ProfitMargin);
            //}
            //else
            //{
            //    query = query.OrderByDescending(o => o.ProfitMargin);
            //}

            //if (PriceOrder == Orderofresults.Ascending)
            //{
            //    query = query.OrderBy(o => o.TotalRev);
            //}
            //else
            //{
            //    query = query.OrderByDescending(o => o.TotalRev);
            //}

            //List<AppUser> users1 = query.ToList();
            //List<AppUser> newList = new List<AppUser>();

            //return View("ReportCResults", users1);


        }


        [Authorize(Roles = "Manager")]
        public ActionResult ReportD()
        {
            ViewBag.Profit = GetTotalProfit();
            ViewBag.Cost = GetTotalCost();
            ViewBag.Revenue = GetTotalRevenue();
            return View();
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ReportE()
        {
            List<Book> books = new List<Book>();
            var bookstopass = _db.Books.ToList();
            books = bookstopass;
            ViewBag.InventoryValue = GetInventoryValue();
            return View(bookstopass);
        }

        public decimal GetInventoryValue()
        {
            decimal totalvalue = 0;
            List<Book> books = new List<Book>();
            var bookstopass = _db.Books.ToList();
            books = bookstopass;

            foreach (Book b in books)
            {
                decimal CurrentBookTotalCost = b.Cost * b.Inventory;
                totalvalue = totalvalue + CurrentBookTotalCost;
            }

            return totalvalue;
        }

        [Authorize(Roles = "Manager")]
        public ActionResult ReportF()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        public async Task<ActionResult> DisplayReportF(SortByF Sort)
        {
            List<AppUser> users = await GetAllEmployees();
            List<AppUser> employeeswReviews = new List<AppUser>();

            AppUser aa = new AppUser();
            _db.Add(aa);
            _db.SaveChanges();


            foreach (AppUser ui in users)
            {
                AppUser a = _db.Users.Include(u => u.ReviewsApproved).FirstOrDefault(u => u.UserName == ui.UserName);

                if (a.ReviewsApproved.Count() != 0)
                {
                    employeeswReviews.Add(a);
                }

            }

            var query = from u in employeeswReviews
                        select u;

            List<AppUser> users1 = query.ToList();

            foreach (AppUser use in users1)
            {
                int ReviewsApproved = 0;
                int ReviewsRejected = 0;
                List<Review> revs = _db.Reviews.Where(u => u.Approver.UserName == use.UserName).ToList();

                foreach (Review rev in revs)
                {
                    if (rev.Rejected == true)
                    {
                        ReviewsRejected++;
                    }
                    ReviewsApproved++;
                }

                use.NumReviewsApproved = ReviewsApproved;
                use.NumReviewsRejected = ReviewsRejected;
            }

            switch (Sort)
            {
                case SortByF.EmpID:
                    return View("ReportFResults", users1.OrderBy(o => o.UserName));
                case SortByF.AppAsc:
                    return View("ReportFResults", users1.OrderBy(o => o.NumReviewsApproved));
                case SortByF.AppDesc:
                    return View("ReportFResults", users1.OrderByDescending(o => o.NumReviewsApproved));
                case SortByF.RejAsc:
                    return View("ReportFResults", users1.OrderBy(o => o.NumReviewsRejected));
                case SortByF.RejDesc:
                    return View("ReportFResults", users1.OrderByDescending(o => o.NumReviewsRejected));
            }

            return View("ReportFResults", users1);


            //if (PMOrder == Orderofresults.Ascending)
            //{
            //    query = query.OrderBy(o => o.ProfitMargin);
            //}
            //else
            //{
            //    query = query.OrderByDescending(o => o.ProfitMargin);
            //}

            //if (PriceOrder == Orderofresults.Ascending)
            //{
            //    query = query.OrderBy(o => o.TotalRev);
            //}
            //else
            //{
            //    query = query.OrderByDescending(o => o.TotalRev);
            //}

            //List<AppUser> users1 = query.ToList();

            //foreach (AppUser use in users1)
            //{
            //    int ReviewsApproved = 0;
            //    int ReviewsRejected = 0;
            //    List<Review> revs = _db.Reviews.Where(u => u.Approver.UserName == use.UserName).ToList();

            //    foreach (Review rev in revs)
            //    {
            //        if (rev.Rejected == true)
            //        {
            //            ReviewsRejected++;
            //        }
            //        ReviewsApproved++;
            //    }

            //    use.NumReviewsApproved = ReviewsApproved;
            //    use.NumReviewsRejected = ReviewsRejected;
            //}

            //return View("ReportFResults", users1);


        }


        //it can't find the subtotal and the ordercost --> margin ends up being 0
        public decimal GetTotalProfit()
        {
            decimal TotalProfit = 0;

            List<Order> completedorders = _db.Orders.Include(o => o.OrderDets).Where(o => o.OrderComplete == true).ToList();

            foreach (Order o in completedorders)
            {
                Order ord = _db.Orders.First(m => m.OrderID == o.OrderID);
                TotalProfit = TotalProfit + ord.OrderMargin;
            }

            return TotalProfit;
        }

        public decimal GetTotalCost()
        {
            decimal TotalCost = 0;
            List<Order> completedorders = _db.Orders.Include(o => o.OrderDets).Where(o => o.OrderComplete == true).ToList();

            foreach (Order o in completedorders)
            {
                TotalCost = TotalCost + o.OrderCost;
            }
            return TotalCost;
        }

        public decimal GetTotalRevenue()
        {
            decimal TotalRev = 0;
            List<Order> completedorders = _db.Orders.Include(o => o.OrderDets).Where(o => o.OrderComplete == true).ToList();

            foreach (Order o in completedorders)
            {
                TotalRev = TotalRev + o.OrderSubtotal;
            }
            return TotalRev;
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
