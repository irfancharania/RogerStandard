using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ActuallyStandard.Helpers;
using ActuallyStandard.ViewModels;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;

namespace ActuallyStandard.Services
{
    public class FeedService : IFeedService
    {
        public async Task<string> GenerateFeed(IEnumerable<ReleaseViewModel> releases)
        {
            const string title = "Changelog Feed";
            var id = new UniqueId();
            var updated = DateTimeOffset.UtcNow;

            var sw = new StringWriterWithEncoding(Encoding.UTF8);
            using (var xmlWriter = XmlWriter.Create(sw, new XmlWriterSettings() { Async = true, Indent = true }))
            {
                var writer = new AtomFeedWriter(xmlWriter);

                await writer.WriteTitle(title);
                await writer.WriteId(id.ToString());
                await writer.WriteUpdated(updated);

                foreach (var release in releases)
                {
                    var item = new SyndicationItem()
                    {
                        Title = release.ReleaseVersion,
                        Id = new UniqueId(release.ReleaseId).ToString(),
                        LastUpdated = release.ReleaseDate
                    };

                    release.Authors.ForEach(x =>
                        item.AddContributor(new SyndicationPerson(x, $"{x}@test.com"))
                    );

                    var sb = new StringBuilder();
                    foreach (var workItemViewModel in release.WorkItems)
                    {
                        sb.Append(workItemViewModel.WorkItemTypeString)
                            .Append(": ")
                            .AppendLine(workItemViewModel.Description)
                            ;
                    }
                    item.Description = sb.ToString();

                    await writer.Write(item);
                }

                xmlWriter.Flush();
            }

            return sw.GetStringBuilder().ToString();
        }

    }
}
