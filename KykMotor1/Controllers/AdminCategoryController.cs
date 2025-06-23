using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KykMotorApp.WebIU.Controllers
{
    public class AdminCategoryController : Controller
    {
        private readonly ICategoryServices _categoryService;
        private readonly IProductServices _productService;
        private readonly ISubbirServices _subbirServices;
        private readonly ISub2CatServices _sub2CatServices;
        private readonly IResimServices _resimServices;

        public AdminCategoryController(ICategoryServices categoryService, IProductServices productServices, ISub2CatServices sub2CatServices, ISubbirServices subbirServices, IResimServices resimServices)
        {
            _categoryService = categoryService;
            _productService = productServices;
            _subbirServices = subbirServices;
            _sub2CatServices = sub2CatServices;

           _resimServices = resimServices;
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {

            //1. Resimler ile product ilişkili tablolar çekilip önce resimler sonra productlar siliniyor
            var product = _productService.GetCategoryByProduct(Id); 
            if(product != null)
            {
               foreach (var pro in product)
            {
                var resim = _resimServices.GetResimByProductId(pro.Id);

                foreach (var img in resim)
                {
                    _resimServices.DeleteId(img.Id);
                }

                _productService.DeleteId(pro.Id);
              }
            }
       

            // 1. SubBirCategory ile category ilişkili olan kayıtları bul
            var subBirCategories = _subbirServices.GetSubbirCategoriesByCategoryId(Id);
          if(subBirCategories != null)
            {
            // 2. Her bir SubBirCategory için ilgili SubIkiCategory kayıtlarını sil
            foreach (var subBirCategory in subBirCategories)
            {
                var subIkiCategories = _sub2CatServices.GetAllBySubBirId(subBirCategory.Id);

                // SubIkiCategory kayıtlarını sil
                foreach (var subIkiCategory in subIkiCategories)
                {

                    _sub2CatServices.DeleteId(subIkiCategory.Id);
                }

                // 3. SubBirCategory kaydını sil
                _subbirServices.DeleteId(subBirCategory.Id);
            }
          
            }
         
            // 5. Son olarak Category kaydını sil
            // Ardından ürünü silin
            var category = _categoryService.GetById(Id);
            if (category != null)
            {
                
                _categoryService.CetegoryDelete(category.Id);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Kategori Bulunamadı" });
            //_categoryServices.Delete(categoryId);

        }

        public IActionResult Index()
        {
            return View(new CategoryListModel()
            {
                Categories = _categoryService.tumKategorileriGetir()
                //Categories = _categoryService.GetAll()
            }) ; 
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var entity=_categoryService.GetById((int)id);
            if(entity == null)
            {
                return NotFound();
            }
            var model = new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
            };
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Edit(CategoryModel model)
            {
            var entity = _categoryService.GetById(model.Id);

            if (model == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            _categoryService.Update(entity);
            return RedirectToAction("Index");


        }
        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel entity)
        {
            if (entity!=null)
            {
                // CategoryModel'den Category nesnesi oluşturuyoruz
                var category = new Category
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    // Diğer alanlar için de gerekirse doldurma işlemi yapılabilir
                };

                _categoryService.Create(category); // Burada category ekleniyor.
                return RedirectToAction("Index"); // Ekledikten sonra Index sayfasına yönlendiriyor.
            }
             return View(entity);
            //if (ModelState.IsValid)
            //{
            //    _categoryService.Create(entity); // Burada category ekleniyor.
            //    return RedirectToAction("CategoryIndex"); // Ekledikten sonra Index sayfasına yönlendiriyor.
            //}
            //return View(entity);
        }
    }
}
