using Rush.Domain.Entities.ProjectResources;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rush.Domain.Entities.Resources
{
    [Table("Tbl_Resources")]
    public class Resource : BaseEntity
    {

        public required string Name { get; set; }
        public required string Description { get; set; }

        public int Quantity { get; set; }

        public List<ProjectResource> ProjectResources { get; set; } = [];
    }
}

