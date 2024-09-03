using ECommerce.Business.Abstract;
using ECommerce.Entities.Models;
using ECommerce.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public AdminController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int page = 1, int category = 0, bool azOrder = false, bool highLowOrder = false)
        {
            var items = await _productService.GetAllByCategoryAsync(category);
            int pageSize = 10;

            if (azOrder)
            {
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
                AZOrder = azOrder,
                HighLowOrder = highLowOrder,
                CurrentPage = page,
                PageCount = (int)Math.Ceiling(items.Count / (double)pageSize)

            };
            return View(model);
        }

        public async Task<IActionResult> RemoveProduct(int productId)
        {
            await _productService.DeleteAsync(productId);
            TempData.Add("message", "Product was removed successfully!");
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> RemoveCategory(int categoryId)
        {
            await _categoryService.DeleteAsync(categoryId);
            TempData.Add("message", "Category was removed successfully!");
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {
            var vm = new CategoryAddViewModel
            {
                Category = new Category()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(CategoryAddViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _categoryService.AddAsync(vm.Category);
                return RedirectToAction("Index");
            }
            else { return View(vm); }
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var model = new ProductAddViewModel
            {
                Product = new Product()
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult AddProduct(ProductAddViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _productService.AddAsync(vm.Product);
                return RedirectToAction("Index");
            }
            else { return View(vm); }
        }

    }
}
