//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;

//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace KykShopApp.DataAccess.Concrete
//{
//    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ShopContext>
//    {
//        public ShopContext CreateDbContext(string[] args =null)
//        {
//            // appsettings.json dosyasının yolunu ayarlayın
//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();

//            // DbContextOptions oluşturun
//            var optionsBuilder = new DbContextOptionsBuilder<ShopContext>();
//            optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlCon"));

//            return new ShopContext(optionsBuilder.Options);
//        }
//    }
//}
