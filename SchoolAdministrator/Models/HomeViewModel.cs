using SchoolAdministrator.Data.Entities;

namespace SchoolAdministrator.Models
{
    public class HomeViewModel
    {
        public PaginatedList<Product> Products { get; set; }
        public float Quantity { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
