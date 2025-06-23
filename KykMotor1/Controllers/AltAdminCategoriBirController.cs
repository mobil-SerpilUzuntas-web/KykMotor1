
using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.Controllers
{
    public class AltAdminCategoriBirController : Controller
    {
        ICategoryServices _categoryServices;
        IProductServices _productServices;
        ISubbirServices _subbirServices;
        ISub2CatServices _sub2CatServices;
        IResimServices _resimServices;
        public AltAdminCategoriBirController(ICategoryServices categoryServices, IProductServices productServices, ISubbirServices subbirServices, ISub2CatServices sub2CatServices, IResimServices resimServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
            _subbirServices = subbirServices;
            _sub2CatServices = sub2CatServices;
            _resimServices = resimServices;
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var product = _productServices.GetProductsBySubBir(Id);
               if(product != null)
            {
                foreach (var pro in product)
                {
                    var resim = _resimServices.GetResimByProductId(pro.Id);

                    foreach (var img in resim)
                    {
                        _resimServices.DeleteId(img.Id);
                    }

                    _productServices.DeleteId(pro.Id);
                }
            }

            var subIkiCat = _sub2CatServices.GetAllBySubBirId(Id);
            if (subIkiCat != null)
            {
                // 2. Her bir SubBirCategory için ilgili SubIkiCategory kayıtlarını sil
                foreach (var subİkiCategory in subIkiCat)
                {
                   _sub2CatServices.DeleteId(subİkiCategory.Id);
               
                }

            }

            var subbirCat = _subbirServices.GetById(Id);
            if (subbirCat != null)
            {
                _subbirServices.Delete(subbirCat);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Alt Bir Kategori Bulunamadı" });
           
           }
        public IActionResult Index()
        {
            return View(new CategoryListModel()
            {
                SubbirCategoris = _subbirServices.tumAltBirListe()
                //SubbirCategoris = _subbirServices.GetAll()
            }) ;
               
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new BirAltCategoryModel()
            {
                Categories = _categoryServices.GetAll(),
            };
           
            return View(model);
        }


        [HttpPost]
        public IActionResult Create(BirAltCategoryModel model)
        {

            var entity = new SubbirCategori
            {
                Name = model.Name,
                CategoryId = model.CategoryId,
          
            };

            _subbirServices.Create(entity);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            // Diğer gerekli verileri al
            var categories = _categoryServices.GetAll();
            var entity = _subbirServices.GetById((int)id);
            if(entity == null)
            { 
                return NotFound();
            }
            var model = new BirAltCategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Categories = categories.ToList(),
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(BirAltCategoryModel model)
        {
            var entity = _subbirServices.GetById(model.Id);
            if (model == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.CategoryId = model.CategoryId;
            _subbirServices.Update(entity);

            return RedirectToAction("Index");

        }
    }
}

