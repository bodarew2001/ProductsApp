using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using testApp.Data;
using testApp.Models;

namespace testApp.Services
{
    public class ProductService
    {
        MarketContext _context;
        public ProductService()
        {
            _context = new MarketContext();
        }
        public ProductService(MarketContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }
        public Product GetById(int id)
        {
            return _context.Products.Where(x => x.Id == id).FirstOrDefault();
        }
        public async Task Create(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return;
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            if (!ProductExists(product.Id))
            {
                return;
            }
            else
            {
                return;
            }
        }
        public async Task Delete(int id)
        {
            if (_context.Products == null)
            {
                return;
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            await _context.SaveChangesAsync();
        }
        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
