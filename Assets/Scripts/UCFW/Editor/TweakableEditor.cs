using UnityEditor;

namespace UCFW.Editorscripts
{
    /// <summary>
    /// An extension of the Editor class to tweak the drawn properties
    /// Dont use OnInspectorGUI with this editor
    /// </summary>
    public class TweakableEditor : Editor
    {
        string[] _excludes = new string[] { };

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawBeforeProperties();
            DrawPropertiesExcluding(serializedObject, _excludes);
            DrawAfterProperties();

            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Set the fields by name that shall not be drawn
        /// </summary>
        /// <param name="excludes">the names of the fields</param>
        protected void SetExcludes(params string[] excludes)
        {
            if(excludes == null)
            {
                _excludes = new string[] { };
            }
            else
            {
                _excludes = excludes;
            }
        }

        /// <summary>
        /// Override this to draw something before the fields
        /// </summary>
        protected virtual void DrawBeforeProperties() { }

        /// <summary>
        /// Override this to draw something after the fields
        /// </summary>
        protected virtual void DrawAfterProperties() { }
    }
}
