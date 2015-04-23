using Microsoft.Security.Application;
using System.IO;
using System.Web.Util;

namespace CP.NLayer.Web.Mvc4.Common
{
    public class AntiXssEncoder : HttpEncoder
    {
        public AntiXssEncoder()
        {
        }

        protected override void HtmlEncode(string value, TextWriter output)
        {
            output.Write(Encoder.HtmlEncode(value));
        }

        protected override void HtmlAttributeEncode(string value, TextWriter output)
        {
            output.Write(Encoder.HtmlAttributeEncode(value));
        }

        protected override void HtmlDecode(string value, TextWriter output)
        {
            base.HtmlDecode(value, output);
        }

        // Some code omitted but included in the sample
    }
}