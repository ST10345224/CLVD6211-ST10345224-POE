using KhumaloCraftPOE.Data;
using KhumaloCraftPOE.Models;
using Microsoft.EntityFrameworkCore;

namespace KhumaloCraftPOE.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly KhumaloCraftPOEDbContext _context; // Replace with your actual DbContext class name

        public ProductRepository(KhumaloCraftPOEDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Find(id);
        }

        public void AddProduct(Product product)
        {
            
            _context.Products.Add(product);
            _context.SaveChanges();
            
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var productToDelete = _context.Products.Find(id);
            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

