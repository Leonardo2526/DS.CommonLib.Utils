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
        private static string DefaultDirName = @"%USERPROFILE%\Desktop\";
        private static string DefaultLogName = "OutputLog";
        private static string DefaultLogExt = ".log";


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


        //public static string GetPath(string CurDateTime, string DirName = @"%USERPROFILE%\Desktop\Logs\", string FileName = "Log", string FileExt = ".txt")
        public static string GetPath(PathStringType pathStringType, Type type = null)
        {
            CheckValues(pathStringType, type);

            string ExpDirName = Environment.ExpandEnvironmentVariables(DefaultDirName);

            if (Directory.Exists(ExpDirName) == false)
            {
                Directory.CreateDirectory(ExpDirName);
            }

            if (!HasWritePermissionOnDir(ExpDirName))
            {
                return "";
            }

            return Environment.ExpandEnvironmentVariables(DefaultDirName + CurDateTime + "_" + FileName + FileExt);
        }

        private static void CheckValues(PathStringType pathStringType, Type type)
        {
            if (type is not null)
            {
                DefaultLogName = type.Namespace;
            }

            switch (pathStringType)
            {
                case PathStringType.Default:                    
                        break;
                default:
                    break;
            }
        }


        public enum PathStringType
        {
            Default, 
            CurDateTime, 
            Custom
        }
    }
}
