using BusinessObjects;

namespace Repositories.Interface
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
        List<Product> Search(string keyword);
    }
}