using KykMotor1.Models;
using KykMotorApp.WebIU.Areas.Admin.Models;
using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace KykMotor1.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        ICategoryServices _categoryServices;
        IProductServices _productServices;
        IResimServices _resimServices;

        public HomeController(ICategoryServices categoryServices, IProductServices productServices, IResimServices resimServices)
        {
            _productServices= productServices;
            _categoryServices = categoryServices;
            _resimServices= resimServices;
        }

        public ActionResult sliders()
        {


            return View();
        }
        public ActionResult Index()
        {
            var model = new ProductListModel
            {
                Categories = _categoryServices.GetAll() // Veritabanından kategorileri getiren bir servis
            };

            return View(model);
        }

        public ActionResult TextDetay(int Id)
        {
            return View();
        }
        //public class ProductDetayViewModel
        //{
        //    public string ProductName { get; set; }
        //    public decimal Price { get; set; }
        //    public string Description { get; set; }
        //    public int KoliAdet { get; set; }
        //    public List<Resim> Resim { get; set; }
        //}
        public IActionResult urunDetay(int Id)
        {
            if (Id == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var resimler = _resimServices.GetResimByProductId(Id);
            var product = _productServices.GetById(Id);


            if (product == null)
            {
                return NotFound("Resimmm Kullanıcı bulunamadı.");
            }

            // View modelini dolduruyoruz


            var productDetayModel = new ProductDetayViewModel
            {
                Id = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Description = product.Description,
                KoliAdet = product.KoliAdet,
                Resims = resimler,

            };

            return View(productDetayModel);
        }

        public IActionResult detayText(int Id)
        {
            if (Id == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var resimler = _resimServices.GetResimByProductId(Id);
            var product = _productServices.GetById(Id);


            if (product == null)
            {
                return NotFound("Resimmm Kullanıcı bulunamadı.");
            }

            // View modelini dolduruyoruz


            var productDetayModel = new ProductDetayViewModel
            {
                Id = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Description = product.Description,
                KoliAdet = product.KoliAdet,
                Resims = resimler,

            };

            return View(productDetayModel);
        }

        public IActionResult Detay(int Id)
        {
            if (Id == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var resimler = _resimServices.GetResimByProductId(Id);
            var product = _productServices.GetById(Id);
         

            if (product == null)
            {
                return NotFound("Resimmm Kullanıcı bulunamadı.");
            }

            // View modelini dolduruyoruz


            var productDetayModel = new ProductDetayViewModel
            {
                Id = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Description = product.Description,
                KoliAdet = product.KoliAdet,
                Resims = resimler,

            };

            return View(productDetayModel);
        }


    }
}

