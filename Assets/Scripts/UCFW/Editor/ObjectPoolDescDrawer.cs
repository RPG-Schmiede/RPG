using UnityEngine;
using UnityEditor;

namespace UCFW.Editorscripts
{
    [CustomPropertyDrawer(typeof(ObjectPoolDesc))]
    public class ObjectPoolDescDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float originalWidth = position.width;
            position.xMax = originalWidth * 0.7f;

            SerializedProperty objectProperty = property.FindPropertyRelative("prefab");
            objectProperty.objectReferenceValue = EditorGUI.ObjectField(position, label, objectProperty.objectReferenceValue, typeof(GameObject), false);


            if (objectProperty.objectReferenceValue != null)
            {
                position.xMin = position.xMax + 2.0f;
                position.width = 50.0f;

                EditorGUI.LabelField(position, "Capacity: ", GUIStyle.none);

                position.xMin = position.xMax + 2.0f;
                position.width = originalWidth * 0.25f;

                SerializedProperty cap = property.FindPropertyRelative("capacity");
                EditorGUI.PropertyField(position, cap, GUIContent.none);

                if (cap.intValue < 1)
                {
                    cap.intValue = 1;
                }
            }
        }
    }
}
