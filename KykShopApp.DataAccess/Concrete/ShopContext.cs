using KykShopApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Concrete
{


//IdentityDbContext<ApplicationUser, ApplicationRole, string>
    public class ShopContext: IdentityDbContext<ApplicationUser,ApplicationRole,String> 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=KykDatabase;integrated security=true;");

        }
        //public ShopContext(DbContextOptions<ShopContext> options):base(options) { }


        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubbirCategori> SubbirCategories { get; set; }
        public DbSet<SubIkiCategory> SubIkiCategories { get; set; }
        public DbSet<Resim> Resims { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<BayiBasvuru> BayiBasvurus { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Buyerr> Buyerrs { get; set; }
        public DbSet<ShippingAddres> ShippingAddress { get; set; }
        public DbSet<BillingAddres> BillingAddress { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                 new(){Id=1,Name="Latde Bayan Eldiven",Price=500,Description="Rahat Konfor",KoliAdet=20,CategoryId=1,SubBirId=1,SubIkiId=1,IsDeleted=true,
                 }
               

            });

            modelBuilder.Entity<Category>().HasData(new Category[]
              {
                 new(){Id=1,Name="Bisiklet Yp",IsDeleted=true},
                 new(){Id=2,Name="Motor Yp",IsDeleted=true}

             });
            modelBuilder.Entity<SubbirCategori>().HasData(new SubbirCategori[]
           {
                 new(){Id=1,Name="Bisiklet Aksesuarları",IsDeleted=true,CategoryId=1},
                 new(){Id=2,Name="Bisiklet Sepetler",IsDeleted=true,CategoryId=1}
           });
            modelBuilder.Entity<SubIkiCategory>().HasData(new SubIkiCategory[]
            {
                 new(){Id=1,Name="Bayan Eldiven",IsDeleted=true,SubBirId=1},
                 new(){Id=2,Name="Erkek Eldiven",IsDeleted=true,SubBirId=1}
            });

            modelBuilder.Entity<ShippingAddres>()
            .HasOne(sa => sa.Buyerr) // ShippingAddress bir Buyer'e ait
            .WithMany(b => b.ShippingAddress) // Buyer birden çok ShippingAddress'e sahiptir
            .HasForeignKey(sa => sa.BuyerId) // Foreign Key ShippingAddress tablosunda
             .OnDelete(DeleteBehavior.Restrict); // Silme davranışı: Buyer silinse bile ShippingAddress silinmez

            modelBuilder.Entity<BillingAddres>()
         .HasOne(o => o.Buyerr)//BillingAdress bir Buyer,e ayit
         .WithOne(b => b.BillingAddres) //Buyer bir BillingAddress ayit
         .HasForeignKey<BillingAddres>(o => o.BuyerId)//Forenkey
         .OnDelete(DeleteBehavior.Restrict); // Silme davranışı: Buyer silinse bile BillingAddres silinmez

            modelBuilder.Entity<Order>()
           .HasOne(o => o.Buyerr) // Order -> Buyer navigasyon özelliği
          .WithMany(b => b.Orders) // Buyer -> Order navigasyon özelliği
          .HasForeignKey(o => o.BuyerId) // Foreign key tanımı
          .OnDelete(DeleteBehavior.Restrict); // Silme davranışı


            // modelBuilder.Entity<BillingAddres>()
            //.HasOne(o => o.Order) // BillingAddress bir Order'a bağlı
            //.WithOne(b => b.BillingAddress) // Order bir BillingAddress'e sahiptir
            //.HasForeignKey<BillingAddres>(b => b.OrderId) // Foreign Key, BillingAddress tablosunda
            //.OnDelete(DeleteBehavior.Restrict); // Silme davranışı: Order silinse bile BillingAddress etkilenmez

            //modelBuilder.Entity<ShippingAddres>()
            //    .HasOne(o => o.Order) // ShippingAddres bir Order'a bağlı
            //    .WithOne(s => s.ShippingAddress)
            //    .HasForeignKey<ShippingAddres>(s => s.OrderId)
            //    .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Notification>()
            //Her Notification Bir BayiBasvuru Ayittir
            .HasOne(n => n.BayiBasvuru)
            //Her BayiBasvuru Birden fazla Notifications Ayittir
            .WithMany(b => b.Notifications)
            .HasForeignKey(n => n.BayiBasvuruId);

            modelBuilder.Entity<Product>()
                //Her Product Bir Category Ayittir
                .HasOne(c => c.Category)
                //Her Category Birden fazla Product Ayittir
                .WithMany(p => p.Products)
                //Yabancıl Anahtar
                .HasForeignKey(c => c.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);


          
            modelBuilder.Entity<Product>()
               //Her Product Bir SubbirCategori Ayittir
               .HasOne(c => c.SubbirCategori)
               //Her SubbirCategori Birden fazla Product Ayittir
               .WithMany(p => p.Products)
               //Yabancıl Anahtar
               .HasForeignKey(c => c.SubBirId)
               .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Product>()
              //Her Product Bir SubIkiCategory Ayittir
              .HasOne(c => c.SubIkiCategory)
              //Her SubIkiCategory Birden fazla Product Ayittir
              .WithMany(p => p.Products)
              //Yabancıl Anahtar
              .HasForeignKey(c => c.SubIkiId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
              //Her Product Bir Category Ayittir
              .HasMany(r => r.Resims)
              //Her Category Birden fazla Product Ayittir
              .WithOne(p => p.Product)
              //Yabancıl Anahtar
              .HasForeignKey(p => p.ProductId)
             .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Category>()
                //Her Categorinin birden çok Sub1Category var
                .HasMany(s => s.SubbirCategories)
                //Her Sub1Category Bir Category var
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId)
              .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<SubbirCategori>()
               //Her SubbirCategori  birden çok SubIkiCategories var
               .HasMany(s => s.SubIkiCategories)
               //Her Bir SubIkiCategories Bir 
               .WithOne(s => s.SubbirCategori)
               .HasForeignKey(s => s.SubBirId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(x => new { x.LoginProvider, x.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
        }
    }

}








//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DbMotorKyk;integrated security=true;");
//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//{
//    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DbMotorKyk;integrated security=true;");

//}




//Not:Modelin bak eger içinde sadece ilişki olarak bir tip verilmişse Örnek Roduct tablasu Product modelin de Categorynin sade CadegoriId verilmi o halde burada bire bir iliş ki var biri çok ilişki Category tablosunda oluşturulmuştur yani Category tablasuna List Type Products verilmiş bu metotları dogru kullanmak istiyorsan önce model bakıcaksın modelin içinde tanımlarda eger sadece modelin ilişkili oldugu alana sadece modelin Id atıldıyse ve model type bir özellik eklendiyse bu o modelin içinde bire bir ilişki oldugunu diger modelde bire çok ilişki tanımlandıgın ve diger modele List ytpe bir model gittigini gözlemlersin Bire bir ilişki olan model için HasOne ve WithMany metotları kullanılacak. Ve diger modele bakılgında List Type bir model özelligi verildigini göreceksin bu kısım işde modeller arasında ki bire çok ilişki kısmı bu şartlardada HasMany ve WithOne metotları kullanacaksın