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
            Console.WriteLine(ShoppingCart.ShowShoppingCart(out summa));
            Console.WriteLine($"Frakt ({OrderShippingMethod.Name}) \t{OrderShippingMethod.Price:C}");
            Console.WriteLine($"Total att betala: {summa + OrderShippingMethod.Price:C}");

            var payment = Payment.SelectPaymentMethod();

            var newOrderHistory = new Models.OrderHistory() { CustomerId = orderCustomer.Id, OrderDate = DateTime.Now, ShippingId = OrderShippingMethod.Id, PaymentId = payment.Id, ShippingAdress = orderCustomer.Adress, ShippingZipCode = orderCustomer.ZipCode, ShippingCity = orderCustomer.City, TotalAmount = summa + OrderShippingMethod.Price };

            using (var dbOrderHistory = new Models.FurnitureOnlineContext())
            {
                var orderList = dbOrderHistory.OrderHistories;
                orderList.Add(newOrderHistory);
                dbOrderHistory.SaveChanges();
            }
           } 


    }
}
