using Jungle.Api.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jungle.Api.Entities
{
    [Table("Categories", Schema = "Stock")]
    public class Category : ISoftDelete
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation properties
        public List<Product>? Products { get; set; }

        // Soft delete properties
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOnUtc { get; set; }
    }
}
