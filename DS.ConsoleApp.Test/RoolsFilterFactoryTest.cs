using DS.ClassLib.VarUtils.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal static class RoolsFilterFactoryTest
    {
        static Func<(string, string), bool> strRool1 = (f) =>
        {
            Console.WriteLine("Item1: " + f.Item1);
            Console.WriteLine("Item2: " + f.Item2);
            return true;
        };

        static Func<(int, string), bool> strRool2 = (f) =>
        {
            Console.WriteLine("Item11: " + f.Item1);
            Console.WriteLine("Item21: " + f.Item2);
            return true;
        };

        public static Func<(string, string), bool> ToElementsRool(this Func<(int, string), bool> mEPRool)
        {
            bool func((string, string) f)
            {
                var e1 = f.Item1;
                var e2 = f.Item2;

                Int32.TryParse(e1, out int e1Int);

                if (e1 is string mEPCurve && e2 is string famIns)
                {
                    var arg = (e1Int, famIns);
                    return mEPRool.Invoke(arg);
                }

                return true;
            }

            return func;
        }

        public static void Test1()
        {
            var rools = new List<Func<(string, string), bool>>()
            {
                strRool1,
                strRool2.ToElementsRool()
            };

            var factory = new RulesFilterFactory<string, string>(rools);
            var filter = factory.GetFilter();

            string id = "1";
            string id1 = "2";
            string s1 = "MyItem";
            var arg = (id, id1);

            Console.WriteLine(filter.Invoke(arg)); 
        }
    }
}
