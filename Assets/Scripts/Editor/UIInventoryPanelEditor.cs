using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(UIInventoryPanel))]
public class UIInventoryPanelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        // --- Custom UI ---
        UIInventoryPanel myTarget = (UIInventoryPanel)target;

        // -- Sidebar Slot Debugging --
        if(GUILayout.Button("Set Slots"))
        {
            myTarget.SetSlots();
        }

        if (GUILayout.Button("Switch Normal Layout"))
        {
            myTarget.SwitchToNormal();
        }

        if (GUILayout.Button("Switch Battle Layout"))
        {
            myTarget.SwitchToBattle();
        }

        if (GUILayout.Button("Swipe down"))
        {
            myTarget.Swipe(true);
        }

        if (GUILayout.Button("Swipe up"))
        {
            myTarget.Swipe(false);
        }


    }
}
