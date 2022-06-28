

namespace DS.ClassLib.FileSystemUtils
{
    public static class FileUtils
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
            if (DirectoryUtils.IsDirExistAndWritable(fullPath))
            {
                System.IO.File.WriteAllText(fullPath, string.Empty);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Copy all the files & Replaces any files with the same name
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <returns>Returns true if one of files have been copied. Return false if nothing have been copied.</returns>
        public static bool CopyFiles(string sourcePath, string targetPath)
        {
            bool copied = false;
            string[] allFileNames = Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories);

            foreach (string sourceFileName in allFileNames)
            {
                string destFileName = sourceFileName.Replace(sourcePath, targetPath);
                File.Copy(sourceFileName, destFileName, true);
                copied = true;
            }

            return copied;
        }
    }
}