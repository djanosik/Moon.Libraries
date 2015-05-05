using System.IO;

namespace Moon.IO.FileSystem
{
    /// <summary>
    /// <see cref="FileInfo" /> extension methods.
    /// </summary>
    public static class FileInfoExtensions
    {
        /// <summary>
        /// Creates a file and optionally a directory.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="createDirectory">Indicates whether to create a directory as well.</param>
        public static FileStream Create(this FileInfo file, bool createDirectory)
        {
            if (createDirectory)
            {
                file.Directory?.Create();
            }

            return file.Create();
        }
    }
}