using Jungle.Api.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jungle.Api.Entities
{
    [Table("Customers", Schema = "Sales")]
    public class Customer : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        // Navigation properties
        public List<Order>? Orders { get; set; }

        // Soft delete properties
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
    }
}
