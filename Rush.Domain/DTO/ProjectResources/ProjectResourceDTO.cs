namespace Rush.Domain.DTO.ProjectResources
{
    public class ProjectResourceDTO: BaseDTO
    {
        public Guid ProjectId { get; set; }
        public Guid ResourceId { get; set; }
        public int Quantity { get; set; }
        public int UsedQuantity { get; set; }
    }
}
