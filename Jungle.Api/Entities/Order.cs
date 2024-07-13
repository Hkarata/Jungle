namespace Jungle.Api.Entities
{
    [Table("Orders", Schema = "Sales")]
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; } = string.Empty;

        // Navigation properties
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }

        public List<OrderItem>? OrderItems { get; set; }
    }
}
