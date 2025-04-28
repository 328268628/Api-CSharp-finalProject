using Entits;

namespace Services
{
    public interface ICategoryServices
    {
        //Task<Category> AddCategory(Category category);
        //Task DeleteCategory(int id, Category categoryToDelete);
        Task<List<Category>> GetCategory();
        //Task<Category> GetCategoryById(int id);
        //Task UpdateCategory(int id, Category categoryToUpdate);
        
    }
}