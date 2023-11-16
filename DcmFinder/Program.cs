// See https://aka.ms/new-console-template for more information

using DcmFinder;
using FellowOakDicom;

try
{
    foreach (var file in Directory.EnumerateFiles("c:\\target\\target1"))
    {

        if (DicomFile.HasValidHeader(file))
        {
            var dicomFile = DicomFile.Open(file);

            var sopInstanceUID = dicomFile.Dataset.GetString(DicomTag.SOPInstanceUID);

            Console.WriteLine(sopInstanceUID == "2.25.68458751207555755306905863337134582389" ? $"Filepath: {file} : SopInstanceUID: {sopInstanceUID}" : $"Unable to find file with SopInstanceUID of {sopInstanceUID}");
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Unable to enumerate directory:  {ex.Message}");
}



