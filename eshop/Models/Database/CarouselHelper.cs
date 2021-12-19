using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class CarouselHelper
    {
        public static IList<Carousel> GenerateCarousel()
        {           
            IList<Carousel> carousels = new List<Carousel>()
            {
                new Carousel() {
                    DataTarget = "#myCarousel",
                    ImageSrc = "/images/carousel/grip_carousel.jpg",
                    ImageAlt = "Grip Backpacks",
                    CarouselContent ="<a class=\"btn btn-default\" href=\"/Products/Detail/5\">" +
                   "Get Yours Now!</a>"
                },

                new Carousel() {
                    DataTarget = "#myCarousel",
                    ImageSrc = "/images/carousel/disc_carousel.jpg",
                    ImageAlt = "Disky",
                    CarouselContent = ""
                },

                new Carousel() {
                    DataTarget = "#myCarousel",
                    ImageSrc = "/images/carousel/putt_carousel.jpg",
                    ImageAlt = "Putting",
                    CarouselContent = ""
                }

                //new Carousel() {
                //    DataTarget = "#myCarousel",
                //    ImageSrc = "/images/banner3.svg",
                //    ImageAlt = "ASP.NET",
                //    CarouselContent = "Learn how Microsoft's Azure cloud platform allows you to build, deploy, and scale web apps." +
                //    "<a class=\"btn btn-default\" href=\"https://go.microsoft.com/fwlink/?LinkID=525027&clcid=0x409\">" +
                //    "Learn More</a>"
                //},

                //new Carousel() {
                //    DataTarget = "#myCarousel",
                //    ImageSrc = "/images/carousel/Information-Technology.jpg",
                //    ImageAlt = "Information Technology",
                //    CarouselContent = "Information Technology." +
                //    "<a class=\"btn btn-default\" href=\"https://google.com\">" +
                //    "Learn More</a>"
                //}
            };

            return carousels;
        }
    }
}
