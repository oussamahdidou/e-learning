using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.helpers
{
    public class CloudinaryUrlHelper
    {
        public static string ExtractFileName(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentException("URL cannot be null or empty.", nameof(url));
            }
            var uri = new Uri(url);
            var path = uri.AbsolutePath;
            var segments = path.Split('/');
            return segments.Length > 0 ? segments[^1] : string.Empty;
        }
    }
}