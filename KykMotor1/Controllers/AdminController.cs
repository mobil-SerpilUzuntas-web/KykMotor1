using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace KykMotorApp.WebIU.Controllers
{
    public class AdminController : Controller
    {
        ICategoryServices _categoryServices;
        IProductServices _productServices;
        ISubbirServices _subbirServices;
        ISub2CatServices _sub2CatServices;
        IResimServices _resimServices;
        private INotificationServices _notificationServices;
        public AdminController(ICategoryServices categoryServices,IProductServices productServices, ISubbirServices subbirServices, ISub2CatServices sub2CatServices, IResimServices resimServices, INotificationServices notificationServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
            _subbirServices = subbirServices;
            _sub2CatServices = sub2CatServices;
            _resimServices = resimServices;
            _notificationServices = notificationServices;
        }

       
        public IActionResult AdminPanel()
        {
            return View();
        }

        [HttpGet]
        // GetUnreadNotificationsCount bu metot yeni bayi başvurusu yapılınca çalışıyor ve Index sayfasında ki  return Json(unreadCount);
      

        [HttpGet]
        public IActionResult ResimEkle(int Id)
        {
            var resims = _resimServices.GetResimByProductId(Id); // Product'a ait resimleri çekiyoruz
            var model = new ResimModel
            {
                ProductId = Id, // Sadece ProductId'yi kullanıyoruz
                Resims = resims.ToList()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ResimEkle(ResimModel model, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                // Dosyayı kaydetme işlemi
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // Resim kaydı veritabanına eklenir
                var resim = new Resim
                {
                    Name = fileName,
                    ProductId = model.ProductId
                };

                _resimServices.Create(resim);
            }
            return RedirectToAction("ResimEkle");
            //return RedirectToAction("ProductsEdit", new { id = model.ProductId });
        }


        //Admin Product Start
        public IActionResult Index()
        {

            return View(new ProductListModel()
            {
                Products = _productServices.GetAllProductByCategory(),


            });
        }


        //public IActionResult DeleteImages(int id)
        //{
        //    // Ardından ürünü silin
        //    var images = _resimServices.GetById(id);
        //    if (images != null)
        //    {
        //        _resimServices.Delete(images);
        //        return Json(new { success = true });
        //    }
        //    return Json(new { success = false, message = "Resim Bulunamadı" });
        //}

        //[HttpGet]
        //public IActionResult ProductsEdit(int? id) {
        //    if(id== null)
        //    {
        //        return NotFound();
        //    }

        //    // Diğer gerekli verileri al
        //    var categories = _categoryServices.GetAll();
        //    var subbirCategories = _subbirServices.GetAll();
        //    var subIkiCategories = _sub2CatServices.GetAll();

        //    var entity = _productServices.GetById((int)id);
        //    if(entity == null)
        //    { 
        //        return NotFound();
        //    }
        //    var model = new ProductModel()
        //    {
        //        Id= entity.Id,
        //        Name=entity.Name,
        //        Price=entity.Price,
        //        KoliAdet=entity.KoliAdet,
        //        Description=entity.Description,
        //        Categories = categories.ToList(),
        //        SubBirCategories = subbirCategories.ToList(),
        //        SubIkiCategories = subIkiCategories.ToList(),
        //        CategoryId =entity.CategoryId,
        //        SubBirId=entity.SubBirId,
        //        SubIkiId=entity.SubIkiId,
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult ProductsEdit(ProductModel model)
        //{
        //    var entity = _productServices.GetById(model.Id);
        //    if(model == null)
        //    {
        //        return NotFound();
        //    }

        //    entity.Name = model.Name;
        //    entity.Price = model.Price;
        //    entity.KoliAdet = model.KoliAdet;
        //    entity.Description = model.Description;
        //    entity.CategoryId = model.CategoryId;
        //    entity.SubBirId = model.SubBirId;
        //    entity.SubIkiId = model.SubIkiId;

        //    _productServices.Update(entity);


        //    return RedirectToAction("Index");

        //}

        //[HttpGet]
        //public IActionResult CreateProduct()
        //{
        //    var model = new ProductModel
        //    {
        //        Categories = _categoryServices.GetAll(),
        //        SubBirCategories = _subbirServices.GetAll(),
        //        SubIkiCategories = _sub2CatServices.GetAll()
        //    };
        //    return View(model);
        //}

        //[HttpPost]
        //public IActionResult CreateProduct(ProductModel model)
        //{

        //    var entity = new Product
        //    {
        //        Name = model.Name,
        //        Price = model.Price,
        //        Description = model.Description,
        //        KoliAdet = model.KoliAdet,
        //        CategoryId = model.CategoryId,
        //        SubBirId = model.SubBirId,
        //        SubIkiId = model.SubIkiId,
        //    };

        //    _productServices.Create(entity);
        //    return RedirectToAction("Index");
        //}

        ////Resimler tablosundaki Productlara Ayit Resimleri siliyoruz sonrada Productları siliyoruz
        //[HttpPost]
        //public IActionResult DeleteProduct(int id)
        //{
        //    // Önce ilişkili resimleri silin
        //    var resims = _resimServices.GetResimByProductId(id);
        //    if (resims != null)
        //    {
        //        foreach (var resim in resims)
        //        {
        //            _resimServices.Delete(resim);
        //        }
        //    }

        //    // Ardından ürünü silin
        //    var product = _productServices.GetById(id);
        //    if (product != null)
        //    {
        //        _productServices.Delete(product);
        //        return Json(new { success = true });
        //    }
        //    return Json(new { success = false, message = "Ürün bulunamadı." });
        //}

        ////Admin Product End

        ////Admin Category Start





    }
}

//[HttpGet]
//public IActionResult ResimEkle(int id)
//{

//    return View(new ProductListModel()
//    {

//        Resims = _resimServices.GetResimByProductId(id)
//    });

//}


//[HttpPost]
//public IActionResult ResimEkle(int id, IFormFile file)
//{
//    if (file != null && file.Length > 0)
//    {
//        // Dosya işleme işlemleri (örneğin, sunucuya kaydetme)
//        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/kykİmg", file.FileName);

//        using (var stream = new FileStream(filePath, FileMode.Create))
//        {
//            file.CopyTo(stream);
//        }

//        // Resmi veritabanına ekleme işlemi
//        _resimServices.Create(new Resim
//        {
//            ProductId = id,
//            Name = file.FileName // Dosya adını veya tam yolu veritabanına kaydedebilirsin
//        });
//    }

//    // Ürün resimleri tekrar yüklenecek
//    return View(new ProductListModel
//    {
//        Resims = _resimServices.GetResimByProductId(id)
//    });
//}
