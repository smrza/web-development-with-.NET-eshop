using eshop.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using eshop.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using eshop.Models.Identity;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class ProductController : Controller
    {
        IHostingEnvironment Env;
        readonly EshopDBContext EshopDBContext;
        readonly ILogger<ProductController> logger;

        //IList<Product> products = DatabaseFake.Products;

        public ProductController(EshopDBContext eshopDBContext, IHostingEnvironment env, ILogger<ProductController> logger)
        {
            this.EshopDBContext = eshopDBContext;
            this.Env = env;
            this.logger = logger;
        }

        public async Task<IActionResult> ProductSetting()
        {
            this.logger.LogInformation("ProductController ProductSetting was called.");

            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.Products = await EshopDBContext.Products.ToListAsync();
            return View(productViewModel);
        }

        public IActionResult Create()
        {
            this.logger.LogInformation("ProductController Create was called.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            this.logger.LogInformation("ProductController HttpPost create was called.");
            if (ModelState.IsValid)
            {
                product.ImageSrc = String.Empty;

                FileUpload fup = new FileUpload(Env.WebRootPath, "product", "image");
                product.ImageSrc = await fup.FileUploadAsync(product.Image);

                EshopDBContext.Products.Add(product);

                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(ProductSetting));
            }
            else return View(product);
        }

        
        public IActionResult Edit(int id)
        {
            this.logger.LogInformation("ProductController edit was called.");
            Product product = EshopDBContext.Products.Where(prodItem => prodItem.ID == id).FirstOrDefault();
            if (product != null)
            {
                return View(product);
            }
            else
            {
                return NotFound();
            }
        }
        

        [HttpPost]
        public async Task<IActionResult> Edit(Product prod)
        {
            this.logger.LogInformation("ProductController HttpPost edit was called.");
            Product product = EshopDBContext.Products.Where(prodItem => prodItem.ID == prod.ID).FirstOrDefault();
            if (product != null)
            {

                var subscriptions = EshopDBContext.UserProducts.Include(produ => produ.Product).Include(u => u.User).Where(id => id.ProductID == product.ID);
                var subscriptionsItems = subscriptions.ToList();

                product.Name = prod.Name;
                product.Description = prod.Description;

                if (product.Price != prod.Price)
                {
                    foreach (var item in subscriptionsItems)
                    {
                        EmailSender.SendEmail("Změna ceny produktu", $"Produkt, který sledujete {prod.Name} má změněnou cenu z ${product.Price} na ${prod.Price}", item.User.Email);
                    }
                }

                product.Price = prod.Price;

                if (product.InStock != prod.InStock)
                {
                    foreach (var item in subscriptionsItems)
                    {
                        if (prod.InStock == true)
                        {
                            EmailSender.SendEmail("Změna skladu.", $"Produkt, který sledujete {prod.Name} byl naskladněn.", item.User.Email);
                        }
                        else
                        {
                            EmailSender.SendEmail("Změna skladu.", $"Produkt, který sledujete {prod.Name} je vyprodán.", item.User.Email);
                        }
                    }
                }

                product.InStock = prod.InStock;
                //product.ImageSrc = prod.ImageSrc;
                product.ImageAlt = prod.ImageAlt;

                FileUpload fup = new FileUpload(Env.WebRootPath, "product", "image");
                if (String.IsNullOrWhiteSpace(prod.ImageSrc = await fup.FileUploadAsync(prod.Image)) == false)
                {
                    product.ImageSrc = prod.ImageSrc;
                }

                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(ProductSetting));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            this.logger.LogInformation("ProductController delete was called.");
            Product product = EshopDBContext.Products.Where(prodItem => prodItem.ID == id).FirstOrDefault();
            if (product != null)
            {
                EshopDBContext.Products.Remove(product);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(ProductSetting));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
