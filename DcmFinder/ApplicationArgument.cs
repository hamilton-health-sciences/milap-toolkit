namespace DcmFinder
{
    public class ApplicationArgument
    {
        public ApplicationArgument()
        {
            this.SopInstanceUid = new List<string>();
        }
        public string? Directory { get; set; }
        public List<string> SopInstanceUid { get; set; }
        public bool IsValid => this.Directory != null && !this.SopInstanceUid.Any();
    }
}
