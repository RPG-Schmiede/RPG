using UnityEngine;
using UnityEditor;
using System;

namespace UCFW.Editorscripts
{
    /// <summary>
    /// A generic popup item
    /// </summary>
    /// <typeparam name="T">the type of the popup items</typeparam>
    public class EditorLayoutGenericPopup<T> : Showable
    {
        /// <summary>
        /// Create a new Popup
        /// </summary>
        /// <param name="initialIndex">The index of the initial popup item</param>
        /// <param name="items">The items of the popup</param>
        public EditorLayoutGenericPopup(int initialIndex, params T[] items)
            : this(string.Empty, initialIndex, (t) => t.ToString(), items) { }

        /// <summary>
        /// Create a new Popup
        /// </summary>
        /// <param name="initialIndex">The index of the initial popup item</param>
        /// <param name="toString">A method to extract the name from a item</param>
        /// <param name="items">The items of the popup</param>
        public EditorLayoutGenericPopup(int initialIndex, Func<T, string> toString, params T[] items)
            : this(string.Empty, initialIndex, toString, items) { }


        /// <summary>
        /// Create a new Popup
        /// </summary>
        /// <param name="label">The label of the popup</param>
        /// <param name="initialIndex">The index of the initial popup item</param>
        /// <param name="items">The items of the popup</param>
        public EditorLayoutGenericPopup(string label, int initialIndex, params T[] items)
            : this(label, initialIndex, (t) => t.ToString(), items) { }

        /// <summary>
        /// Create a new Popup
        /// </summary>
        /// <param name="label">The label of the popup</param>
        /// <param name="initialIndex">The index of the initial popup item</param>
        /// <param name="toString">A method to extract the name from a item</param>
        /// <param name="items">The items of the popup</param>
        public EditorLayoutGenericPopup(string label, int initialIndex, Func<T, string> toString, params T[] items)
            : this(toString, items)
        {
            if (_index != -1)
            {
                _index = Mathf.Clamp(initialIndex, 0, items.Length);
            }
            _label = label;
        }

        /// <summary>
        /// Create a new Popup
        /// </summary>
        /// <param name="items">The items of the popup</param>
        public EditorLayoutGenericPopup(params T[] items)
            : this((t) => { return t.ToString(); }, items) { }

        /// <summary>
        /// Create a new Popup
        /// </summary>
        /// <param name="toString">A method to extract the name from a item</param>
        /// <param name="items">The items of the popup</param>
        public EditorLayoutGenericPopup(Func<T, string> toString, params T[] items)
        {
            if (items.IsEmpty() || toString == null)
            {
                _index = -1;
            }
            else
            {
                _items = items;
                _names = new string[_items.Length];
                for (int i = 0; i < _items.Length; ++i)
                {
                    _names[i] = toString(_items[i]);
                }
            }
        }

        private int _index = 0;
        private T[] _items = null;
        private string[] _names = null;
        private string _label = string.Empty;

        /// <summary>
        /// The current item
        /// </summary>
        public T currentItem
        {
            get { return _index == -1 ? default(T) : _items[_index]; }
        }

        /// <summary>
        /// The name of the current item
        /// </summary>
        public string currentName
        {
            get { return _index == -1 ? "Invalid" : _names[_index]; }
        }

        /// <summary>
        /// The index of the current item
        /// </summary>
        public int index
        {
            get { return _index; }
        }

        /// <summary>
        /// The label of the popup
        /// </summary>
        public string label
        {
            get { return _label; }
            set { _label = value == null ? string.Empty : value; }
        }

        /// <summary>
        /// Show the popup
        /// </summary>
        public void Show()
        {
            if (_index != -1)
            {
                if (_label == string.Empty || _label == null)
                {
                    _index = EditorGUILayout.Popup(_index, _names);
                }
                else
                {
                    _index = EditorGUILayout.Popup(_label, _index, _names);
                }
            }
            else
            {
                GUILayout.Label("Popup does not contain items");
            }
        }
    }
}
