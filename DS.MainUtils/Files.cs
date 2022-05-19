using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;

namespace DS.MainUtils
{
    public static class Files
    {
        /// <summary>
        /// Check if file is empty;
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns>Return true if file by fullPath is empty. Return null if it isn't.</returns>
        public static bool IsFileEmpty(string fullPath)
        {
            long fileLength = new FileInfo(fullPath).Length;

            if (fileLength == 0 || (fileLength == 3 && File.ReadAllBytes(fullPath).SequenceEqual(new byte[] { 239, 187, 191 })))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Clear txt file by path.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns>Return true if clerance was successful. Return false if it wasn't.</returns>
        public static bool ClearFile(string fullPath)
        {
            if (IsDirExistAndWritable(fullPath))
            {
                System.IO.File.WriteAllText(fullPath, string.Empty);
                return true;
            }

            return false;
        }


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
                if ((FileSystemRights.Write & rule.FileSystemRights) != FileSystemRights.Write) continue;

                if (rule.AccessControlType == AccessControlType.Allow)
                    writeAllow = true;
                else if (rule.AccessControlType == AccessControlType.Deny)
                    writeDeny = true;
            }
            return writeAllow && !writeDeny;
        }
    }

}
