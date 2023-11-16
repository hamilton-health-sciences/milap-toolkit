using CsvHelper.Configuration;

namespace orthancsv.Models
{
    public class StudyCsvMap : ClassMap<StudyCsv>
    {
        public StudyCsvMap()
        {
            this.Map(m => m.OrthancStudyId).Index(0).Name("OrthancStudyId");
            this.Map(m => m.AccessionNumber).Index(1).Name("Accession#");
        }
    }
}
