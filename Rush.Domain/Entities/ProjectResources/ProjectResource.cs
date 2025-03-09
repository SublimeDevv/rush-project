using Rush.Domain.Entities.Resources;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rush.Domain.Entities.ProjectResources
{
    [Table("Tbl_ProjectResources")]
    public class ProjectResource : BaseEntity
    {

        [ForeignKey("ProjectId")]
        public Guid ProjectId { get; set; }

        [ForeignKey("ResourceId")]
        public Guid ResourceId { get; set; }
        public Resource? Resource { get; set; }

        public int Quantity { get; set; }
        public int UsedQuantity { get; set; }

    }
}

