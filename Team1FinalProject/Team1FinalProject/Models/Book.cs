using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Team1FinalProject.Models
{
    public class Book
    {
        public Int32 BookID { get; set; }

        [Display(Name = "Unique Number")]
        public Int32 UniqueNum { get; set; }

        [Display(Name = "Book Title")]
        [Required(ErrorMessage = "Title is required")]
        public String Title { get; set; }

        [Display(Name = "Book Author")]
        [Required(ErrorMessage = "Author is required")]
        public String Author { get; set; }

        [Display(Name = "Publish Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishDate { get; set; }

        [Display(Name = "Description")]
        public String Description { get; set; }

        [Display(Name = "Inventory")]
        [Range(0, 10000, ErrorMessage = "Inventory must be positive")]
        public Int32 Inventory { get; set; }

        [Display(Name = "Reorder Level")]
        [Range(0, 10000, ErrorMessage = "Reorder level must be positive")]
        public Int32 ReorderLevel { get; set; }

        [Display(Name = "Book Cost")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(.01, 100000, ErrorMessage = "Cost must be greater than zero")]
        public Decimal Cost { get; set; }

        [Display(Name = "Book Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(.01, 100000, ErrorMessage = "Price must be greater than zero")]
        public Decimal Price { get; set; }

        [Display(Name = "Discontinued")]
        public Boolean ActiveSell { get; set; }

        //added property
        public Decimal AvgRating
        {
            get
            {
                if (ReviewsApproved.Count == 0)
                    return 0;
                else
                    return ReviewsApproved.Average(r => r.Rating);
            }
        }

        //added constructor
        public Book()
        {
            if (ReviewsApproved == null)
            {
                ReviewsApproved = new List<Review>();
            }
        }

        public Genre Genre { get; set; }
        //public List<Review> ReviewsWritten { get; set; } Do we have to have this on the books model?
        public List<Review> ReviewsApproved { get; set; }
        public List<OrderDet> OrderDets { get; set; }
        public List<InvoiceDet> InvoiceDets { get; set; }
    }
}
