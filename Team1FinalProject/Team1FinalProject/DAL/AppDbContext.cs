using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;



namespace Team1FinalProject.DAL
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDet> InvoiceDets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDet> OrderDets { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }


    }
}
