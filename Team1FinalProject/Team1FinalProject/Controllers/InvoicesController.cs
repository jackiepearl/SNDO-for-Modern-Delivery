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
    [Authorize(Roles = "Manager")]
    public class InvoicesController : Controller
    {
        private readonly AppDbContext _context;

        public InvoicesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Invoices
        public async Task<IActionResult> Index()
        {
            var invoices = await _context.Invoices.Include(i => i.InvoiceDets).OrderByDescending(i => i.InvoiceID).ToListAsync();
            return View(invoices);
        }


        public IActionResult ManualReorder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Book book = _context.Books.Include(o => o.InvoiceDets)
                .ThenInclude(o => o.Invoice)
                .FirstOrDefault(o => o.BookID == id);
            if (book == null)
            {
                return NotFound();
            }

            //find an invoice that hasn't been completed
            Invoice curInv = _context.Invoices
                .FirstOrDefault(i => i.ProDatePlaced == default(DateTime));

            //if every invoice has been completed, create a new one
            if (curInv == null)
            {
                Invoice newInv = new Invoice();

                //assign user property here?
                //might not need to know who created the invoice since invoicedets has that info
                InvoiceDet idet = new InvoiceDet();
                idet.Book = book;
                idet.BookCost = book.Cost;
                idet.Invoice = newInv;
                _context.Add(newInv);
                _context.SaveChanges();
                return View(idet);
            }
            //if an invoice has not been completed yet
            else
            {
                InvoiceDet idet = new InvoiceDet();
                idet.Book = book;
                idet.BookCost = book.Cost;
                idet.Invoice = curInv;
                return View(idet);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ManualReorder(InvoiceDet idet)
        {
            Book book = _context.Books.Include(b => b.Genre)
                .Include(b => b.InvoiceDets)
                .ThenInclude(b => b.Invoice)
                .FirstOrDefault(b => b.BookID == idet.Book.BookID);
            //need to change this to add to weighted average
            book.Cost = idet.BookCost;
            idet.Book = book;
            idet.QuantityArrived = 0;
            Invoice inv = _context.Invoices.Find(idet.Invoice.InvoiceID);
            idet.Invoice = inv;
            AppUser user = _context.Users.FirstOrDefault(o => o.UserName == User.Identity.Name);
            idet.User = user;

            ModelState.Clear();
            this.TryValidateModel(idet);

            if (ModelState.IsValid)
            {
                _context.InvoiceDets.Add(idet);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = idet.Invoice.InvoiceID });
            }

            return View(idet);
        }


        public IActionResult AutoReorder()
        {

            ViewBag.Books = GetAutoReorderBooks();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AutoReorder(int[] SelectedBooks, int QuantityOrdered)
        {
            Invoice i = new Invoice();
            foreach (int bookid in SelectedBooks)
            {
                Book book = _context.Books.FirstOrDefault(b => b.BookID == bookid);
                InvoiceDet ivd = new InvoiceDet();
                ivd.Invoice = i;
                ivd.Book = book;
                ivd.QuantityOrdered = QuantityOrdered;
                ivd.BookCost = book.Cost;
                ivd.QuantityArrived = 0;
                AppUser user = _context.Users.FirstOrDefault(o => o.UserName == User.Identity.Name);
                ivd.User = user;

                if (ModelState.IsValid)
                {
                    _context.InvoiceDets.Add(ivd);
                    _context.SaveChanges();
                }
            }

            i.ProDatePlaced = DateTime.Today;
            _context.Update(i);
            _context.SaveChanges();
            return RedirectToAction("Details", new { id = i.InvoiceID });
        }

        private MultiSelectList GetAutoReorderBooks()
        {
            var books = _context.Books.Include(b => b.InvoiceDets).Where(b => b.Inventory < b.ReorderLevel);
            List<Book> BooksToOrder = new List<Book>();
            foreach (Book b in books)
            {
                Int32 qtyord = 0;
                Int32 qtyarr = 0;
                var idets = _context.InvoiceDets.Include(i => i.Book).Where(i => i.Book.BookID == b.BookID);

                foreach (InvoiceDet id in idets)
                {
                    qtyord = qtyord + id.QuantityOrdered;
                    qtyarr = qtyarr + id.QuantityArrived;
                }

                Int32 futureInv = b.Inventory + qtyord - qtyarr;

                if (futureInv < b.ReorderLevel)
                {
                    BooksToOrder.Add(b);
                }
            }

            MultiSelectList booklist = new MultiSelectList(BooksToOrder, "BookID", "Title");
            return booklist;
        }


        // GET: Invoices/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = _context.Invoices
                .Include(i => i.InvoiceDets)
                .ThenInclude(i => i.Book)
                .FirstOrDefault(i => i.InvoiceID == id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: Invoices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceID,ProDatePlaced,ProDateArrived")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = _context.Invoices
                .Include(i => i.InvoiceDets)
                .ThenInclude(i => i.Book)
                .FirstOrDefault(i => i.InvoiceID == id);
            if (invoice == null)
            {
                return NotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InvoiceID,ProDatePlaced,ProDateArrived")] Invoice invoice)
        {
            if (id != invoice.InvoiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceID))
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
            return View(invoice);
        }

        // GET: Invoices/SubmitInvoice/5
        public IActionResult SubmitInvoice(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = _context.Invoices
                .Include(i => i.InvoiceDets)
                .ThenInclude(i => i.Book)
                .FirstOrDefault(i => i.InvoiceID == id);

            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: Invoices/SubmitInvoice/5
        [HttpPost, ActionName("SubmitInvoice")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitInvoiceConfirmed(int id)
        {
            var invoice = _context.Invoices
                .Include(i => i.InvoiceDets)
                .ThenInclude(i => i.Book)
                .FirstOrDefault(i => i.InvoiceID == id);
            invoice.ProDatePlaced = DateTime.Today;
            _context.Invoices.Update(invoice);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceExists(int id)
        {
            return _context.Invoices.Any(e => e.InvoiceID == id);
        }


        private SelectList GetAllBooks()
        {
            List<Book> Books = _context.Books.ToList();
            SelectList allBooks = new SelectList(Books, "BookID", "Title");
            return allBooks;
        }
    }
}
