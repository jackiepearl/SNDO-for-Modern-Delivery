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

namespace Team1FinalProject.Controllers
{
    public enum Sort { SortTitle, SortAuthor, SortPopular, SortDateOldest, SortDateNewest, SortRating, InStock }

    public class BooksController : Controller
    {
        private AppDbContext _db;

        public BooksController(AppDbContext context)
        {
            _db = context;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            List<Book> AllBooks = new List<Book>();
            //var query = from b in _db.Books select b;
            AllBooks = _db.Books.Include(b => b.ReviewsApproved).ToList();

            ViewBag.TotalBookCount = _db.Books.Count();
            ViewBag.Display = "Displaying " + ViewBag.TotalBookCount.ToString()
                              + " out of " + ViewBag.TotalBookCount.ToString() + " books";

            return View("Index", AllBooks);
        }


        public ActionResult DetailedSearch()
        {
            ViewBag.AllGenres = GetAllGenres();
            return View();
        }

        public ActionResult DisplaySearchResults(String SearchTitle, Sort SortBy, String SearchAuthor,
                                        int SelectedGenre, String SearchUnique, Boolean StockSort)
        {


            List<Book> SelectedBooks;

            var query = from b in _db.Books select b;

            if (SearchTitle != null && SearchTitle != "")
            {
                query = query.Where(b => b.Title.Contains(SearchTitle));
            }

            if (SearchAuthor != null && SearchAuthor != "")
            {
                query = query.Where(b => b.Author.Contains(SearchAuthor));
            }

            if (SelectedGenre != 0)
            {
                query = query.Where(b => b.Genre.GenreID == SelectedGenre);
            }

            if (SearchUnique != null && SearchUnique != "")
            {
                Decimal decUnique;
                try
                {
                    decUnique = Convert.ToDecimal(SearchUnique);
                }
                catch
                {
                    ViewBag.Message = SearchUnique + " is not a valid unique number";
                    ViewBag.AllGenres = GetAllGenres();
                    return View("DetailedSearch");
                }

                query = query.Where(b => b.UniqueNum == decUnique);
            }

            if (StockSort == true)
            {
                query = query.Where(b => b.Inventory > 0);
            }

            SelectedBooks = query.Include(b => b.Genre)
                .Include(b => b.ReviewsApproved)
                .Include(b => b.OrderDets)
                .ToList();

            ViewBag.SelectedCount = SelectedBooks.Count;
            ViewBag.TotalBookCount = _db.Books.Count();
            ViewBag.Display = "Displaying " + ViewBag.SelectedCount.ToString()
                              + " out of " + ViewBag.TotalBookCount.ToString() + " books";
            switch (SortBy)
            {
                case Sort.SortTitle:
                    return View("Index", SelectedBooks.OrderBy(x => x.Title));
                case Sort.SortAuthor:
                    return View("Index", SelectedBooks.OrderBy(x => x.Author));
                case Sort.SortPopular:
                    return View("Index", SelectedBooks.OrderBy(x => x.AvgRating));
                case Sort.SortDateNewest:
                    return View("Index", SelectedBooks.OrderByDescending(x => x.PublishDate));
                case Sort.SortDateOldest:
                    return View("Index", SelectedBooks.OrderBy(x => x.PublishDate));
                case Sort.SortRating:
                    return View("Index", SelectedBooks.OrderByDescending(x => x.AvgRating));
            }
            return View("Index",SelectedBooks);
            
        }

        //GET: Books/Details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //TODO: add in reviews later on
            var book = _db.Books
                .Include(r => r.ReviewsApproved)
                .Include(g => g.Genre)
                .FirstOrDefault(b => b.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        //JACKIE - added create and edit methods
        //need to only allow managers to create and edit
        // GET: Books1/Create
        [Authorize(Roles = "Manager")]
        public IActionResult Create()
        {
            ViewBag.AllGenres = GetAllGenresMustSelectOne();
            return View();
        }

        // POST: Books1/Create
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int SelectedGenre, [Bind("BookID,Title,Author,PublishDate,Description,ReorderLevel,Cost,Price,ActiveSell")] Book book)
        {
            book.UniqueNum = GenerateNextUnique.GetNextUnique(_db);
            book.Inventory = 0;
            book.Genre = _db.Genres.Find(SelectedGenre);

            if (ModelState.IsValid)
            {
                _db.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.AllGenres = GetAllGenresMustSelectOne();
            return View(book);
        }

        // GET: Books1/Edit/5
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books
                .Include(g => g.Genre)
                .FirstOrDefaultAsync(b => b.BookID == id);

            if (book == null)
            {
                return NotFound();
            }

            ViewBag.AllGenres = GetAllGenresMustSelectOne();
            return View(book);
        }

