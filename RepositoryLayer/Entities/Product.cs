namespace RepositoryLayer.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public int? CategoryID { get; set; } = null!;
        public Category Category { get; set; } = null!;

        public int? OrderID { get; set; } = null!;
        public Order Order { get; set; } = null!;
    }
}
