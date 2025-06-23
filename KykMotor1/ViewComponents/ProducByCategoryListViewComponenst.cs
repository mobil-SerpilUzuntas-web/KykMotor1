using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.ViewComponents
{
    public class ProducByCategoryListViewComponenst: ViewComponent
    {
        private readonly IProductServices _productServices;
        private readonly ICategoryServices _categoryService;
        public ProducByCategoryListViewComponenst(IProductServices productServices, ICategoryServices categoryService)
        {
            _productServices = productServices;
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(int categoryId)
        {
            var model = new ProductListModel
            {
                   Products = _productServices.GetProductsByCategory(categoryId) // Kategorileri veritabanından getirir
            };
               return View("Default", model);
            //var model = new ProductListModel
            //{
            //  // Kategorileri veritabanından getirir
            //};
            //return View();
            /*  return View("Default", model);*/ // Default.cshtml adında bir View döndürür
        }
    }
}
