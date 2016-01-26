using System;
using System.IO;

namespace Moon.IO
{
    /// <summary>
    /// Utility for dealing with file / folder paths.
    /// </summary>
    public static class Pathy
    {
        /// <summary>
        /// Returns random file name without extension.
        /// </summary>
        public static string GetRandomFileName()
        {
            var fileName = Path.GetRandomFileName();
            fileName = fileName.Replace('.', '-').ToLowerInvariant();
            return $"f{fileName}";
        }

        /// <summary>
        /// Returns whether the path is absolute.
        /// </summary>
        /// <param name="path">The path to check.</param>
        public static bool IsAbsolutePath(string path)
        {
            Requires.NotNull(path, nameof(path));

            return new Uri(path, UriKind.RelativeOrAbsolute).IsAbsoluteUri;
        }

        /// <summary>
        /// Returns whether the path is relative.
        /// </summary>
        /// <param name="path">The path to check.</param>
        public static bool IsRelativePath(string path)
        {
            Requires.NotNull(path, nameof(path));

            return !IsAbsolutePath(path);
        }

        /// <summary>
        /// Converts a URL relative to the <paramref name="basePath" /> to absolute URL.
        /// </summary>
        /// <param name="basePath">The absolute base path.</param>
        /// <param name="relativePath">The relative path to convert to absolute.</param>
        public static string MakeAbsolute(string basePath, string relativePath)
        {
            Requires.NotNull(basePath, nameof(basePath));
            Requires.NotNull(relativePath, nameof(relativePath));

            if (IsAbsolutePath(relativePath))
            {
                return relativePath;
            }

            basePath = Path.GetDirectoryName(basePath);

            if (!IsAbsolutePath(basePath))
            {
                throw new InvalidOperationException("The base file path is not absolute.");
            }

            relativePath = RemoveLeadingSlash(relativePath);

            var splittedPath = relativePath.Split('?');
            relativePath = splittedPath[0];

            var absolutePath = Path.Combine(basePath, relativePath);
            absolutePath = new Uri(absolutePath).LocalPath;

            if (splittedPath.Length > 1)
            {
                absolutePath += "?" + splittedPath[1];
            }

            return Normalize(absolutePath);
        }

        /// <summary>
        /// Converts the absolute file path to server-relative file path.
        /// </summary>
        /// <param name="rootPath">The absolute root path.</param>
        /// <param name="filePath">The absolute file path.</param>
        public static string MakeRelativeToRoot(string rootPath, string filePath)
        {
            Requires.NotNull(rootPath, nameof(rootPath));
            Requires.NotNull(filePath, nameof(filePath));

            var splittedPath = filePath.Split('?');
            filePath = splittedPath[0];

            var rootUri = new Uri(rootPath);
            var fileUri = new Uri(filePath);

            if (!rootUri.IsAbsoluteUri)
            {
                throw new Exception("The root path is not absolute.");
            }
            if (!fileUri.IsAbsoluteUri)
            {
                throw new Exception("The file path is not absolute.");
            }

            if (!rootUri.IsBaseOf(fileUri))
            {
                throw new Exception("The root path is not base of the file path.");
            }

            var relativePath = "/" + rootUri.MakeRelativeUri(fileUri);

            if (splittedPath.Length > 1)
            {
                relativePath += "?" + splittedPath[1];
            }

            return Normalize(relativePath);
        }

        /// <summary>
        /// Normalizes the specified file path.
        /// </summary>
        /// <param name="path">The path to normalize.</param>
        public static string Normalize(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return path;
            }

            if (path.StartsWith(@"\??\", StringComparison.Ordinal))
            {
                path = path.Substring(4);
            }

            path = ReplaceBackSlashes(path);
            path = RemoveRedundantSlashes(path);
            return path;
        }

        /// <summary>
        /// Prepends leading slash.
        /// </summary>
        /// <param name="path">The file / folder path.</param>
        public static string PrependLeadingSlash(string path)
        {
            Requires.NotNull(path, nameof(path));

            path = Normalize(path);

            if (path.Length == 0 || path[0] != '/')
            {
                path = "/" + path;
            }

            return path;
        }

        /// <summary>
        /// Appends trailing slash to the given path.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        public static string AppendTrailingSlash(string folderPath)
        {
            Requires.NotNull(folderPath, nameof(folderPath));

            folderPath = Normalize(folderPath);
            var lastIndex = folderPath.Length - 1;

            if (folderPath.Length == 0 || folderPath[lastIndex] != '/')
            {
                folderPath += "/";
            }

            return folderPath;
        }

        /// <summary>
        /// Removes leading slash.
        /// </summary>
        /// <param name="path">The file / folder path.</param>
        public static string RemoveLeadingSlash(string path)
        {
            Requires.NotNull(path, nameof(path));

            path = Normalize(path);

            if (path.StartsWith("/", StringComparison.Ordinal))
            {
                path = path.Substring(1);
            }
            return path;
        }

        /// <summary>
        /// Removes trailing slash.
        /// </summary>
        /// <param name="folderPath">The folder path.</param>
        public static string RemoveTrailingSlash(string folderPath)
        {
            Requires.NotNull(folderPath, nameof(folderPath));

            folderPath = Normalize(folderPath);

            if (string.IsNullOrEmpty(folderPath))
            {
                return folderPath;
            }

            var lastIndex = folderPath.Length - 1;

            if (folderPath[lastIndex] == '/')
            {
                folderPath = folderPath.Substring(0, lastIndex);
            }

            return folderPath;
        }

        static string RemoveRedundantSlashes(string path)
        {
            const string schemeSep = "://";

            Requires.NotNull(path, nameof(path));

            var schemeSepPosition = path.IndexOf(schemeSep, StringComparison.Ordinal);
            var scheme = string.Empty;

            if (schemeSepPosition >= 0)
            {
                scheme = path.Substring(0, schemeSepPosition);
                path = path.Substring(schemeSepPosition + schemeSep.Length);
            }

            while (path.Contains("//"))
            {
                path = path.Replace("//", "/");
            }

            if (schemeSepPosition >= 0)
            {
                path = scheme + schemeSep + path;
            }

            return path;
        }

        static string ReplaceBackSlashes(string path)
        {
            Requires.NotNull(path, nameof(path));

            return path.Replace("\\", "/");
        }
    }
}