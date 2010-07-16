namespace nu.core.FileSystem
{
    public class DotNetCurrentWorkingDirectory :
        DotNetDirectory,
        CurrentWorkingDirectory
    {
        public DotNetCurrentWorkingDirectory(IFileSystem fileSystem)
            : base(fileSystem.GetCurrentDirectory().Name)
        {
        }
    }
}