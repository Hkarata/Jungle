namespace Jungle.Shared.Requests;

public class CreateProductDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string>? Images { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Guid TenantId { get; set; }
    public List<Guid>? Categories { get; set; }
}