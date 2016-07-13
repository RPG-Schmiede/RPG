using System;
using UnityEngine;

namespace UCFW
{
    /// <summary>
    /// A collection of extension methods
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Calls this action only when it's unequal to null 
        /// </summary>
        /// <param name="action">the action</param>
        public static void SafeCall(this Action action)
        {
            if(action != null)
            {
                action();
            }
        }

        /// <summary>
        /// Calls this action only when it's unequal to null 
        /// </summary>
        /// <typeparam name="T">The type of the parameter</typeparam>
        /// <param name="action">the action</param>
        /// <param name="param">the parameter</param>
        public static void SafeCall<T>(this Action<T> action, T param)
        {
            if(action != null)
            {
                action(param);
            }
        }

        /// <summary>
        /// Checks whether a component exists
        /// </summary>
        /// <param name="go">the gameobject</param>
        /// <param name="type">the type of the component</param>
        /// <returns></returns>
        public static bool HasComponent(this GameObject go, Type type)
        {
            return go.GetComponent(type) != null;
        }

        /// <summary>
        /// Checks whether a component exists
        /// </summary>
        /// <param name="go">the gameobject</param>
        /// <param name="type">the type of the component</param>
        /// <param name="component">get the component</param>
        /// <returns></returns>
        public static bool HasComponent(this GameObject go, Type type, out Component component)
        {
            component = go.GetComponent(type);
            return component != null;
        }

        /// <summary>
        /// Checks whether a component exists
        /// </summary>
        /// <typeparam name="T">the type of the component</typeparam>
        /// <param name="go">the gameobject</param>
        /// <returns></returns>
        public static bool HasComponent<T>(this GameObject go)
        {
            return go.GetComponent<T>() != null;
        }

        /// <summary>
        /// Checks whether a component exists
        /// </summary>
        /// <typeparam name="T">the type of the component</typeparam>
        /// <param name="go">the gameobject</param>
        /// <param name="component">get the component</param>
        /// <returns></returns>
        public static bool HasComponent<T>(this GameObject go, out T component)
        {
            component = go.GetComponent<T>();
            return component != null;
        }

        /// <summary>
        /// Checks whether a component exists
        /// </summary>
        /// <param name="bb">the behaviour</param>
        /// <param name="type">the type of the component</param>
        /// <returns></returns>
        public static bool HasComponent(this BetterBehaviour bb, Type type)
        {
            return bb.GetComponent(type) != null;
        }

        /// <summary>
        /// Checks whether a component exists
        /// </summary>
        /// <param name="bb">the behaviour</param>
        /// <param name="type">the type of the component</param>
        /// <param name="component">get the component</param>
        /// <returns></returns>
        public static bool HasComponent(this BetterBehaviour bb, Type type, out Component component)
        {
            component = bb.GetComponent(type);
            return component != null;
        }

        /// <summary>
        /// Checks whether a component exists
        /// </summary>
        /// <typeparam name="T">the type of the component</typeparam>
        /// <param name="bb">the behaviour</param>
        /// <returns></returns>
        public static bool HasComponent<T>(this BetterBehaviour bb)
        {
            return bb.GetComponent<T>() != null;
        }

        /// <summary>
        /// Checks whether a component exists
        /// </summary>
        /// <typeparam name="T">the type of the component</typeparam>
        /// <param name="bb">the behaviour</param>
        /// <param name="component">get the component</param>
        /// <returns></returns>
        public static bool HasComponent<T>(this BetterBehaviour bb, out T component)
        {
            component = bb.GetComponent<T>();
            return component != null;
        }

        /// <summary>
        /// Checks whether this array is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name=""></param>
        /// <returns></returns>
        public static bool IsEmpty(this Array array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// Get a random element from this array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T GetRandom<T>(this T[] array)
        {
            if(array.IsEmpty())
            {
                return default(T);
            }
            else
            {
                return array[UnityEngine.Random.Range(0, array.Length)];
            }
        }

        public static T[] Copy<T>(this T[] array)
        {
            T[] res = new T[array == null ? 0 : array.Length];

            for(int i=0; i<res.Length; ++i)
            {
                res[i] = array[i];
            }

            return res;
        }

        /// <summary>
        /// Get a random value from a vector2, where x is the min and y is the max value
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static float GetRandom(this Vector2 vec)
        {
            return UnityEngine.Random.Range(vec.x, vec.y);
        }

        /// <summary>
        /// Creates a new empty game object and parents it to this transform
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="name">the name of the game object</param>
        /// <returns></returns>
        public static Transform CreateEmptyChild(this Transform transform, string name)
        {
            Transform result = new GameObject(name).transform;
            result.SetParent(transform);

            return result;
        }
    }
}
