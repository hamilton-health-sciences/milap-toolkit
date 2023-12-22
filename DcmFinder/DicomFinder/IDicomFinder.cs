namespace DcmFinder.DicomFinder
{
    public interface IDicomFinder
    {
        string FindFilenameBySopInstanceUid(string sopInstanceUid, string directory);
        void WriteFilePathBySopInstanceUid(string pathToCsv, string directory);
    }
}