using FellowOakDicom;

namespace DcmFinder.DicomFinder
{
    public class DicomFinder : IDicomFinder
    {
        public string FindFilenameBySopInstanceUid(string sopUid, string directory)
        {
            var fileName = "";

            try
            {
                foreach (var file in Directory.EnumerateFiles(directory))
                {
                    if (DicomFile.HasValidHeader(file))
                    {
                        var dicomFile = DicomFile.Open(file);

                        var sopInstanceUid = dicomFile.Dataset.GetString(DicomTag.SOPInstanceUID);

                        if (sopInstanceUid == sopUid)
                        {
                            fileName = file;
                            Console.WriteLine($"Filepath: {file} : SopInstanceUID: {sopInstanceUid}");
                            break;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to enumerate directory:  {ex.Message}");
                throw new NotImplementedException();
            }

            return fileName.Remove(0,directory.Length + 1);
        }

        public void WriteFilenameBySopInstanceUid(string pathToCsv, string directory)
        {
            throw new NotImplementedException();
        }
    }

}
