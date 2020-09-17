using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team1FinalProject.Models
{
    public class Order
    {
        public Int32 OrderID { get; set; }

        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Order Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderCost
        {
            get { return OrderDets.Sum(rd => rd.TotalBookCost); }
        }

        [Display(Name = "Order Total")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderTotal
        {
            get { return OrderSubtotal + ShipCost; }
            
        }

        //Get Quantity of all the orderdetails for Report B
        public int NumberOfBooks
        {

            get { return OrderDets.Sum(rd => rd.Quantity); }
                //int NumBooks = 0;
                //foreach (OrderDet od in OrderDets)
                //{
                //    NumBooks = NumBooks + od.Quantity;
                //}

                //return NumBooks;
           
        }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal WACC { get; set; }

        [Display(Name = "Shipping Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal ShipCost { get; set; }

        [Display(Name = "Order Subtotal")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderSubtotal
        {
            get { return OrderDets.Sum(rd => rd.TotalBookPrice); }
            set { }
        }

        public Order()
        {
            if (OrderDets == null)
            {
                OrderDets = new List<OrderDet>();
            }
        }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public Decimal OrderMargin
        {
            get { return OrderSubtotal - OrderCost; }
        }

        //make status an Enum
        public Boolean OrderComplete { get; set; }

        public CreditCard OrderCard { get; set; }
        public Coupon Coupon { get; set; }
        public AppUser AppUser { get; set; }
        public List<OrderDet> OrderDets { get; set; }
    }
}
