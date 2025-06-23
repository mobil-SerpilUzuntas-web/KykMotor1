using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KykMotorApp.WebIU.Controllers
{
    public class AltAdminCategoryIkiController : Controller
    {
        ICategoryServices _categoryServices;
        IProductServices _productServices;
        ISubbirServices _subbirServices;
        ISub2CatServices _sub2CatServices;
        IResimServices _resimServices;
        public AltAdminCategoryIkiController(ICategoryServices categoryServices, IProductServices productServices, ISubbirServices subbirServices, ISub2CatServices sub2CatServices,IResimServices resimServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
            _subbirServices = subbirServices;
            _sub2CatServices = sub2CatServices;
            _resimServices = resimServices;
        }

        public IActionResult Delete(int Id)
        {
           
            var product = _productServices.GetSubCategoryByProduct(Id);
            if (product != null)
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

            if(subIkiCat != null)
            {
                _sub2CatServices.DeleteId(Id);
                return Json(new { success = true });
            }
            return Json(new { success = false, message = "Alt İki Kategori Bulunamadı" });
        }

        


        public IActionResult Index()
        {
            return View(new CategoryListModel()
            {
                SubIkiCategories = _sub2CatServices.tumAltIkiListe()
            });
        }

        [HttpGet]
        public IActionResult Create() 
        {

            var model = new BirAltCategoryModel()
            {
                SubbirCategori = _subbirServices.GetAll()
            };

            return View(model);

        }


        [HttpPost]
        public IActionResult Create(BirAltCategoryModel model)
        {

            var entity = new SubIkiCategory
            {
                Id = model.Id,
                Name = model.Name,
                SubBirId = model.SubBirId,

            };

            _sub2CatServices.Create(entity);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Diğer gerekli verileri al
            var subbircate = _subbirServices.GetAll();
            var entity = _sub2CatServices.GetById((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new BirAltCategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                SubbirCategori = subbircate.ToList(),
            };
            return View(model);
        }


        [HttpPost]
        public IActionResult Edit(BirAltCategoryModel model)
        {
            var entity = _sub2CatServices.GetById(model.Id);
            if (model == null)
            {
                return NotFound();
            }
            entity.Name = model.Name;
            entity.SubBirId = model.SubBirId;
            _sub2CatServices.Update(entity);

            return RedirectToAction("Index");

        }

    }
 }


