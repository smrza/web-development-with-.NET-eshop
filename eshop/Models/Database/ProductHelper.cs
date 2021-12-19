using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop.Models.Database
{
    public static class ProductHelper
    {
        public static IList<Product> GenerateProduct()
        {
            IList<Product> products = new List<Product>()
            {
                new Product() {
                    Name = "Misprint Opto Jade",
                    Description = "Jade je vyroben tak, aby byl o něco stabilnější než Diamond. Pokud jste si tedy druhý zmiňovaný zamilovali, ale už jej přerostli, sáhněte po tomhle.",
                    Price = 14,
                    InStock = true,
                    ImageSrc = "/images/product/jade.jpg",
                    ImageAlt = "Jade"
                },

                new Product() {
                    Name = "Origio Burst Warship",
                    Description = "Hrajete discgolf pro zábavu a nechcete si kupovat zbytečně moc disků? Warship je pro vás ideální. Je mile poslušný ať ho hodí kdokoli, drží jakýkoli náklon mu dáte a do ruky padne perfektně.",
                    Price = 12,
                    InStock = true,
                    ImageSrc = "/images/product/warship.jpg",
                    ImageAlt = "Warship"
                },

                new Product() {
                    Name = "A-Medium Sparrow",
                    Description = "První putter od značky Disctroyer rozhodně nezklame. Má příjemný profil, rychlost tak akorát a je i celkem stabilní, proto si jej oblíbíte zejména pro příhozy a ve větru a kratší drivy.",
                    Price = 18,
                    InStock = true,
                    ImageSrc = "/images/product/sparrow.jpg",
                    ImageAlt = "Sparrow"
                },

                new Product() {
                    Name = "Cale Leiviska Spectrum 400 M4",
                    Description = "M4 je hlavně pro ty, kdo zatím nedisponují příliš velkou rychlostí a silou hodu. Poletí totiž krásně rovně i v malé rychlosti a na to, že jde o midrange, létá extra daleko.",
                    Price = 20,
                    InStock = true,
                    ImageSrc = "/images/product/M4.jpg",
                    ImageAlt = "M4"
                },

                new Product() {
                    Name = "Grip Bag AX4",
                    Description = "Legendární bagy řady AX jsou už po mnoho let tím nejlepším, co může discgolfista nosit na zádech. AX4 je nejnovější a zároveň nejvytříbenější verzí, která kdy spatřila světlo světa. ",
                    Price = 230,
                    InStock = true,
                    ImageSrc = "/images/product/grip.jpg",
                    ImageAlt = "Grip Bag"
                },

                new Product() {
                    Name = "Discgolfová sada Kastaplast (putter, midrange, driver)",
                    Description = "Prémiová sada od švédské značky Kastaplast je to pravé ořechové pro všechny fajnšmekry. Součástí jsou tři disky určené pro začátečníky, kteří to s discgolfem myslí vážně. ",
                    Price = 50,
                    InStock = true,
                    ImageSrc = "/images/product/kastaplast.jpg",
                    ImageAlt = "Kastaplast Starter Set"
                },

                new Product() {
                    Name = "Kevin Jones Spectrum 300 A2",
                    Description = "A2 je levotočivý stabilní disk, který přesně zapadá mezi midrange a puttery. Je skvělý pro silové hráče, kteří chtějí mít jistotu, že ho nepřeperou. ",
                    Price = 18,
                    InStock = true,
                    ImageSrc = "/images/product/A2.jpg",
                    ImageAlt = "A2"
                }
            };

            return products;
        }
    }
}
