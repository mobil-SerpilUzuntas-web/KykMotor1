using KykShopApp.Business.Abstract;
using KykShopApp.DataAccess.Abstract;
using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Concrete
{
    public class NotificationManager : INotificationServices
    {
    
        private INotificationDal _notificationDal;
        public NotificationManager(INotificationDal notificationDal)
        {
            _notificationDal = notificationDal;

        }
        public async Task CreateAsync(Notification entity)
        {
            await _notificationDal.CreateAsync(entity);
        }

        public async Task<int> GetUnreadNotificationsCountAsync()
        {
            return await _notificationDal.GetUnreadNotificationsCountAsync();
        }

        public void Delete(Notification entity)
        {
            _notificationDal.Delete(entity);
        }

        public List<Notification> GetAll()
        {
            return _notificationDal.GetAll();
        }

        public Notification GetById(int id)
        {
            return _notificationDal.GetById(id);
        }

        public void Update(Notification entity)
        {
            _notificationDal.Update(entity);
        }


        public void MarkAllAsRead()
        {
            _notificationDal.MarkAllAsRead();
        }
    }
}
