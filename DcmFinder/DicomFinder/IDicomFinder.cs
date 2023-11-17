namespace DcmFinder.DicomFinder
{
    public interface IDicomFinder
    {
        string FindFilenameBySopInstanceUid(string sopInstanceUid, string directory);
        void WriteFilenameBySopInstanceUid(string pathToCsv, string directory);
    }
}