

using Assignment_1.Models;

namespace Assignment_1.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product GetById(int id);
        List<Product> GetByCategory(string category);
        void Add(Product product);
        bool Delete(int id);
    }
}
