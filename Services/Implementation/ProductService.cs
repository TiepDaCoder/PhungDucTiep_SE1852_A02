using BusinessObjects;
using Repositories.Implementation;
using Repositories.Interface;
using Services.Interface;

namespace Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public List<Product> GetAll() => _productRepository.GetAll();
        public void Add(Product product) => _productRepository.Add(product);
        public void Update(Product product) => _productRepository.Update(product);
        public void Delete(int productId) => _productRepository.Delete(productId);
        public List<Product> Search(string keyword) => _productRepository.Search(keyword);
    }
}