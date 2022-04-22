using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DS.MainUtils
{
    public static class PathUtils
    {
        /// <summary>
        /// Check if current directory has write permissions.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>Return true if directory is available for write. Return false if it isn't. </returns>
        private static bool HasWritePermissionOnDir(string directory)
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


        public static string GetPath(string CurDateTime, string DirName = @"%USERPROFILE%\Desktop\Logs\", string FileName = "Log", string FileExt = ".txt")
        {
            string ExpDirName = Environment.ExpandEnvironmentVariables(DirName);

            if (Directory.Exists(ExpDirName) == false)
            {
                Directory.CreateDirectory(ExpDirName);
            }

            return Environment.ExpandEnvironmentVariables(DirName + CurDateTime + "_" + FileName + FileExt);
        }
    }
}
