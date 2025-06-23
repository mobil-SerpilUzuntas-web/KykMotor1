using KykShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Business.Abstract
{
    public interface INotificationServices
    {
        Task CreateAsync(Notification entity);
        Task<int> GetUnreadNotificationsCountAsync();//Yeni metod: Okunmamış bildirimleri sayar
        void MarkAllAsRead(); // Tüm bildirimleri okunmuş olarak işaretler
        Notification GetById(int id); //Bir Id gönderiliyor Id si uyan Notification(Bildirimi)Getirecek genelde Detay için kullanılır
        List<Notification> GetAll(); //Bu metot ise tüm bildirimleri Listeliyor
        void Update(Notification entity); //Bu metot Güncelliyor
        void Delete(Notification entity);
    }
}
