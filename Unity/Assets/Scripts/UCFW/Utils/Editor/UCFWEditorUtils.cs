using UnityEditor;
using UnityEngine;
using System;
namespace UCFW.Editorscripts
{
    /// <summary>
    /// A collection of utilities for the Unityeditor
    /// </summary>
    public static class UCFWEditorUtils
    {
        public static SceneAsset SceneField(string label, SceneAsset scene)
        {
            int index = 0;
            string levelName = string.Empty;
            return SceneField(label, scene, ref index, ref levelName);
        }

        public static SceneAsset SceneField(string label, SceneAsset scene, ref int buildIndex, ref string levelName)
        {
            SceneAsset output = EditorGUILayout.ObjectField(label, scene, typeof(SceneAsset), false) as SceneAsset;

            if(output != scene)
            {
                scene = CheckSceneAsset(scene, ref buildIndex, ref levelName);
            }
            scene = output;
            return scene;
        }

        public static SceneAsset CheckSceneAsset(SceneAsset scene, ref int buildIndex, ref string levelName)
        {
            if (scene == null)
            {
                buildIndex = -1;
                levelName = string.Empty;
                return null;
            }
            else
            {
                EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;
                string scenePath = AssetDatabase.GetAssetPath(scene);
                bool sceneInBuildSettings = false;

                for (int i = 0; i < buildScenes.Length; ++i)
                {
                    if (scenePath == buildScenes[i].path)
                    {
                        sceneInBuildSettings = true;
                        buildIndex = i;
                        break;
                    }
                }

                if (sceneInBuildSettings)
                {
                    string[] splittedPath = scenePath.Split(new char[] { '/', '.' });
                    levelName = splittedPath[splittedPath.Length - 2];
                }
                else
                {
                    scene = null;
                    buildIndex = -1;
                }

                return scene;
            }
        }

        public static SceneAsset LoadSceneAssetByName(string levelName, out int buildIndex)
        {
            SceneAsset scene = null;
            buildIndex = -1;
            EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;

            for (int i = 0; i < buildScenes.Length; ++i)
            {
                string[] splittedScenePath = buildScenes[i].path.Split(new char[] { '/', '.' });
                if (levelName == splittedScenePath[splittedScenePath.Length - 2])
                {
                    scene = AssetDatabase.LoadAssetAtPath(buildScenes[i].path, typeof(SceneAsset)) as SceneAsset;

                    if (scene != null)
                    {
                        buildIndex = i;
                        break;
                    }
                }
            }

            return scene;
        }

        public static SceneAsset LoadSceneAssetByName(string levelName)
        {
            int buildIndex;
            return LoadSceneAssetByName(levelName, out buildIndex);
        }
    }
}
