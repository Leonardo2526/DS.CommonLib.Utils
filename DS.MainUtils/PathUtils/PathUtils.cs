using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DS.MainUtils.PathUtils
{
    public class PathBuilder
    {
        public string DirName { get; private set; }
        public string LogName { get; private set; }
        private string LogExt { get;  set; }

        //Get current date and time 
        private static string CurDate = DateTime.Now.ToString("yyMMdd");
        private static string CurDateTime = DateTime.Now.ToString("yyMMdd_HHmmss");


        public PathBuilder(string fileName = "", string dirName = "", DirOption dirOption = DirOption.Desktop, Type type = null)
        {
            DirName = @"%USERPROFILE%\Desktop\";
            LogName = "OutputLog";
            LogExt = ".log";

            UpdateDefaults(fileName, dirName, dirOption, type);
        }


        /// <summary>
        /// Get directory path and file name.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dirName"></param>
        /// <param name="dirOption"></param>
        /// <param name="type"></param>
        /// <returns>Return directory by </returns>
        public string GetPath()
        {          

            string ExpDirName = Environment.ExpandEnvironmentVariables(DirName);

            if (Directory.Exists(ExpDirName) == false)
            {
                Directory.CreateDirectory(ExpDirName);
            }

            if (!HasWritePermissionOnDir(ExpDirName))
            {
                return "";
            }

            string dir = $"{ExpDirName}+{LogName}+{LogExt}";

            return Environment.ExpandEnvironmentVariables(dir);
        }

        #region PrivateMethods


        /// <summary>
        /// Check if current directory has write permissions.
        /// </summary>
        /// <param name="directory"></param>
        /// <returns>Return true if directory is available for write. Return false if it isn't. </returns>
        private bool HasWritePermissionOnDir(string directory)
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

        /// <summary>
        /// Update default names of output directory and log file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dirName"></param>
        /// <param name="dirOption"></param>
        /// <param name="type"></param>
        private void UpdateDefaults(string fileName, string dirName, DirOption dirOption, Type type)
        {

            UpdateFileName(fileName, dirOption);
            UpdateDirName(dirName, dirOption, type);

        }

        private void UpdateFileName(string fileName, DirOption dirOption)
        {

            if (fileName != "")
            {
                LogName = fileName;
            }
            else
            {
                switch (dirOption)
                {
                    case DirOption.CurDateTime:
                        LogName = $"{CurDateTime}_{LogName}";
                        break;
                    case DirOption.CurDate:
                        LogName = $"{CurDate}_{LogName}";
                        break;
                }
            }
        }

        private void UpdateDirName(string dirName, DirOption dirOption, Type type)
        {
            if (dirName != "")
            {
                DirName = dirName;
            }
            else if (type is not null)
            {
                DirName = type.Assembly.Location;
            }
            else
            {
                switch (dirOption)
                {
                    case DirOption.Desktop:
                        break;
                }
            }
        }

        #endregion


        public enum DirOption
        {
            /// <summary>
            /// Save log file to desktop
            /// </summary>
            Desktop,
            /// <summary>
            /// Add to log name current date
            /// </summary>
            CurDate,
            /// <summary>
            /// Add to log name current date and time
            /// </summary>
            CurDateTime, 
        }
    }
}
