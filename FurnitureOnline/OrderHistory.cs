using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureOnline
{
    class OrderHistory
    {
          public static void Checkout()
          {
            var orderCustomer = Customer.DetermineMember();
            var OrderShippingMethod = ShippingMethod.SelectShippingMethod();

            double? summa;
            string orderSummary = ShoppingCart.ShowShoppingCart(out summa) + $"Frakt ({OrderShippingMethod.Name}) \t{OrderShippingMethod.Price:C}\nTotal att betala: {summa + OrderShippingMethod.Price:C}";
            Console.WriteLine(orderSummary);

            var payment = Payment.SelectPaymentMethod();

            var newOrderHistory = new Models.OrderHistory() { CustomerId = orderCustomer.Id, OrderDate = DateTime.Now, ShippingId = OrderShippingMethod.Id, PaymentId = payment.Id, ShippingAdress = orderCustomer.Adress, ShippingZipCode = orderCustomer.ZipCode, ShippingCity = orderCustomer.City, TotalAmount = summa + OrderShippingMethod.Price };

            using (var dbOrderHistory = new Models.FurnitureOnlineContext())
            {
                var orderList = dbOrderHistory.OrderHistories;
                orderList.Add(newOrderHistory);
                dbOrderHistory.SaveChanges();

                var cartlist = from
                                  cart in dbOrderHistory.ShoppingCarts
                               join
                               product in dbOrderHistory.Products on cart.ProductsId equals product.ArticleNumber
                               select new Models.ShowShoppingCartQuery { ArticleNumber = cart.ProductsId, ProductName = product.Name, Quantity = cart.AmountOfItems, UnitPrice = product.CurrentPrice };

                foreach (var item in cartlist)
                {
                    using (var dbOrderDetail = new Models.FurnitureOnlineContext())
                    {
                        var OrderDetailList = dbOrderDetail.OrderDetails;
                        var newOrderDetail = new Models.OrderDetail() { OrderId = newOrderHistory.Id, ProductId = 1, Price = item.UnitPrice, Quantity = item.Quantity };
                       
                        OrderDetailList.Add(newOrderDetail);
                        dbOrderDetail.SaveChanges();
                    }
                }

                Console.WriteLine("Orderbekräftelse:\n" + orderSummary);
            }
          } 

          

    }
}
