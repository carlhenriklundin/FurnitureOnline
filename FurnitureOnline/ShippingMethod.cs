using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureOnline
{
    class ShippingMethod
    {

        public static void ShowShippingMethod()
        {
            using (var db = new Models.FurnitureOnlineContext())
            {
                var shippingMethodList = db.Shippings; 


                foreach (var shippingMethod in shippingMethodList)
                {
                    Console.WriteLine($"{shippingMethod.Id,-20}{shippingMethod.Name, -15}{shippingMethod.Price, -10}{shippingMethod.Specification}");
                }

            }
        }

        public static Models.Shipping SelectShippingMethod()
        {
            using (var db = new Models.FurnitureOnlineContext())
            {
                var shippingMethodsList = db.Shippings;

                foreach (var shipping in shippingMethodsList)
                {
                    Console.WriteLine($" {shipping.Id}\t{shipping.Name}\t{shipping.Price:c}\n----------------\n{shipping.Specification}\n\n");
                }

                Console.WriteLine("Vilken fraktmetod vill du använda? (Ange Id-nr.) \n");
                int selectedShippingMethod = Convert.ToInt32(Console.ReadLine());

                return db.Shippings.SingleOrDefault(s => s.Id == selectedShippingMethod);
            }

        }


    }
}
