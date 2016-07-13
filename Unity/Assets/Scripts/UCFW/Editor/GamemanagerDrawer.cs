using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace UCFW.Editorscripts
{
    /// <summary>
    /// The drawer of the Gamemanager
    /// </summary>
    [CustomEditor(typeof(Gamemanager))]
    public class GamemanagerDrawer : GenericEditor<Gamemanager>
    {
        private EditorLayoutGenericPopup<Type> _gamestatePopup = null;

        protected override void OnAwake()
        {
            OnEnable();
        }

        private void OnEnable()
        {
            SetExcludes("m_Script");

            LoadGamestateDescriptions();
        }

        /// <summary>
        /// Search for gamestates and load the descriptions
        /// </summary>
        private void LoadGamestateDescriptions()
        {
            if (specifiedTarget != null && specifiedTarget.gamestateDescs != null)
            {
                Type[] gamestateTypes = ReflectionUtils.SearchForDerivedTypes<Gamestate>();

                if (specifiedTarget.initialGamestateType == null)
                {
                    specifiedTarget.initialGamestateType = gamestateTypes[0];
                }

                int initIndex = UpdateGamestateDescriptions(gamestateTypes);

                _gamestatePopup = new EditorLayoutGenericPopup<Type>("Initial State: ", initIndex, AdjustGamestateName, gamestateTypes);

            }
        }

        /// <summary>
        /// Update the gamestate descriptions of the target
        /// </summary>
        /// <param name="gamestateTypes">The types of the used gamestates</param>
        /// <returns></returns>
        private int UpdateGamestateDescriptions(Type[] gamestateTypes)
        {
            List<GamestateDesc> curDescs = new List<GamestateDesc>(specifiedTarget.gamestateDescs);
            int index = 0;

            for (int i = 0; i < gamestateTypes.Length; ++i)
            {
                bool descAvailable = false;
                GamestateDesc cur;
                for (int j = 0; j < curDescs.Count; ++j)
                {
                    if (curDescs[j].type.type == gamestateTypes[i])
                    {
                        cur = curDescs[j];
                        descAvailable = true;
                        break;
                    }
                }


                if (!descAvailable)
                {
                    cur = new GamestateDesc();
                    cur.name = AdjustGamestateName(gamestateTypes[i]);
                    cur.type = gamestateTypes[i];
                    curDescs.Add(cur);
                }

                if (specifiedTarget.initialGamestateType.type == gamestateTypes[i])
                {
                    index = i;
                }
            }

            // Clear list (if gamestates were deleted)
            for (int i = 0; i < curDescs.Count; ++i)
            {
                bool isAvailable = false;
                for (int j = 0; j < gamestateTypes.Length; ++j)
                {
                    if (curDescs[i].type.type == gamestateTypes[j])
                    {
                        isAvailable = true;
                        break;
                    }
                }
                if (!isAvailable)
                {
                    curDescs.RemoveAt(i--);
                }
            }


            specifiedTarget.gamestateDescs = curDescs.ToArray();
            return index;
        }

        private string AdjustGamestateName(Type gamestateType)
        {
            return gamestateType.Name;
        }

        protected override void DrawAfterProperties()
        {
            if (specifiedTarget.gamestateDescs.IsEmpty())
            {
                GUILayout.Label("No gamestates available");
            }
            else
            {
                _gamestatePopup.Show();
                specifiedTarget.initialGamestateType.type = _gamestatePopup.currentItem;

                GUILayout.BeginVertical("box");
                GUILayout.Label("Gamestates:", EditorStyles.boldLabel);

                for (int i = 0; i < specifiedTarget.gamestateDescs.Length; ++i)
                {
                    DrawGamestateDesc(ref specifiedTarget.gamestateDescs[i]);
                }
                GUILayout.EndVertical();

                AssetDatabase.SaveAssets();
            }
        }

        public static void DrawGamestateDesc(ref GamestateDesc desc)
        {

            GUILayout.Label(desc.name + ": ", EditorStyles.boldLabel);
            SceneAsset obj = UCFWEditorUtils.SceneField("Required Scene: ", desc.sceneObject as SceneAsset, ref desc.buildIndex, ref desc.sceneName) as SceneAsset;

            if (obj != desc.sceneObject && desc.buildIndex == -1)
            {
                Debug.LogWarning("No valid scene object. Remember it has to be enabled in the build settings");
            }
            desc.sceneObject = obj;

            if (desc.sceneObject != null)
            {
                GUILayout.BeginHorizontal();

                desc.preload = GUILayout.Toggle(desc.preload, "Preload");
                GUILayout.FlexibleSpace();
                desc.reloadRequired = GUILayout.Toggle(desc.reloadRequired, "Force Reload");

                GUILayout.EndHorizontal();
            }
        }
    }
}
