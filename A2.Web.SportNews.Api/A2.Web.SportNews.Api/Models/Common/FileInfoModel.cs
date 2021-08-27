namespace A2.Web.SportNews.Api.Models.Common
{
    public enum TransmittedFileFormat
    {
        Unknown,
        Png,
        Jpg
    }

    public class FileInfoModel
    {
        public string FileName { get; set; }
        public TransmittedFileFormat Format { get; set; }
        public string FileB64 { get; set; }
    }
}
