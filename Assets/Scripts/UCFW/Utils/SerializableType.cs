using UnityEngine;
using System;

namespace UCFW
{
    /// <summary>
    /// A class to serialize the System.Type class
    /// </summary>
    [System.Serializable]
    public class SerializableType
    {
        /// <summary>
        /// Create a new serializable type
        /// </summary>
        /// <param name="type"></param>
        public SerializableType(Type type)
        {
            _type = type.FullName;
        }

        [SerializeField]
        private string _type = string.Empty;

        /// <summary>
        /// The underlying type
        /// </summary>
        public Type type
        {
            get { return Type.GetType(_type); }
            set { _type = value.AssemblyQualifiedName; }
        }

        public static implicit operator Type(SerializableType sType)
        {
            return sType.type;
        }

        public static implicit operator SerializableType(Type type)
        {
            return new SerializableType(type);
        }
    }
}
