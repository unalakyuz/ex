using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trendyol.Case.Models
{
    public class ListPageModel
    {
        public IList<Product>Products { get; set; }
        public int PageCount { get; set; }

        public ListPageModel()
        {
            Products = new List<Product>();
        }
    }
}
