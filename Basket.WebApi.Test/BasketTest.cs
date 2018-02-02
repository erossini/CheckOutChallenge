using Basket.DAL.Enums;
using Basket.DAL.Models;
using Basket.DAL.Requests;
using Basket.DAL.Responses;
using Basket.WebApi.Controllers;
using Basket.WebApi.Models;
using Basket.WebApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Xunit;

namespace Basket.WebApi.Test
{
    public class BasketTest
    {
        private BasketContext GetContextWithData()
        {
            var options = new DbContextOptionsBuilder<BasketContext>()
                              .UseInMemoryDatabase(Guid.NewGuid().ToString())
                              .Options;
            var context = new BasketContext(options);

            // add seeds
            context.Products.Add(new ProductModel() { SKU = "P111", Description = "Surface Phone", Price = 100, Quantity = 100 });
            context.Products.Add(new ProductModel() { SKU = "P112", Description = "Surface Pro 1", Price = 120, Quantity = 200 });
            context.Products.Add(new ProductModel() { SKU = "P113", Description = "Surface Pro 2", Price = 140, Quantity = 200 });
            context.Products.Add(new ProductModel() { SKU = "P111", Description = "Windows10 Pro", Price = 123, Quantity = 600 });

            context.Baskets.Add(new BasketRequest() { ClientId = "A111", Quantity = 5, SKU = "P111", TotalPrice = 500 });
            context.Baskets.Add(new BasketRequest() { ClientId = "A111", Quantity = 1, SKU = "P112", TotalPrice = 200 });
            context.Baskets.Add(new BasketRequest() { ClientId = "A115", Quantity = 1, SKU = "P112", TotalPrice = 200 });
            context.SaveChanges();

            return context;
        }

        [Fact(DisplayName = "Get return one value from the test model")]
        public void GetReturnOneValueFromTheTestModel()
        {
            using (var context = GetContextWithData())
            using (var controller = new BasketController(context))
            {
                List<BasketRequest> result = (List<BasketRequest>)controller.Get("A115");

                Assert.NotNull(result);
                Assert.Single(result);
                Assert.True(result[0].SKU == "P112" && result[0].TotalPrice == 200 && result[0].Quantity == 1);
            }
        }

        [Fact(DisplayName = "Get return two values from the test model")]
        public void GetReturnTwoValueFromTheTestModel2()
        {
            using (var context = GetContextWithData())
            using (var controller = new BasketController(context))
            {
                List<BasketRequest> result = (List<BasketRequest>)controller.Get("A111");

                Assert.NotNull(result);
                Assert.Equal(2, result.Count);
                Assert.True((result[0].SKU == "P111" && result[0].TotalPrice == 500 && result[0].Quantity == 5) &&
                            (result[1].SKU == "P112" && result[1].TotalPrice == 200 && result[1].Quantity == 1));
            }
        }

        [Fact(DisplayName = "Add a product in a basket")]
        public void AddProductToABasket()
        {
            using (var context = GetContextWithData())
            using (var controller = new BasketController(context))
            {
                BasketRequest model = new BasketRequest();
                model.ClientId = "A111";
                model.Quantity = 1;
                model.SKU = "P113";

                IActionResult result = controller.Add(model);
                Assert.IsType<OkResult>(result);
            }
        }

        [Fact(DisplayName = "Add two product in a basket")]
        public void CheckAddProductsToABasket()
        {
            using (var context = GetContextWithData())
            using (var controller = new BasketController(context))
            {
                BasketRequest model = new BasketRequest();
                model.ClientId = "A120";
                model.Quantity = 1;
                model.SKU = "P113";

                IActionResult result = controller.Add(model);
                Assert.IsType<OkResult>(result);

                model = new BasketRequest();
                model.ClientId = "A120";
                model.Quantity = 1;
                model.SKU = "P113";

                result = controller.Add(model);
                Assert.IsType<OkResult>(result);

                List<BasketRequest> list = (List<BasketRequest>)controller.Get("A120");
                Assert.NotNull(list);
                Assert.Single(list);
                Assert.True(list[0].SKU == "P113" && list[0].TotalPrice == 280 && list[0].Quantity == 2);
            }
        }

        [Fact(DisplayName = "Try add an unknown product in a basket")]
        public void TryAddProductToABasket()
        {
            using (var context = GetContextWithData())
            using (var controller = new BasketController(context))
            {
                BasketRequest model = new BasketRequest();
                model.ClientId = "A111";
                model.Quantity = 1;
                model.SKU = "P116";

                IActionResult result = controller.Add(model);
                Assert.IsType<BadRequestObjectResult>(result);

                BasketResponse value = (BasketResponse)((Microsoft.AspNetCore.Mvc.ObjectResult)result).Value;
                Assert.Equal(BasketError.ProductNotAvailable, value.Error);
                Assert.False(value.Success);
                Assert.Equal(0, (int)value.SuccessResult);
            }
        }

        [Fact(DisplayName = "Empty your basket")]
        public void EmptyYourBasket()
        {
            using (var context = GetContextWithData())
            using (var controller = new BasketController(context))
            {
                BasketRequest model = new BasketRequest();
                model.ClientId = "A120";
                model.Quantity = 1;
                model.SKU = "P113";

                IActionResult result = controller.Add(model);
                Assert.IsType<OkResult>(result);

                model = new BasketRequest();
                model.ClientId = "A120";
                model.Quantity = 1;
                model.SKU = "P113";

                result = controller.Add(model);
                Assert.IsType<OkResult>(result);

                controller.EmptyBasket("A120");
                List<BasketRequest> list = (List<BasketRequest>)controller.Get("A120");
                Assert.True(list.Count == 0);
            }
        }

        [Fact(DisplayName = "Verify creation token")]
        public void TokenTest()
        {
            using (var context = GetContextWithData())
            using (var controller = new TokenController())
            {
                IActionResult result = controller.Create(new DAL.Models.Requests.TokenRequest("test1", "test1"));
                Assert.IsType<OkObjectResult>(result);

                TokenResponse token = (TokenResponse)((ObjectResult)result).Value;
                Assert.NotEmpty(token.Token);
            }
        }
    }
}