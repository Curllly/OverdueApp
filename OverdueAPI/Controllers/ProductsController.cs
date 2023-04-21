using Microsoft.AspNetCore.Mvc;
using OverdueAPI.Models;
using OverdueAPI.Repositories;
using OverdueAPI.Services;

namespace OverdueAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private IProductServise ProductServise { get; set; }
        private IBaseRepository<Product> Products { get; set; }
        public ProductsController(IProductServise productServise, IBaseRepository<Product> products)
        {
            ProductServise = productServise;
            Products = products;
        }
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(Products.GetAll());
        }
        [HttpPost]
        public JsonResult Post(Product product)
        {
            try
            {
                ProductServise.CreateProduct(product);
                return new JsonResult("Продукт успешно добавлен");
            }
            catch
            {
                return new JsonResult(BadRequest()); // -> Спросить
            }
        }
        [HttpPut] // -> Спросить
        public JsonResult Put(Product product) 
        {
            bool success = true;
            var toUpgradeProduct = Products.Get(product.Id);
            try
            {
                if (toUpgradeProduct != null)
                {
                    toUpgradeProduct = Products.Update(product);
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success ? new JsonResult($"Продукт успешно обновлён {toUpgradeProduct.Id}")
                : new JsonResult(BadRequest()); // -> Спросиить
        }
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            bool success = true;
            var toDeleteProduct = Products.Get(id);
            try
            {
                if ( toDeleteProduct != null )
                {
                    Products.Delete(toDeleteProduct.Id);
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success ? new JsonResult("Продукт успешно удалён") 
                : new JsonResult(BadRequest());
        }
    }
}
