

namespace BusinessLayer.DTO
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }

        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public CategoryDTO? CategoryDTO { get; set; }

        public int? OrderID { get; set; }
        public OrderDTO? OrderDTO { get; set; }
        public string CategoryPicture { get; set; }     // Base64
        public string CategoryDescription { get; set; } // Mô tả

    }
}
