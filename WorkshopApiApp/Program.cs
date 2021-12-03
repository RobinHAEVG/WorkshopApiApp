using System;
using WorkshopApi;

namespace WorkshopApiApp
{
    class Program
    {
        static void Main(string[] args)
        {
            V2();
            Console.ReadKey();
        }

        private static void V2()
        {
            var clientV2 = new WorkshopApiV2("http://144.76.198.141:6789", "T1noKRpZOOKFMmVYifoczdL4sqgPAOq3vGtTr6WF");
            clientV2.Authenticate();

            // hole alle Produkte
            //var allProducts = clientV2.GetAllProducts();
            //Console.WriteLine("Alle Produkte:");
            //foreach (Product p in allProducts)
            //{
            //    Console.WriteLine($"\t{p.Id}: {p.Name} kostet {p.Price} Euronen.");
            //}

            // hole ein bestimmtes Produkt
            //var singleProduct = clientV2.GetProduct(1);
            //Console.WriteLine($"\nEinzelnes Produkt mit ID 1: {singleProduct.Name} kostet {singleProduct.Price} Euro.");

            // füge ein neues Produkt hinzu
            //Product newProduct = new Product()
            //{
            //    Name = "Robins tolles Dingsbums",
            //    Price = 1337.98m,
            //};
            //clientV2.AddProduct(newProduct);
            //Console.WriteLine($"Neu hinzugefügtes Produkt hat die ID {newProduct.Id}");

            // editiere Name und Preis eines vorhandenen Produkts
            //var editProduct = new Product()
            //{
            //    Id = 105,
            //    Name = "Robins richtig tolles Dingensbummens",
            //    Price = 4337.99m,
            //};
            //clientV2.EditProduct(editProduct);
            //Console.WriteLine($"Produkt mit ID {editProduct.Id} heißt jetzt {editProduct.Name} und kostet {editProduct.Price} Euro.");

            // lösche vorhandenes Produkt
            //clientV2.RemoveProduct(105);
            //Console.WriteLine("Produkt mit der ID 105 gelöscht.");

            /* *** */

            // hole alle Reviews für ein Produkt
            var allReviews = clientV2.GetAllReviews(1);
            foreach (Review r in allReviews)
            {
                Console.WriteLine($"Review #{r.Id} für Produkt-ID 1 vergibt {r.Rating} Sterne: {r.Text}");
            }

            // hole ein spezifisches Produkt
            var singleReview = clientV2.GetReview(1, 101);

        }

        private static void V1()
        {
            var clientV1 = new WorkshopApiV1("http://144.76.198.141:6789", "T1noKRpZOOKFMmVYifoczdL4sqgPAOq3vGtTr6WF");

            // hole alle Produkte
            //var allProducts = clientV1.GetAllProducts();
            //Console.WriteLine("Alle Produkte:");
            //foreach (Product p in allProducts)
            //{
            //    Console.WriteLine($"\t{p.Id}: {p.Name} kostet {p.Price} Euronen.");
            //}

            // hole ein bestimmtes Produkt
            //var singleProduct = clientV1.GetProduct(1);
            //Console.WriteLine($"\nEinzelnes Produkt mit ID 1: {singleProduct.Name} kostet {singleProduct.Price} Euro.");

            // füge ein neues Produkt hinzu
            //Product newProduct = new Product()
            //{
            //    Name = "Robins tolles Dingsbums",
            //    Price = 1337.98m,
            //};
            //clientV1.AddProduct(newProduct);
            //Console.WriteLine($"Neu hinzugefügtes Produkt hat die ID {newProduct.Id}");

            // editiere Name und Preis eines vorhandenen Produkts
            //var editProduct = new Product()
            //{
            //    Id = 101,
            //    Name = "Robins richtig tolles Dingensbummens",
            //    Price = 4337.99m,
            //};
            //clientV1.EditProduct(editProduct);
            //Console.WriteLine($"Produkt mit ID {editProduct.Id} heißt jetzt {editProduct.Name} und kostet {editProduct.Price} Euro.");

            // lösche vorhandenes Produkt
            //clientV1.RemoveProduct(101);
            //Console.WriteLine("Produkt mit der ID 101 gelöscht.");
        }
    }
}
