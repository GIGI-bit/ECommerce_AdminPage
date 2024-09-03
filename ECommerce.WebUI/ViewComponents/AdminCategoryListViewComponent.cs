using ECommerce.Business.Abstract;
using ECommerce.Business.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace ECommerce.WebUI.ViewComponents
{
    public class AdminCategoryListViewComponent : ViewComponent
    {
        private ICategoryService categoryService;

        public AdminCategoryListViewComponent(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ViewViewComponentResult Invoke()
        {
            var categories = categoryService.GetAllAsync().Result;
            var param = HttpContext.Request.Query["category"];
            var category = int.TryParse(param, out var categoryId);
            var model = new CategoryListViewModel
            {
                Categories = categories,
                CurrentCategory = category ? categoryId : 0,
            };
            return View(model);
        }


    }
}
