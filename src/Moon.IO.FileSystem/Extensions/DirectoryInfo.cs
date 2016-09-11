using System.IO;

namespace Moon.IO
{
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Copies an existing directory to a new destination, disallowing the overwriting of
        /// existing files.
        /// </summary>
        /// <param name="source">The directory to be copied.</param>
        /// <param name="destDirName">
        /// The name and path to which to copy this directory. The destination cannot be another
        /// disk volume.
        /// </param>
        public static void CopyTo(this DirectoryInfo source, string destDirName)
            => source.CopyTo(destDirName, true);

        /// <summary>
        /// Copies an existing directory to a new destination, allowing the overwriting of existing files.
        /// </summary>
        /// <param name="source">The directory to be copied.</param>
        /// <param name="destDirName">
        /// The name and path to which to copy this directory. The destination cannot be another
        /// disk volume.
        /// </param>
        /// <param name="overwriteFiles">
        /// <c>true</c> to allow an existing file to be overwritten; otherwise, <c>false</c>.
        /// </param>
        public static void CopyTo(this DirectoryInfo source, string destDirName, bool overwriteFiles)
        {
            Requires.NotNullOrWhiteSpace(destDirName, nameof(destDirName));

            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            foreach (var file in Directory.GetFiles(source.FullName))
            {
                File.Copy(file, Path.Combine(destDirName, Path.GetFileName(file)), overwriteFiles);
            }

            foreach (var directory in source.GetDirectories())
            {
                CopyTo(directory, destDirName, overwriteFiles);
            }
        }

        /// <summary>
        /// Deletes the folder if it exists.
        /// </summary>
        /// <param name="directory">The <see cref="DirectoryInfo" /> instance.</param>
        /// <param name="recursive">
        /// true to delete this directory, its subdirectories, and all files; otherwise, false.
        /// </param>
        public static void DeleteIfExists(this DirectoryInfo directory, bool recursive = false)
        {
            if (directory.Exists)
            {
                directory.Delete(recursive);
            }
        }

        /// <summary>
        /// Returns a subdirectory on the specified path.
        /// </summary>
        /// <param name="directory">The <see cref="DirectoryInfo" /> instance.</param>
        /// <param name="path">The subdirectory path.</param>
        public static DirectoryInfo Subdirectory(this DirectoryInfo directory, string path)
        {
            Requires.NotNullOrWhiteSpace(path, nameof(path));

            var basePath = Pathy.AppendTrailingSlash(directory.FullName);
            return new DirectoryInfo(Pathy.MakeAbsolute(basePath, path));
        }
    }
}