using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team1FinalProject.DAL;
using Team1FinalProject.Models;

namespace Team1FinalProject.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly AppDbContext _context;

        public ReviewsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public IActionResult Index()
        {
            List<Review> Reviews = new List<Review>();

            if(User.IsInRole("Customer"))
            {
       
                Reviews = _context.Reviews.Include(r =>r.Book).Include(r =>r.Author).Where(r=> r.Author.UserName == User.Identity.Name).ToList();
            }

            if (User.IsInRole("Employee") || User.IsInRole("Manager"))
            {
                Reviews = _context.Reviews.Where(r => r.Approver != null).ToList();

            }
            return View(Reviews);
        }

        public IActionResult ReviewPending()
        {
            List<Review> Reviews = new List<Review>();

            Reviews = _context.Reviews.Where(r => r.Approver == null).ToList();

            return View(Reviews);

        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                .FirstOrDefaultAsync(m => m.ReviewID == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        [Authorize(Roles ="Customer")]
        public IActionResult Create()
        {
            ViewBag.prevBooks = GetPurchasedBooks();
            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Customer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int SelectedBook, Decimal SelectedRating,[Bind("ReviewID,ReviewContent,Rating")] Review review)
        {

            Review rev = _context.Reviews.Include(r => r.Author).ThenInclude(r => r.ReviewsWritten).Where(r =>r.Author.UserName == User.Identity.Name).FirstOrDefault(r => r.Id_book == SelectedBook);
            int numID = SelectedBook;

            if (rev == null)
            {
                Review revToAdd = new Review();
                
                revToAdd.Id_book = numID;
                revToAdd.Rating = SelectedRating;
                revToAdd.ReviewContent = review.ReviewContent;
                revToAdd.Rejected = false;


                string id = User.Identity.Name;
                AppUser user = _context.Users.FirstOrDefault(u => u.UserName == id);

                revToAdd.Author = user;

                _context.Add(revToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                           
            }
            else
            {
                return View("Error", new string[] { "You can only review a book once!" });
            }

          
        }

        // GET: Reviews/Edit/5
        [Authorize(Roles = "Employee, Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Review review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Employee,Manager")]
        public async Task<IActionResult> Edit(int ApprovalStatus, int id, [Bind("ReviewID,ReviewContent,Rating")] Review review)
        {
            Review userRev = _context.Reviews.Include(r=>r.Author).FirstOrDefault(r =>r.ReviewID == id);

            if (id != review.ReviewID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (ApprovalStatus == 0)
                {
                    try
                    {
                        string currentUserName = User.Identity.Name;
                        AppUser approver = _context.Users.FirstOrDefault(x => x.UserName == currentUserName);
                        userRev.Approver = approver;
                        Book book = _context.Books.FirstOrDefault(b => b.BookID == userRev.Id_book);
                        userRev.ReviewContent = review.ReviewContent;
                        userRev.Rejected = false;
                        book.ReviewsApproved.Add(userRev);

                        _context.Update(userRev);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReviewExists(review.ReviewID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                if (ApprovalStatus == 1)
                {
                    try
                    {
                        string currentUserName = User.Identity.Name;
                        AppUser rejecter = _context.Users.FirstOrDefault(x => x.UserName == currentUserName);
                        userRev.Approver = rejecter;
                        userRev.Rejected = true;
                        userRev.ReviewContent = review.ReviewContent;
                        _context.Update(userRev);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ReviewExists(review.ReviewID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(review);
        }

        //// GET: Reviews/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var review = await _context.Reviews
        //        .FirstOrDefaultAsync(m => m.ReviewID == id);
        //    if (review == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(review);
        //}

        //// POST: Reviews/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var review = await _context.Reviews.FindAsync(id);
        //    _context.Reviews.Remove(review);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.ReviewID == id);
        }

        private SelectList GetPurchasedBooks()
        {
            List<Order> prevord = new List<Order>();
            prevord = _context.Orders.Include(o => o.OrderDets).ThenInclude(o => o.Book).Where(o => o.AppUser.UserName == User.Identity.Name).Where(o => o.OrderComplete == true).ToList();
            List<Book> prevbooks = new List<Book>();
            foreach (Order ord in prevord)
            {
                List<OrderDet> ods = new List<OrderDet>();
                ods = ord.OrderDets.ToList();
                foreach (OrderDet od in ods)
                {
                    Book book = od.Book;

                    if(!prevbooks.Contains(book))
                    {
                        prevbooks.Add(book);
                    }
                }
            }

            SelectList prevBooks = new SelectList(prevbooks, "BookID", "Title");
            return prevBooks;
          
        }
    }
}
