using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models
{
    public class HomeViewModel
    {
        public CarouselViewModel carouselViewModel { get; set; }
        public ProductViewModel productViewModel { get; set; }

        public HomeViewModel()
        {
        }

        public HomeViewModel(CarouselViewModel carouselViewModel, ProductViewModel productViewModel)
        {
            this.carouselViewModel = carouselViewModel;
            this.productViewModel = productViewModel;
        }
    }
}
