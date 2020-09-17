using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team1FinalProject.DAL;
using Team1FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Team1FinalProject.Controllers;

namespace Team1FinalProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Orders
        [Authorize]
        public IActionResult Index()
        {
            List<Order> Orders = new List<Order>();

            if (User.IsInRole("Customer"))
            {
                Orders = _context.Orders.Include(o => o.OrderDets).Include(o=>o.OrderCard).Include(o => o.Coupon).Where(o => o.OrderComplete == true).Where(o => o.AppUser.UserName == User.Identity.Name).OrderBy(o =>o.OrderDate).ToList();
            }

            else
            {
              Orders =  _context.Orders.Include(o => o.OrderDets).Include(o => o.OrderCard).Include(o => o.Coupon).Where(o =>o.OrderComplete == true).OrderBy(o => o.OrderDate).ToList();
            }
        
            return View(Orders);
        }

        [Authorize(Roles ="Customer")]
        public IActionResult ShoppingCart()
        {
            if (_context.Orders.Where(o => o.AppUser.UserName == User.Identity.Name).Where(o=>o.OrderComplete == false).Count() == 0)
            {
                Order ord = new Order();

                string id = User.Identity.Name;
                AppUser user = _context.Users.FirstOrDefault(u => u.UserName == id);
                ord.AppUser = user;

                ord.OrderDate = DateTime.Now;
                _context.Add(ord);
                _context.SaveChanges();
            }

            Order order = _context.Orders.Include(o => o.OrderDets).ThenInclude(o => o.Book)
                .Where(o => o.AppUser.UserName == User.Identity.Name).Where(o => o.OrderComplete == false).First();

            //if(order.OrderDets.Count() != 0 && order.OrderDets != null)
            //{
            //    List<Book> BooksRemoved = new List<Book>();
            //    bool OutOfStock = false;
            //    foreach (OrderDet od in order.OrderDets)
            //    {

            //        if (od.Quantity < od.Book.Inventory)
            //        {
            //            BooksRemoved.Add(od.Book);
            //            OutOfStock = true;
            //            _context.Remove(od);
            //            _context.SaveChanges();
            //        }


            //    }
            //    _context.Update(order);
            //    _context.SaveChanges();

            //    if (OutOfStock == true)
            //    {
            //        return View("RemovalError", new string[] { BooksRemoved.ToString() + "were removed" });
            //    }

            //}

            int orderID = order.OrderID;
            order.ShipCost = CalcShipCost(orderID);
            _context.Update(order);
            _context.SaveChanges();
            return View(order);
        }

        // GET: Orders/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(o => o.OrderCard).Include(o => o.Coupon).Include(o => o.AppUser).Include(o =>o.OrderDets).ThenInclude(o=>o.Book)
                .FirstOrDefaultAsync(m => m.OrderID == id);

 

            if (order == null)
            {
                return NotFound();
            }

            //JACKIE - return view without edit buttons for orders completed
            //need to make status a boolean and change name to something intuitive
            //if (order.Status == false)
            //{
            //    return View("DetailsCompleted", new { id = id });
            //}
            order.ShipCost = CalcShipCost(id);

            return View(order);
        }

        // GET: Orders/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,OrderDate,OrderCost,OrderTotal,ShipCost,Status")] Order order)
        {
            order.OrderDate = System.DateTime.Today;
            string id = User.Identity.Name;
            AppUser user = _context.Users.FirstOrDefault(u => u.UserName == id);
            order.AppUser = user;

            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("AddToOrder", new { id = order.OrderID });
            }
            return View(order);
        }

        [Authorize]
        public IActionResult AddToOrder(int? id)
        {
            if (id == null)
            {
                return View("Error", new string[] { "You must specify an order to add!" });
            }
            Order ord = _context.Orders.Find(id);
            if (ord == null)
            {
                return View("Error", new string[] { "Order not found!" });
            }

            OrderDet od = new OrderDet() { Order = ord };

            ViewBag.AllBooks = GetAllBooks();
            return View("AddToOrder", od);

        }

        [HttpPost]
        [Authorize]
        public IActionResult AddToOrder(OrderDet od,int SelectedBook)
        {
            Book book = _context.Books.Find(SelectedBook);
            if (book.ActiveSell == true)
            {
                return View("AddError", new string[] { "Sorry! This book has discontinued selling." });
            }

            od.Book = book;

            Order ord = _context.Orders.Where(o => o.AppUser.UserName == User.Identity.Name).Where(o => o.OrderComplete == false).First();

            od.Order = ord;
            

            int bookInv = book.Inventory;

            if(od.Quantity > bookInv)
            {
                return View("AddError", new string[] { "Please select less than " + bookInv.ToString() });   
            }

           
            if (ModelState.IsValid)
            {

                od.TotalBookPrice = od.Book.Price * od.Quantity;
                od.TotalBookCost = od.Book.Cost * od.Quantity;

                _context.OrderDets.Add(od);
                _context.SaveChanges();
                return RedirectToAction("ShoppingCart", new { id = od.Order.OrderID });
            }
            ViewBag.AllBooks = GetAllBooks();
            return View("ShoppingCart",od);

        }

        // GET: Orders/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = _context.Orders
                                       .Include(o => o.OrderDets)
                                       .ThenInclude(o => o.Book)
                                        .FirstOrDefault(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }


        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Order order)
        {
            //Find the related registration in the database
            Order Dbord = _context.Orders.Find(order.OrderID);

            //Update the database
            _context.Orders.Update(Dbord);

            //Save changes
            _context.SaveChanges();

            //Go back to index
            return RedirectToAction(nameof(Index));

        }

        [Authorize]
        public IActionResult RemoveFromOrder(int? id)
        {
            if (id == null)
            {
                return View("Error", new string[] { "You need to specify an order id" });
            }

            Order ord = _context.Orders.Include(o => o.OrderDets).ThenInclude(o => o.Book).FirstOrDefault(o => o.OrderID == id);

            if (ord == null || ord.OrderDets.Count == 0)
            {
                //TODO: Need to make a view to distinguish between completed orders and Details
                return RedirectToAction("Details", new { id = id });
            }

            //pass the list of order details to the view
            return View(ord.OrderDets);
        }

        [Authorize]
        public IActionResult AddToCart(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var book = _context.Books.Include(o => o.OrderDets).ThenInclude(o => o.Order).FirstOrDefault(o => o.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            if (book.ActiveSell == true)
            {
                return View("AddError", new string[] { "Sorry! This book has discontinued selling." });
            }

            //wont even let u try to order a book if there's no inventory
            if (book.Inventory == 0)
            {
                return View("AddError", new string[] { "Sorry! This book is not in stock." });
            }

           

            Order curorder = _context.Orders.Include(o => o.OrderDets).ThenInclude(o => o.Book).Where(o => o.AppUser.UserName == User.Identity.Name)
                                            .Where(o => o.OrderComplete == false).FirstOrDefault();
            


            if (curorder == null)
            {
                Order ord = new Order();

                string username = User.Identity.Name;
                AppUser user = _context.Users.FirstOrDefault(u => u.UserName == username);

                ord.AppUser = user;
                OrderDet od1 = new OrderDet();
                od1.Order = ord;
                od1.Book = book;

                _context.Add(ord);
                _context.SaveChanges();
                return View(od1);
            }

            foreach(OrderDet ods in curorder.OrderDets)
            {
                if(ods.Book.BookID == id)
                {
                    return View("AlreadyInCartError", new string[] { "Item is already in the cart. Proceed to shopping cart to change quantity" });
                }

            }



            OrderDet od = new OrderDet();
            od.Order = curorder;
            od.Book = book;

            return View(od);
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddToCart(OrderDet od)
        {
            Book book = _context.Books.Find(od.Book.BookID);

            od.Book = book;

            Order ord = _context.Orders.Find(od.Order.OrderID); 

            od.Order = ord;

            od.TotalBookPrice = od.Book.Price * od.Quantity;


            int bookInv = book.Inventory;

            if (od.Quantity > bookInv)
            {
                return View("AddError", new string[] { "Please select less than " + bookInv.ToString() });
            }

            //katie added this to my invoice controller when there was no reason for the invoicedet modelstate to be invalid
            ModelState.Clear();
            this.TryValidateModel(od);

            if (ModelState.IsValid)
            {
                _context.OrderDets.Add(od);
                _context.SaveChanges();
                return RedirectToAction("ShoppingCart");
            }

            return View("ShoppingCart");

        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }

        private SelectList GetAllBooks()
        {
            //added a where clause to filter books based on inventory
            List<Book> Books = _context.Books.Where(b => b.Inventory > 0).ToList();
            SelectList allBooks = new SelectList(Books, "BookID", "Title");
            return allBooks;
        }

        private Decimal CalcShipCost(int? id)
        {
            var order = _context.Orders.Include(o => o.OrderDets).FirstOrDefault(o => o.OrderID == id);
            //var order = _context.Orders.Include(o => o.OrderDets).ThenInclude(o => o.Book).Where(o => o.OrderComplete == false).First();
            int intBooksQuant = order.OrderDets.Sum(rd => rd.Quantity);
            Decimal ShipCost = 0;
            if (intBooksQuant >= 1)
            {
                ShipCost = ShipCost + 3.5m;
                ShipCost = ShipCost + (intBooksQuant - 1) * 1.5m;
            }
            return ShipCost;
        }



    }
}
