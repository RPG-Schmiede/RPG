using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UCFW.Editorscripts
{
    [CustomPropertyDrawer(typeof(SceneIdentifier))]
    public class SceneIdentifierDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty nameProperty = property.FindPropertyRelative("_sceneName");
            SerializedProperty objectProperty = property.FindPropertyRelative("_sceneObject");
            SerializedProperty buildIndexProperty = property.FindPropertyRelative("_buildIndex");

            int buildIndex = buildIndexProperty.intValue;
            SceneAsset sceneAsset = objectProperty.objectReferenceValue as SceneAsset;
            string name = nameProperty.stringValue;

            if(sceneAsset == null && name != string.Empty)
            {
                sceneAsset = UCFWEditorUtils.LoadSceneAssetByName(name, out buildIndex);
            }

            sceneAsset = EditorGUI.ObjectField(position, label, sceneAsset, typeof(SceneAsset), false) as SceneAsset;

            sceneAsset = UCFWEditorUtils.CheckSceneAsset(sceneAsset, ref buildIndex, ref name);
            if (sceneAsset != null && buildIndex != -1)
            {
                nameProperty.stringValue = name;
                objectProperty.objectReferenceValue = sceneAsset;
            }
            else if (sceneAsset == null)
            {
                nameProperty.stringValue = string.Empty;
                objectProperty.objectReferenceValue = null;
            }
            buildIndexProperty.intValue = buildIndex;
            EditorGUI.EndProperty();
        }
    }
}
