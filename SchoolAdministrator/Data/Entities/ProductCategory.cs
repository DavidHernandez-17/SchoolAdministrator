namespace SchoolAdministrator.Data.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public Subject Subject { get; set; }

    }
}
