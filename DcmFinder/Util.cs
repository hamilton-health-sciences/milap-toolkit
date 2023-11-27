namespace DcmFinder;

/// <summary>
/// The Util class
/// </summary>
public static class Util
{
    public static ApplicationArgument ParseApplicationArguments(string[] args)
    {
        var applicationArgument = new ApplicationArgument();

        if (args.Length != 2)
        {
            return applicationArgument;
        }

        applicationArgument.DicomDirectory = args.Where(arg => arg.ToLowerInvariant().StartsWith(ApplicationConstant.DcmDir)).Select(arg => arg.Split("=")[1]).FirstOrDefault();
        applicationArgument.SopInstanceUid = args.Where(arg => arg.ToLowerInvariant().StartsWith(ApplicationConstant.SopUid)).Select(arg => arg.Split("=")[1]).FirstOrDefault();
        applicationArgument.SopInstanceUidCsv = args.Where(arg => arg.ToLowerInvariant().StartsWith(ApplicationConstant.SopUidCsv)).Select(arg => arg.Split("=")[1]).FirstOrDefault();

        return applicationArgument;
    }
}
