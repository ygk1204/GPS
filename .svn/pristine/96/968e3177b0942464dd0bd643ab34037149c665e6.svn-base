using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace jQuery.Treeview
{
    public static class FileSystemInfosRepository
    {
        #region Fields
        private static List<string> _nodeIdMappings;
        private const string _fileSystemInfoRoot = @"D:\GPS_FolderList\";
        #endregion

        #region Constructor
        static FileSystemInfosRepository()
        {
            _nodeIdMappings = new List<string>();
        }
        #endregion

        #region Methods
        public static int? GetNodeId(FileSystemInfo item)
        {
            if (item.FullName == _fileSystemInfoRoot)
                return null;
            else if (_nodeIdMappings.Contains(item.FullName))
                return _nodeIdMappings.IndexOf(item.FullName);
            else
            {
                _nodeIdMappings.Add(item.FullName);
                return (_nodeIdMappings.Count - 1);
            }
        }

        public static DirectoryInfo GetDirectoryInfo(int? nodeId)
        {
            if (nodeId.HasValue)
            {
                if ((_nodeIdMappings.Count > nodeId.Value) && Directory.Exists(_nodeIdMappings[nodeId.Value]))
                    return new DirectoryInfo(_nodeIdMappings[nodeId.Value]);
                else
                    return null;
            }
            else
                return new DirectoryInfo(_fileSystemInfoRoot);
        }

        public static IEnumerable<FileSystemInfo> GetFileSystemInfos(int? rootNodeId)
        {
            DirectoryInfo rootDirectoryInfo = GetDirectoryInfo(rootNodeId);

            if (rootDirectoryInfo != null)
                return from childFileSystemInfo in rootDirectoryInfo.GetFileSystemInfos()
                       orderby childFileSystemInfo is DirectoryInfo descending
                       select childFileSystemInfo;
            else
                return null;
        }
        #endregion
    }
}
