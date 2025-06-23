using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.Controllers
{
    public class AdminProductController : Controller
    {
        ICategoryServices _categoryServices;
        IProductServices _productServices;
        ISubbirServices _subbirServices;
        ISub2CatServices _sub2CatServices;
        IResimServices _resimServices;
        public AdminProductController(ICategoryServices categoryServices, IProductServices productServices, ISubbirServices subbirServices, ISub2CatServices sub2CatServices, IResimServices resimServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
            _subbirServices = subbirServices;
            _sub2CatServices = sub2CatServices;
            _resimServices = resimServices;
        }
        //Admin Product Start
        public IActionResult Index()
        {

            return View(new ProductListModel()
            {
                Products = _productServices.GetAllProductByCategory(),


            });
        }


        //Ürün Resmini Silme Metodu
        [HttpPost]
        public IActionResult DeleteImages(int id)
        {
            var images = _resimServices.GetById(id);
            if (images != null)
            {
                _resimServices.DeleteId(images.Id);
                return Json(new { success = true });

            }
            return Json(new { success = false, message = "Resim Bulunamadı" });
            // Ardından ürünü silin
            //var images = _resimServices.GetById(id);
            //if (images != null)
            //{
            //    _resimServices.Delete(images.Id);
            //    //_resimServices.DeleteId(images.Id);
            //    //return Json(new { success = true });
            //}
            //
        }
        [HttpGet]
        public IActionResult ResimEkle(int Id)
        {
            var resims = _resimServices.GetResimByProductId(Id); 
            // Product'a ait resimleri çekiyoruz
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


        [HttpGet]
        public IActionResult ProductsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Diğer gerekli verileri al
            var categories = _categoryServices.GetAll();
            var subbirCategories = _subbirServices.GetAll();
            var subIkiCategories = _sub2CatServices.GetAll();

            var entity = _productServices.GetById((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new ProductModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                KoliAdet = entity.KoliAdet,
                Description = entity.Description,
                Categories = categories.ToList(),
                SubBirCategories = subbirCategories.ToList(),
                SubIkiCategories = subIkiCategories.ToList(),
                CategoryId = entity.CategoryId,
                SubBirId = entity.SubBirId,
                SubIkiId = entity.SubIkiId,
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ProductsEdit(ProductModel model)
        {
            var entity = _productServices.GetById(model.Id);
            if (model == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.KoliAdet = model.KoliAdet;
            entity.Description = model.Description;
            entity.CategoryId = model.CategoryId;
            entity.SubBirId = model.SubBirId;
            entity.SubIkiId = model.SubIkiId;

            _productServices.Update(entity);


            return RedirectToAction("Index");

        }

        //Resimler tablosundaki Productlara Ayit Resimleri siliyoruz sonrada Productları siliyoruz
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

        //Admin Product End
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            // Önce ilişkili resimleri silin
            var resims = _resimServices.GetResimByProductId(id);
            if (resims != null)
            {
                foreach (var resim in resims)
                {
                    _resimServices.DeleteId(resim.Id);
                }
            }

            // Ardından ürünü silin
            var product = _productServices.GetById(id);
            if (product != null)
            {
                _productServices.DeleteId(product.Id);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Ürün bulunamadı." });
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var model = new ProductModel
            {
                Categories = _categoryServices.GetAll(),
                SubBirCategories = _subbirServices.GetAll(),
                SubIkiCategories = _sub2CatServices.GetAll()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateProduct(ProductModel model)
        {

            var entity = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                KoliAdet = model.KoliAdet,
                CategoryId = model.CategoryId,
                SubBirId = model.SubBirId,
                SubIkiId = model.SubIkiId,           
            };

            _productServices.Create(entity);
            return RedirectToAction("Index");
        }
    }
}

