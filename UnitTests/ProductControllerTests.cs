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
    public class ProductControllerTests
    {
        [Fact]
        public void GetDataMessage()
        {
            var products = new Mock<IBaseRepository<Product>>();
            var service = new Mock<IProductServise>();

            var product = GetProduct();

            products.Setup(x => x.GetAll()).Returns(new List<Product> { product });

            ProductsController controller = new ProductsController(service.Object, products.Object);
            JsonResult result = controller.Get() as JsonResult;

            Assert.Equal(new List<Product> { product }, result?.Value );
        }
        [Fact]
        public void GetNotNull()
        {
            var products = new Mock<IBaseRepository<Product>>();
            var service = new Mock<IProductServise>();

            products.Setup(x => x.Create(GetProduct())).Returns(GetProduct());
            ProductsController controller = new ProductsController(service.Object, products.Object);

            JsonResult result = controller.Get() as JsonResult;

            Assert.NotNull(result);
        }
        [Fact]
        public void PostDataMessage() 
        {
            var products = new Mock<IBaseRepository<Product>>();
            var service = new Mock<IProductServise>();
            
            products.Setup(x => x.Create(GetProduct())).Returns(GetProduct());

            ProductsController controller = new ProductsController(service.Object, products.Object);

            JsonResult result = controller.Post(GetProduct()) as JsonResult;

            Assert.Equal("Продукт успешно добавлен", result?.Value);
        }
        [Fact]
        public void UpdateDataMessage() 
        {
            var products = new Mock<IBaseRepository<Product>>();
            var service = new Mock<IProductServise>();
            var product = GetProduct();

            products.Setup(x => x.Get(product.Id)).Returns(product);
            products.Setup(x => x.Update(product)).Returns(product);

            ProductsController controller = new ProductsController(service.Object, products.Object);

            JsonResult result = controller.Put(product) as JsonResult;

            Assert.Equal($"Продукт успешно обновлён {product.Id}", result?.Value);
        }
        [Fact]
        public void DeleteDataMessage()
        {
            var products = new Mock<IBaseRepository<Product>>();
            var service = new Mock<IProductServise>();
            var product = GetProduct();

            products.Setup(x => x.Get(product.Id)).Returns(product);
            products.Setup(x => x.Delete(product.Id));

            ProductsController controller = new ProductsController(service.Object, products.Object);

            JsonResult result = controller.Delete(product.Id) as JsonResult;

            Assert.Equal("Продукт успешно удалён", result?.Value);
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