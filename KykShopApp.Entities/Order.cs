using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KykShopApp.Entities
{
    public class Order
    {
        public Order() 
        { 
            OrderItems = new List<OrderItem>(); 
        }  
        public int Id { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }
        public EnumOrderState OrderState { get; set; }
        public EnumPaymentType PaymetType { get; set; }
   
        public string OrderNote { get; set; }
        public string PaymentId { get; set; }
        public string ConversationId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int ShippingAddresId { get; set; }
        public int BillingAddresId { get; set; }

        // İlişkili sınıflar
        // public BillingAddres BillingAddress { get; set; }
        //public ShippingAddres ShippingAddress { get; set; }

        public int BuyerId { get; set; }
        public Buyerr Buyerr { get; set; }

        // BillingAddress ile ilişki


    }

    public enum EnumOrderState
    {
        Waiting =0,
        UnPaind =1,
        Completed =2,
    }
    public enum EnumPaymentType
    {
        CreditCart=0,
        Eft=1,
    }
}


