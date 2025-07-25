

namespace BusinessLayer.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public int OrderAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;

        public int CustomerID { get; set; }
        public CustomerDTO CustomerDTO { get; set; }
    }
}
