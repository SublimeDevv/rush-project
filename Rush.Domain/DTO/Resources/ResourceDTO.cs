namespace Rush.Domain.DTO.Resources
{
    public class ResourceDTO: BaseDTO
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public int Quantity { get; set; }

    }
}
