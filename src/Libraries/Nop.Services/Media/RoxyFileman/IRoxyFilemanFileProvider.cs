using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace Nop.Services.Media.RoxyFileman
{
    public interface IRoxyFilemanFileProvider : IFileProvider
    {
        /// <summary>
        /// Create configuration file for RoxyFileman
        /// </summary>
        /// <param name="pathBase"></param>
        /// <param name="lang"></param>
        /// <returns>A task that represents the asynchronous operation</returns>
        Task<RoxyFilemanConfig> GetOrCreateConfigurationAsync(string pathBase, string lang);

        /// <summary>
        /// Copy the directory with the embedded files and directories
        /// </summary>
        /// <param name="sourcePath">Path to the source directory</param>
        /// <param name="destinationPath">Path to the destination directory</param>
        void CopyDirectory(string sourcePath, string destinationPath);

        IEnumerable<RoxyDirectoryInfo> GetDirectories(string type, bool isRecursive = true, string rootDirectoryPath = "");

        /// <summary>
        /// Get files in the passed directory
        /// </summary>
        /// <param name="directoryPath">Path to the files directory</param>
        /// <param name="type">Type of the files</param>
        /// <returns>
        /// The list of <see cref="RoxyImageInfo"/>
        /// </returns>
        IEnumerable<RoxyImageInfo> GetFiles(string directoryPath = "", string type = "");

        /// <summary>
        /// Moves a file or a directory and its contents to a new location
        /// </summary>
        /// <param name="sourceDirName">The path of the file or directory to move</param>
        /// <param name="destDirName">
        /// The path to the new location for sourceDirName. If sourceDirName is a file, then destDirName
        /// must also be a file name
        /// </param>
        void DirectoryMove(string sourceDirName, string destDirName);

        /// <summary>
        /// Moves a specified file to a new location, providing the option to specify a new file name
        /// </summary>
        /// <param name="sourcePath">The name of the file to move. Can include a relative or absolute path</param>
        /// <param name="destinationPath">The new path and name for the file</param>
        void FileMove(string sourcePath, string destinationPath);

        /// <summary>
        /// Rename the directory
        /// </summary>
        /// <param name="sourcePath">Path to the source directory</param>
        /// <param name="newName">New name of the directory</param>
        void RenameDirectory(string sourcePath, string newName);

        /// <summary>
        /// Rename the file
        /// </summary>
        /// <param name="sourcePath">Path to the source file</param>
        /// <param name="newName">New name of the file</param>
        void RenameFile(string sourcePath, string newName);

        /// <summary>
        /// Delete the file
        /// </summary>
        /// <param name="path">Path to the file</param>
        void DeleteFile(string path);

        /// <summary>
        /// Copy the file
        /// </summary>
        /// <param name="sourcePath">Path to the source file</param>
        /// <param name="destinationPath">Path to the destination file</param>
        void CopyFile(string sourcePath, string destinationPath);

        Task SaveFileAsync(string destinationPath, string fileName, string contentType, Stream fileStream);

        /// <summary>
        /// Create the new directory
        /// </summary>
        /// <param name="parentDirectoryPath">Path to the parent directory</param>
        /// <param name="name">Name of the new directory</param>
        void CreateDirectory(string parentDirectoryPath, string name);

        /// <summary>
        /// Delete the directory
        /// </summary>
        /// <param name="path">Path to the directory</param>
        void DeleteDirectory(string path);

        byte[] CreateImageThumbnail(string sourcePath, string contentType);

        byte[] CreateZipArchiveFromDirectory(string path);
    }
}