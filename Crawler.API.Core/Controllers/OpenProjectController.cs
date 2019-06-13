using Crawler.API.Core.Filter;
using Crawler.API.Core.Interfaces;
using Crawler.API.Core.Models;
using Crawler.API.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crawler.API.Core.Controllers
{
    [ApiController]
    [OpenProjectFilter]
    public class OpenProjectController : ControllerBase
    {
        private readonly IHttpService _httpClientService;
        private readonly DateTime _today;
        private readonly DateTime _last2Week;
        private readonly DateTime _next2week;

        public OpenProjectController(IHttpService httpClientService)
        {
            _httpClientService = httpClientService;
            _today = DateTime.Now;
            _last2Week = _today.AddDays(-14).Date + new TimeSpan(0, 0, 0);
            _next2week = _today.AddDays(28).Date + new TimeSpan(23, 59, 59);
        }

        [Route("api/openproject/versions")]
        public async Task<ActionResult> GetVersions()
        {
            CollectionViewModel<VersionViewModel> versions = await GetMyVersions();
            return Ok(versions);
        }

        [Route("api/openproject/timeentryactivities")]
        public ActionResult GetTimeEntryActivities()
        {
            var activities = new List<TimeEntryActivity>() {
                new TimeEntryActivity {
                    Id = 2, Name = "Specification", Order = 1
                },
                new TimeEntryActivity {
                    Id = 3, Name = "Development", Order = 2
                },
                new TimeEntryActivity {
                        Id = 4, Name = "Testing", Order = 3
                },
                new TimeEntryActivity {
                    Id = 19, Name = "Reproduce Bug", Order = 4
                },
                new TimeEntryActivity {
                    Id = 18, Name = "Fix Bug", Order = 5
                },
                new TimeEntryActivity {
                    Id = 5, Name = "Support", Order = 6
                },
                new TimeEntryActivity {
                    Id = 6, Name = "Other", Order = 7
                }
            };

            return Ok(activities);
        }

        [Route("api/openproject/createtimeentry")]
        [HttpPost]
        public async Task<ActionResult> CreateTimeEntry(TimeEntryViewModel model)
        {
            var url = "https://travel2pay.openproject.com/api/v3/time_entries";
            var createTimeEntryRequest = new OPCreateTimeEntryRequest()
            {
                Comment = new OPCreateTimeEntryRequestComment { Raw = model.Comment },
                Hours = $"PT{model.Hours}H",
                Links = new OPCreateTimeEntryModelLink()
                {
                    Activity = new OPCreateTimeEntryModelActivity { Href = $"/api/v3/time_entries/activities/{model.ActivityId}" },
                    Project = new OPCreateTimeEntryModelProject { Href = "/api/v3/projects/2" },
                    WorkPackage = new OPCreateTimeEntryModelWorkPackage { Href = $"/api/v3/work_packages/{model.WorkPackageId}" }
                },
                SpentOn = model.SpentOn.ToString("yyyy-MM-dd")
            };
            var timeEntry = await _httpClientService
                .Create(Request.Headers["openProjectAPIKey"].First())
                .Post<OPCreateTimeEntryRequest, OpCreateTimeEntryResponse>(url, createTimeEntryRequest);

            return Ok(timeEntry);
        }

        [Route("api/openproject/updatetimeentry")]
        [HttpPost]
        public async Task<ActionResult> UpdateTimeEntry(TimeEntryViewModel model)
        {
            var url = "https://travel2pay.openproject.com/api/v3/time_entries/" + model.Id;
            var createTimeEntryRequest = new OPCreateTimeEntryRequest()
            {
                Comment = new OPCreateTimeEntryRequestComment { Raw = model.Comment },
                Hours = $"PT{model.Hours}H",
                Links = new OPCreateTimeEntryModelLink()
                {
                    Activity = new OPCreateTimeEntryModelActivity { Href = $"/api/v3/time_entries/activities/{model.ActivityId}" },
                    Project = new OPCreateTimeEntryModelProject { Href = "/api/v3/projects/2" },
                    WorkPackage = new OPCreateTimeEntryModelWorkPackage { Href = $"/api/v3/work_packages/{model.WorkPackageId}" }
                },
                SpentOn = model.SpentOn.ToString("yyyy-MM-dd")
            };
            var timeEntry = await _httpClientService
                .Create(Request.Headers["openProjectAPIKey"].First())
                .Patch<OPCreateTimeEntryRequest, OpCreateTimeEntryResponse>(url, createTimeEntryRequest);

            return Ok(timeEntry);
        }

        [Route("api/openproject/deletetimeentry/{id}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteTimeEntry(long id)
        {
            var url = "https://travel2pay.openproject.com/api/v3/time_entries/" + id;
            var result = await _httpClientService
                .Create(Request.Headers["openProjectAPIKey"].First())
                .Delete(url);

            return Ok(result);
        }

        private async Task<CollectionViewModel<VersionViewModel>> GetMyVersions()
        {
            var openVersions = await GetOpenedVersions();

            var versions = new CollectionViewModel<VersionViewModel>();
            var tasks = new List<Task>();
            foreach (var version in openVersions)
            {
                tasks.Add(Task.Run(() => GetWorkpackagesByVersion(versions, version)));
            }

            Task.WaitAll(tasks.ToArray());

            versions.Elements = versions.Elements.OrderBy(x => x.Title).ThenBy(x => x.Id).ToList();
            return versions;
        }

        private async Task GetWorkpackagesByVersion(CollectionViewModel<VersionViewModel> versions, OPVersion version)
        {
            var workPackageByVersion = await GetWorkPackageByVersion(version);

            workPackageByVersion = CombineParentPackage(workPackageByVersion);
            versions.Elements.Add(new VersionViewModel
            {
                Id = version.Id,
                Title = version.Name,
                WorkPackages = workPackageByVersion.ToList()
            });
        }

        private IEnumerable<WorkPackage> CombineParentPackage(IEnumerable<WorkPackage> workPackageByVersion)
        {
            var workPackageByVersionIds = workPackageByVersion.Select(x => x.Id);
            var parentWorkPackages = workPackageByVersion.Where(x => !x.ParentId.HasValue);
            var parentWorkPakageIds = parentWorkPackages.Select(x => x.Id);

            var childrentWorkPackages = workPackageByVersion.Where(x => x.ParentId.HasValue);
            var result = new List<WorkPackage>();

            result.AddRange(parentWorkPackages);
            result.AddRange(workPackageByVersion
                .Where(x => x.ParentId.HasValue)
                .Where(x => !workPackageByVersionIds.Contains(x.ParentId.Value))
            );

            result.ForEach(parent => parent.Children.AddRange(childrentWorkPackages.Where(x => x.ParentId.Value == parent.Id)));

            return result;
        }

        private async Task<IEnumerable<WorkPackage>> GetWorkPackageByVersion(OPVersion version)
        {
            string url = "https://travel2pay.openproject.com/api/v3/projects/2/work_packages?pageSize=1000&offset=1&filters=[{\"status\":{\"operator\":\"o\",\"values\":[]}},{\"version\":{\"operator\":\"=\",\"values\":[\"" + version.Id + "\"]}}]&sortBy=[[\"parent\",\"asc\"]]";
            var wps = await _httpClientService
                .Create(Request.Headers["openProjectAPIKey"].First())
                .Get<OPCollection<OPWorkPackage>>(url);

            if (wps == null || wps.Embedded == null || wps.Embedded.Elements == null) return new List<WorkPackage>();

            return wps.Embedded.Elements.Select(x => new WorkPackage
            {
                Id = x.Id,
                Subject = $"{x.Id} - {x.Subject} - {x.Links?.Type?.Title}",
                ProjectId = GetId(x.Links.Project.Href).Value,
                ProjectName = x.Links.Project.Title,
                VersionId = GetId(x.Links.Version.Href).Value,
                VersionTitle = x.Links.Version.Title,
                ParentId = GetId(x.Links.Parent.Href)
            });
        }

        private long? GetId(string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            return long.Parse(str.Substring(str.LastIndexOf("/") + 1));
        }

        private async Task<List<OPVersion>> GetOpenedVersions()
        {
            var versions = await _httpClientService
                .Create(Request.Headers["openProjectAPIKey"].First())
                .Get<OPCollection<OPVersion>>("https://travel2pay.openproject.com/api/v3/versions");

            var result = versions.Embedded.Elements
                //.Where(x => x.Status == "open" && x.Name.StartsWith("Team Bubble"))
                .Where(x => x.Status == "open")
                //.Where(x => x.Name.StartsWith("Team Genius"))
                .Where(SameTime)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Id)
                .Take(20)
                .ToList();
            var today = DateTime.Now;

            return result;
        }

        private bool SameTime(OPVersion version)
        {
            var name = version.Name;
            var lastSpaceIndex = name.Trim().LastIndexOf(' ');
            if (lastSpaceIndex == -1) return false;
            var versionDateStr = name.Substring(name.Trim().LastIndexOf(' '));
            if (!DateTime.TryParse(versionDateStr, out DateTime versionDate)) return false;

            if ((_last2Week < versionDate) && (versionDate < _next2week)) return true;

            return false;
        }
    }
}