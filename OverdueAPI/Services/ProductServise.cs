using OverdueAPI.Models;
using OverdueAPI.Repositories;

namespace OverdueAPI.Services
{
    public class ProductServise : IProductServise
    {
        public ProductServise(IBaseRepository<Product> products) 
        { 
            Products = products;
        }
        private IBaseRepository<Product> Products { get; set; }
        public void CreateProduct(Product product)
        {
            Product temp = new Product()
            {
                Id = product.Id,
                Title = product.Title,
                Manufactured = product.Manufactured,
                BestBefore = product.BestBefore,
                PlaceId = product.PlaceId,
                Place = product.Place,
                Count = product.Count,
            };
            Products.Create(temp);
        }
    }
}
