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
using Team1FinalProject.Utilities;


namespace Team1FinalProject.Controllers
{
    public class CheckOutController : Controller
    {
        private AppDbContext _context;

        public CheckOutController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Customer")]
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                return View("Error", new string[] { "Order not found" });
            }
            var order = _context.Orders.Include(o => o.OrderDets).ThenInclude(o => o.Book).FirstOrDefault(o=>o.OrderID == id);

            if(order.OrderDets.Count() == 0)
            {
                return View("Error", new string[] { "Must have something in your cart before you checkout" });
            }

            order.ShipCost = CalcShipCost();
            ViewBag.Cards = GetAllCards();
            return View(order);
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult Index(int id, int SelectedCard, string SearchCoupon)
        {
            Order ord = _context.Orders.Include(o=>o.AppUser).Include(o=>o.OrderDets).ThenInclude(o=>o.Book).Include(o => o.Coupon).FirstOrDefault(o=>o.OrderID == id);

            if (SearchCoupon != null)
            {
                Coupon coupon = _context.Coupons.FirstOrDefault(c => c.CouponCode == SearchCoupon);
                DateTime now = DateTime.Now;
                if (coupon == null)
                {
                    ord.ShipCost = CalcShipCost();
                }
                else
                {
                    //List<Order> prevOrders = _context.Orders.Include(o => o.Coupon).Where(o => o.AppUser.UserName == User.Identity.Name).ToList();
                    //foreach (Order o in prevOrders)
                    //{
                    //    if (o.Coupon.CouponCode != null)
                    //    {
                    //        if(o.Coupon.CouponCode != SearchCoupon)
                    //        {
                    //            return View("Error", new string[] { "You've used this coupon before!" });
                    //        }
                    //    }
                    //}
                    if (coupon.ActiveStatus == true)
                    {
                        if (now <= coupon.EndDate && now >= coupon.BeginDate)
                        {
                            if (coupon.FreeShip == true)
                            {
                                if (ord.OrderSubtotal >= coupon.BaselineValue)
                                {
                                    ord.ShipCost = 0;
                                }
                                else
                                {
                                    ord.ShipCost = CalcShipCost();
                                }
                            }
                            else
                            {
                                List<OrderDet> orderDets = ord.OrderDets.ToList();
                                //Connect the books
                                foreach (OrderDet od in orderDets)
                                {
                                    od.TotalBookPrice = od.TotalBookPrice - (((coupon.PercentOff) / 100) * od.TotalBookPrice);
                                    _context.Update(od);
                                    _context.SaveChanges();
                                }
                                ord.OrderSubtotal = ord.OrderSubtotal - (((coupon.PercentOff) / 100) * ord.OrderSubtotal);
                                ord.Coupon = coupon;
                                ord.ShipCost = CalcShipCost();

                                _context.Update(ord);
                                _context.SaveChanges();
                            }
                        }
                    }
                
                    
                }
                
                
            }
            if (SearchCoupon == null)
            {
                ord.ShipCost = CalcShipCost();
            }

            CreditCard card = _context.CreditCards.FirstOrDefault(o => o.CreditCardID == SelectedCard);

            ord.OrderCard = card;
            ViewBag.UserAddress = GetUserAddress();

            _context.Update(ord);
            _context.SaveChanges();
        
            return RedirectToAction("ReviewOrder",ord);
        }

