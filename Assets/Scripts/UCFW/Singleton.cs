using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.IO;

namespace UCFW
{
    /// <summary>
    /// A base class for the generic Singleton. DO NOT derive directly from this class!
    /// </summary>
    public abstract class Singleton : BetterBehaviour
    {
        public static readonly string PREFAB_PATH = "Prefabs";
        public static readonly string PREFAB_NAME = "Gamemanager";

#if UNITY_EDITOR
        [MenuItem("UCFW/Create Gamemanager Prefab")]
        private static void CreatePrefab()
        {
            string prefabPath = "/Resources/" + Singleton.PREFAB_PATH + "/";
            string prefabName = Singleton.PREFAB_NAME + ".prefab";

            if (!Directory.Exists(Application.dataPath + prefabPath))
            {
                Directory.CreateDirectory(Application.dataPath + prefabPath);
                AssetDatabase.Refresh();
            }

            string finalPath = "Assets" + prefabPath + prefabName;
            UnityEngine.Object o = PrefabUtility.CreateEmptyPrefab(finalPath);
            GameObject gamemanagerObject = new GameObject(Singleton.PREFAB_NAME);
            gamemanagerObject = PrefabUtility.ReplacePrefab(gamemanagerObject, o, ReplacePrefabOptions.ConnectToPrefab);

            Type[] singletypes = ReflectionUtils.SearchForDerivedTypes<Singleton>((t) => !t.IsAbstract && !gamemanagerObject.HasComponent(t));

            for (int i = 0; i < singletypes.Length; ++i)
            {
                gamemanagerObject.AddComponent(singletypes[i]);
            }
        }
#endif
    }

    /// <summary>
    /// The base class for a Singleton in Unity
    /// </summary>
    /// <typeparam name="T">The type of the Singleton</typeparam>
    public abstract class Singleton<T> : Singleton where T : Singleton<T>
    {
        static T _instance = null;

        static GameObject _singletonObject = null;

        /// <summary>
        /// The instance of the singleton (if instance is null, prefab is loaded)
        /// </summary>
        public static T instance
        {
            get
            {
                if (_instance == null)
                {
                    LoadInstance();
                }
                return _instance;
            }
        }

        /// <summary>
        /// The instance of the singleton (can be null)
        /// </summary>
        /// <returns></returns>
        public static T GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// Creates the instance for the singleton
        /// </summary>
        private static void LoadInstance()
        {
            if (_singletonObject == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    _singletonObject = Instantiate(Resources.Load(PREFAB_PATH + "/" + PREFAB_NAME)) as GameObject;
                    _instance = _singletonObject.GetComponent<T>();
                }
                else
                {
                    _singletonObject = _instance.gameObject;
                }

                DontDestroyOnLoad(_singletonObject);

            }

            if (_instance == null && !_singletonObject.HasComponent(out _instance))
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    _instance = _singletonObject.AddComponent<T>();
                    Debug.LogWarning(string.Format("Singleton of type {0} does not exist and was created temporarily. Maybe you forget to add it to the GameManager?", typeof(T).Name), _instance);
                }
                else
                {
                    Debug.LogWarning(string.Format("Singleton on type {0} lies on another gameobject than the gamemanager", typeof(T).Name), _instance);
                }
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }

            if (_instance != this)
            {
                Destroy(this);
            }
            else
            {
                InitBetterBehaviour();
                OnAwake();
            }
        }
    }
}
