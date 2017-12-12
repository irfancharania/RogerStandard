using System.IO;
using System.Text;

namespace ActuallyStandard.Helpers
{
    public class StringWriterWithEncoding : StringWriter
    {
        public StringWriterWithEncoding(Encoding encoding)
        {
            Encoding = encoding;
        }

        public override Encoding Encoding { get; }
    }
}
