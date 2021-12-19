using eshop.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class UserProductViewModel
    {
        public Product Product { get; set; }

        public User User { get; set; }

        public IList<UserProduct> UserProducts { get; set; }

        public UserProductViewModel(Product product, User user, IList<UserProduct> userProducts)
        {
            Product = product;
            User = user;
            UserProducts = userProducts;
        }
    }
}
