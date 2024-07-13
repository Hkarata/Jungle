using Jungle.Api.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jungle.Api.Entities
{
    [Table("Products", Schema = "Stock")]
    public class Product : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Navigation properties
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public Guid TenantId { get; set; }
        public Tenant? Tenant { get; set; }

        // Soft delete properties
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
    }
}
