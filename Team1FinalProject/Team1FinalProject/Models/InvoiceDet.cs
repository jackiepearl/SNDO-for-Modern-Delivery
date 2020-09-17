using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team1FinalProject.Models
{
    public class InvoiceDet
    {
        public Int32 InvoiceDetID { get; set; }

        [Display(Name = "Quantity Ordered")]
        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, 10000, ErrorMessage = "Quantity must be greater than zero")]
        public Int32 QuantityOrdered { get; set; }

        [Display(Name = "Quantity Received")]
        [Required(ErrorMessage = "Quantity is required")]
        [Range(0, 10000, ErrorMessage = "Quantity must be greater than zero")]
        public Int32 QuantityArrived { get; set; }

        [Display(Name = "Book Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(.01, 10000, ErrorMessage = "Cost must be greater than zero")]
        [Required(ErrorMessage = "Book cost is required")]
        public Decimal BookCost { get; set; }

        public AppUser User { get; set; }
        public Invoice Invoice { get; set; }
        public Book Book { get; set; }

        public Decimal ExtendedCost
        {
            get { return BookCost * QuantityOrdered; }
        }
    }
}
