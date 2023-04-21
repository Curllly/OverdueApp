using Microsoft.AspNetCore.Mvc;
using Moq;
using OverdueAPI.Controllers;
using OverdueAPI.Models;
using OverdueAPI.Repositories;
using OverdueAPI.Services;
using System.Reflection.Metadata;
using Xunit;

namespace UnitTests
{
    public class ProductServiceTests
    {
        [Fact]
        public void ProductSuccess()
        {
            var products = new Mock<IBaseRepository<Product>>();
            var service = new Mock<IProductServise>();
            var product = GetProduct();

            products.Setup(x => x.Create(product)).Returns(product);

            service.Object.CreateProduct(product);

            service.Verify(x => x.CreateProduct(product));

        }
        public Product GetProduct()
        {
            return new Product
            {
                Id = 1,
                Title = "Title",
                Manufactured = DateTime.Now.Date,
                BestBefore = DateTime.Now.Date.AddDays(20),
                Count = 1,
                PlaceId = 1,
            };
        }
    }
}
