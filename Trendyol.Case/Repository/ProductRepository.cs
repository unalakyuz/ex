using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trendyol.Case.Models;

namespace Trendyol.Case.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public IList<Product> GetProducts()
        {
            // 100 item created as default
            if (!_context.Products.Any())
            {
                for (int i = 0; i < 100; i++)
                {
                    _context.Products.Add(new Product { Id = i + 1, Name = "product-" + i, LastUpdatedTime = null });
                }
                _context.SaveChanges();
            }
            return _context.Products.ToList();
        }

        // add 20 more product
        public void AddNewProduct()
        {
            int start = _context.Products.Count() + 1;
            int end = _context.Products.Count() + 20;

            for (int i = start; i <= end; i++)
            {
                _context.Products.Add(new Product { Id = i, Name = "new product-" + i, LastUpdatedTime = null });
                _context.SaveChanges();
            }
        }

        public void UpdateProduct(int id)
        {
            var product = _context.Products.Where(g => g.Id == id).SingleOrDefault();
            product.LastUpdatedTime = DateTime.Now;

            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
