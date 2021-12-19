using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using eshop.Models;
using eshop.Models.Database;
using eshop.Models.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eshop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = nameof(Roles.Admin) + ", " + nameof(Roles.Manager))]
    public class CarouselController : Controller
    {
        IHostingEnvironment Env;
        readonly EshopDBContext EshopDBContext;
        readonly ILogger<CarouselController> logger;

        //IList<Carousel> carousels = DatabaseFake.Carousels;

        public CarouselController(EshopDBContext eshopDBContext, IHostingEnvironment env, ILogger<CarouselController> logger)
        {
            this.EshopDBContext = eshopDBContext;
            this.Env = env;
            this.logger = logger;
        }

        public async Task<IActionResult> CarouselSetting()
        {
            this.logger.LogInformation("CarouselController carouselsetting was called.");
            CarouselViewModel carouselViewModel = new CarouselViewModel();
            carouselViewModel.Carousels = await EshopDBContext.Carousels.ToListAsync();
            return View(carouselViewModel);
        }

        public IActionResult Create()
        {
            this.logger.LogInformation("CarouselController Create was called.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Carousel carousel)
        {
            this.logger.LogInformation("CarouselController httppost create was called.");
            if (ModelState.IsValid)
            {
                carousel.ImageSrc = String.Empty;

                FileUpload fup = new FileUpload(Env.WebRootPath, "carousel", "image");
                carousel.ImageSrc = await fup.FileUploadAsync(carousel.Image);

                EshopDBContext.Carousels.Add(carousel);

                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(CarouselSetting));
            }
            else return View(carousel);
        }

        public IActionResult Edit(int id)
        {
            this.logger.LogInformation("CarouselController edit was called.");
            Carousel carousel = EshopDBContext.Carousels.Where(carousItem => carousItem.ID == id).FirstOrDefault();
            if (carousel != null)
            {
                return View(carousel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Carousel car)
        {
            this.logger.LogInformation("CarouselController httppost edit was called.");
            Carousel carousel = EshopDBContext.Carousels.Where(carousItem => carousItem.ID == car.ID).FirstOrDefault();
            if (carousel != null)
            {
                carousel.DataTarget = car.DataTarget;
                carousel.ImageAlt = car.ImageAlt;
                carousel.CarouselContent = car.CarouselContent;

                FileUpload fup = new FileUpload(Env.WebRootPath, "carousel", "image");
                if (String.IsNullOrWhiteSpace(car.ImageSrc = await fup.FileUploadAsync(car.Image)) == false) 
                { 
                    carousel.ImageSrc = car.ImageSrc;
                }

                await EshopDBContext.SaveChangesAsync();

                return RedirectToAction(nameof(CarouselSetting));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            this.logger.LogInformation("CarouselController delete was called.");
            Carousel carousel = EshopDBContext.Carousels.Where(carousItem => carousItem.ID == id).FirstOrDefault();
            if (carousel != null)
            {
                EshopDBContext.Carousels.Remove(carousel);
                await EshopDBContext.SaveChangesAsync();
                return RedirectToAction(nameof(CarouselSetting));
            }
            else
            {
                return NotFound();
            }
        }
    }
}
