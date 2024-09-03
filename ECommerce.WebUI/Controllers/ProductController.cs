using ECommerce.Business.Abstract;
using ECommerce.Entities.Models;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: ProductController
        public async Task<IActionResult> Index(int page=1,int category=0,bool azOrder=false, bool highLowOrder=false)
        {
            var items=await _productService.GetAllByCategoryAsync(category);
            int pageSize = 10;

            if (azOrder) {
                items = items.OrderBy(x => x.ProductName).ToList();
            
            }
            else
            {
                items = items.OrderByDescending(x => x.ProductName).ToList();

            }

            if (highLowOrder)
            {
                items = items.OrderByDescending(x => x.UnitPrice).ToList();
                
            }
            else
            {
                items = items.OrderBy(x => x.UnitPrice).ToList();
            }

            var model = new ProductListViewModel
            {
                Products = items.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                PageSize = pageSize,
                CurrentCategory = category,
                AZOrder=azOrder,
                HighLowOrder=highLowOrder,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(items.Count / (double)pageSize)

            };
            return View(model);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Search(string key)
        {
            if (key != null)
            {
                var list = await _productService.GetAllAsync();
                var results = list.Where(p => p.ProductName.ToLower().StartsWith(key.ToLower())).ToList();
                var listDto = new ProductListViewModel { Products = results };

                return PartialView("_SearchedProductListPartial", listDto);
            }
            else return Ok();

        }

    }
}
