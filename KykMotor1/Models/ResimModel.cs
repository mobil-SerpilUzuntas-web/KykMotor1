using KykShopApp.Entities;

namespace KykMotorApp.WebIU.Models
{
    public class ResimModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public List<Resim> Resims { get; set; } = new List<Resim>();
    }
}
