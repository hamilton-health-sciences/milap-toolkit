// See https://aka.ms/new-console-template for more information

using DcmFinder;
using DcmFinder.DicomFinder;
using FellowOakDicom;

var appArguments = Util.ParseApplicationArguments(args);

if (appArguments.IsInvalid)
{
    Console.WriteLine($"Invalid Argument(s) : {args}");
    return;
}


var dicomFinder = new DicomFinder();

var fileName = dicomFinder.FindFilenameBySopInstanceUid(appArguments.SopInstanceUid!, appArguments.DicomDirectory!);

Console.WriteLine(fileName == null ? "File name is not found" : $"file name is {fileName}");




