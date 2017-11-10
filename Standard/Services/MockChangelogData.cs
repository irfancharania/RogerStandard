using System;
using System.Collections.Generic;
using System.Linq;
using Changelog;

namespace Standard.Services
{
    public class MockChangelogData : IChangelogData
    {
        static List<Dtos.ReleaseDto> _changelog;

        static MockChangelogData()
        {
            _changelog = new List<Dtos.ReleaseDto>
            {
                new Dtos.ReleaseDto
                {
                    ReleaseVersion = "1.0.0",
                    ReleaseDate = DateTime.UtcNow.AddDays(-1),
                    Authors = new string[] { "Irfan", "Adam" },
                    WorkItems = new Dtos.WorkItemDto[] {
                        new Dtos.WorkItemDto{
                            Id = 1,
                            WorkItemType = 1,
                            Description = "hi"
                        }
                    }
                },
                new Dtos.ReleaseDto
                {
                    ReleaseVersion = "1.0.1",
                    ReleaseDate = DateTime.UtcNow,
                    Authors = new string[] { "Adam", "Irfan" },
                    WorkItems = new Dtos.WorkItemDto[] {
                        new Dtos.WorkItemDto{
                            Id = 2,
                            WorkItemType = 1,
                            Description = "hello"
                        }
                    }
                }

            };
        }

        public Dtos.ReleaseDto Get(string version) =>
            _changelog.FirstOrDefault(x => x.ReleaseVersion == version);

        public IEnumerable<Dtos.ReleaseDto> GetAll() =>
            _changelog;
    }
}
