using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AngularJSDemo.Libs
{
    public class UtilityLibrary
    {
        public static string WordWrap(string text, int width)
        {
            int pos, next;
            StringBuilder sb = new StringBuilder();

            // Lucidity check
            if (width < 1)
                return text;

            // Parse each line of text
            for (pos = 0; pos < text.Length; pos = next)
            {
                // Find end of line
                int eol = text.IndexOf(Environment.NewLine, pos);
                if (eol == -1)
                    next = eol = text.Length;
                else
                    next = eol + Environment.NewLine.Length;

                // Copy this line of text, breaking into smaller lines as needed
                if (eol > pos)
                {
                    do
                    {
                        int len = eol - pos;
                        if (len > width)
                            len = BreakLine(text, pos, width);
                        sb.Append(text, pos, len);
                        sb.Append(Environment.NewLine);

                        // Trim whitespace following break
                        pos += len;
                        while (pos < eol && Char.IsWhiteSpace(text[pos]))
                            pos++;
                    } while (eol > pos);
                }
                else sb.Append(Environment.NewLine); // Empty line
            }
            return sb.ToString();
        }

        private static int BreakLine(string text, int pos, int max)
        {
            
            int i = max;
            while (i >= 0 && !Char.IsWhiteSpace(text[pos + i]))
                i--;

            if (i < 0)
                return max;

            while (i >= 0 && Char.IsWhiteSpace(text[pos + i]))
                i--;

            return i + 1;
        }

        public static string GetBaseURL()
        {
            string baseUrl = string.Empty;

            var request = HttpContext.Current.Request;
            var appUrl = HttpRuntime.AppDomainAppVirtualPath;

            if (string.IsNullOrWhiteSpace(appUrl)) appUrl += "/";

            baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);
            int lastIndex = baseUrl.LastIndexOf("/");
            if (lastIndex != (baseUrl.Length - 1))
            {
                baseUrl += "/";
            }

            return baseUrl;
        }
    }
}