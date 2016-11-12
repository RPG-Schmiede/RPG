using UnityEngine;
using UnityEditor;
using UCFW;
using UCFW.Editorscripts;
using System;

[CustomEditor(typeof(ChangeGamestateButton))]
public class ChangeGamestateButtonDrawer : GenericEditor<ChangeGamestateButton>
{
    private EditorLayoutGenericPopup<Type> _gamestatePopup = null;

    protected override void OnAwake()
    {
        SetExcludes("m_Script", "gamestateIndex");
    }

    void OnEnable()
    {
        Type[] gamestateTypes = ReflectionUtils.SearchForDerivedTypes<Gamestate>();

        int startIndex = 0;
        if (specifiedTarget.gameObject != null)
        {
            for (int i = 0; i < gamestateTypes.Length; ++i)
            {
                if (gamestateTypes[i] == specifiedTarget.gamestate.type)
                {
                    startIndex = i;
                    break;
                }
            }
        }

        _gamestatePopup = new EditorLayoutGenericPopup<Type>(startIndex, (t) => t.Name, gamestateTypes);
    }

    protected override void DrawAfterProperties()
    {
        _gamestatePopup.Show();
        specifiedTarget.gamestate = _gamestatePopup.currentItem;
    }
}
