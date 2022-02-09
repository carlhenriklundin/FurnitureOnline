using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureOnline
{
    class Payment
    {
        public static void ShowPaymentMethod()
        {
            using (var db = new Models.FurnitureOnlineContext())
            {
                var paymentMethodList = db.Shippings;


                foreach (var paymentMethod in paymentMethodList)
                {
                    Console.WriteLine($"{paymentMethod.Id,-20}{paymentMethod.Name,-15}{paymentMethod.Specification}");
                }

            }
        }

        public static Models.Payment SelectPaymentMethod()
        {
            using (var db = new Models.FurnitureOnlineContext())
            {
                var paymentMethodsList = db.Shippings;

                foreach (var payment in paymentMethodsList)
                {
                    Console.WriteLine($" {payment.Id}\t{payment.Name}\t{payment.Specification}\n\n");
                }

                Console.WriteLine("Vilket betalningsmedel vill du använda? (Ange Id-nr.) \n");
                int selectedPaymentMethod = Convert.ToInt32(Console.ReadLine());

                return db.Payments.SingleOrDefault(s => s.Id == selectedPaymentMethod);
            }

        }
    }
}
