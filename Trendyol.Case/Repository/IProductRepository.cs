using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trendyol.Case.Models;

namespace Trendyol.Case.Repository
{
    public interface IProductRepository
    {
        IList<Product> GetProducts();
        void AddNewProduct();
        void UpdateProduct(int id);
    }
}
