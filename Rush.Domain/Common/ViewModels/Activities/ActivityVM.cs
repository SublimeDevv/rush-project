using Rush.Domain.Common.ViewModels.Tasks;
using static Rush.Domain.Common.Util.Enums;

namespace Rush.Domain.Common.ViewModels.Activities
{
    public class ActivityVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public TaskVM Task { get; set; }
    }
}
