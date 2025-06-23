using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Concrete
{
    public class EfCoreNotication : EfCoreGenericRepository<Notification, ShopContext>, INotificationDal
    {
        public EfCoreNotication(ShopContext context) : base(context)
        {
        }

        public async Task<int> GetUnreadNotificationsCountAsync()
        {
            using (var context = new ShopContext())
            {
                return await context.Notifications.CountAsync(n => !n.IsRead);
            }
        }

        //Notifications tablosundan false olanları çekip  var notifications degişkenine atıyor
        //fonra bu getirdigi notifications degişkenin de saklanan liste elemanlarını foreach tek tek geziyor ve false olanlara true atıyor ve ve sonrada o true atılan tabloları update metodu ile güncelliyor ve degişiklikleri SaveChanges olarak kaydediyor.
        public void MarkAllAsRead()
        {
            using (var context = new ShopContext())
            {
                var notifications = context.Notifications.Where(n => !n.IsRead).ToList();
                foreach (var notification in notifications)
                {
                    notification.IsRead = true;
                    context.Notifications.Update(notification);
                }
                context.SaveChanges();
            }
        }
    }
}
