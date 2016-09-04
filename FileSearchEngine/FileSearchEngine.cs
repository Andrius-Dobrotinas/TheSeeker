using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TheSeeker.FileSystem
{
    /// <summary>
    /// Searches file system for files and folders
    /// </summary>
    public class FileSearchEngine : SearchEngineBase<FileSystemInfo>
    {
        public override void Search(string location, string searchPattern, CancellationToken cancellationToken)
        {
            DirectoryInfo root = new DirectoryInfo(location);
            if (!root.Exists)
                return;

            foreach (var directory in new[] { root }.Union(root.EnumerateDirectories(searchPattern, SearchOption.AllDirectories)).AsParallel())
            {
                try
                {
                    FindFiles(directory, searchPattern, cancellationToken);
                }
                catch (UnauthorizedAccessException e)
                {
                    OnSearchException(e);
                }
            };
        }

        protected void FindFiles(DirectoryInfo directory, string searchPattern, CancellationToken cancellationToken)
        {
            foreach (var file in directory.EnumerateFiles(searchPattern, SearchOption.AllDirectories))
            {
                try
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }
                    OnItemFound(file);
                }
                catch (UnauthorizedAccessException e)
                {
                    OnSearchException(e);
                }
            }
        }
    }
}
