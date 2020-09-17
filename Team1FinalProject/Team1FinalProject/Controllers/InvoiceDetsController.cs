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
    [Authorize(Roles="Manager")]
    public class InvoiceDetsController : Controller
    {
        private readonly AppDbContext _context;

        public InvoiceDetsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult BooksOnOrder()
        {
            var query = from i in _context.InvoiceDets select i;
            query = query.Where(i => i.QuantityArrived < i.QuantityOrdered);
            query = query.Where(i => i.Invoice.ProDatePlaced != default(DateTime));
            query = query.OrderByDescending(i => i.Invoice.InvoiceID);
            List<InvoiceDet> BooksOnOrder = query
                .Include(v => v.Invoice)
                .Include(v => v.Book)
                .Include(v => v.User)
                .ToList();
            return View("BooksOnOrder", BooksOnOrder);
        }

        // GET: InvoiceDets
        public async Task<IActionResult> Index()
        {
            return View(await _context.InvoiceDets.ToListAsync());
        }

        // GET: InvoiceDets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDet = await _context.InvoiceDets
                .FirstOrDefaultAsync(m => m.InvoiceDetID == id);
            if (invoiceDet == null)
            {
                return NotFound();
            }

            return View(invoiceDet);
        }

        // GET: InvoiceDets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InvoiceDets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InvoiceDetID,QuantityOrdered,QuantityArrived,BookCost")] InvoiceDet invoiceDet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(invoiceDet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(invoiceDet);
        }

        // GET: InvoiceDets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            InvoiceDet invoiceDet = _context.InvoiceDets
                .Include(i => i.Invoice)
                .Include(i => i.Book)
                .FirstOrDefault(i => i.InvoiceDetID == id);
            if (invoiceDet == null)
            {
                return NotFound();
            }
            return View(invoiceDet);
        }

        // POST: InvoiceDets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InvoiceDet invoiceDet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    InvoiceDet ivd = _context.InvoiceDets
                        .Include(i => i.Invoice)
                        .Include(i => i.Book)
                        .FirstOrDefault(i => i.InvoiceDetID == invoiceDet.InvoiceDetID);
                    Book book = _context.Books.Find(ivd.Book.BookID);
                    Int32 NumBooksLeft = ivd.QuantityOrdered - ivd.QuantityArrived;

                    //user inputs a quantity lower than what's needed to complete the invoicedet
                    if (invoiceDet.QuantityArrived < NumBooksLeft)
                    {
                        ivd.Book.Inventory = ivd.Book.Inventory + invoiceDet.QuantityArrived;
                        invoiceDet.QuantityArrived = ivd.QuantityArrived + invoiceDet.QuantityArrived;
                        ivd.QuantityArrived = invoiceDet.QuantityArrived;
                        ivd.QuantityOrdered = invoiceDet.QuantityOrdered;
                        ivd.BookCost = invoiceDet.BookCost;
                        book.Cost = invoiceDet.BookCost;
                        _context.Update(ivd);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(BooksOnOrder));
                    }
                    //user inputs a quantity equal to or greater than what's needed to complete the invoicedet
                    else
                    {
                        ivd.Book.Inventory = ivd.Book.Inventory + NumBooksLeft;
                        ivd.QuantityArrived = invoiceDet.QuantityOrdered;
                        ivd.QuantityOrdered = invoiceDet.QuantityOrdered;
                        ivd.BookCost = invoiceDet.BookCost;
                        //change to weighted average
                        book.Cost = invoiceDet.BookCost;
                        _context.Update(ivd);
                        _context.SaveChanges();

                        Invoice i = _context.Invoices.Include(n => n.InvoiceDets).FirstOrDefault(n => n.InvoiceID == ivd.Invoice.InvoiceID);
                        Int32 qtyarr = 0;
                        Int32 qtyord = 0;
                        foreach (InvoiceDet id in i.InvoiceDets)
                        {
                            qtyarr = qtyarr + id.QuantityArrived;
                            qtyord = qtyord + id.QuantityOrdered;
                        }
                        if (qtyarr == qtyord)
                        {
                            i.ProDateArrived = DateTime.Today;
                            _context.Update(i);
                            _context.SaveChanges();
                        }

                        return RedirectToAction(nameof(BooksOnOrder));
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceDetExists(invoiceDet.InvoiceDetID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(invoiceDet);
        }

        // GET: InvoiceDets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoiceDet = await _context.InvoiceDets
                .FirstOrDefaultAsync(m => m.InvoiceDetID == id);
            if (invoiceDet == null)
            {
                return NotFound();
            }

            return View(invoiceDet);
        }

        // POST: InvoiceDets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var invoiceDet = await _context.InvoiceDets.FindAsync(id);
            _context.InvoiceDets.Remove(invoiceDet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceDetExists(int id)
        {
            return _context.InvoiceDets.Any(e => e.InvoiceDetID == id);
        }
    }
}
