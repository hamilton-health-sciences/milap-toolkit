namespace DcmFinder
{
    public class ApplicationArgument
    {
        public string? DicomDirectory { get; set; }
        public string? SopInstanceUidCsv { get; set; }
        public string? SopInstanceUid { get; set; }
        public bool IsInvalid => string.IsNullOrWhiteSpace(this.DicomDirectory) || (string.IsNullOrWhiteSpace(this.SopInstanceUidCsv) && string.IsNullOrWhiteSpace(this.SopInstanceUid));
    }
}
