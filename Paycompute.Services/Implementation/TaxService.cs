using System;
using System.Collections.Generic;
using System.Text;

namespace Paycompute.Services.Implementation
{
    public class TaxService : ITaxService
    {
        public decimal taxRate;
        public decimal tax;
        public decimal TaxAmount(decimal totalAmount)
        {
            if(totalAmount<=1042)
            {
                taxRate = 0.0m;
                tax = totalAmount * taxRate;
            }
            else if(totalAmount <= 3125)
            {
                taxRate = 0.2m;
                tax = (totalAmount - 1042) * taxRate;
            }
            else if(totalAmount<=12500)
            {
                taxRate = 0.40m;
                tax = ((3125 - 1042) * 0.2m) + ((totalAmount - 3125) * taxRate);
            }
            else
            {
                taxRate = 0.45m;
                tax = ((3125 - 1042) * 0.2m) + ((12500 - 3125) * 0.4m) + ((totalAmount-12500) *0.45m);
            }
            return tax;
        }
    }
}
