using BusinessObjects;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        LucySalesDataContext context = new LucySalesDataContext();

        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public Category? GetCategoryById(int id)
        {
            return context.Categories.FirstOrDefault(c => c.CategoryID == id);
        }

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            context.Categories.Update(category);
            context.SaveChanges();
        }

        public void DeleteCategory(int id)
        {
            var category = context.Categories.FirstOrDefault(c => c.CategoryID == id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }
    }
}
