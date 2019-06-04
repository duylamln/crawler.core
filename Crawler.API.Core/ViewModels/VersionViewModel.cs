using System.Collections.Generic;

namespace Crawler.API.Core.ViewModels
{
    public class VersionViewModel
    {
        public VersionViewModel()
        {
            WorkPackages = new List<WorkPackage>();
        }
        public long Id { get; set; }
        public string Title { get; set; }
        public List<WorkPackage> WorkPackages { get; set; }
    }

    public class WorkPackage
    {
        public WorkPackage()
        {
            Children = new List<WorkPackage>();
        }
        public string Subject { get; set; }
        public long Id { get; set; }
        public long VersionId { get; set; }
        public string VersionTitle { get; set; }
        public long ProjectId { get; set; }
        public string ProjectName { get; set; }
        public long? ParentId { get; set; }
        public List<WorkPackage> Children { get; set; }
    }
}
