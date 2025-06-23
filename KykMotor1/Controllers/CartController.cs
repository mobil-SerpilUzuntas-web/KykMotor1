using IyzipayCore;
using IyzipayCore.Request;
using IyzipayCore.Model;
using KykMotorApp.WebIU.Models;
using KykShopApp.Business.Abstract;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using System.Runtime.CompilerServices;



namespace KykMotorApp.WebIU.Controllers
{
    [Authorize]
    //Bu Sayfa aslında Sepet sayfası
    public class CartController : Controller
    {
        private ICartServices _cartServices;
        private UserManager<ApplicationUser> _userManager;
        private IResimServices _resimServices;
        private IOrderServices _orderServices;
        private IBuyerServices _buyerServices;
        private IShippingServices _shippingServices;
        private IBillingServices _billingServices;



        public CartController(ICartServices cartServices, UserManager<ApplicationUser> userManager, IResimServices resimServices, IOrderServices orderServices, IBuyerServices buyerServices, IShippingServices shippingServices, IBillingServices billingServices)
        {
            _resimServices = resimServices;
            _userManager = userManager;
            _cartServices = cartServices;
            _orderServices = orderServices;
            _buyerServices = buyerServices;
            _shippingServices = shippingServices;
            _billingServices = billingServices;

        }
        [HttpPost]
        public IActionResult BuyerAndAdresCreate(BuyerModel model)
        {
            var userId = _userManager.GetUserId(HttpContext.User); // Kullanıcı ID'sini al

            // Buyer'ı al veya oluştur
            var existingBuyer = _buyerServices.GetOrCreateBuyerByUserId(userId);
            

            if (existingBuyer == null)
            {
                // Yeni Buyer'ı oluştur
                var buyer = new Buyerr
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    IdentityNumber = model.IdentityNumber,
                    GsmNumber = model.GsmNumber,
                    Email= model.Email,
                    ZipCode= model.ZipCode,
                    LastLoginDate = DateTime.Now,
                    RegistrationDate = DateTime.Now,
                    City = model.City,
                    Country = model.Country,
                    RegistrationAddress = model.RegistrationAddress,
                    UserId = userId
                };

                // Yeni Buyer'ı kaydet
                _buyerServices.AddBuyer(buyer);
            }
            var buyerId = _buyerServices.getBuyerId(userId);
            var adres = new ShippingAddres
            {
                ContactName = model.Name,
                City = model.City,
                Country = model.Country,
                Description = model.RegistrationAddress,
                ZipCode = model.ZipCode,
                UserId = userId,
                BuyerId = buyerId,

            };
             _shippingServices.Create(adres);
            return RedirectToAction("Index", "Home");
        }
        //[HttpPost]
        //public IActionResult BuyerAndAdresCreate(BuyerModel model)
        //{
        //    var userId = _userManager.GetUserId(HttpContext.User);

        //    // Önce mevcut Buyer var mı kontrol et
        //    var existingBuyer = _buyerServices.GetBuyerByUserId(userId);

        //    if (existingBuyer == null) // Eğer userId ile kayıtlı bir Buyer yoksa yeni oluştur
        //    {
        //        var buyer = new Buyerr
        //        {
        //            Name = model.Name,
        //            Surname = model.Surname,
        //            IdentityNumber = model.IdentityNumber,
        //            GsmNumber = model.GsmNumber,
        //            LastLoginDate = DateTime.Now,
        //            RegistrationDate = DateTime.Now,
        //            City = model.City,
        //            Country = model.Country,
        //            RegistrationAddress = model.RegistrationAddress,
        //            UserId = userId
        //        };

        //        _buyerServices.AddBuyerCreate(buyer, userId);
        //          existingBuyer = buyer; // Yeni oluşturulan buyer'ı referans al
        //    }

        //    // Adres ekleme işlemi
        //    var adres = new ShippingAddres
        //    {
        //        ContactName = model.Name,
        //        City = model.City,
        //        Country = model.Country,
        //        ZipCode = model.ZipCode,
        //        Description = model.RegistrationAddress,
        //        UserId = userId,
        //        BuyerId = existingBuyer.Id // Buyer Id'yi ekliyoruz
        //    };

