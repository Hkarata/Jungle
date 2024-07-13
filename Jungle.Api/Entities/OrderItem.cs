using System.ComponentModel.DataAnnotations.Schema;

namespace Jungle.Api.Entities
{
    [Table("OrderItems", Schema = "Sales")]
    public class OrderItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }

    public static class OrderItemExtensions
    {
        public static decimal CalculateTotalPrice(this OrderItem orderItem)
        {
            return orderItem.UnitPrice * orderItem.Quantity;
        }
    }
}
