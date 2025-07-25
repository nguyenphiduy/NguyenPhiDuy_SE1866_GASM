

namespace RepositoryLayer.Entities
{
    public class Order
    {
        public int OrderID { get; set; }
        public int OrderAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int CustomerID { get; set; }
        public Customer Customer { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
