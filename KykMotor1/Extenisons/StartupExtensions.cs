using KykMotorApp.WebIU.CustomValidator;
using KykShopApp.DataAccess.Concrete;
using KykShopApp.Entities;
using Microsoft.Extensions.Options;

namespace KykMotorApp.WebIU.Extenisons
{
    public static class StartupExtensions
    {
        public static void AddIdentityWithExh(this IServiceCollection services)
        {

              services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._<@+*/?";
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail = false;

                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                //RequireNonAlphanumeric +*/ gibi işaretlerdir
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 0;
                //Lockout.MaxFailedAccessAttempts BU ÖZELL?K kullan?c?n?n 5 kere yanl?? girme hakk? olu?turur
                options.Lockout.MaxFailedAccessAttempts = 5;
                //A??ag?da ki özellik kullan?c? 5 defa ?ifresini yada kullan?c? ad?n? yanl?? girdimi 3 daki giri? yapam?yacak bloke geliyor
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Lockout.AllowedForNewUsers = true;
            }).AddPasswordValidator<PaswordValidator>().AddUserValidator<UserValidator>().AddEntityFrameworkStores<ShopContext>();

          
        }

        public static void ConfigureApplicationCookieExh(this IServiceCollection services)
        {
            services.ConfigureApplicationCookie(options =>
            {
                var cookieBuilder = new CookieBuilder();

                cookieBuilder.Name = "KykAppCookie";
                options.LoginPath = "/account/login";
                options.LogoutPath = "/account/logoult";
                //Kullan?c? yetkisi olmayan bir sayfaya girmeye çal??t?g?nda "/register/accessdenied" yönlendirilecek paht adresi
                options.AccessDeniedPath = new PathString("/Account/AccessDenied");
                //ExpireTimeSpan amac? kullan?c? giri? yapt?kdan sonra 60 dakika cookie bilgileri tutulur egerki 60 dakika hiç bir ?ekilde kullan?c? uygulamay? kullanmazsan otomatikman örnek admin panelindeyse otomatikman uygulamakapat?l?r. eger 59 dakikada bir hareket olursa cookie süresi tekrardan ba?lar 
                options.Cookie = cookieBuilder;
                options.ExpireTimeSpan = TimeSpan.FromDays(60);
                options.SlidingExpiration = true;
                cookieBuilder.HttpOnly = true;
                //Prevent Cross-Site Attacks
                cookieBuilder.SameSite = SameSiteMode.Strict;
            });
        }
    }
}
