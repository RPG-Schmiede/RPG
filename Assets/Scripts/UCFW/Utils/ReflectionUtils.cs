using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace UCFW
{
    /// <summary>
    /// A collection of utilities used for reflection
    /// </summary>
    public static class ReflectionUtils
    {
        /// <summary>
        /// Searches for all types that derive from one type
        /// </summary>
        /// <typeparam name="T">the base type</typeparam>
        /// <param name="assemblies">the assemblies that are searched</param>
        /// <returns></returns>
        public static Type[] SearchForDerivedTypesFromAssemblies<T>(Func<Type, bool> predicate, params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0) return new Type[] { };

            List<Type> types = new List<Type>();

            for (int i = 0; i < assemblies.Length; ++i)
            {
                types.AddRange(assemblies[i].GetTypes().Where((t) => typeof(T).IsAssignableFrom(t) && t != typeof(T) && predicate(t)));
            }

            return types.ToArray();
        }

        public static Type[] SearchForDerivedTypesFromAssemblies<T>(params Assembly[] assemblies)
        {
            return SearchForDerivedTypesFromAssemblies<T>((t) => true, assemblies);
        }

        public static Type[] SearchForDerivedTypes<T>()
        {
            return SearchForDerivedTypes<T>((t) => true);
        }

        /// <summary>
        /// Search for all types that derive from one type (in all assemblies)
        /// </summary>
        /// <typeparam name="T">the base type</typeparam>
        /// <returns></returns>
        public static Type[] SearchForDerivedTypes<T>(Func<Type, bool> predicate)
        {
            return SearchForDerivedTypesFromAssemblies<T>(predicate, AppDomain.CurrentDomain.GetAssemblies());
        }
    }
}
