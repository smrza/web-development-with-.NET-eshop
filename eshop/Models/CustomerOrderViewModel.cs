using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class CustomerOrderViewModel
    {
        IList<Order> Orders { get; set; }
    }
}
