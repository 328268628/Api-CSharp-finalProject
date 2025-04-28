using Entits;

namespace Repository
{
    public interface IProductRepository
    {
        //Task<Product> AddProduct(Product product);
        //Task DeleteProduct(int id, Product productToDelete);
        Task<List<Product>> GetProduct(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds);
        Task<Product> GetById(int productId);
        //Task UpdateProduct(int id, Product productToUpdate);
    }
}