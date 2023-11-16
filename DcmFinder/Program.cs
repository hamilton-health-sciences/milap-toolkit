// See https://aka.ms/new-console-template for more information

using DcmFinder;
using FellowOakDicom;

var appArguments = Util.ParseApplicationArguments(args);

if (appArguments.IsInvalid)
{
    Console.WriteLine($"Invalid Argument(s) : {args}");
    return;
}

try
{
    foreach (var file in Directory.EnumerateFiles(appArguments.DicomDirectory))
    {
        if (DicomFile.HasValidHeader(file))
        {
            var dicomFile = DicomFile.Open(file);

            var sopInstanceUid = dicomFile.Dataset.GetString(DicomTag.SOPInstanceUID);

            if (sopInstanceUid == appArguments.SopInstanceUid)
            {
                Console.WriteLine($"Filepath: {file} : SopInstanceUID: {sopInstanceUid}");
                return;
            }
        }
    }

    Console.WriteLine($"Unable to locale file with the following SopInstanceUid: {appArguments.SopInstanceUid}");
}
catch (Exception ex)
{
    Console.WriteLine($"Unable to enumerate directory:  {ex.Message}");
}



