using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureOnline.Models;

namespace FurnitureOnline
{
    class Products
    {
        public static string ShowChosenProducts()
        {
            using (var db = new FurnitureOnlineContext())
            {
                var products = db.Products;

                var chosenProducts = products.Where(s => s.ChosenItem == true);
                string returnString = "\nNågra utvadla produkter:\n\n";

                foreach (var product in chosenProducts)
                {
                    returnString += $"{product.Name} ({product.CurrentPrice}) kr \t";
                }
                return returnString;

            }
        }



        public static string ShowAllProducts()
        {
            using (var db = new FurnitureOnlineContext())
            {
                var productList = from
                                    product in db.Products
                                  join
                                    cateogry in db.Categories on product.CategoryId equals cateogry.Id
                                  join
                                    supplier in db.Suppliers on product.SupplierId equals supplier.Id
                                  select new ProductListQuery { Id = product.Id, ProductName = product.Name, Price = product.CurrentPrice, StockUnit = product.StockUnit, CategoryName = cateogry.Name, Description = product.Description, ArticleNumber = product.ArticleNumber, Color = product.Color, Material = product.Material, SupplierName = supplier.Name };


                string returnString = $"PRODUKTLISTA\n\n{"ART.NR.",-10}{"PRODUKTNAMN",-25}{"PRIS",-14}{"KATEGORI",-17}{"LEVERANTÖR", -20}{"LAGERSALDO",-25}\n";

                foreach (var product in productList)
                {
                    returnString += $"{product.ArticleNumber,-10}{product.ProductName,-25}{string.Format("{0:0.00}", product.Price) + " kr",-14}{product.CategoryName,-17}{product.SupplierName,-20}{product.StockUnit,-17}\n";
                }

                return returnString;
            }
        }


        public static string ShowAProdukt(int articleNr)
        {
            using (var db = new FurnitureOnlineContext())
            {
                var productList = from
                                    product in db.Products
                                  join
                                    cateogry in db.Categories on product.CategoryId equals cateogry.Id
                                  join
                                    supplier in db.Suppliers on product.SupplierId equals supplier.Id
                                  select new ProductListQuery { Id = product.Id, ProductName = product.Name, Price = product.CurrentPrice, StockUnit = product.StockUnit, CategoryName = cateogry.Name, Description = product.Description, ArticleNumber = product.ArticleNumber, Color = product.Color, Material = product.Material, SupplierName = supplier.Name };


                var specificProduct = productList.Where(p => p.ArticleNumber == articleNr).ToList();
                return $"{specificProduct[0].ProductName.ToUpper()}\n\nProduktbeskrivning:\n{specificProduct[0].Description}\n\nPRODUKTFAKTA:\nArtikelnr: {specificProduct[0].ArticleNumber}\nKategori: {specificProduct[0].CategoryName}\nLeverantör: {specificProduct[0].SupplierName}\nFärg: {specificProduct[0].Color}\nMaterial: {specificProduct[0].Material}\nPris: {specificProduct[0].Price}\n";
            }
        }



    }
}