        //    _shippingServices.AddShippingCreate(adres, userId);

        //    return RedirectToAction("Index", "Home");
        //}
        //    [HttpPost]
        //    public IActionResult BuyerAndAdresCreate(BuyerModel model)
        //    {
        //        var userId = _userManager.GetUserId(HttpContext.User);

        //        var buyer = new Buyerr
        //        {
        //            Name = model.Name,
        //            Surname = model.Surname,
        //            IdentityNumber = model.IdentityNumber,
        //            GsmNumber = model.GsmNumber,
        //            LastLoginDate = DateTime.Now.ToString(),
        //            RegistrationDate = DateTime.Now.ToString(),
        //            City = model.City,
        //            Country = model.Country,
        //            RegistrationAddress = model.RegistrationAddress,
        //            UserId = userId,

        //            _buyerServices.AddBuyer(buyer, userId)
        //        }
        //        var adres = new ShippingAddres
        //        {
        //            ContactName = model.Name,
        //            City = model.City,
        //            Country = model.Country,
        //            ZipCode = model.ZipCode,
        //            Description = model.RegistrationAddress,
        //            UserId = userId,
        //            _shippingServices.Addshipping(adres, userId);
        //    }
        //    return RedirectToAction("Home","Index");
        //}



        //Burada aslında sepet Index sayfası
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            //Bukısmı async yapılacak sorarsın
            var cart = await _cartServices.GetCartByUserAsync(userId);
            var shippingAdress = await _shippingServices.GetAllShipping(userId);
            var blingAdress = await _billingServices.GetAllBilling(userId);

           
            return View(new CartModel()
            {
                CartId = cart.Id,
                
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    Quantity = i.Quantity,
                    Resims = i.Product.Resims,

                }).ToList()
            });
        }
       
  

        //aşşagıda ki var cart ile sayfadaki yann taraftaki Sipariş Özeti kısmında ki kısım için 
        //var cart = await _cartServices.GetCartByUserAsync(userId);
        public async Task<IActionResult> Odeme()
        {
            var userId = _userManager.GetUserId(User);
            var cart = await _cartServices.GetCartByUserAsync(userId);
            var orderModel = new OrderModel();

            return View(orderModel);
        }
        public async Task<IActionResult> Teslimat()
        {

            var userId = _userManager.GetUserId(User);
            var cart = await _cartServices.GetCartByUserAsync(userId);
            var buyer = await _buyerServices.GetBuyer(userId);
            var sAdress = await _shippingServices.GetAllShipping(userId);
            var bAdres = await _billingServices.GetAllBilling(userId);
            var orderModel = new OrderModel();
            orderModel.CartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    Quantity = i.Quantity,
                    Resims = i.Product.Resims,



                }).ToList()
            };
            var buyers = await _buyerServices.GetAllBuyer(userId);
            return View(new OrderModel()
            {
                BuyerModel = buyers.Select(b => new BuyerModel
                {
                    BuyerId= b.Id,
                    Name= b.Name,
                    Surname= b.Surname,
                    Email= b.Email,
                    GsmNumber= b.GsmNumber,
                    //IdentityNumber= b.IdentityNumber,
                    RegistrationAddress= b.RegistrationAddress, 
                    LastLoginDate= b.LastLoginDate,
                    City= b.City,
                    Country= b.Country,
                    ZipCode= b.ZipCode,
                    UserId= userId,

                }).ToList()
            }) ;
               //ShippingModel = await _shippingServices.GetlShippingAll(userId),
               // BillingModel = await _billingServices.GetBillingAll(userId),
          

           
            //Bukısmı async yapılacak sorarsın
          

           
           
            //orderModel.BuyerModel = new BuyerModel()
            //{
            //    BuyerId = buyer.Id,
            //    Name = buyer.Name,
            //    Surname = buyer.Surname,
            //    Email = buyer.Email,
            //    GsmNumber = buyer.GsmNumber,
            //    IdentityNumber = buyer.IdentityNumber,
            //    RegistrationAddress = buyer.RegistrationAddress,
            //    LastLoginDate = buyer.LastLoginDate,
            //    City = buyer.City,
            //    Country = buyer.Country,
            //    ZipCode = buyer.ZipCode,
            //    UserId = userId,

            //};
            ////Açılış Sayfasında Kayıtlı Alıcı Adres Gösretilecek
            //orderModel.ShippingModel = new ShippingModel()
            //{
            //    BuyerId = sAdress.BuyerId,
            //    Id = sAdress.Id,
            //    ContactName = sAdress.ContactName,
            //    Description = sAdress.Description,
            //    City = sAdress.City,
            //    Country = sAdress.Country,
            //    ZipCode = sAdress.ZipCode,
            //    UserId = userId,
            //};
            ////Açılış Sayfasında Kayıtlı Fatura Adresi Gösterilecek
            //orderModel.BillingModel = new BillingModel()
            //{
            //    BuyerId = bAdres.BuyerId,
            //    BillingId = bAdres.Id,
            //    ContactName = bAdres.ContactName,
            //    Description = bAdres.Description,
            //    City = bAdres.City,
            //    Country = bAdres.Country,
            //    ZipCode = bAdres.ZipCode,
            //    UserId = userId,
            //};

          

         
        }
        [HttpPost]
        public async Task<IActionResult> Teslimat(OrderModel model)
        {

            if(model != null)
            {
                
                var userId = _userManager.GetUserId(User);

                //SaveBuyer(model, userId);

                var buyer = await _buyerServices.GetBuyer(userId);
                var buyerEklenenSonKayıt = await _buyerServices.GetBuyerSonKayit(userId);
                //SaveShippingAddres(model, userId, buyerEklenenSonKayıt.Id);
                //SaveBillingAddres(model, userId, buyerEklenenSonKayıt.Id);
            }
            
          
            return RedirectToAction("Teslimat", "Cart");
           
        }
        [HttpGet]
        public async Task<IActionResult> UpdateTeslimat(int? BuyerId)
        {
            if (BuyerId == null)
            {
                return NotFound();
            }

            //var adresUpdate = _buyerServices.EditBuyer(BuyerId);
            return View();
               
        }
        

        [HttpPost]
        public async Task<IActionResult> UpdateTeslimat(OrderModel model,int BuyerId)
        {

            return RedirectToAction("Teslimat", "Cart");
        }
        //private void SaveBuyer(OrderModel model, string userId)
        //{

        //    var buyer = new Buyerr();
        //    buyer.Id=buyer.Id;
        //    buyer.Name = model.BuyerModel.Name;
        //    buyer.Surname = model.BuyerModel.Surname;
        //    buyer.City = model.BuyerModel.City;
        //    buyer.Country= model.BuyerModel.Country;
        //    buyer.Email = model.BuyerModel.Email;
        //    buyer.GsmNumber = model.BuyerModel.GsmNumber;
        //    buyer.RegistrationAddress = model.BuyerModel.RegistrationAddress;
        //    //TcKimlik no=IdentityNumber
        //    buyer.IdentityNumber = model.BuyerModel.IdentityNumber;
        //    buyer.RegistrationDate = model.BuyerModel.RegistrationDate;
        //    buyer.LastLoginDate = model.BuyerModel.LastLoginDate;
        //    buyer.ZipCode = model.BuyerModel.ZipCode;
        //    buyer.UserId = userId;
        //    _buyerServices.Create(buyer);

        //}
        //private void SaveShippingAddres(OrderModel model, string userId, int buyerId)
        //{
        //    var shipping = new ShippingAddres();
        //    shipping.ContactName = model.ShippingModel.ContactName;
        //    shipping.Description = model.ShippingModel.Description;
        //    shipping.ZipCode = model.ShippingModel.ZipCode;
        //    shipping.City = model.ShippingModel.City;
        //    shipping.Country = model.ShippingModel.Country;
        //    shipping.UserId = userId;
        //    shipping.BuyerId = buyerId;
        //    _shippingServices.Create(shipping);
        //}

        //private void SaveBillingAddres(OrderModel model, string userId, int buyerId)
        //{
        //    var billing = new BillingAddres();
        //    billing.ContactName = model.BillingModel.ContactName;
        //    billing.Description = model.BillingModel.Description;
        //    billing.ZipCode = model.BillingModel.ZipCode;
        //    billing.City = model.BillingModel.City;
        //    billing.Country = model.BillingModel.Country;
        //    billing.BuyerId = buyerId;
        //    billing.UserId = userId;

        //    _billingServices.Create(billing);
        //}

        [HttpPost]
        //Burada aslında sepet Post metodu Sepete gönderilen veriler eklenecek sayfası
        public async Task<IActionResult> AddToCartAsync(int productId,int quantity)
        {
           
            var userId = _userManager.GetUserId(User);

            await _cartServices.AddToCartAsync(userId,productId,quantity);
            return RedirectToAction("Index");   
        }

        [HttpPost]
        public async Task<IActionResult> FromDeleteCart(int cartItemId)
        {
            var userId = _userManager.GetUserId(User);
            await _cartServices.DeleteFromCart(userId, cartItemId);
            return RedirectToAction("Index");  
                
        }
        //Açılacak
        //public async Task<IActionResult> checkout()
        //{

        //    var userId =  _userManager.GetUserId(User);
        //    //Bukısmı async yapılacak sorarsın
        //    var cart = await _cartServices.GetCartByUserAsync(userId);
        //    var orderModel = new OrderModel();
          
        //    orderModel.CartModel = new CartModel()
        //    {
        //        CartId = cart.Id,
        //        CartItems = cart.CartItems.Select(i => new CartItemModel()
        //        {
        //            CartItemId = i.Id,
        //            ProductId = i.ProductId,
        //            ProductName = i.Product.Name,
        //            Price = (decimal)i.Product.Price,
        //            Quantity = i.Quantity,
        //            Resims = i.Product.Resims,
                    
 

        //        }).ToList()
        //    };

        //    return View(orderModel);
        //}

        //Açılacak

        //[HttpPost]
        //public async Task<IActionResult> checkout(OrderModel model)
        //{
        //    //if (ModelState.IsValid == true)
        //    //{
        //        var userId = _userManager.GetUserId(User);
        //        //Bukısmı async yapılacak sorarsın 
        //        var cart = await _cartServices.GetCartByUserAsync(userId);
           
        //    var buyer = await _buyerServices.GetBuyer(userId);
        //    //NOT YAPILACAKLAR  var shipping, var billing SERVİCES KATMANLARINDA GetShipping ve GetBilling metotlarını oluşturacak 
        //    var shipping = await _shippingServices.GetAllShipping(userId);
        //    var billing = await _billingServices.GetAllBilling(userId);
        //    //model.CartModel = yukardaki checkout metodunun içinde ki OrderModel model içinden geliyor
        //    //new CartModel ise CartModel ordan geliyor
        //    model.BuyerModel = new BuyerModel()
        //    {
        //        BuyerId = buyer.Id,
        //        UserId = userId,
        //        Name = buyer.Name,
        //        Surname = buyer.Surname,
        //        City = buyer.City,
        //        Country = buyer.Country,
        //        Email = buyer.Email,
        //        GsmNumber = buyer.GsmNumber,
        //        ZipCode = buyer.ZipCode,
        //        RegistrationAddress = buyer.RegistrationAddress,
        //        IdentityNumber = buyer.IdentityNumber,
        //        RegistrationDate = buyer.RegistrationDate,


        //    };
        //    model.BillingModel = new BillingModel()
        //    {
        //        BillingId = billing.Id,
        //        BuyerId = billing.BuyerId
              

        //    };
                 
        //        model.CartModel = new CartModel()
        //        {
        //            CartId = cart.Id,
        //            CartItems =cart.CartItems.Select(i=>new CartItemModel()
        //            {
        //                CartItemId = i.Id,
        //                ProductId = i.ProductId,
        //                ProductName = i.Product.Name,
        //                Price = (decimal)i.Product.Price,
        //                Quantity = i.Quantity,
        //                Resims = i.Product.Resims,

        //            }).ToList()

        //        };
             
        //        var payment = await PaymentProcess(model);
        //        if (payment.Status == "success")
        //        {
                 
        //        //Bura Açılacak
        //        SaveOrder(model, payment, userId);
        //        //ClearCart(cart.Id);
        //        return View("Success");
        //        }
        //    //}
        //    return View(model);
        //    //ödeme işlemi
        //    //ödeme işlemi başarılıysa yani payment.Create metodundan gelen deger success ise o zaman gelen degerleri Order Tablosuna kayıt yapmak lazım yani şipariş kaydı oluşturmak ş
           
        //}

        //Açılacak

        private void SaveOrder(OrderModel model, Payment payment, string userId)
        {
            var order = new Order();
            order.OrderNumber = new Random().Next(111111, 999999).ToString();
            order.OrderState = EnumOrderState.Completed;
            if (order.PaymetType == EnumPaymentType.CreditCart)
            {
                order.PaymetType = EnumPaymentType.CreditCart;
            }
            else
            {
                order.PaymetType = EnumPaymentType.Eft;
            }
            order.PaymentId = payment.PaymentId;
            order.ConversationId = payment.ConversationId;
            order.OrderDate = new DateTime();
            order.BuyerId = model.buyerId;
            order.BillingAddresId = 1;
            order.ShippingAddresId = 1;
            order.OrderState = EnumOrderState.Completed;
            order.OrderNote = model.OrderNote;
            foreach (var item in model.CartModel.CartItems)
            {
                var orderItem = new OrderItem()
                { 
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId,
                };
                order.OrderItems.Add(orderItem);
            }

            _orderServices.Create(order);
        }
        private void ClearCart(int Id)
        {
            _cartServices.CartItemsDelete(Id);

        }
        //Açılacak
   //     private async Task<Payment> PaymentProcess(OrderModel model)
   //     {
   //         Options options = new Options();
   //         options.ApiKey = "sandbox-2nmc9mK4APpo2yDoAxziL6kpDVb5j5yH";
   //         options.SecretKey = "sandbox-mlQtBDC1KwkB4YR0GxYOwUOPJToi3SC2";
   //         options.BaseUrl = "https://sandbox-api.iyzipay.com";

   //         CreatePaymentRequest request = new CreatePaymentRequest();
   //         request.Locale = Locale.TR.ToString();
   //         request.ConversationId = Guid.NewGuid().ToString();
   //         request.Price = model.CartModel.TotalNormal().ToString().Split(",")[0];
   //         request.PaidPrice = model.CartModel.TotalKdv().ToString().Split(",")[0];
   //         request.Currency = Currency.TRY.ToString();
   //         request.Installment = 1;
   //         request.BasketId = model.CartModel.CartId.ToString();
   //         request.PaymentChannel = PaymentChannel.WEB.ToString();
   //         request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

   //         PaymentCard paymentCard = new PaymentCard();
   //         paymentCard.CardHolderName = model.CartName;
   //         paymentCard.CardNumber = model.CartNumber;
   //         paymentCard.ExpireMonth = model.ExpirationMonth;
   //         paymentCard.ExpireYear = model.ExpirationYear;
   //         paymentCard.Cvc = model.Cvv;
   //         paymentCard.RegisterCard = 0;
   //         request.PaymentCard = paymentCard;

			////PaymentCard paymentCard = new PaymentCard();
			////paymentCard.CardHolderName = "John Doe";
			////paymentCard.CardNumber = "5528790000000008";
			////paymentCard.ExpireMonth = "12";
			////paymentCard.ExpireYear = "2030";
			////paymentCard.Cvc = "123";
			////paymentCard.RegisterCard = 0;
			////request.PaymentCard = paymentCard;
			//Buyer buyer = new Buyer();
			//buyer.Id = model.buyerId.ToString();
   //         buyer.Name = model.BuyerModel.Name;
			//buyer.Surname = model.BuyerModel.Surname;
			//buyer.GsmNumber = model.BuyerModel.GsmNumber;
			//buyer.Email = model.BuyerModel.Email;
   //         buyer.IdentityNumber = model.BuyerModel.IdentityNumber;
   //         buyer.LastLoginDate = "2013-04-21 15:12:09";
   //         buyer.RegistrationDate = "2013-04-21 15:12:09";
   //         buyer.RegistrationAddress = ""; 
			//buyer.Ip = "85.34.78.112";
			//buyer.City ="";
			//buyer.Country = "";
			//buyer.ZipCode = "";
			//request.Buyer = buyer;
			////Buyer buyer = new Buyer();
			////buyer.Id = "BY789";
			////buyer.Name = "John";
			////buyer.Surname = "Doe";
			////buyer.GsmNumber = "+905350000000";
			////buyer.Email = "email@email.com";
			////buyer.IdentityNumber = "74300864791";
			////buyer.LastLoginDate = "2015-10-05 12:43:35";
			////buyer.RegistrationDate = "2013-04-21 15:12:09";
			////buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
			////buyer.Ip = "85.34.78.112";
			////buyer.City = "Istanbul";
			////buyer.Country = "Turkey";
			////buyer.ZipCode = "34732";
			////request.Buyer = buyer;

			//Address shippingAddress = new Address();
   //         shippingAddress.ContactName = "Jane Doe";
   //         shippingAddress.City = "Istanbul";
   //         shippingAddress.Country = "Turkey";
   //         shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
   //         shippingAddress.ZipCode = "34742";
   //         request.ShippingAddress = shippingAddress;

   //         Address billingAddress = new Address();
   //         billingAddress.ContactName = "Jane Doe";
   //         billingAddress.City = "Istanbul";
   //         billingAddress.Country = "Turkey";
   //         billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
   //         billingAddress.ZipCode = "34742";
   //         request.BillingAddress = billingAddress;
   //         List<BasketItem> basketItems = new List<BasketItem>();
          
   //         foreach (var item in model.CartModel.CartItems)
   //         {
   //             BasketItem basketItem = new BasketItem();
   //             basketItem.Id = item.ProductId.ToString();
   //             basketItem.Name = item.ProductName;
   //             basketItem.Category1 = "Telefon";
   //             basketItem.ItemType = BasketItemType.PHYSICAL.ToString();
   //             basketItem.Price = item.NormalTotal().ToString().Split(",")[0];
   //             basketItems.Add(basketItem);
   //         }
   //          request.BasketItems = basketItems;

   //         return await Task.Run(()=> Payment.Create(request, options));

           
   //     }


   


        //FaturaAdresKayit(model, userId);
        //AdresKaydet(model, userId);
        private void AdresKaydet(OrderModel model, string userId)
        {
            throw new NotImplementedException();
        }

        private void FaturaAdresKayit(OrderModel model, string userId)
        {
            throw new NotImplementedException();
        }


        //_billingAddressService.Add(model.BillingAddress);
        //_shippingAddressService.Add(model.ShippingAddress);

        public async Task<IActionResult> register(string userId)
        {
            var user = _userManager.GetUserId(User);
            //Bukısmı async yapılacak sorarsın
            var cart = await _cartServices.GetCartByUserAsync(user);
            return View();
        }


        public async Task<IActionResult> SepetGoster()
        {
            var userId = _userManager.GetUserId(User);
            //Bukısmı async yapılacak sorarsın
            var cart = await _cartServices.GetCartByUserAsync(userId);
            //var cartModel = new CartModel();
            var cartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    ProductId = i.ProductId,
                    ProductName = i.Product.Name,
                    Price = (decimal)i.Product.Price,
                    Quantity = i.Quantity,
                    Resims = i.Product.Resims,

                }).ToList()
            };
            return View(cartModel);
        }
    }
}



