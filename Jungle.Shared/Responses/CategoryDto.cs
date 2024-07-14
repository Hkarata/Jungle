namespace Jungle.Shared.Responses
{
    public record CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<ProductDto>? Products { get; set; }
    }
}
