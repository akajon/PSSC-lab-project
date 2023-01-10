using System;
using Main.Domain;
using static Main.Domain.Orders;
using System.IO;
using System.Text.Json;
using PSSC_lab_project.Main.Domain;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Security;

namespace Main {
    class Program
    {
        static void Main(string[] args)
        {
            List<Account> Accounts = LoadAccounts();
            List<Product> Products = LoadProducts();

            Account currentAccount;

            Console.WriteLine("\nSelectati un cont:");
            foreach (Account account in Accounts)
            {
                Console.WriteLine(account);
            }
            var selectedOption = Int16.Parse(ReadValue("Cont selectat: "));

            currentAccount = Accounts.ElementAt(selectedOption);
            Console.WriteLine("Ati selectat contul: " + currentAccount.ToString());

            do
            {
                Console.WriteLine("\nSelectati un produs:");
                foreach (Product product in Products)
                {
                    Console.WriteLine(product);
                }
                selectedOption = Int16.Parse(ReadValue("Produs selectat: "));
                Product selectedProduct = Products.ElementAt(selectedOption);
                Console.WriteLine("Ati selectat produsul: " + selectedProduct.ToString());

                int cantitate = (Int16.Parse(ReadValue("\nIntroduceti cantitatea: ")));
                Amount amount = new Amount(cantitate);

                var unvalidatedOrder = new UnvalidatedOrder(currentAccount, selectedProduct, amount);

                IOrders result = ValidateOrder(unvalidatedOrder);
                result.Match(
                    whenUnvalidatedOrders: unvalidatedResult => unvalidatedResult,
                    whenPublishedOrders: publishedResult => publishedResult,
                    whenInvalidatedOrders: invalidResult => invalidResult,
                    whenValidatedOrders: validatedResult => PublishOrders(validatedResult)
                );
                Console.WriteLine(result);
            } while (true);
        }

        private static IOrders ValidateOrder(UnvalidatedOrder unvalidatedOrder) =>
            unvalidatedOrder.amount.Value > unvalidatedOrder.product.Amount ?
            new InvalidatedOrders(new List<UnvalidatedOrder>(), "Amount requested bigger than stock!")
            : new ValidatedOrders(new List<ValidatedOrder>());

        private static IOrders PublishOrders(ValidatedOrder validOrders) =>
            new PublishedOrders(new List<ValidatedOrder>(), DateTime.Now);

        private static string? ReadValue(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }

        private static List<Account> LoadAccounts()
        {
            var AccountList = new List<Account>();
            string text = File.ReadAllText("C:/Users/pearja/Documents/UNI/IS IV S1/PSSC/PSSC-lab-project/Main/Data/Account.json");
            JArray array = JArray.Parse(text);
            foreach (JObject obj in array.Children<JObject>())
            {
                var FirstName = obj["FirstName"].ToString();
                var LastName = obj["LastName"].ToString();
                var Role = obj["Role"].ToString();
                var ShippingAddress = obj["ShippingAddress"].ToString();
                if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) || string.IsNullOrEmpty(Role)
                    || string.IsNullOrEmpty(ShippingAddress))
                {
                    Console.WriteLine("Invalid input data in JSON");
                    Environment.Exit(1);
                }
                AccountList.Add(new Account(FirstName, LastName, Role, ShippingAddress));
                Console.WriteLine("Loaded User: " + FirstName + " | " + LastName + " | " + Role + " | " + ShippingAddress);
            }
            return AccountList;
        }

        private static List<Product> LoadProducts()
        {
            var ProductList = new List<Product>();
            string text = File.ReadAllText("C:/Users/pearja/Documents/UNI/IS IV S1/PSSC/PSSC-lab-project/Main/Data/Product.json");
            JArray array = JArray.Parse(text);
            foreach (JObject obj in array.Children<JObject>())
            {
                var Name = obj["Name"].ToString();
                var Price = Int16.Parse(obj["Price"].ToString());
                var Amount = Int16.Parse(obj["Amount"].ToString());
                if (string.IsNullOrEmpty(Name) || Price <= 0 || Amount < 0)
                {
                    Console.WriteLine("Invalid input data in JSON");
                    Environment.Exit(1);
                }
                ProductList.Add(new Product(Name, Price, Amount));
                Console.WriteLine("Loaded Product: " + Name + " | " + Price + " | " + Amount);
            }
            return ProductList;
        }
    }
}