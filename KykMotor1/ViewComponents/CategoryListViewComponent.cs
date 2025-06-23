using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.ViewComponents
{
    public class CategoryListViewComponent: ViewComponent
    {
        private readonly ICategoryServices _categoryService;

        public CategoryListViewComponent(ICategoryServices categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            return View(new CategoryListModel()
            {
                Categories = _categoryService.GetAll()

            });

        }
    }
}
