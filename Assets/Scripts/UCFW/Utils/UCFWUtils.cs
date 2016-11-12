using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UCFW
{
    /// <summary>
    /// A collection of utility methods for Unity
    /// </summary>
    public static partial class UCFWUtils
    {
        /// <summary>
        /// Exit the game (in game and editor)
        /// </summary>
        public static void ExitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
