using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trendyol.Case.Models;
using Trendyol.Case.Repository;

namespace Trendyol.Case.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void AddNewProduct()
        {
             _productRepository.AddNewProduct();
        }

        public IList<Product> GetProducts()
        {
           return _productRepository.GetProducts();
        }

        public void UpdateProduct(int id)
        {
            _productRepository.UpdateProduct(id);
        }
    }
}
