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
    [Authorize(Roles ="Customer")]
    public class OrderDetsController : Controller
    {
        private readonly AppDbContext _context;

        public OrderDetsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: OrderDets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDet = _context.OrderDets.Find(id);
            if (orderDet == null)
            {
                return NotFound();
            }
            return View(orderDet);
        }

        // POST: OrderDets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(OrderDet orderDet)
        {
            OrderDet DbOrdDet = _context.OrderDets
                                   .Include(o => o.Order)
                                   .Include(o => o.Book)
                                   .FirstOrDefault(o => o.OrderDetID == orderDet.OrderDetID);

            Book book = _context.Books.FirstOrDefault(b => b.BookID == DbOrdDet.Book.BookID);
            int bookInv = book.Inventory;

            if (DbOrdDet.Quantity > bookInv)
            {
                return View("AddError", new string[] { "Please select less than " + bookInv.ToString() });
            }

            DbOrdDet.Quantity = orderDet.Quantity;
            DbOrdDet.TotalBookPrice = DbOrdDet.Quantity * DbOrdDet.Book.Price;
          

            if (ModelState.IsValid)
            {
                _context.OrderDets.Update(DbOrdDet);
                _context.SaveChanges();
                return RedirectToAction("Details", "Orders", new { id = DbOrdDet.Order.OrderID });

            }

            return View(orderDet);
        }

        // GET: OrderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDets
                .FirstOrDefaultAsync(m => m.OrderDetID == id);

            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // POST: OrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderDetail = await _context.OrderDets.Include(o => o.Order).FirstOrDefaultAsync(o => o.OrderDetID == id);
            int OrderID = orderDetail.Order.OrderID;

            _context.OrderDets.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction("ShoppingCart", "Orders", new { id = OrderID });
        }

        private bool OrderDetExists(int id)
        {
            return _context.OrderDets.Any(e => e.OrderDetID == id);
        }
    }
}
