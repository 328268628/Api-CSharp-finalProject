using Entits;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository : IProductRepository
    {
        AdoNetManageContext _AdoNetManageContext;

        public ProductRepository(AdoNetManageContext manageDbContext)
        {
            this._AdoNetManageContext = manageDbContext;
        }

        //public async Task<Product> AddProduct(Product product)
        //{
        //    await _AdoNetManageContext.Products.AddAsync(product);
        //    await _AdoNetManageContext.SaveChangesAsync();
        //    return product;

        //}


        //public async Task UpdateProduct(int id, Product productToUpdate)
        //{
        //    productToUpdate.Id = id;

        //    _AdoNetManageContext.Products.Update(productToUpdate);

        //    await _AdoNetManageContext.SaveChangesAsync();
        //}

        //public async Task<List<Product>> GetProduct()
        //{
        //    //return await _AdoNetManageContext.Products.Include(c => c.Category).ToListAsync();


        //}

       public async Task<List<Product>> GetProduct(int position, int skip, string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            var query = _AdoNetManageContext.Products.Where(product =>
              (desc == null ? (true) : (product.Name.Contains(desc)))
              && ((minPrice == null) ? (true) : (product.Price >= minPrice))
              && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
              && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
              .OrderBy(product => product.Price).Include(a => a.Category);
            List<Product> products = await query.ToListAsync();
            return products;
        }



        public async Task<Product> GetById(int productId)
        {
            Product product = await _AdoNetManageContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            return product;
        }

        //public async Task DeleteProduct(int id, Product productToDelete)
        //{
        //    productToDelete.Id = id;

        //    _AdoNetManageContext.Products.Remove(productToDelete);

        //    await _AdoNetManageContext.SaveChangesAsync();

        //}


    }
}
