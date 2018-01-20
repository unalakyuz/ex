using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ReflectionIT.Mvc.Paging;
using System;
using System.Diagnostics;
using System.Linq;
using Trendyol.Case.Models;
using Trendyol.Case.Service;

namespace Trendyol.Case.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductService _productService;

        public HomeController(IMemoryCache memoryCache, IProductService productService) : base(memoryCache, productService)
        {
            _productService = productService;
        }

        //this action can be also cached with output caching by page parameter in need.
        public IActionResult Index(int page = 1)
        {
            var products = Products().OrderBy(p => p.Id).Skip((page - 1) * 10).Take(10).ToList();
            return View(new ListPageModel { Products = products, PageCount = PageCount(Products().Count, 10) });
        }

        private int PageCount(int total, int perPage)
        {
            int temp;
            return Math.DivRem(total, perPage, out temp) +1;
          
        }

        public IActionResult RunInterval()
        {
            CheckProductCacheStatus();
            return RedirectToAction("Index");
        }

        public IActionResult AddNewProduct()
        {
            _productService.AddNewProduct();
            return RedirectToAction("Index");
        }

        public IActionResult UpdateProduct(int id)
        {   
            _productService.UpdateProduct(id);
            return RedirectToAction("Index");
        }




        #region MyRegion

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
