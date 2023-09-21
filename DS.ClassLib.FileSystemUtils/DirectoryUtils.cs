using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.FileSystemUtils
{
    public static class DirectoryUtils
    {
        /// <summary>
        /// Check if directory exist and has write permissions.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns>Return true if directory exist and has write permissions. Return false if it isn't.</returns>
        public static bool IsDirExistAndWritable(string fullPath)
        {
            string ExpDirName = Environment.ExpandEnvironmentVariables(fullPath);

            if (Directory.Exists(ExpDirName))
            {
                if (HasWritePermissionOnDir(ExpDirName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if current directory has write permissions.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>Return true if directory is available for write. Return false if it isn't. </returns>
        public static bool HasWritePermissionOnDir(string directory)
        {
            //Check directory permissions
            var writeAllow = false;
            var writeDeny = false;
            var accessControlList = Directory.GetAccessControl(directory);
            if (accessControlList == null)
                return false;
            var accessRules = accessControlList.GetAccessRules(true, true, typeof(System.Security.Principal.SecurityIdentifier));
            if (accessRules == null)
                return false;

            //Check rules
            foreach (FileSystemAccessRule rule in accessRules)
            {
                if (rule.FileSystemRights == FileSystemRights.FullControl 
                    || rule.FileSystemRights == FileSystemRights.Write 
                    || rule.FileSystemRights == FileSystemRights.Modify)
                { writeAllow = rule.AccessControlType == AccessControlType.Allow; }
                if(writeAllow) { return true; }
            }
            return false;
        }

        /// <summary>
        /// Delete all files and folders in input directory
        /// </summary>
        /// <param name="dirPath"></param>
        public static void ClearFolder(string dirPath)
        {
            DirectoryInfo di = new DirectoryInfo(dirPath);

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }
    }
}
