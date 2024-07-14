using Jungle.Api.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jungle.Api.Entities
{
    [Table("Categories", Schema = "Stock")]
    public class Category : ISoftDelete, IAudit
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        // Navigation properties
        public List<Product>? Products { get; set; }

        // Soft delete properties
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }

        // Audit properties
        public DateTime CreatedOnUtc { get; set; }
        public DateTime? UpdatedOnUtc { get; set; }
    }
}
