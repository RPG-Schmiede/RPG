using UnityEngine;
using UnityEditor;
using System;

namespace UCFW.Editorscripts
{
    /// <summary>
    /// A Menubar to simplifiy menus in editorscripting
    /// </summary>
    public class EditorLayoutMenu : Showable
    {
        /// <summary>
        /// A Menuitem for a editorLayoutMenu
        /// </summary>
        public struct MenuItem
        {
            /// <summary>
            /// Create a new menuitem
            /// </summary>
            /// <param name="name">the name of the menu entry</param>
            /// <param name="showMethod">the method to draw the menu when this item is selected</param>
            public MenuItem(string name, Action showMethod)
            {
                _showAction = showMethod;
                _name = name;
            }

            private Action _showAction;
            private string _name;

            /// <summary>
            /// the method to draw the menu when this item is selected
            /// </summary>
            public Action showMethod
            {
                get { return _showAction; }
            }

            /// <summary>
            /// the name of the menu entry
            /// </summary>
            public string name
            {
                get { return _name; }
            }
        }

        /// <summary>
        /// Create a new EditorLayoutMenu
        /// </summary>
        /// <param name="drawAfterMenubar">A method for thecontent that is drawn between the menubar and the menuitem content</param>
        /// <param name="initialIndex">The index of the initial selected menuitem</param>
        /// <param name="menuItems">The menu items</param>
        public EditorLayoutMenu(Action drawAfterMenubar, int initialIndex, params MenuItem[] menuItems)
            : this(initialIndex, menuItems)
        {
            _drawEverytime = drawAfterMenubar;
        }

        /// <summary>
        /// Create a new EditorLayoutMenu
        /// </summary>
        /// <param name="drawAfterMenubar">A method for thecontent that is drawn between the menubar and the menuitem content</param>
        /// <param name="menuItems">The menu items</param>
        public EditorLayoutMenu(Action drawAfterMenubar, params MenuItem[] menuItems)
            : this(menuItems)
        {
            _drawEverytime = drawAfterMenubar;
        }

        /// <summary>
        /// Create a new EditorLayoutMenu
        /// </summary>
        /// <param name="initialIndex">The index of the initial selected menuitem</param>
        /// <param name="menuItems">The menu items</param>
        public EditorLayoutMenu(int initialIndex, params MenuItem[] menuItems)
            : this(menuItems)
        {
            if (_index != -1)
            {
                _index = Mathf.Clamp(initialIndex, 0, menuItems.Length);
            }
        }

        /// <summary>
        /// Create a new EditorLayoutMenu
        /// </summary>
        /// <param name="menuItems">The menu items</param>
        public EditorLayoutMenu(params MenuItem[] menuItems)
        {
            if (menuItems.Length == 0)
            {
                _index = -1;
            }
            else
            {
                _index = 0;

                _names = new string[menuItems.Length];
                _showMethods = new Action[menuItems.Length];

                for (int i = 0; i < menuItems.Length; ++i)
                {
                    _names[i] = menuItems[i].name;
                    _showMethods[i] = menuItems[i].showMethod;
                }
            }
        }

        private string[] _names = null;
        private Action[] _showMethods = null;
        private int _index = 0;
        private Action _drawEverytime = null;

        /// <summary>
        /// The index of the current selected item
        /// </summary>
        public int index
        {
            get { return _index; }
        }

        /// <summary>
        /// Show the menu
        /// </summary>
        public void Show()
        {
            if (_index != -1)
            {
                _index = GUILayout.Toolbar(_index, _names);
                _drawEverytime.SafeCall();
                _showMethods[_index]();
            }
            else
            {
                GUILayout.Label("Menubar has no items");
            }
        }
    }
}
