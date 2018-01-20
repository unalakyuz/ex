using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trendyol.Case.Models;

namespace Trendyol.Case.Service
{
    public interface IProductService
    {
        IList<Product> GetProducts();
        void AddNewProduct();
        void UpdateProduct(int id);
    }
}
