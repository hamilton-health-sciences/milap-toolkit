using CsvHelper.Configuration;

namespace DcmFinder.Models
{
    public class FilePathCsvMap : ClassMap<FilePathCsv>
    {
        public FilePathCsvMap()
        {
            this.Map(m => m.SopInstanceUid).Index(0).Name(nameof(FilePathCsv.SopInstanceUid));
            this.Map(m => m.FilePath).Index(1).Name(nameof(FilePathCsv.FilePath));
        }
    }
}
