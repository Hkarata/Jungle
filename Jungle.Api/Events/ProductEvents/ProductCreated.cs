namespace Jungle.Api.Events.ProductEvents;

public class ProductCreated : Event
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public string Images { get; set; } = string.Empty;
    public List<string>? ProductCategories { get; set; }
    public decimal ProductPrice { get; set; }
    public int ProductQuantity { get; set; }
    public decimal TotalPrice => ProductPrice * ProductQuantity;
    public override Guid StreamId => ProductId;
}