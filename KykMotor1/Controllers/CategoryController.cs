using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KykMotorApp.WebIU.Controllers
{
    [AllowAnonymous]
    public class CategoryController : Controller
    {
        ICategoryServices _categoryServices;
        IProductServices _productServices;
        IResimServices _resimServices;
        ISubbirServices _subbirServices;
        ISub2CatServices _sub2CatServices;

        public CategoryController(ICategoryServices categoryServices, IProductServices productServices, IResimServices resimServices, ISubbirServices subbirServices, ISub2CatServices sub2CatServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
            _resimServices = resimServices;
            _subbirServices = subbirServices;
            _sub2CatServices = sub2CatServices;
        }
   
        public IActionResult IndexText(int Id)
        {
            var product = _productServices.GetProductsByCategory(Id);

            return View(new ProductListModel()
            {
                Products = product,
                Categories = _categoryServices.GetAll(),
            });
        }
        public IActionResult Index(int Id)
        {
            var product = _productServices.GetProductsByCategory(Id);
           
            return View(new ProductListModel()
            {
                Products = product,
                Categories = _categoryServices.GetAll(),
              
            }) ; 
        }

     
        //O halde burada yapalım 
        public IActionResult DetayCategorySlider(int Id)
        {
            return View(new ProductListModel()
            {
                Resims = _resimServices.GetResimByCategoryId(Id),
            });
        }

        [HttpGet]
        public JsonResult AllProduct()
        {
            // Tüm ürünleri servis aracılığıyla çekiyoruz
            var products = _productServices.UrunleriGetir();

            return Json(new ProductListModel
            {
                Products = products
            });
        }
      
        public IActionResult Detay(int id)
        {
            var product = _productServices.GetProductByIdWithDetails(id);

            if (product == null)
            {
                return NotFound();
            }


            var model = new ProductDetayModel
            {
                Product = product,
                Category = product.Category,
                Resims = product.Resims,
                SubbirCategori = product.SubbirCategori,
                subIkiCategory = product.SubIkiCategory,

            };

            return View(model);
        }


        [HttpGet]
        public JsonResult CategoriBir(int categoryId)
        {
            // Seçilen kategoriye göre ürünleri ve resimlerini getiriyoruz
            var products = _productServices.GetProductsByCategory(categoryId);

            var result = products.Select(p => new {
                p.Name,
                p.Price,
                p.Id,
                category = p.Category.Name,
                Resims = p.Resims.Select(r => r.Name).ToList()
            });


            return Json(new { products = result });
        }

        [HttpGet]
        public JsonResult SubBirKategori(int subBirId)
        {
            var products = _productServices.GetProductsBySubBir(subBirId);

            var result = products.Select(p => new
            {
                p.Name,
                category = p.Category.Name,
                p.Id,
                subbircategori = p.SubbirCategori.Name,
                Resims = p.Resims.Select(r => r.Name).ToList()

            });
            return Json(new { products = result });

        }

        [HttpGet]
        public JsonResult SubIkiKategori(int subIkiId)
        {
            var products = _productServices.GetProductsBySubIki(subIkiId);

            var result = products.Select(p => new
            {
                p.Name,
                p.Id,
                subikicategori = p.SubIkiCategory.Name,
                Resims = p.Resims.Select(r => r.Name).ToList()

            });

            return Json(new { products = result });
        }

        //***********************************************************************************
        //Drop Dow Menu List Metotları start
        [HttpGet]
        public JsonResult GetSubbir(int categoryId)
        {
            var subbirCat = _subbirServices.ListByFilter(categoryId);
            return Json(new SelectList(subbirCat, "Id", "Name"));

        }

        [HttpGet]
        public JsonResult GetSub2Categories(int subbirId)
        {
            // Veriyi al
            var sub2Categories = _sub2CatServices.GetSub2FilterCategory(subbirId);

            // Verinin boş olup olmadığını kontrol et
            if (sub2Categories == null)
            {
                return Json(new List<SelectListItem>());
            }

            // `SelectListItem` kullanarak JSON formatını oluştur
            var selectListItems = sub2Categories.Select(cat => new SelectListItem
            {
                Value = cat.Id.ToString(),
                Text = cat.Name
            }).ToList();

            // JSON olarak döndür
            return Json(selectListItems);
        }

        //***********************************************************************************

    }
}
