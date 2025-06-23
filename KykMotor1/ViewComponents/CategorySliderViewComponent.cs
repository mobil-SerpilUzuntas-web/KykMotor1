using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KykMotorApp.WebIU.ViewComponents
{
    public class CategorySliderViewComponent:ViewComponent
    {
        private readonly IResimServices _resimServices;

        public CategorySliderViewComponent(IResimServices resimServices)
        {
            _resimServices = resimServices;
        }
        public IViewComponentResult Invoke(int? Id)
        {
            if (Id == null)
            {
                return View(new ProductListModel()
                {
                    Resims = new List<Resim>() // Eğer Id null ise boş liste döndür
                });
            }

            // Güncellenmiş metodu çağır
            var resimler = _resimServices.GetResimBySubikiCategoryId(Id.Value);

            return View(new ProductListModel()
            {
                Resims = resimler
            });
        }
        //public IViewComponentResult Invoke(int subIkiCategoryId)
        //{
        //    var productImages = _context.Products
        //        .Where(p => p.SubİkiCategoryId == subIkiCategoryId)
        //        .Select(p => new
        //        {
        //            ProductId = p.Id,
        //            ProductName = p.Name,
        //            FirstImage = _context.Resims
        //                .Where(r => r.ProductId == p.Id)
        //                .OrderBy(r => r.Id)
        //                .Select(r => r.Name)
        //                .FirstOrDefault()
        //        })
        //        .ToList();

        //    return View(productImages);
        //}
        //public IViewComponentResult Invoke(int? Id)
        //{
        //    if (Id == null)
        //    {
        //        return View(new ProductListModel()
        //        {
        //            Resims = new List<Resim>() // Eğer Id null ise boş liste döndür
        //        });
        //    }

        //    return View(new ProductListModel()
        //    {
        //        Resims = _resimServices.GetResimBySubikiCategoryId(Id.Value),
        //    });


        //}
    }
}
