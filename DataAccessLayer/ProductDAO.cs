using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        private readonly LucySalesDataContext _context;

        public ProductDAO()
        {
            _context = new LucySalesDataContext();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products
                .Include(p => p.Category)
                .ToList();
        }

        public void Add(Product p)
        {
            _context.Products.Add(p);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            var existing = _context.Products.Find(product.ProductID);
            if (existing != null)
            {
                existing.ProductName = product.ProductName;
                existing.UnitPrice = product.UnitPrice;
                existing.UnitsInStock = product.UnitsInStock;
                existing.UnitsOnOrder = product.UnitsOnOrder;
                existing.ReorderLevel = product.ReorderLevel;
                existing.Discontinued = product.Discontinued;
                existing.CategoryID = product.CategoryID;
                existing.SupplierID = product.SupplierID;
                existing.QuantityPerUnit = product.QuantityPerUnit;

                _context.SaveChanges();
            }
        }

        public void Delete(int productId)
        {
            var p = _context.Products.Find(productId);
            if (p != null)
            {
                _context.Products.Remove(p);
                _context.SaveChanges();
            }
        }

        public List<Product> Search(string keyword)
        {
            return _context.Products
                .Where(p => p.ProductName.Contains(keyword))
                .Include(p => p.Category)
                .ToList();
        }
    }
}