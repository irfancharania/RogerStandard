using System;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ActuallyStandard.Helpers;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;

namespace ActuallyStandard.Services
{
    public class FeedService : IFeedService
    {
        public async Task<string> GetFeed()
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

                // Create item
                var item = new SyndicationItem()
                {
                    Title = "Atom Writer Avaliable",
                    Description = "The new Atom Writer is now available as a NuGet Package!",
                    Id = "https://www.nuget.org/packages/Microsoft.SyndicationFeed.ReaderWriter",
                    Published = DateTimeOffset.UtcNow,
                    LastUpdated = DateTimeOffset.UtcNow
                };

                item.AddContributor(new SyndicationPerson("test", "test@mail.com"));

                await writer.Write(item);
                xmlWriter.Flush();
            }

            return sw.GetStringBuilder().ToString();
        }
    }
}
