using System;
using System.Collections.Generic;

namespace UCFW
{
    /// <summary>
    /// A generic Eventsystem
    /// </summary>
    public static class Eventsystem
    {
        private static Dictionary<int, Dictionary<Type, object>> _idHandlers = new Dictionary<int, Dictionary<Type, object>>();

        /// <summary>
        /// Subscribe for an event
        /// </summary>
        /// <typeparam name="T">The type of the event package</typeparam>
        /// <param name="id">The id of the event</param>
        /// <param name="handler">The event handler</param>
        public static void Subscribe<T>(int id, Action<T> handler)
        {
            Dictionary<Type, object> dict;
            if(_idHandlers.TryGetValue(id, out dict))
            {
                object handlerObj;
                if (dict.TryGetValue(typeof(T), out handlerObj))
                {
                    dict[typeof(T)] = (handlerObj as Action<T>) + handler;
                }
                else
                {
                    dict.Add(typeof(T), handler);
                }
                //_idHandlers[id] = dict;
            }
            else
            {
                dict = new Dictionary<Type, object>();
                dict.Add(typeof(T), handler);
                _idHandlers.Add(id, dict);
            }
        }

        /// <summary>
        /// Unsubscribe a hander from a package type (all ids)
        /// </summary>
        /// <typeparam name="T">The package type</typeparam>
        /// <param name="handler">The handler</param>
        public static void Unsubscribe<T>(Action<T> handler)
        {
            foreach(var dict in _idHandlers.Values)
            {
                object handlerObj;
                if (dict.TryGetValue(typeof(T), out handlerObj))
                {
                    dict[typeof(T)] = (handlerObj as Action<T>) - handler;
                }
            }
        }

        /// <summary>
        /// Unsubscribe from a package type (from a spefific id)
        /// </summary>
        /// <typeparam name="T">The package type</typeparam>
        /// <param name="id">The id</param>
        /// <param name="handler">The handler</param>
        public static void Unsubscribe<T>(int id, Action<T> handler)
        {
            Dictionary<Type, object> dict;
            if(_idHandlers.TryGetValue(id, out dict))
            {
                object handlerObj;
                if (dict.TryGetValue(typeof(T), out handlerObj))
                {
                    dict[typeof(T)] = (handlerObj as Action<T>) - handler;
                    //_idHandlers[id] = dict;
                }
            }
        }

        /// <summary>
        /// Unsubscribe all eventhandlers from the event system
        /// </summary>
        public static void UnsubscribeAll()
        {
            _idHandlers.Clear();
        }

        /// <summary>
        /// Unsubscribe all event handlers from a specific event
        /// </summary>
        /// <param name="id">the id of the event</param>
        public static void UnsubscribeAll(int id)
        {
            _idHandlers.Remove(id);
        }

        /// <summary>
        /// Send a package to all handlers
        /// </summary>
        /// <typeparam name="T">the type of the package</typeparam>
        /// <param name="id">the id of the event</param>
        /// <param name="package">the package</param>
        public static void Raise<T>(int id, T package)
        {
            Dictionary<Type, object> dict;
            if (_idHandlers.TryGetValue(id, out dict))
            {
                object handlerObj;
                if (dict.TryGetValue(typeof(T), out handlerObj))
                {
                    (handlerObj as Action<T>).SafeCall(package);
                }
            }
        }
    }
}
