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
        }
    }
}
