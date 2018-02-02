using Basket.DAL.Models;
using Basket.DAL.Models.Requests;
using Basket.DAL.Requests;
using Basket.DAL.Responses;
using Basket.Library;
using Basket.Library.Helpers;
using Basket.Library.Interfaces;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Basket.ConsoleApp
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async System.Threading.Tasks.Task MainAsync(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Request a new token...");
            Console.WriteLine("");
            BasketService bs = new BasketService("test1", "test1");
            await bs.CreateToken();

            if (bs != null)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Token");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(bs.Token);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("");
                Console.WriteLine("Token expired date");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(bs.TokenExpiredDate.ToString());
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Token creation is failed.");
            }

            await ShowProducts(bs.AuthorizationToken);

            Console.WriteLine("");

            BasketRequest request = new BasketRequest();
            request.ClientId = "A300";
            request.SKU = "A111";
            request.Quantity = 1;
            bool addResult = await bs.AddItem(request);
            if (addResult)
            {
                Console.WriteLine("Add a new item in the basket");
                Console.ForegroundColor = ConsoleColor.White;
                await ShowProducts(bs.AuthorizationToken);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Add a new item in the basket is failed!");
            }

            await ShowBasket(bs, "A300");

            Console.WriteLine("");
            Console.WriteLine("Done.");
            Console.ReadKey();
        }

        private static async Task ShowBasket(BasketService bs, string clientId)
        {
            List<BasketRequest> list = await bs.GetBasket(clientId);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("");
            Console.WriteLine("SKU\tPrice\tQty");
            Console.ForegroundColor = ConsoleColor.White;

            foreach (BasketRequest product in list)
                Console.WriteLine($"{product.SKU}\t{product.TotalPrice}\t{product.Quantity}");
        }

        private static async Task ShowProducts(string auth)
        {
            ProductService ps = new ProductService(auth);
            List<ProductModel> list = await ps.GetProducts();

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("");
            Console.WriteLine("SKU\tPrice\tQty");
            Console.ForegroundColor = ConsoleColor.White;

            foreach (ProductModel product in list)
                Console.WriteLine($"{product.SKU}\t{product.Price}\t{product.Quantity}");
        }
    }
}