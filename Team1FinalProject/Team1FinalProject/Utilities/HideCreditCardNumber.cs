using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1FinalProject.Models;

namespace Team1FinalProject.Utilities
{
    public class HideCreditCardNumber
    {
        public string Hide_number(string CreditCardNumber)
        {
            string ccn = CreditCardNumber;
            string result = ccn.Substring(ccn.Length - 4).PadLeft(ccn.Length, '*');
            return result;
        }
    }
}
