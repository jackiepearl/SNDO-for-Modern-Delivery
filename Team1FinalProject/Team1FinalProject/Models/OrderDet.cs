using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team1FinalProject.Models
{
    public class OrderDet
    {
        public Int32 OrderDetID { get; set; }

        [Display(Name = "Quantity")]
        public Int32 Quantity { get; set; }

        [Display(Name = "Total Book Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal TotalBookCost { get; set; }

        [Display(Name = "Total Book Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal TotalBookPrice { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal BookCost
        {
            get { return TotalBookCost / Quantity; }
        }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal BookPrice
        {
            get { return TotalBookPrice / Quantity; }
        }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal Margin
        {
            get { return BookPrice - BookCost; }
        }

        public Order Order { get; set; }
        public Book Book { get; set; }
    }
}
