using DS.ClassLib.VarUtils.Iterators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.ConsoleApp.Test
{
    internal static class ActionTwoWayEnumeratorTest
    {
        public static void StringIterator()
        {
            var items = GetStringItems();

            Predicate<string> nextAction = (string s) =>
            {
                Debug.WriteLine("nextAction: " + s);
                return true;
            };

            Predicate<string> backAction = (string s) =>
            {
                Debug.WriteLine("backAction: " + s);
                return true;
            };

            Predicate<string> previousAction = (string s) =>
            {
                Debug.WriteLine("\npreviousAction: " + s);
                return true;
            };

            var iterator = new ActionTwoWayEnumerator<string>(items)
            {
                NextAction = nextAction, 
                BackAction = backAction,
                PreviousAction = previousAction
            };

            //iterate to end
            int i = 0;
            while (iterator.MoveNext())
            {
                Console.WriteLine($"{i}: {iterator.Current}");
                i++;
            }

            Console.WriteLine("MoveBack:");
            //iterate to start
            while (iterator.MoveBack())
            {
                Console.WriteLine($"{i}: {iterator.Current}");
                i++;
            }
        }

        private static IEnumerable<string> GetStringItems()
         => new List<string>()
            {
                "a" ,"b", "c","d","e","f","g"
            };

    }
}
