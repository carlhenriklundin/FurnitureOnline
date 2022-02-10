using System;

namespace FurnitureOnline
{
    class Customer
    {

        public static Models.Customer DetermineMember()
        {
            Console.WriteLine("Är du medlem?");
            string input = Console.ReadLine();



            var member = new Models.Customer();



            if (input == "Ja")
            {
                member = MemberLogin();
            }
            else if (input == "Nej")
            {
                Console.WriteLine("Vill du bli medlem?");
                string input2 = Console.ReadLine();



                if (input2 == "Ja")
                {
                    member = CreateNewMember();
                }
                else
                {
                    member = GuestShopping();
                }
            }
            return member;
        }
        public static Models.Customer CreateNewMember()
        {
            Console.Write("Personnummer (format xxxxxxx-xxxx): ");
            string idNumber = Console.ReadLine();
            Console.Write("Ange ett användnarmn: ");
            string userName = Console.ReadLine();
            Console.Write("Ange ett lösenord: ");
            string passWord = Console.ReadLine();
            Console.Write("Förnamn: ");
            string firstName = Console.ReadLine();
            Console.Write("Efternamn: ");
            string lastName = Console.ReadLine();
            Console.Write("Adress: ");
            string adress = Console.ReadLine();
            Console.Write("Post nummer: ");
            string zipCode = Console.ReadLine();
            Console.Write("Stad: ");
            string city = Console.ReadLine();
            Console.Write("Telefonnummer");
            string phoneNumber = Console.ReadLine();
            Console.Write("E-post: ");
            string email = Console.ReadLine();


            var newCustomer = new Models.Customer() { IdNumber = idNumber, UserName = userName, Password = passWord, FirstName = firstName, LastName = lastName, Adress = adress, ZipCode = zipCode, City = city, PhoneNumber = phoneNumber, Email = email, Membership = true };

            using (var db = new Models.FurnitureOnlineContext())
            {
                var customerList = db.Customers;
                customerList.Add(newCustomer);
                db.SaveChanges();
            }

            return newCustomer;
        }

        public static Models.Customer GuestShopping()
        {
            Console.Write("Personnummer (format xxxxxxx-xxxx): ");
            string idNumber = Console.ReadLine();
            Console.Write("Förnamn: ");
            string firstName = Console.ReadLine();
            Console.Write("Efternamn: ");
            string lastName = Console.ReadLine();
            Console.Write("Adress: ");
            string adress = Console.ReadLine();
            Console.Write("Post nummer: ");
            string zipCode = Console.ReadLine();
            Console.Write("Stad: ");
            string city = Console.ReadLine();
            Console.Write("Telefonnummer");
            string phoneNumber = Console.ReadLine();
            Console.Write("E-post: ");
            string email = Console.ReadLine();


            var newCustomer = new Models.Customer() { IdNumber = idNumber, FirstName = firstName, LastName = lastName, Adress = adress, ZipCode = zipCode, City = city, PhoneNumber = phoneNumber, Email = email };

            using (var db = new Models.FurnitureOnlineContext())
            {
                var customerList = db.Customers;
                customerList.Add(newCustomer);
                db.SaveChanges();
            }

            return newCustomer;
        }

        public static Models.Customer MemberLogin()
        {
            bool correct = false;
 
            while (!correct)
            {


                Console.WriteLine("Ange ditt användarnamn, personnummer(ÅÅÅÅMMDD-NNNN) eller email: ");
                string input = Console.ReadLine();
                Console.WriteLine("Ange ditt lösenord:");
                string password = Console.ReadLine();



                using (var db = new Models.FurnitureOnlineContext())
                {
                    var customerList = db.Customers;



                    {
                        foreach (var customers in customerList)
                        {
                            if ((customers.UserName == input || customers.IdNumber == input || customers.Email == input) && customers.Password == password)
                            {
                                correct = true;
                                return customers;
                            }
                        }

                        Console.WriteLine("Felaktig inmatning, ange enligt exemplet");
                    }

                }
            }

            return null;
        }
    }
}