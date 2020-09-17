using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team1FinalProject.Models
{
    public class Invoice
    {
        public Int32 InvoiceID { get; set; }

        [Display(Name = "Order Placed Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProDatePlaced { get; set; }

        [Display(Name = "Full Order Received Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProDateArrived { get; set; }

        //may not need user
        //invoicedet tracks that
        public AppUser User { get; set; }
        public List<InvoiceDet> InvoiceDets { get; set; }

        //added property
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal InvoiceTotal
        {
            get { return InvoiceDets.Sum(d => d.ExtendedCost); }
        }

        public Invoice()
        {
            if (InvoiceDets == null)
            {
                InvoiceDets = new List<InvoiceDet>();
            }
        }
    }
}
