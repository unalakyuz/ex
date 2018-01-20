using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Trendyol.Case.Models;
using Trendyol.Case.Service;

namespace Trendyol.Case.Controllers
{

    public class BaseController : Controller
    {
        private IMemoryCache _cache;
        private readonly IProductService _productService;

        public BaseController(IMemoryCache memoryCache, IProductService productService)
        {
            _cache = memoryCache;
            _productService = productService;
        }

        protected IList<Product> Products()
        {
            return _cache.Get("ProductList") as IList<Product>;
        }

        protected void CheckProductCacheStatus()
        {
            if (_cache.Get("ProductList") != null)
            {
                var cachedProducts = _cache.Get("ProductList") as IList<Product>;
                var unCachedProducts = _productService.GetProducts();

                foreach (var unCachedProduct in unCachedProducts)
                {
                    var cachedProduct = cachedProducts.Where(g => g.Id == unCachedProduct.Id).SingleOrDefault();
                    if (cachedProduct != null)
                    {
                        if (unCachedProduct.LastUpdatedTime.HasValue && cachedProduct.LastUpdatedTime == null)
                        {
                            cachedProduct.Name = unCachedProduct.Name;
                            cachedProduct.LastUpdatedTime = unCachedProduct.LastUpdatedTime;
                        }
                        if (unCachedProduct.LastUpdatedTime.HasValue && cachedProduct.LastUpdatedTime.HasValue && unCachedProduct.LastUpdatedTime.Value != cachedProduct.LastUpdatedTime.Value)
                        {
                            cachedProduct.Name = unCachedProduct.Name;
                            cachedProduct.LastUpdatedTime = unCachedProduct.LastUpdatedTime;
                        }
                    }
                    else
                    {
                        var newlyAddedProduct = new Product
                        {
                            Id = unCachedProduct.Id,
                            Name = unCachedProduct.Name,
                            LastUpdatedTime = unCachedProduct.LastUpdatedTime
                        };

                        (_cache.Get("ProductList") as IList<Product>).Add(newlyAddedProduct);
                    }
                }
            }
            else
            {
                _cache.Set("ProductList", _productService.GetProducts());
            }
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var currentUrl = HttpContext.Request.Path;

            // Assuming products are listed in main page and the current url is / otherwise we can cache the products without url constraints.
            if (currentUrl == "/")
            {
                if (_cache.Get("ProductList") == null)
                {
                    _cache.Set("ProductList", _productService.GetProducts());
                }
            }

            base.OnActionExecuting(context);
        }
    }
}