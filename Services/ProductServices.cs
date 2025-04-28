using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entits;
using Repository;

namespace Services
{
    public class ProductServices : IProductServices
    {
        IProductRepository productRepository;

        public ProductServices(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        //public async Task<Product> AddProduct(Product product)
        //{
        //    return await productRepository.AddProduct(product);

        //}

        //public async Task DeleteProduct(int id, Product productToDelete)
        //{

        //    await productRepository.DeleteProduct(id, productToDelete);
        //}

        public async Task<List<Product>> GetProduct(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            return await productRepository.GetProduct(position, skip, desc, minPrice, maxPrice, categoryIds);
        }

        //public async Task UpdateProduct(int id, Product productToUpdate)
        //{
        //    await productRepository.UpdateProduct(id, productToUpdate);
        //}







    }
}
