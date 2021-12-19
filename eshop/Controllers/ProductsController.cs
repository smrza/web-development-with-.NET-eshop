using eshop.Models;
using eshop.Models.ApplicationServices;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Controllers
{
    public class ProductsController : Controller
    {
        ISecurityApplicationService iSecure;
        readonly ILogger<ProductsController> logger;
        readonly EshopDBContext EshopDBContext;
        IHostingEnvironment Env;

        public ProductsController(EshopDBContext eshopDBContext, IHostingEnvironment env, ILogger<ProductsController> logger, ISecurityApplicationService iSecure)
        {
            this.EshopDBContext = eshopDBContext;
            this.Env = env;
            this.logger = logger;
            this.iSecure = iSecure;
        }

        public async Task<IActionResult> Detail(int ID)
        {
            this.logger.LogInformation("Product detail page was called.");

            Product product = EshopDBContext.Products.Where(i => i.ID == ID).FirstOrDefault();
            User user = await iSecure.GetCurrentUser(User);
            var eshopDBContext = EshopDBContext.UserProducts.Include(i => i.User).Include(i => i.Product);

            UserProductViewModel userProductViewModel = new UserProductViewModel(product, user, await eshopDBContext.ToListAsync());

            return View(userProductViewModel);
        }

        public async Task<IActionResult> ProductSubscription(int ID)
        {
            logger.LogInformation("ProductsController ProductSubscription was called.");
            User user = await iSecure.GetCurrentUser(User);

            if(user != null)
            {
                Product product = EshopDBContext.Products.Where(i => i.ID == ID).FirstOrDefault();

                if (product != null)
                {
                    UserProduct userProduct = new UserProduct() { UserID = user.Id, ProductID = ID };
                    EshopDBContext.UserProducts.Add(userProduct);
                    await EshopDBContext.SaveChangesAsync();
                }

                var eshopDBContext = EshopDBContext.UserProducts.Include(i => i.User).Include(i => i.Product);
                UserProductViewModel userProductViewModel = new UserProductViewModel(product, user, await eshopDBContext.ToListAsync());

                return RedirectToAction("Detail", new { ID = product.ID });
            }


            return NotFound();
        }

        public async Task<IActionResult> ProductUnSubscription(int ID)
        {
            logger.LogInformation("ProductsController ProductUnSubscription was called.");
            User user = await iSecure.GetCurrentUser(User);
            Product product = EshopDBContext.Products.Where(i => i.ID == ID).FirstOrDefault();
            var eshopDBContext = EshopDBContext.UserProducts.Include(i => i.User).Include(i => i.Product);
            UserProductViewModel userProductViewModel = new UserProductViewModel(product, user, await eshopDBContext.ToListAsync());
            int tmpID = 0;

            if(user != null && userProductViewModel.Product != null && userProductViewModel != null)
            {
                foreach (var item in userProductViewModel.UserProducts)
                {
                    if (item.ProductID == ID && item.UserID == user.Id)
                    {
                        tmpID = item.ID;
                    }
                }

                if (tmpID != 0)
                {
                    var itemToRemove = await EshopDBContext.UserProducts.FindAsync(tmpID);
                    EshopDBContext.UserProducts.Remove(itemToRemove);
                    await EshopDBContext.SaveChangesAsync();
                }

                return RedirectToAction("Detail", new { ID = product.ID });
            }

            return NotFound();
        }
    }
}
