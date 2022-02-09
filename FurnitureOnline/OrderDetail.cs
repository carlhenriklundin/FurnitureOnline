using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureOnline.Models;

namespace FurnitureOnline
{
    class OrderDetail
    {
        public static void CreateOrderDetail() 
        {

            using (var db = new FurnitureOnlineContext())
            {


                var cartList = from
                                        cart in db.ShoppingCarts
                               join
                                 product in db.Products on cart.ProductsId equals product.ArticleNumber
                               select new ShowShoppingCartQuery { Id = cart.Id, ArticleNumber = product.Id, ProductName = product.Name, Quantity = cart.AmountOfItems, UnitPrice = product.CurrentPrice };
            }




        }
    }
}
