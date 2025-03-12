namespace Rush.Domain.Common.ViewModels.Resources
{
    public class ResourceDataDashboardVM
    {
        public int TotalResources { get; set; }
        public int AssignedResources { get; set; }
        public int ProjectsWithResources { get; set; }
        public string MostUsedResource { get; set; }
        public object ResourcesByMonth { get; set; }
    }
}
