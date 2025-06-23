using KykShopApp.Business.Abstract;
using KykShopApp.Business.Concrete;
using KykShopApp.DataAccess.Abstract;
using KykShopApp.DataAccess.Concrete;
using KykShopApp.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using KykMotorApp.WebIU.Extenisons;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.MaxDepth = 64;
    });
builder.Services.AddDbContext<ShopContext>();
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>
().AddEntityFrameworkStores<ShopContext>
().AddDefaultTokenProviders();

//.AddDefaultTokenProviders();
//builder.Services.AddDbContext<ShopContext>(options =>
//builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
//.AddEntityFrameworkStores<ShopContext>()
//.AddDefaultTokenProviders());

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ ";
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedPhoneNumber = false;
    options.SignIn.RequireConfirmedEmail = false;

    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    //Lockout.MaxFailedAccessAttempts BU ÖZELL?K kullan?c?n?n 5 kere yanl?? girme hakk? olu?turur
    options.Lockout.MaxFailedAccessAttempts = 5;
    //A??ag?da ki özellik kullan?c? 5 defa ?ifresini yada kullan?c? ad?n? yanl?? girdimi 3 daki giri? yapam?yacak bloke geliyor
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
    options.Lockout.AllowedForNewUsers = true;

});
builder.Services.ConfigureApplicationCookie(options =>
{
    var cookieBuilder = new CookieBuilder();

    cookieBuilder.Name = "KykAppCookie";
    options.LoginPath = "/Account/SingIn";
    options.LogoutPath = "/Account/Logoult";
    //Kullan?c? yetkisi olmayan bir sayfaya girmeye çal??t?g?nda "/register/accessdenied" yönlendirilecek paht adresi
    options.AccessDeniedPath = new PathString("/Account/AccessDenied");
    //ExpireTimeSpan amac? kullan?c? giri? yapt?kdan sonra 60 dakika cookie bilgileri tutulur egerki 60 dakika hiç bir ?ekilde kullan?c? uygulamay? kullanmazsan otomatikman örnek admin panelindeyse otomatikman uygulamakapat?l?r. eger 59 dakikada bir hareket olursa cookie süresi tekrardan ba?lar 
    options.Cookie = cookieBuilder;
    options.ExpireTimeSpan = TimeSpan.FromDays(60);
    options.SlidingExpiration = true;
    cookieBuilder.HttpOnly = true;
    //Prevent Cross-Site Attacks
    cookieBuilder.SameSite = SameSiteMode.Strict;
    //cookieBuilder.SameSite = SameSiteMode.Strict;
});




//AddIdentity ayarlar? için metot Bu metot Kendi Haz?rlad?g?m StartupExtension S?n?f?ndan geliyor neden böyle yapt?m Program.cs temiz olsun diye
//builder.Services.AddIdentityWithExh();
////Cookie için metot Bu metot KendiHaz?rlad?g?m StartupExtension S?n?f?ndan geliyor neden böyle yapt?m Program.cs temiz olsun diye
//builder.Services.ConfigureApplicationCookieExh();


builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});


builder.Services.AddMvc();
// Client-side do?rulama i?lemlerinin etkinle?tirilmesi için HtmlHelperOptions.ClientValidationEnabled ayar? yap?l?r.Kullan?c? eger ki input lardan birini bo? geçerce validate i?lemleri için bu k?sm? aktif ediyoruz
builder.Services.AddControllersWithViews()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = true;
    });




//Add your dependencies here
builder.Services.AddScoped<INotificationDal, EfCoreNotication>();
builder.Services.AddScoped<IBayiBasvuruDal,EfCoreDalBayiBasvuru>();
builder.Services.AddScoped<IBillingDal, EfCoreBillingDal>();
builder.Services.AddScoped<IShippingAddressDal, EfCoreShippingDal>();
builder.Services.AddScoped<IBuyerDal, EfCoreBuyerDal>();
builder.Services.AddScoped<IOrderDal, EfCoreOrderDal>();
builder.Services.AddScoped<ICartDal, EfCoreCartDal>();
builder.Services.AddScoped<ICategoryDal, EfCoreCategoryDal>();
builder.Services.AddScoped<ISubbirDal, EfCoreSubBirDal>();
builder.Services.AddScoped<ISubIkiCatDal, EfCoreSub2CatDal>();
builder.Services.AddScoped<IProductDal, EfCoreDalProduct>();
builder.Services.AddScoped<IResimDal, EfCoreDalResim>();
// Use EfCoreProductDal instead of MemoryProductDal
builder.Services.AddScoped<INotificationServices, NotificationManager>();
builder.Services.AddScoped<IBayiBasvurusuServices, BayiBasvuruManager>();
builder.Services.AddScoped<IBillingServices, BillingManager>();
builder.Services.AddScoped<IShippingServices, ShippingManager>();
builder.Services.AddScoped<IBuyerServices,BuyerManager>();
builder.Services.AddScoped<ICartServices, CartManager>();
builder.Services.AddScoped<IOrderServices, OrderManager>();
builder.Services.AddScoped<IProductServices, ProductManager>();
builder.Services.AddScoped<ICategoryServices, CategoryManager>();
builder.Services.AddScoped<ISubbirServices, SubbirManager>();
builder.Services.AddScoped<ISub2CatServices, Sub2CatManager>();
builder.Services.AddScoped<IResimServices, ResimManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
 
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
		name: "areas",
		pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
	);

    endpoints.MapDefaultControllerRoute();
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=index}/{id?}"

  );

app.MapControllerRoute(
    name: "adminProductEdit",
    pattern: "admin/urunduzenle/{id?}",
    defaults: new { controller = "AdminProduct", action = "ProductsEdit" }
);


app.MapControllerRoute(
    name: "adminProductCreate",
    pattern: "admin/ürünekle/{id?}",
    defaults: new { controller = "AdminProduct", action = "CreateProduct" }
);

app.MapControllerRoute(
    name: "cart",
    pattern: "cart",
    defaults: new { controller = "Cart", action = "Index" }
);

app.MapControllerRoute(
    name: "checkout",
    pattern: "cart",
    defaults: new { controller = "Cart", action = "Checkout" }
);
app.Run();
//app.MapControllerRoute(
//   name: "areas",
//   pattern: "{area:exists}/{controller=Home}{action=Index}/{id?}");




//app.MapControllerRoute(
//    name: "adminProductsListe",
//    pattern: "admin/urunlerlistesi",
//    defaults: new { controller = "AdminProduct", action = "Index" }
//);





