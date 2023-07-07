using System;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using DS.ClassLib.FileSystemUtils;

namespace DS.ClassLib.FileSystemUtils
{
    public class DirPathBuilder
    {
        public DirPathBuilder(string fileName = "", string dirName = "",
            DirOption dirOption = DirOption.Default, LogNameOption logNameOption = LogNameOption.Default)
        {
            LogName = "OutputLog";
            LogExt = ".log";

            UpdateDefaults(dirOption, logNameOption, fileName, dirName);
        }

        public string DirName { get; private set; }
        public string LogName { get; private set; }
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        #region Fields

        private readonly string LogExt;

        //Get current date and time 
        private static readonly string CurDate = DateTime.Now.ToString("yyMMdd");
        private static readonly string CurDateTime = DateTime.Now.ToString("yyMMdd_HHmmss");

        #endregion


        /// <summary>
        /// Get directory path and file name. If current directory name is absent it'll be created. 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dirName"></param>
        /// <param name="dirOption"></param>
        /// <param name="type"></param>
        /// <returns>Return directory name.</returns>
        public string GetPath()
        {

            string ExpDirName = Environment.ExpandEnvironmentVariables(DirName);

            if (Directory.Exists(ExpDirName))
            {
                if (!DirectoryUtils.HasWritePermissionOnDir(ExpDirName))
                {
                    throw new InvalidOperationException($"There is no permission to write into this directory: {DirName}");
                }
            }
            else
            {
                Directory.CreateDirectory(ExpDirName);
            }

            string dir = ExpDirName + "\\" + LogName + LogExt;

            return Environment.ExpandEnvironmentVariables(dir);
        }


        #region PrivateMethods


        /// <summary>
        /// Update default names of output directory and log file.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dirName"></param>
        /// <param name="dirOption"></param>
        /// <param name="type"></param>
        private void UpdateDefaults(DirOption dirOption, LogNameOption logNameOption, string fileName, string dirName)
        {

            UpdateFileName(fileName, logNameOption);
            UpdateDirName(dirName, dirOption);

        }

        private void UpdateFileName(string fileName, LogNameOption logNameOption)
        {

            if (fileName != "")
            {
                LogName = fileName;
            }

            switch (logNameOption)
            {
                case LogNameOption.Default:
                    break;
                case LogNameOption.CurDateTime:
                    LogName = $"{CurDateTime}_{LogName}";
                    break;
                case LogNameOption.CurDate:
                    LogName = $"{CurDate}_{LogName}";
                    break;
            }

        }

        private void UpdateDirName(string dirName, DirOption dirOption)
        {
            if (dirName != "")
            {
                DirName = dirName;
            }
            else
            {
                switch (dirOption)
                {
                    case DirOption.Default:
                        DirName = AssemblyDirectory;
                        break;
                    case DirOption.Desktop:
                        DirName = @"%USERPROFILE%\Desktop\";
                        break;
                }
            }
        }



        #endregion


        public enum DirOption
        {
            /// <summary>
            /// Save log file to application folder
            /// </summary>
            Default,
            /// <summary>
            /// Save log file to desktop
            /// </summary>
            Desktop,
        }

        public enum LogNameOption
        {
            /// <summary>
            /// No option
            /// </summary>
            Default,
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