        // POST: Books1/Edit/5
        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int SelectedGenre, [Bind("BookID,Title,Author,PublishDate,Description,Inventory,ReorderLevel,Cost,Price,ActiveSell")] Book book)
        {
            if (id != book.BookID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Book dbBook = await _db.Books
                        .Include(g => g.Genre)
                        .FirstOrDefaultAsync(b => b.BookID == book.BookID);

                    if (dbBook.ActiveSell == false && book.ActiveSell == true)
                    {
                        List<AppUser> UsersWithBookInCart = new List<AppUser>();
                        List<AppUser> Customers = _db.Users.Include(u => u.Orders).Where(o => o.Orders.Count() > 0).ToList();


                        foreach (AppUser cust in Customers)
                        {
                            Boolean bolUserHas = false;
                            List<Order> orders = _db.Orders.Include(o => o.AppUser).Where(u => u.AppUser.UserName == cust.UserName).ToList();
                            foreach (Order ord in orders.Where(r => r.OrderComplete == false))
                            {
                                List<OrderDet> ods = _db.OrderDets.Include(b => b.Book).ToList();
                                foreach (OrderDet od in ods)
                                {

                                    if (od.Book.BookID == book.BookID)
                                    {
                                        bolUserHas = true;

                                    }
                                }

                            }
                            if (bolUserHas == true)
                            {
                                UsersWithBookInCart.Add(cust);
                            }

                        }

                        foreach (AppUser us in UsersWithBookInCart)
                        {
                            string subjectline = "Discontinued Book";
                            string body = "One of the books in your shopping cart has been discontinued. Your shopping cart has been updated.";
                            Utilities.SendEmail.Send_Email(us.Email, subjectline, body);


                            List<Order> orders = _db.Orders.Include(o => o.AppUser).Where(u => u.AppUser.UserName == us.UserName).ToList();
                            foreach (Order ord in orders.Where(r => r.OrderComplete == false))
                            {
                                List<OrderDet> ods = _db.OrderDets.Include(b => b.Book).ToList();
                                foreach (OrderDet od in ods)
                                {

                                    if (od.Book.BookID == book.BookID)
                                    {

                                        OrderDet ood = _db.OrderDets.FirstOrDefault(p => p.OrderDetID == od.OrderDetID);
                                        _db.Remove(ood);
                                        _db.SaveChanges();

                                    }
                                }


                            }
                        }
                    }


                    dbBook.BookID = book.BookID;
                    dbBook.Title = book.Title;
                    dbBook.Author = book.Author;
                    dbBook.PublishDate = book.PublishDate;
                    dbBook.Description = book.Description;
                    dbBook.Inventory = book.Inventory;
                    dbBook.ReorderLevel = book.ReorderLevel;
                    dbBook.Cost = book.Cost;
                    dbBook.Price = book.Price;
                    dbBook.ActiveSell = book.ActiveSell;
                    dbBook.Genre = _db.Genres.Find(SelectedGenre);

                    _db.Update(dbBook);
                    _db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.BookID))
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
            ViewBag.AllGenres = GetAllGenresMustSelectOne();
            return View(book);
        }

        private bool BookExists(int id)
        {
            return _db.Books.Any(e => e.BookID == id);
        }

        //JACKIE - method to allow a manager to add a new genre
        [Authorize(Roles = "Manager")]
        public IActionResult AddNewGenre()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewGenre(String GenreString)
        {
            if (GenreString != null && GenreString != "")
            {
                Genre genre = new Genre();
                genre.GenreName = GenreString;
                _db.Add(genre);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        public SelectList GetAllGenres()
        {
            List<Genre> Genres = _db.Genres.ToList();
            Genre SelectNone = new Genre() { GenreID = 0, GenreName = "All Genres" };
            Genres.Add(SelectNone);
            SelectList AllGenres = new SelectList(Genres.OrderBy(g => g.GenreID), "GenreID", "GenreName");
            return AllGenres;
        }

        //JACKIE - needed to make another method for populating the viewbag to create
        //an order (can't have the option to not select a genre like in the method above)
        public SelectList GetAllGenresMustSelectOne()
        {
            List<Genre> Genres = _db.Genres.ToList();
            SelectList AllGenres = new SelectList(Genres.OrderBy(g => g.GenreID), "GenreID", "GenreName");
            return AllGenres;
        }
    }
}
