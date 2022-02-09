using System;

namespace FurnitureOnline
{
    class Program


        
    {
        static void Main(string[] args)
        {


            Console.WriteLine("Välkommen till vår Webshop\n\n");
            Console.WriteLine(Products.ShowChosenProducts());
            Console.WriteLine("\n----------------------------------------\n");
            Console.WriteLine(Products.ShowAllProducts());
            Console.WriteLine("\nVilken produkt vill du klicka in på?");
            int input = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine(Products.ShowAProdukt(input));

            Console.WriteLine(@"Vill du lägga denna artikel i varukorgen? skriv i så fall 'Ja'");
            string stringInput = Console.ReadLine();

            if (stringInput == "Ja")
            {
                Console.WriteLine(@"Hur många exemeplar av denna artikel vill du lägga in i kundkorgen?");
                int number = Convert.ToInt32(Console.ReadLine());

                var newProductInCart = new Models.ShoppingCart() { ProductsId = input, AmountOfItems = number };
                ShoppingCart.AddProductToCart(newProductInCart);
            }

            OrderHistory.Checkout();
           
            //Console.WriteLine(ShoppingCart.ShowShoppingCart(out _));
        }
    }
}
