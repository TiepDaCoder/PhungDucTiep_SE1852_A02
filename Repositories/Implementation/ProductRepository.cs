using BusinessObjects;
using DataAccessLayer;
using Repositories.Interface;

namespace Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDAO _productDAO;

        public ProductRepository()
        {
            _productDAO = new ProductDAO();
        }

        public List<Product> GetAll() => _productDAO.GetAllProducts();
        public void Add(Product product) => _productDAO.Add(product);
        public void Update(Product product) => _productDAO.Update(product);
        public void Delete(int productId) => _productDAO.Delete(productId);
        public List<Product> Search(string keyword) => _productDAO.Search(keyword);
    }
}