using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Team1FinalProject.Models
{
    public class CreditCard
    {
        public int CreditCardID { get; set; }

        public string CardType { get; set; }

        //piazza said we only need to store last 4 digits
        [Required]
        [Display(Name = "Last four digits")]
        [DisplayFormat(DataFormatString ="************{0}")]
        [StringLength(4, MinimumLength = 4)]
        public string CreditCardNumber { get; set; }

        
        public AppUser User { get; set; }
    }
}
