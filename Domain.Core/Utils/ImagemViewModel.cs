namespace Autoglass.Domain.Core.Utils
{
    public class ImagemViewModel
    {
        public byte[] RawData { get; set; }
        public string MimeType { get; set; }

        public ImagemViewModel(byte[] rawData, string mimeType)
        {
            RawData = rawData;
            MimeType = mimeType;
        }
    }
}
