using eshop.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    [Table(nameof(UserProduct))]
    public class UserProduct : Entity
    {
        [ForeignKey(nameof(User))]
        public int UserID { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductID { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }

        public UserProduct()
        {
        }

        public UserProduct(int userID, int productID)
        {
            UserID = userID;
            ProductID = productID;
        }

        public UserProduct(int userID, int productID, User user, Product product) : this(userID, productID)
        {
            User = user;
            Product = product;
        }
    }
}
