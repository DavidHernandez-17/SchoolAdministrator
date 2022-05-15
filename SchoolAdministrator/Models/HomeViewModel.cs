using SchoolAdministrator.Data.Entities;

namespace SchoolAdministrator.Models
{
    public class HomeViewModel
    {
        public ICollection<Product> Products { get; set; }

        public float Quantity { get; set; }

    }
}
