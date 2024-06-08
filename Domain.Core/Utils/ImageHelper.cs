using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Autoglass.Domain.Core.Utils
{
    public static class ImageHelper
    {
        private static readonly Regex DataUriPattern = new Regex(@"^data\:(?<type>image\/(png|tiff|jpg|jpeg|gif));base64,(?<data>[A-Z0-9\+\/\=]+)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
        private static readonly Regex DataUriPatternPdf = new Regex(@"^data\:(?<type>application\/(png|tiff|jpg|jpeg|gif|pdf));base64,(?<data>[A-Z0-9\+\/\=]+)$", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.IgnoreCase);
        public static ImagemViewModel ConvertBase64ToImage(string image)
        {
            var isPdf = false;
            if (string.IsNullOrWhiteSpace(image)) return null;

            if (image.Contains("/pdf"))
                isPdf = true;

            Match match = isPdf ? DataUriPatternPdf.Match(image) : DataUriPattern.Match(image);
            if (!match.Success) return null;

            string mimeType = isPdf ? match.Groups["type"].Value.Replace("application/", "") : match.Groups["type"].Value.Replace("image/", "");
            string base64Data = match.Groups["data"].Value;

            try
            {
                byte[] rawData = Convert.FromBase64String(base64Data);
                return rawData.Length == 0 ? null : new ImagemViewModel(rawData, mimeType);
            }
            catch (FormatException)
            {
                return null;
            }
        }
    }
}
