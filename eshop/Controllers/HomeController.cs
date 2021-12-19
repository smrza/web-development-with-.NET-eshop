using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eshop.Models;
using eshop.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace eshop.Controllers
{
    public class HomeController : Controller
    {
        //IList<Carousel> carousels = DatabaseFake.Carousels;
        readonly EshopDBContext EshopDBContext;
        readonly ILogger<HomeController> logger;

        public HomeController(EshopDBContext eshopDBContext, ILogger<HomeController> logger)
        {
            this.EshopDBContext = eshopDBContext;
            this.logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            this.logger.LogInformation("Index page was called.");

            var vmC = new CarouselViewModel();
            vmC.Carousels = await EshopDBContext.Carousels.ToListAsync();

            var vmP = new ProductViewModel();
            vmP.Products = await EshopDBContext.Products.ToListAsync();

            var vm = new HomeViewModel(vmC, vmP);
            return View(vm);            
        }

        public IActionResult About()
        {
            //ViewData["Message"] = "Discgolf Shop.";
            //throw new Exception("něco se stalo");
            this.logger.LogInformation("About page was called.");
            return View();
        }

        public IActionResult Contact()
        {
            //ViewData["Message"] = "Discgolf Shop Contact.";
            this.logger.LogInformation("Contact page was called.");

            return View();
        }

        public IActionResult Privacy()
        {
            this.logger.LogInformation("Privacy page was called.");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var featureException = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            this.logger.LogError("Exception occured: " + featureException.Error.ToString() + Environment.NewLine + "Exception Path: " + featureException.Path);

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ErrorCodeStatus(int? statusCode = null)
        {
            string originalURL = String.Empty;
            var features = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if(features != null)
            {
                originalURL = features.OriginalPathBase + features.OriginalPath + features.OriginalQueryString;
            }

            var statCode = statusCode.HasValue ? statusCode.Value : 0;

            this.logger.LogWarning("Status Code: " + statCode + "Original URL: " + originalURL);

            if (statCode == 404)
            {
                _404ViewModel vm404 = new _404ViewModel()
                {
                    StatusCode = statCode
                };
                return View(statusCode.ToString(), vm404);
            }
            else if(statCode > 399 && statCode < 600)
            {
                _400599ViewModel vm400599 = new _400599ViewModel()
                {
                    StatusCode = statCode
                };
                return View(statusCode.ToString(), vm400599);
            }

            ErrorCodeStatusViewModel vm = new ErrorCodeStatusViewModel()
            {
                StatusCode = statCode,
                OriginalURL = originalURL
            };
            return View(vm);
        }
    }
}
