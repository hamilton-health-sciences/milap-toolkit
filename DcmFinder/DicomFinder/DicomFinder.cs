using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using CsvHelper;
using DcmFinder.Models;
using FellowOakDicom;

namespace DcmFinder.DicomFinder
{
    public class DicomFinder : IDicomFinder
    {
        public string FindFilenameBySopInstanceUid(string sopUid, string directory)
        {
            var filePath = "";

            try
            {
                var fileList = Directory.GetFiles(directory, "*.dcm", SearchOption.AllDirectories).ToList();

                foreach (var file in fileList)
                {
                    if (!DicomFile.HasValidHeader(file)) continue;

                    var dicomFile = DicomFile.Open(file);

                    var sopInstanceUid = dicomFile.Dataset.GetString(DicomTag.SOPInstanceUID);

                    if (sopInstanceUid == sopUid.Trim())
                    {
                        filePath = file;
                        Console.WriteLine($"Filepath: {file} : SopInstanceUID: {sopInstanceUid}");
                        break;
                    }
                }
                
                // open the file automatically
                
                // Process.Start(new ProcessStartInfo { FileName = fileName, UseShellExecute = true });

                return Path.GetFullPath(filePath);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to enumerate directory:  {ex.Message}");
                throw new NotImplementedException();
            }

           
        }

        public void WriteFilenameBySopInstanceUid(string pathToCsv, string directory)
        {
            List<FilePathCsv> records;

            using (var reader = new StreamReader(pathToCsv))
            using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Context.RegisterClassMap<FilePathCsvMap>();

                records = csvReader.GetRecords<FilePathCsv>().ToList();

                Console.WriteLine($"Total records : {records?.Count()}");

                foreach (var record in records)
                {
                    var filePath = this.FindFilenameBySopInstanceUid(record.SopInstanceUid, directory) ?? string.Empty;

                    record.FilePath = filePath;
                }
            }

            using (var writer = new StreamWriter(pathToCsv))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.Context.RegisterClassMap<FilePathCsvMap>();

                Console.WriteLine("Initiating population of csv");

                csvWriter.WriteHeader<FilePathCsv>();
                csvWriter.NextRecord();
                csvWriter.WriteRecords(records);
            }

            Console.WriteLine("csv population complete");
        }
    }

}