        [Authorize(Roles ="Customer")]
        public IActionResult ReviewOrder(Order ord)
        {
            Order order = _context.Orders.Include(o=>o.AppUser).Include(o=>o.OrderCard).Include(o => o.OrderDets).ThenInclude(o => o.Book).FirstOrDefault(o => o.OrderID == ord.OrderID);
            
            ViewBag.UserAddress = GetUserAddress();
            return View(order);
        
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public IActionResult ReviewOrder(int? id)
        {
            if (id != null)
            {
                Order order = _context.Orders.Include(o => o.OrderDets).ThenInclude(o => o.Book).FirstOrDefault(o => o.OrderID == id);

                string emailsubject = "Your Order is on the way!";
                string uid = User.Identity.Name;
                AppUser user = _context.Users.FirstOrDefault(u => u.UserName == uid);
                string emailrecipient = user.Email;
                OrderDet FirstorderDet = order.OrderDets.FirstOrDefault();
                int ordDetID = FirstorderDet.OrderDetID;
                string emailbody = "We also recommend you checkout these titles: " + GenerateBookRecommendations(ordDetID);

                Utilities.SendEmail.Send_Email(emailrecipient, emailsubject, emailbody);

                order.OrderComplete = true;
                

                //removing the ordered books from the database
                List<OrderDet> DetailsforBooks = new List<OrderDet>();
                DetailsforBooks = order.OrderDets.ToList();

                foreach (OrderDet O in DetailsforBooks)
                {
                    int intAmttoReduce = O.Quantity;
                    Book book = O.Book;
                    book.Inventory = book.Inventory - intAmttoReduce;

                    _context.Update(book);
                    _context.SaveChanges();
                }

                _context.Update(order);
                _context.SaveChanges();
            }

            Order order1 = _context.Orders.Include(o => o.OrderDets).ThenInclude(o => o.Book).FirstOrDefault(o => o.OrderID == id);
            OrderDet FirstorderDet1 = order1.OrderDets.FirstOrDefault();
            int ordDetID1 = FirstorderDet1.OrderDetID;

            ViewBag.Recommendations = GenerateBookRecommendations(ordDetID1);
            
            return View("CompleteOrder");
        }


        private SelectList GetAllCards()
        {
            List<CreditCard> Cards = new List<CreditCard>();
            Cards = _context.CreditCards.Where(c => c.User.UserName == User.Identity.Name).ToList();
            SelectList allCards = new SelectList(Cards, "CreditCardID", "CreditCardNumber");
            return allCards;
           
        }

        private Decimal CalcShipCost()
        {
            var order = _context.Orders.Include(o => o.OrderDets).ThenInclude(o => o.Book).Where(o => o.OrderComplete == false).First();
            int intBooksQuant = order.OrderDets.Sum(rd => rd.Quantity);
            Decimal ShipCost = 0;
            if (intBooksQuant > 0)
            {
                ShipCost = ShipCost + 3.5m;
                ShipCost = ShipCost + (intBooksQuant - 1) * 1.5m;
            }
            return ShipCost;
        }

        private String GetUserAddress()
        {
            string id = User.Identity.Name;
            AppUser user = _context.Users.FirstOrDefault(u => u.UserName == id);
            return user.StAddress + ", " + user.City + ", " + user.State + "  " + user.ZipCode;
        }

        private String GenerateEmailMessage()
        {
            var order = _context.Orders.Include(o => o.OrderDets).ThenInclude(o => o.Book).Where(o => o.OrderComplete == false).First();
            string OrderMessage = "Your Order is Complete!";

            //Link with GenerateBookRecommendations and add to OrderMessage
            return OrderMessage;
        }

        private String GenerateBookRecommendations(int? id)
        {
            List<Book> BooksToRec = new List<Book>();
            OrderDet od = _context.OrderDets.Include(o => o.Book.Genre).FirstOrDefault(o => o.OrderDetID == id);
            Book OrderedBook = od.Book;
            Genre OrderedGenre = OrderedBook.Genre;
            
            var old_orderDetails = _context.OrderDets.Where(m => m.Order.OrderComplete == true)
                .Where(o => o.Order.AppUser.UserName == User.Identity.Name).Include(m => m.Book).ToList();
            List<Book> DoNotRec = new List<Book>();
            foreach (OrderDet ord in old_orderDetails)
            {
                DoNotRec.Add(ord.Book);
            }

            var BooksSameGenreAuthor = _context.Books.Where(b => b.Genre == OrderedBook.Genre)
                .Where(b => b.Author == OrderedBook.Author).ToList();
            var maxValue = BooksSameGenreAuthor.Max(x => x.AvgRating);
            Book HighestRated = BooksSameGenreAuthor.First(x => x.AvgRating == maxValue);
            if (DoNotRec.Contains(HighestRated) == false)
            {
                BooksToRec.Add(HighestRated);
            }
            var BooksSameGenre = _context.Books.Where(b => b.Genre == OrderedBook.Genre)
                .Where(b => b.Author != OrderedBook.Author).ToList();
            var maxValue2 = BooksSameGenre.Max(x => x.AvgRating);
            Book HighestRated2 = BooksSameGenre.First(x => x.AvgRating == maxValue2);

            if ((HighestRated2.AvgRating >= 4) && (DoNotRec.Contains(HighestRated2) == false))
            {
                BooksToRec.Add(HighestRated2);
                BooksSameGenre.Remove(HighestRated2);
                DoNotRec.Add(HighestRated2);
            }

            var maxValue3 = BooksSameGenre.Max(x => x.AvgRating);
            Book HighRated3 = BooksSameGenre.First(x => x.AvgRating == maxValue3);

            if ((HighRated3.AvgRating >= 4) && (DoNotRec.Contains(HighRated3) == false) && (HighestRated2.Author != HighRated3.Author))
            {
                BooksToRec.Add(HighRated3);
                BooksSameGenre.Remove(HighRated3);
                DoNotRec.Add(HighRated3);
            }

            if (BooksToRec.Count() == 3)
            {
                string final_recs = "";
                foreach (Book b in BooksToRec)
                {
                    final_recs = final_recs + b.Title.ToString() + "      ";
                }

                return final_recs;
            }

            BooksSameGenre = BooksSameGenre.Where(x => x.AvgRating >= 4).ToList();

            foreach (Book b in BooksSameGenre)
            {
                if (DoNotRec.Contains(b) == false)
                {
                    BooksToRec.Add(b);
                    DoNotRec.Add(b);
                    if (BooksToRec.Count() == 3)
                    {
                        string final_recs = "";
                        foreach (Book bb in BooksToRec)
                        {
                            final_recs = final_recs + bb.Title.ToString() + "      ";
                        }

                        return final_recs;
                    }
                }
            }

            var GenreBooks = _context.Books.Where(b => b.Genre == OrderedBook.Genre).ToList();
            foreach (Book b in GenreBooks)
            {
                if (DoNotRec.Contains(b) == false)
                {
                    BooksToRec.Add(b);
                    DoNotRec.Add(b);
                    if (BooksToRec.Count() == 3)
                    {
                        string final_recs = "";
                        foreach (Book bb in BooksToRec)
                        {
                            final_recs = final_recs + bb.Title.ToString() + "      ";
                        }

                        return final_recs;
                    }
                }
            }

            List<Book> books = _context.Books.ToList();
            while (BooksToRec.Count() < 3)
            {
                var maxRate = books.Max(x => x.AvgRating);
                Book b = books.First(x => x.AvgRating == maxRate);
                books.Remove(b);
                if (DoNotRec.Contains(b) == false)
                {
                    BooksToRec.Add(b);
                    DoNotRec.Add(b);
                    if (BooksToRec.Count() == 3)
                    {
                        string final_recs = "";
                        foreach (Book bb in BooksToRec)
                        {
                            final_recs = final_recs + bb.Title.ToString() + "      ";
                        }

                        return final_recs;
                    }
                }
            }

            string recs = "";
            foreach (Book bb in BooksToRec)
            {
                recs = recs + bb.Title.ToString() + "\n";
            }

            return recs;








            ////list of books that'll be passed back
            //List<Book> BooksToRec = new List<Book>();

            ////order details of existing order
            //OrderDet od = _context.OrderDets.Include(o => o.Book.Genre).FirstOrDefault(o => o.OrderDetID == id);
            //string Author = od.Book.Author;

            ////List to find old order details
            //var old_orderDetails = _context.OrderDets.Where(m => m.Order.OrderComplete == true).Where(o => o.Order.AppUser.UserName == User.Identity.Name).Include(m => m.Book).ToList();
            //List<OrderDet> old_ods = old_orderDetails;
            //List<Book> PrevBoughtBooks = new List<Book>();

            //List<Book> BookswithSameAuthor = new List<Book>();
            //var sameAuthor = _context.Books.Where(b => b.Author == Author).ToList();
            //BookswithSameAuthor = sameAuthor;

            //foreach (OrderDet old_od in old_ods)
            //{
            //    Book tempbook = old_od.Book;
            //    PrevBoughtBooks.Add(tempbook);
            //}

            ////distinct previously bought books
            //List<Book> prevDistinctBooks = PrevBoughtBooks.Distinct().ToList();

            ////Adding books with Same Author & Genre
            //while (BooksToRec.Count() < 3)
            //{

            //    Genre genre = od.Book.Genre;
            //    string genrename = genre.GenreName.ToString();
            //    var books = _context.Books.Where(o => o.Genre.GenreName == genrename).ToList();
            //    List<Book> authorbookstorate = new List<Book>();
            //    foreach (Book bok in BookswithSameAuthor)
            //    {
            //        if (books.Contains(bok))
            //        {
            //            authorbookstorate.Add(bok);

            //        }

            //    }
            //    List<Book> ratedbooks = new List<Book>();
            //    var query = from b in authorbookstorate
            //                select b;
            //    query = query.OrderByDescending(b => b.AvgRating);
            //    ratedbooks = ratedbooks.ToList();
            //    foreach (Book book1 in ratedbooks)
            //    {
            //        if (!prevDistinctBooks.Contains(book1))
            //        {
            //            if (!BooksToRec.Contains(book1))
            //            {
            //                BooksToRec.Add(book1);
            //            }
            //        }
            //    }

            //}

            ////if top list isn't filled find books of same genre with different authors where rating > 4
            //while (BooksToRec.Count() < 3)
            //{
            //    Genre genre = od.Book.Genre;
            //    string genrename = genre.GenreName.ToString();
            //    var books = _context.Books.Where(o => o.Genre.GenreName == genrename).ToList();
            //    foreach (Book b in books)
            //    {
            //        List<String> Authors = new List<string>();
            //        Authors.Add(b.Author);
            //        List<String> usedAuthors = new List<string>();
            //        foreach (var a in Authors)
            //        {
            //            if (b.Author == a)
            //            {
            //                if (!prevDistinctBooks.Contains(b))
            //                {
            //                    if (!usedAuthors.Contains(a))
            //                    {
            //                        if (!BooksToRec.Contains(b))
            //                        {
            //                            if (b.AvgRating >= 4)
            //                            {
            //                                BooksToRec.Add(b);
            //                                usedAuthors.Add(a);
            //                            }

            //                        }
            //                    }


            //                }
            //            }
            //        }


            //    }
            //}

            ////if list isn't filled yet, add whatever is left in the genre
            //while (BooksToRec.Count() < 3)
            //{
            //    string genre = od.Book.Genre.GenreName;
            //    var books = _context.Books.Where(o => o.Genre.GenreName == genre).ToList();
            //    foreach (Book b in books)
            //    {
            //        if (!prevDistinctBooks.Contains(b))
            //        {
            //            if (!BooksToRec.Contains(b))
            //            {
            //                BooksToRec.Add(b);
            //            }

            //        }

            //    }
            //}

            ////if list isn't filled yet, just add highest rated books
            //if (BooksToRec.Count() < 3)
            //{
            //    List<Book> unorderedRemainingBooks = new List<Book>();
            //    List<Book> orderedRemainingBooks = new List<Book>();

            //    var books = _context.Books.ToList();
            //    unorderedRemainingBooks = books;
            //    var query = from b in unorderedRemainingBooks
            //                select b;
            //    query = query.OrderByDescending(b => b.AvgRating);
            //    orderedRemainingBooks = query.ToList();

            //    while (BooksToRec.Count() <= 3)
            //    {
            //        foreach (Book bok in orderedRemainingBooks)
            //        {
            //            if (!prevDistinctBooks.Contains(bok))
            //            {
            //                if (!BooksToRec.Contains(bok))
            //                {
            //                    BooksToRec.Add(bok);
            //                }
            //            }
            //        }
            //    }
            //}


            //string final_recs = "";
            //foreach (Book b in BooksToRec)
            //{
            //    final_recs = final_recs + b.Title.ToString() + "   ";
            //}


            //return final_recs;
        }

    }

}