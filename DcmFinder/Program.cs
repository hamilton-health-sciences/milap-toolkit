// See https://aka.ms/new-console-template for more information

using DcmFinder;
using DcmFinder.DicomFinder;

var appArguments = Util.ParseApplicationArguments(args);

if (appArguments.IsInvalid)
{
    Console.WriteLine($"Invalid Argument(s) : {args}");
    return;
}

var dicomFinder = new DicomFinder();

if (appArguments.SopInstanceUidCsv != null)
{
    if (!File.Exists(appArguments.SopInstanceUidCsv))
    {
        Console.WriteLine($"File not found : {appArguments.SopInstanceUidCsv}");
        return;
    }

    dicomFinder.WriteFilePathBySopInstanceUid("C:\\csv\\sop.csv", "C:\\ctp2\\CTP\\roots\\FileStorageService");
    Console.WriteLine("csv population complete");
}
else
{
    var filePath = dicomFinder.FindFilenameBySopInstanceUid(appArguments.SopInstanceUid!, appArguments.DicomDirectory!);
    Console.WriteLine(filePath == null ? "File not found" : $"path to file is: {filePath}");
}











