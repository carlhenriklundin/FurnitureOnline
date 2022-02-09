using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureOnline
{
    class ShoppingCart
    {
        public static void AddProductToCart(Models.ShoppingCart cart)
        {
            using (var db = new Models.FurnitureOnlineContext())
            {
                var cartTable = db.ShoppingCarts;
                var updateQuanityProduct = cartTable.SingleOrDefault(u => u.ProductsId == cart.ProductsId);

                if (updateQuanityProduct == null)
                {
                    cartTable.Add(cart);
                    db.SaveChanges();
                }

                else UpdateQuantityInCart(cart.ProductsId, updateQuanityProduct.AmountOfItems + cart.AmountOfItems);
            }
        }

        public static void UpdateQuantityInCart(int? articleNumber, int? numberOfItem)
        {
            using (var db = new Models.FurnitureOnlineContext())
            {
                var articleToUpdate = db.ShoppingCarts.SingleOrDefault(c => c.ProductsId == articleNumber);
                articleToUpdate.AmountOfItems = numberOfItem;
                db.SaveChanges();
            }
        }


        public static string ShowShoppingCart(out double? summa)
        {
             summa = 0;
            using (var db = new Models.FurnitureOnlineContext())
            {
                
                var cartList = from
                                    cart in db.ShoppingCarts
                               join
                                 product in db.Products on cart.ProductsId equals product.ArticleNumber
                               select new Models.ShowShoppingCartQuery { Id = cart.Id, ArticleNumber = product.Id, ProductName = product.Name, Quantity = cart.AmountOfItems, UnitPrice = product.CurrentPrice};

                string returnstring = $"KUNDVAGN\n\n{"ART.NR.",-10}{"ANTAL",-10}{"PRODUKTNAMN",-30}{"PRIS",-15}{"TOTALT",-10}\n";
                

                foreach (var cartItem in cartList)
                {
                    if (cartItem.Quantity != 0) returnstring += $"{cartItem.ArticleNumber,-10}{cartItem.Quantity,-10}{cartItem.ProductName,-30}{string.Format("{0:0.00}", cartItem.UnitPrice) + " kr",-15}{string.Format("{0:0.00}", cartItem.TotalAmount) + " kr",-10}\n";
                    summa += cartItem.TotalAmount;
                }
                returnstring += $"\n\n{"----------",70}\n{"Summa att betala:         ",60}{string.Format("{0:0.00}", summa) + " kr"}";
                return returnstring;
            }
        }

    }
}
