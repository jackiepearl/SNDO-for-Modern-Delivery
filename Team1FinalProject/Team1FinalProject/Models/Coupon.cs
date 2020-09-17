using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Team1FinalProject.Models
{
    public class Coupon
    {
        public String CouponID { get; set; }

        [Display(Name = "Coupon Code")]
        public String CouponCode { get; set; }

        [Display(Name = "Percentage Off")]
        public Decimal PercentOff { get; set; }

        [Display(Name = "Free Shipping?")]
        public Boolean FreeShip { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal BaselineValue { get; set; }

        public Boolean ActiveStatus { get; set; }

        [Display(Name = "Begining Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BeginDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public List<Order> Orders { get; set; }
    }
}
