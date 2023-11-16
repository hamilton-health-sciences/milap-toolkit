namespace DcmFinder;

public static class Util
{
    public static ApplicationArgument ParseApplicationArguments(string[] args)
    {
        var applicationArgument = new ApplicationArgument();
        
        Console.WriteLine(args);

        if (args.Length != 2)
        {
            Console.WriteLine("Invalid number of arguments");
        } 

        return applicationArgument;
    }
}