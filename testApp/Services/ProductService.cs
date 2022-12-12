using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using testApp.Data;
using testApp.Models;

namespace testApp.Services
{
    public class ProductService
    {
        ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Get()
        {
            return _context.Products.ToList();
        }
        public Product GetById(int id)
        {
            return _context.Products.Where(x => x.Id == id).FirstOrDefault();
        }
        public void Create(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return;
            }
            _context.Products.Update(product);
            _context.SaveChanges();
            if (!ProductExists(product.Id))
            {
                return;
            }
            else
            {
                return;
            }
        }
        public void Delete(int id)
        {
            if (_context.Products == null)
            {
                return;
            }
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            _context.SaveChanges();
        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
