using BusinessObjects;
using DataAccessLayer;
using Repositories.Interface;

namespace Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CategoryDAO categoryDAO;

        public CategoryRepository()
        {
            categoryDAO = new CategoryDAO();
        }

        public List<Category> GetAllCategories()
        {
            return categoryDAO.GetCategories();
        }

        public Category? GetCategoryById(int id)
        {
            return categoryDAO.GetCategoryById(id);
        }

        public void AddCategory(Category category)
        {
            categoryDAO.AddCategory(category);
        }

        public void UpdateCategory(Category category)
        {
            categoryDAO.UpdateCategory(category);
        }

        public void DeleteCategory(int id)
        {
            categoryDAO.DeleteCategory(id);
        }
    }
}