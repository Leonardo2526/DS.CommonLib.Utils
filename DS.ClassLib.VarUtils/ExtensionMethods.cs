using DS.ClassLib.VarUtils.Extensions.Tuples;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils
{
    public static class ExtensionMethods
    {
        //Need class to be [Serializable]
        public static T DeepCopy<T>(this T self)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }

        /// <summary>
        /// Get <see cref="Assembly"/> file version.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns>
        /// Returns <see cref="Assembly"/> file version. 
        /// If <paramref name="assembly"/> is <see langword="null"/> retuns null.
        /// </returns>
        public static string Version (this Assembly assembly)
        {
            if (assembly == null) { return null; }
            return FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
        }

        /// <summary>
        /// Get <see cref="Assembly"/> file build <see cref="DateTime"/>.
        /// </summary>
        /// <param name="assembly"></param>
        /// <returns>
        /// Returns build <see cref="DateTime"/>.
        /// If <paramref name="assembly"/> is <see langword="null"/> retuns <see cref="DateTime.MinValue"/>.
        /// </returns>
        public static DateTime BuildTime(this Assembly assembly)
        {
            if (assembly == null) { return DateTime.MinValue; }
            return File.GetLastWriteTime(assembly.Location);
        }

        /// <summary>
        /// Check for <see langword="null"/> of <paramref name="item"/>.
        /// </summary>
        /// <param name="item"></param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="item"/> is equal to <see langword="null"/> or default.
        /// <para>
        /// <see langword="true"/> if <paramref name="item"/> is <see cref="ValueTuple"/> and one of it's fields is <see langword="null"/>.
        /// </para>
        /// <para>
        /// 
        /// Otherwise <see langword="false"/></para>
        /// </returns>
        public static bool IsNullOrDefaultOrTuple(this object item)
            => item is null || item.Equals(default) || item.IsTupleNull();
    }
}
