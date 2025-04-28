using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entits;
using Repository;

namespace Services
{
    public class CategoryServices : ICategoryServices
    {
        ICategoryRepository categoryRepository;

        public CategoryServices(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        //public async Task<Category> AddCategory(Category category)
        //{
        //    return await categoryRepository.AddCategory(category);
        //}

        //public async Task DeleteCategory(int id, Category categoryToDelete)
        //{
        //    await categoryRepository.DeleteCategory(id, categoryToDelete);

        //}

        public async Task<List<Category>> GetCategory()
        {
            return await categoryRepository.GetCategory();
        }


        //public async Task<Category> GetCategoryById(int id)
        //{
        //    return await categoryRepository.GetCategoryById(id);
        //}

        //public async Task UpdateCategory(int id, Category categoryToUpdate)
        //{
        //    await categoryRepository.UpdateCategory(id, categoryToUpdate);
        //}



    }
}
