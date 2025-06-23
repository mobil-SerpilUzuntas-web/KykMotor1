using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.DataAccess.Abstract
{
    public interface INotificationDal : IRepository<Notification>
    {
        Task<int> GetUnreadNotificationsCountAsync();
        void MarkAllAsRead();
    }
}
