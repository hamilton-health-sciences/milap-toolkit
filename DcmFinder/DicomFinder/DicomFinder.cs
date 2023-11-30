using System.Diagnostics;
using System.Reflection;
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
                    //var modality = dicomFile.Dataset.GetString(DicomTag.Modality);

                    if (sopInstanceUid == sopUid)
                    {
                        filePath = file;
                        //Console.WriteLine($"Modality: {modality}");
                        Console.WriteLine($"Filepath: {file} : SopInstanceUID: {sopInstanceUid}");
                        break;
                    }
                }
                
                // open the file automatically
                
                // Process.Start(new ProcessStartInfo { FileName = fileName, UseShellExecute = true });

                return Path.GetFileName(filePath);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to enumerate directory:  {ex.Message}");
                throw new NotImplementedException();
            }

           
        }

        public void WriteFilenameBySopInstanceUid(string pathToCsv, string directory)
        {
            throw new NotImplementedException();
        }
    }

}
