using BusinessObjects;

namespace Services.Interface
{
    public interface IProductService
    {
        List<Product> GetAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
        List<Product> Search(string keyword);
    }
}