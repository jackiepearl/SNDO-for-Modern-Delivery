using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Team1FinalProject.Models
{
    public class Review
    {
        public Int32 ReviewID { get; set; }

        [Display(Name = "Review")]
        [StringLength(100)]
        [Required]
        public String ReviewContent { get; set; }

        [Display(Name = "Rating")]
        [Required]
        public Decimal Rating { get; set; }

        public Boolean Rejected { get; set; }

        public int Id_book { get; set; }


        public AppUser Author { get; set; }
        public AppUser Approver { get; set; }
        public Book Book { get; set; }
    }
}
