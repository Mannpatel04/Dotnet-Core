using Assignment_1.Data;
using Assignment_1.Interfaces;
using Assignment_1.Models;

namespace Assignment_1.Services
{
    public class ProductServices : IProductService
    {

        //Dependency Injection
        private readonly AppDBContext _context;

        public ProductServices(AppDBContext context) 
        {
            _context = context;
        }

        public List<Product> GetAll() {
             return _context.Products.ToList();
        }

        public Product GetById(int id) { 
            return _context.Products.Find(id);
        }

        public List<Product> GetByCategory(string category)
        {
            return _context.Products.Where(p=> p.Category == category).ToList();
        }
        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public bool Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }

        public bool Update(Product product)
        {
            var existing = _context.Products.Find(product.Id);
            if (existing == null) return false;

            existing.Name = product.Name;
            existing.Category = product.Category;

            _context.SaveChanges();
            return true;
        }
    }

}
