using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DS.ClassLib.VarUtils.Extensions.Tuples
{
    public static class TupleExtensions
    {
        private static readonly HashSet<Type> ValueTupleTypes = new HashSet<Type>(new Type[]
        {
        typeof(ValueTuple<>),
        typeof(ValueTuple<,>),
        typeof(ValueTuple<,,>),
        typeof(ValueTuple<,,,>),
        typeof(ValueTuple<,,,,>),
        typeof(ValueTuple<,,,,,>),
        typeof(ValueTuple<,,,,,,>),
        typeof(ValueTuple<,,,,,,,>)
        });

        public static bool IsValueTuple(this object obj) => IsValueTupleType(obj.GetType());
        public static bool IsValueTupleType(this Type type)
        {
            return type.GetTypeInfo().IsGenericType && ValueTupleTypes.Contains(type.GetGenericTypeDefinition());
        }

        public static List<object> GetValueTupleItemObjects(this object tuple) => GetValueTupleItemFields(tuple.GetType()).Select(f => f.GetValue(tuple)).ToList();
        public static List<Type> GetValueTupleItemTypes(this Type tupleType) => GetValueTupleItemFields(tupleType).Select(f => f.FieldType).ToList();
        public static List<FieldInfo> GetValueTupleItemFields(this Type tupleType)
        {
            var items = new List<FieldInfo>();

            FieldInfo field;
            int nth = 1;
            while ((field = tupleType.GetRuntimeField($"Item{nth}")) != null)
            {
                nth++;
                items.Add(field);
            }

            return items;
        }

        /// <summary>
        /// Specifies if <paramref name="tuple"/> is <see cref="ValueTuple"/> and equals to <see langword="null"/>.
        /// </summary>
        /// <param name="tuple"></param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="tuple"/> is <see cref="ValueTuple"/> and has any field equals to <see langword="null"/>.
        /// <para>
        /// Otherwise <see langword="false"/>.
        /// </para>
        /// </returns>
        public static bool IsTupleNull(this object tuple)
        {
            if (!IsValueTuple(tuple)) { return false; }
            var type = tuple.GetType();
            var values = type.GetRuntimeFields().Select(f => f.GetValue(tuple));
            return values.Any(f => f is null || IsTupleNull(f));
        }
    }
}
